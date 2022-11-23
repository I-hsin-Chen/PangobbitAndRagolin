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

    [SerializeField]
    private AnimationCurve jumpCurve;
    public bool isRunning { get; private set; } = false;
    
    private float jumpDuration = 0.75f;
    private float jumpStartTime = float.NegativeInfinity;
    public bool isJumping => (Time.fixedTime < jumpStartTime + jumpDuration) && !collisionState.grounded;
    public float runningDirection { get; private set; } = 0;
    public virtual bool canJump => collisionState.grounded;

    public bool isRabbit = true;
    public bool isPangolin = false;
    public bool isObject = false;
    public bool canMove = true;

    public enum Direction {
        Left, Right
    }

    // for testing
    private GameObject Object;

    private void Awake() {
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<CollisionState>(out collisionState);
        TryGetComponent<FacingDirection>(out faceDirection);
    }


    void Start()
    {
        // Object = GameObject.Find("Object");
    }

    // Update is called once per frame
    void Update()
    {
        if (isRabbit) RabbitCheck();
        else if (isPangolin) PangolinCheck();

        if (!isObject){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(faceDirection.GetDirection(), 0), 3);
            if (hit.collider != null){
                possessTarget = hit.collider.gameObject;
            }
            else possessTarget = null;
            // print(possessTarget);
        }
    }

    private void FixedUpdate()
    {
        // this object isn't possessed currently
        if (!isRabbit && !isPangolin) return;

        if(isRunning) rb.velocity = new Vector2(moveSpeed * runningDirection , rb.velocity.y) ;
        else rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void RabbitCheck()
    {
        if (!isRabbit) return;

        // possess
        if (Input.GetKeyDown(KeyCode.U) && possessTarget != null ) Possess(possessTarget);
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
        else runningDirection = 0;

        if (Input.GetKeyDown(KeyCode.I)) Jump();
    }

    private void PangolinCheck()
    {
        if (!isPangolin) return;

        if (Input.GetKeyDown(KeyCode.Q) && possessTarget != null ) Possess(possessTarget);
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
        else runningDirection = 0;

    }

    private void Jump()
    {
        if(!canJump) return;
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

        // run the possess animation ?
        yield return new WaitForSeconds(0.5f);
        obj.GetComponent<PlayerControl>().isRabbit = isRabbit;
        obj.GetComponent<PlayerControl>().isPangolin = isPangolin;
        this.gameObject.SetActive(false);
    }

    private void PossessBackToPlayer()
    {
        isRabbit = false;
        isPangolin = false;
        possessTarget.SetActive(true);
        possessTarget.transform.position = transform.position + new Vector3(faceDirection.GetDirection(), 0, 0);
    }

    // For object
    public void SetPossessObj(GameObject obj){
        possessTarget = obj;
    }
}
