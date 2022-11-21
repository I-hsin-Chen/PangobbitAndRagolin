using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CollisionState))]
public class Jumper : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected CollisionState collisionState;
    [SerializeField]
    private AnimationCurve jumpCurve;
    [SerializeField][Min(0)]
    private float jumpDuration = 0.75f;
    private float jumpStartTime = float.NegativeInfinity;
    public bool isJumping => (Time.fixedTime < jumpStartTime + jumpDuration) && !collisionState.grounded;
    public bool isRising => !isFalling && !collisionState.grounded;
    public bool isFalling => rb.velocity.y <= 0 && !collisionState.grounded;
    public virtual bool canJump => collisionState.grounded;
    public event System.Action onJump;

    public AudioClip jump_se;
    public AudioClip fall_se;
    private AudioSource audiosource;

    void Start(){
        audiosource = GameObject.Find("/AudioSource").GetComponent<AudioSource>();
    }

    public void Jump(InputAction.CallbackContext ctx){
        if(ctx.performed){
            Jump();
        }
    }
    protected virtual void DoJump(){
        // GameManager.ToggleTiles();
        jumpStartTime = Time.fixedTime;
        // Initial impulse
        rb.velocity = new Vector2(rb.velocity.x, jumpCurve.Evaluate(0));
        onJump?.Invoke();
    }
    public void Jump(){
        if(!canJump) return;
        DoJump();
        audiosource.PlayOneShot(jump_se);
    }
    private void Awake() {
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<CollisionState>(out collisionState);
    }
    private void FixedUpdate() {
        if(isJumping){
            float t = (Time.fixedTime - jumpStartTime) / jumpDuration;
            if(collisionState.touchingCeiling) {
                // Cancel jump
                jumpStartTime = 0;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            else rb.velocity = new Vector2(rb.velocity.x, jumpCurve.Evaluate(t));
        }
    }
}
