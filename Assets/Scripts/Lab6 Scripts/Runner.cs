using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CollisionState))]
[RequireComponent(typeof(FacingDirection))]
public class Runner : MonoBehaviour
{
    private Rigidbody2D rb;
    private CollisionState collisionState;
    private FacingDirection facingDirection;
    [Tooltip("The character's movement speed while grounded. Usually faster than air speed.")]
    [SerializeField][Min(0)]
    private float groundSpeed = 3.0f;
    [Tooltip("The character's movement speed in midair. Usually slower than ground speed.")]
    [SerializeField][Min(0)]
    private float airSpeed = 2.0f;
    public bool isRunning { get; private set; } = false;
    public float runningDirection { get; private set; } = 0;
    public float moveSpeed => (collisionState.grounded ? groundSpeed : airSpeed) * runningDirection;
    public bool runningAgainstWall => (collisionState.touchingRight && runningDirection > 0) ||
                (collisionState.touchingLeft && runningDirection < 0);
    public void Run(InputAction.CallbackContext ctx){
        Run(ctx.ReadValue<Vector2>().x);    
    }
    public void Run(float x){
        isRunning = !Mathf.Approximately(x, 0);
        if(isRunning){
            runningDirection = x > 0 ? 1 : -1;
        }
    }
    private void Awake() {
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<CollisionState>(out collisionState);
        TryGetComponent<FacingDirection>(out facingDirection);
    }
    private void FixedUpdate() {
        if(isRunning){
            // facingDirection.SetDirection(runningDirection);
            if(!runningAgainstWall){
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }

        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
