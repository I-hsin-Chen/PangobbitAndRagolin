using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private GameObject gameManager;
    private Rigidbody2D rb;
    private CollisionState collisionState;
    private FacingDirection faceDirection;
    private ObjectControl objectControl;
    private AudioManager audiomanager;

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

    private bool playerCanMove = true;
    private bool rabbitCanPossess = true;
    private bool pangolinCanPossess = true;
    private int tankRotate = 0;

    public virtual bool isPlayer => !isObject && (isRabbit || isPangolin);
    public virtual bool isPossessedObject => isObject && (isRabbit || isPangolin);

    public bool isRunning { get; private set; } = false;
    public bool isShrinking { get; private set; } = false;
    public bool isDrowning { get; private set; } = false;
    public bool isRolling { get; private set; } = false;
    public virtual bool canJump => isRabbit && collisionState.grounded;

    // Animation
    private Animator animator;
    public int moveState;
    public int jumpState;
    public int idleState;
    public int shrinkState;
    public int drownState;
    public int rollState;
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
        TryGetComponent<ObjectControl>(out objectControl);
        // TryGetComponent<Animator>(out animator);
        gameManager = GameObject.Find("GameManager");
        audiomanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    void Start()
    {
        xScale = transform.localScale.x;
        if (!isObject) animator = GetComponent<Animator>();
        moveState = Animator.StringToHash("Base Layer.Move");
        idleState = Animator.StringToHash("Base Layer.Idle");
        shrinkState = Animator.StringToHash("Base Layer.Shrink");

        drownState = Animator.StringToHash("Base Layer.Drown");
        jumpState = Animator.StringToHash("Base Layer.Jump");
        
        rollState = Animator.StringToHash("Base Layer.Roll");

        if (isPlayer) dialogFollowRefresh(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        playerCanMove = gameManager.GetComponent<GameManager>().GetPlayerCanMove();
        rabbitCanPossess = gameManager.GetComponent<GameManager>().GetRabbitCanPossess();
        pangolinCanPossess = gameManager.GetComponent<GameManager>().GetPangolinCanPossess();
        if (playerCanMove && isRabbit) RabbitCheck();
        else if (playerCanMove && isPangolin) PangolinCheck();

        if (isPlayer){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(faceDirection.GetDirection(), 0), 3);
            RaycastHit2D hit_up = Physics2D.Raycast(transform.position, new Vector2(0, 1), 3.0f);

            // check objects at lefthand side and righthand side
            if (hit.collider != null && hit.collider.gameObject.tag == "Object"){
                GameObject obj = hit.collider.gameObject;

                // temporarily assume that the objected cannot be possessed by two players simultaneously
                if (hit.collider.gameObject.GetComponent<PlayerControl>().isRabbit || hit.collider.gameObject.GetComponent<PlayerControl>().isPangolin){
                    possessTarget = null;
                }
                else {
                    // unhighlight the object that is no more the possess target.
                    if (possessTarget != null && possessTarget != hit.collider.gameObject) 
                        possessTarget.GetComponent<ObjectControl>().unhighlightObject();

                    possessTarget = hit.collider.gameObject;
                    // highlight the object that is the possess target.
                    possessTarget.GetComponent<ObjectControl>().highlightObject(isRabbit);
                }
            }
            // check objects at upside
            else if (hit_up.collider != null && hit_up.collider.gameObject.tag == "Object"){
                GameObject obj = hit_up.collider.gameObject;

                // temporarily assume that the objected cannot be possessed by two players simultaneously
                if (hit_up.collider.gameObject.GetComponent<PlayerControl>().isRabbit || hit_up.collider.gameObject.GetComponent<PlayerControl>().isPangolin){
                    possessTarget = null;
                }
                else {
                    // unhighlight the object that is no more the possess target.
                    if (possessTarget != null && possessTarget != hit_up.collider.gameObject) 
                        possessTarget.GetComponent<ObjectControl>().unhighlightObject();
                    
                    possessTarget = hit_up.collider.gameObject;
                    // highlight the object that is the possess target.
                    possessTarget.GetComponent<ObjectControl>().highlightObject(isRabbit);
                    // if (possessTarget.name == "Clock") print(possessTarget.GetComponent<SpriteRenderer>().color);
                }
            }
            else {
                if (possessTarget != null) possessTarget.GetComponent<ObjectControl>().unhighlightObject();
                possessTarget = null;
            }

            AnimationCheck();
            DirectionCheck();
        }
        else if (isPossessedObject) GetComponent<ObjectControl>().unhighlightObject();
    }

    private void FixedUpdate()
    {
        // this object isn't possessed currently
        if (!isPlayer && !isPossessedObject) return;
        // if (!isRabbit && !isPangolin) return;

        if(isRunning) rb.velocity = new Vector2(moveSpeed * runningDirection , rb.velocity.y) ;
        else rb.velocity = new Vector2(0, rb.velocity.y);
        if (playerCanMove && isPangolin) {
            if(name == "Gear"){
                if(Input.GetKey(KeyCode.W)) objectControl.GearRotate(false);  // gear up
                if(Input.GetKey(KeyCode.S)) objectControl.GearRotate(true); // gear down
            }
            if(name == "ColorBox"){
                if(Input.GetKey(KeyCode.W)) objectControl.ColorBoxRotate(false);  // turn clockwise
                if(Input.GetKey(KeyCode.S)) objectControl.ColorBoxRotate(true); // turn counter-clockwise
            }
            if(name == "Tank" && tankRotate != 0){
                if(tankRotate == 1) objectControl.TankRotate(false);  // turn clockwise
                else objectControl.TankRotate(true); // turn counter-clockwise
                tankRotate = 0;
            }
        }
    }

    private void RabbitCheck()
    {
        if (!isRabbit || isShrinking) return;

        // possess
        if (Input.GetKeyDown(KeyCode.U) && rabbitCanPossess && possessTarget != null ) Possess(possessTarget);

        if(isPlayer || objectControl.canMove){ // some object cannot move
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
        }
        else if (isPossessedObject) {
            if (Input.GetKey(KeyCode.J)) faceDirection.SetDirection(-1);
            else if (Input.GetKey(KeyCode.L)) faceDirection.SetDirection(1);
        }
        
        if (Input.GetKeyDown(KeyCode.I)) Jump();
        if (name == "Tank"){
            if(Input.GetKeyDown(KeyCode.O)){ // shoot
                objectControl.TankShoot();
                audiomanager.PlaySE_Tower();
            }
        }
    }

    private void PangolinCheck()
    {
        if (!isPangolin || isShrinking) return;

        // possess
        if (Input.GetKeyDown(KeyCode.Q) && pangolinCanPossess && possessTarget != null ) Possess(possessTarget);

        if(isPlayer || objectControl.canMove){ // some object cannot move
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
        else if (isPossessedObject) {
            if (Input.GetKey(KeyCode.A)) faceDirection.SetDirection(-1);
            else if (Input.GetKey(KeyCode.D)) faceDirection.SetDirection(1);
        }

        isRolling = (isPlayer && Input.GetKey(KeyCode.W)) ? true : false;
            
        // Test
        if(name.Length >= 5 && name.Substring(0, 5) == "Table"){
            if(Input.GetKeyDown(KeyCode.W)) objectControl.TableRotate(false);  // turn clockwise
            if(Input.GetKeyDown(KeyCode.S)) objectControl.TableRotate(true); // turn counter-clockwise
        }

        // Level_1
        if(name == "Tank"){
            // if(Input.GetKeyDown(KeyCode.W)) objectControl.TankRotate(false);  // turn clockwise
            // if(Input.GetKeyDown(KeyCode.S)) objectControl.TankRotate(true); // turn counter-clockwise
            if(Input.GetKeyDown(KeyCode.E)) {
                objectControl.TankShoot(); // shoot
                audiomanager.PlaySE_Tower();
            }
            if(Input.GetKeyDown(KeyCode.W)) tankRotate = 1;  // turn clockwise
            if(Input.GetKeyDown(KeyCode.S)) tankRotate = -1; // turn counter-clockwise
        }
        if(name == "Clock"){
            if(Input.GetKeyDown(KeyCode.W)) objectControl.ClockRotate(false);  // turn clockwise
            if(Input.GetKeyDown(KeyCode.S)) objectControl.ClockRotate(true); // turn counter-clockwise
        }
        if(name == "Wheel"){
            if(Input.GetKey(KeyCode.W)) objectControl.PulleyWheelRotate(false);  // turn clockwise
            if(Input.GetKey(KeyCode.S)) objectControl.PulleyWheelRotate(true); // turn counter-clockwise
        }

        // Level_3
        if(name == "Server"){
            if(Input.GetKeyDown(KeyCode.W)) objectControl.ServerRotate(false);  // turn clockwise
            if(Input.GetKeyDown(KeyCode.S)) objectControl.ServerRotate(true); // turn counter-clockwise
        }
        if(name == "Gate_Gear"){
            if(Input.GetKey(KeyCode.W)) objectControl.GateClose(false);  // open gate
            if(Input.GetKey(KeyCode.S)) objectControl.GateClose(true); // close gate
        }

        // Level 4
        if (name == "WaterTap"){
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) objectControl.WaterTapRotate();  // open gate
            else objectControl.WaterTapStopRotate(); 
        }

    }

    private void Jump()
    {
        if(!canJump || isShrinking) return;
        audiomanager.PlaySE_Jump();
        rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
    }

    private void Possess(GameObject obj)
    {
        // determine whether player is possessed on a object
        audiomanager.PlaySE_Possess();
        if (!isObject) StartCoroutine(PossessToObject(obj));
        else PossessBackToPlayer();
    }

    private IEnumerator PossessToObject(GameObject obj)
    {
        obj.GetComponent<ObjectControl>().possessSign(isRabbit);

        // Let the possessed object knows when canceling possessing, which animals it should return
        obj.GetComponent<PlayerControl>().SetPossessObj(this.gameObject);
        isShrinking = true;

        // *** For Dialog box follow *** //
        dialogFollowRefresh(obj);
        obj.GetComponent<ObjectControl>().unhighlightObject();

        // run the possess animation ?
        yield return new WaitForSeconds(0.5f);
        obj.GetComponent<PlayerControl>().isRabbit = isRabbit;
        obj.GetComponent<PlayerControl>().isPangolin = isPangolin;

        // let object can move after possess
        obj.gameObject.transform.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        // // modify Rigidbody bodyType when possess
        // if(obj.gameObject.transform.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic) obj.gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        // // level_3
        // if(obj.gameObject.name=="Gear")
        //     obj.gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        // if(obj.gameObject.name=="Gate_Gear")
        //     obj.gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        isShrinking = false;
        this.gameObject.SetActive(false);

    }

    private void PossessBackToPlayer()
    {
        GetComponent<ObjectControl>().unpossessSign();
        dialogFollowRefresh(possessTarget);

        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        isRabbit = false;
        isPangolin = false;
        
        // lock object movement before possess back
        GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
        // modify Rigidbody bodyType after possess
        // if(isKinematic) transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        possessTarget.SetActive(true);

        //renderer.bounds.size.x
        // if (name == "Tank") possessTarget.transform.position = transform.position + new Vector3( (renderer.bounds.size.x / 2 + 0.2f) * faceDirection.GetDirection(), 0, 0);
        // else possessTarget.transform.position = transform.position + new Vector3( renderer.bounds.size.x / 2 * faceDirection.GetDirection() + 0.1f, 0, 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(faceDirection.GetDirection() * renderer.bounds.size.x / 2, 0, 0), new Vector2(faceDirection.GetDirection(), 0), 1);
        RaycastHit2D Nhit = Physics2D.Raycast(transform.position - new Vector3(faceDirection.GetDirection() * renderer.bounds.size.x / 2, 0, 0), new Vector2(-1*faceDirection.GetDirection(), 0), 1);
        RaycastHit2D Uhit = Physics2D.Raycast(transform.position + new Vector3(renderer.bounds.size.y / 2, 0, 0), new Vector2(0, 1), 0.6f);
        RaycastHit2D Dhit = Physics2D.Raycast(transform.position - new Vector3(renderer.bounds.size.y / 2, 0, 0), new Vector2(0, -1), 0.6f);
        
        if (hit.collider == null || hit.collider.gameObject.tag == "Player")
            possessTarget.transform.position = transform.position + new Vector3( (renderer.bounds.size.x / 2 + 0.5f) * faceDirection.GetDirection(), 0.3f, 0);
        else if(Nhit.collider == null || Nhit.collider.gameObject.tag == "Player")
            possessTarget.transform.position = transform.position + new Vector3( (renderer.bounds.size.x / 2 + 0.5f) * -1 * faceDirection.GetDirection(), 0.3f, 0);
        else if(Uhit.collider == null || Uhit.collider.gameObject.tag == "Player")
            possessTarget.transform.position = transform.position + new Vector3( 0, renderer.bounds.size.y / 2 + 0.3f, 0);
        else if(Dhit.collider == null || Dhit.collider.gameObject.tag == "Player")
            possessTarget.transform.position = transform.position - new Vector3( 0, renderer.bounds.size.y / 2 + 0.3f, 0);
        else possessTarget.transform.position = transform.position + new Vector3( renderer.bounds.size.x / 2 * faceDirection.GetDirection() + 0.1f, 0, 0);
    }

    // when to ignore collision
    void OnCollisionEnter2D(Collision2D col){
        if (gameObject.tag != "Player") return;

        // When the other collider is a player or a player in an object, ignore the collision
        if (col.gameObject.tag == "Player" ) // || col.gameObject.tag == "Object" && col.gameObject.GetComponent<PlayerControl>().isPossessedObject)
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<BoxCollider2D>() , GetComponent<BoxCollider2D>());
        if (col.gameObject.name == "ColorBox")
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<PolygonCollider2D>() , GetComponent<BoxCollider2D>());
    }

    // For object
    public void SetPossessObj(GameObject obj){
        possessTarget = obj;
    }

    //Animation control functions
    private void AnimationCheck()
    {
        if (isDrowning) PlayStateIfNotInState(drownState);
        else if (isShrinking) PlayStateIfNotInState(shrinkState);
        else if (isRolling) PlayStateIfNotInState(rollState);
        else if (!canJump && rb.velocity.y >= 1.0f) PlayStateIfNotInState(jumpState);
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

    public void SetIsDrowning(){
        isDrowning = true;
    }

    public void dialogFollowRefresh(GameObject obj){
        string s = " ";
        if (isRabbit) s = "Rabbit";
        else if (isPangolin) s = "Pangolin";

        if (GameObject.Find(s + "Follow") != null){
            DialogFollowControl follow = GameObject.Find(s + "Follow").GetComponent<DialogFollowControl>();
            follow.setFollowObject(obj);
        }
    }

}
