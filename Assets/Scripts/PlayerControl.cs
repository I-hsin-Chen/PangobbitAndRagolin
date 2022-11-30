using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private CollisionState collisionState;
    private FacingDirection faceDirection;

    // Record the obj that the current player can possess to
    private GameObject possessTarget;

    [Tooltip("The character's movement speed while grounded. Usually faster than air speed.")]
    [SerializeField][Min(0)]
    private float moveSpeed = 3.0f;
    public float jumpStrength = 10.0f;

    // [SerializeField]
    // private AnimationCurve jumpCurve;
    // public bool isJumping => (Time.fixedTime < jumpStartTime + jumpDuration) && !collisionState.grounded;
    // private float jumpDuration = 0.75f;
    // private float jumpStartTime = float.NegativeInfinity;
    public float runningDirection { get; private set; } = 0;

    // Player state control
    public bool isRabbit = true;
    public bool isPangolin = false;
    public bool isObject = false;

    public virtual bool isPlayer => !isObject && (isRabbit || isPangolin);
    public virtual bool isPossessedObject => isObject && (isRabbit || isPangolin);

    public bool canMove = true;
    public bool isRunning { get; private set; } = false;
    public bool isShrinking { get; private set; } = false;
    public virtual bool canJump => collisionState.grounded;

    // Animation
    private Animator animator;
    private int moveState;
    private int jumpState;
    private int idleState;
    private int shrinkState;
    private float xScale;

    public enum Direction {
        Left, Right
    }

    // for testing
    private GameObject Object;

    private void Awake() {
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<CollisionState>(out collisionState);
        TryGetComponent<FacingDirection>(out faceDirection);
        // TryGetComponent<Animator>(out animator);
    }


    void Start()
    {
        xScale = transform.localScale.x;
        if (!isObject) animator = GetComponent<Animator>();
        moveState = Animator.StringToHash("Base Layer.Move");
        jumpState = Animator.StringToHash("Base Layer.Jump");
        idleState = Animator.StringToHash("Base Layer.Idle");
        shrinkState = Animator.StringToHash("Base Layer.Shrink");
    }

    // Update is called once per frame
    void Update()
    {
        if (isRabbit) RabbitCheck();
        else if (isPangolin) PangolinCheck();

        if (isPlayer){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(faceDirection.GetDirection(), 0), 3);

            if (hit.collider != null && hit.collider.gameObject.tag == "Object"){
                GameObject obj = hit.collider.gameObject;

                // temporarily assume that the objected cannot be possessed by two players simultaneously
                if (hit.collider.gameObject.GetComponent<PlayerControl>().isRabbit || hit.collider.gameObject.GetComponent<PlayerControl>().isPangolin){
                    possessTarget = null;
                }
                else possessTarget = hit.collider.gameObject;
            }
            else possessTarget = null;
            AnimationCheck();
            DirectionCheck();
        }

    }

    private void FixedUpdate()
    {
        // this object isn't possessed currently
        if (!isPlayer && !isPossessedObject) return;
        // if (!isRabbit && !isPangolin) return;

        if(isRunning) rb.velocity = new Vector2(moveSpeed * runningDirection , rb.velocity.y) ;
        else rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void RabbitCheck()
    {
        if (!isRabbit || isShrinking) return;

        // possess
        if (Input.GetKeyDown(KeyCode.U) && possessTarget != null ) Possess(possessTarget);

        // do something

        if (!canMove) return;

        if (Input.GetKey(KeyCode.J)) {
            runningDirection = -1;
            isRunning = true;
            faceDirection.SetDirection(-1);
        }
        else if (Input.GetKey(KeyCode.L)) {
            runningDirection = 1;
            isRunning = true;
            faceDirection.SetDirection(1);
        }
        else {
            runningDirection = 0;
            isRunning = false;
        }
        if (Input.GetKeyDown(KeyCode.I)) Jump();
    }

    private void PangolinCheck()
    {
        if (!isPangolin || isShrinking) return;

        if (Input.GetKeyDown(KeyCode.Q) && possessTarget != null ) Possess(possessTarget);
        
        // rotate

        if (!canMove) return;

        if (Input.GetKey(KeyCode.A)) {
            runningDirection = -1;
            isRunning = true;
            faceDirection.SetDirection(-1);
        }
        else if (Input.GetKey(KeyCode.D)) {
            runningDirection = 1;
            isRunning = true;
            faceDirection.SetDirection(1);
        }
        else {
            runningDirection = 0;
            isRunning = false;
        }

    }

    private void Jump()
    {
        if(!canJump || isShrinking) return;
        rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
    }

    private void Possess(GameObject obj)
    {
        // determine whether player is possessed on a object
        if (!isObject) StartCoroutine(PossessToObject(obj));
        else PossessBackToPlayer();
    }

    private IEnumerator PossessToObject(GameObject obj)
    {
        // Let the possessed object knows when canceling possessing, which animals it should return
        obj.GetComponent<PlayerControl>().SetPossessObj(this.gameObject);
        isShrinking = true;

        // run the possess animation ?
        yield return new WaitForSeconds(0.5f);
        obj.GetComponent<PlayerControl>().isRabbit = isRabbit;
        obj.GetComponent<PlayerControl>().isPangolin = isPangolin;
        isShrinking = false;
        this.gameObject.SetActive(false);
    }

    private void PossessBackToPlayer()
    {
        isRabbit = false;
        isPangolin = false;
        possessTarget.SetActive(true);
        possessTarget.transform.position = transform.position + new Vector3(faceDirection.GetDirection(), 0, 0);

    }

    // when to ignore collision
    void OnCollisionEnter2D(Collision2D col){
        if (gameObject.tag != "Player") return;

        // When the other collider is a player or a player in an object, ignore the collision
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Object" && col.gameObject.GetComponent<PlayerControl>().isPossessedObject)
        Physics2D.IgnoreCollision(col.gameObject.GetComponent<BoxCollider2D>() , GetComponent<BoxCollider2D>());
    }

    // For object
    public void SetPossessObj(GameObject obj){
        possessTarget = obj;
    }

    //Animation control functions
    private void AnimationCheck()
    {
        if (isShrinking) PlayStateIfNotInState(shrinkState);
        else if (!canJump) PlayStateIfNotInState(jumpState);
        else if (isRunning) PlayStateIfNotInState(moveState);
        else PlayStateIfNotInState(idleState);
    }

    private void DirectionCheck()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3 (faceDirection.GetDirection() * xScale, scale.y, scale.z);
    }

    private bool IsInState(int stateHash){
        return animator.GetCurrentAnimatorStateInfo(0).shortNameHash == stateHash;
    }

    private void PlayState(int stateHash){
        animator.Play(stateHash, 0);
    }

    private void PlayStateIfNotInState(int stateHash){
        if(!IsInState(stateHash))
            PlayState(stateHash);
    }

}
