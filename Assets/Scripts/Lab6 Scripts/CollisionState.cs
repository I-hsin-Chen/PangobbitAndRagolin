using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class CollisionState : MonoBehaviour
{
    private Rigidbody2D rb;
    private ContactFilter2D groundFilter, ceilingFilter, rightFilter, leftFilter;
    private List<ContactPoint2D> contactBuffer = new List<ContactPoint2D>();
    public bool grounded { get; private set; } = false;
    public bool touchingLeft { get; private set; } = false;
    public bool touchingRight { get; private set; } = false;
    public bool touchingCeiling { get; private set; } = false;
    public bool touchingWall => touchingLeft || touchingRight;
    public event System.Action onGrounded;
    private void Awake() {
        // Get components
        TryGetComponent<Rigidbody2D>(out rb);
        // Ground points upwards
        groundFilter.useNormalAngle = true;
        groundFilter.minNormalAngle = 45.0f;
        groundFilter.maxNormalAngle = 45.0f + 90.0f;
        // Ceiling points downwards
        ceilingFilter.useNormalAngle = true;
        ceilingFilter.minNormalAngle = -45.0f - 90.0f;
        ceilingFilter.maxNormalAngle = -45.0f;
        // Walls on the right point to the left
        rightFilter.useNormalAngle = true;
        rightFilter.minNormalAngle = 135.0f;
        rightFilter.maxNormalAngle = 135.0f + 90.0f;
        // Walls on the left point to the right
        leftFilter.useNormalAngle = true;
        leftFilter.minNormalAngle = -45.0f;
        leftFilter.maxNormalAngle = -45.0f + 90.0f;
    }
    private void FixedUpdate() {
        bool groundedBefore = grounded;
        int groundHits = rb.GetContacts(groundFilter, contactBuffer);
        grounded = (groundHits > 0);
        int leftHits = rb.GetContacts(leftFilter, contactBuffer);
        touchingLeft = (leftHits > 0);
        int rightHits = rb.GetContacts(rightFilter, contactBuffer);
        touchingRight = (rightHits > 0);
        int ceilingHits = rb.GetContacts(ceilingFilter, contactBuffer);
        touchingCeiling = (ceilingHits > 0);
        if(!groundedBefore && grounded) onGrounded?.Invoke();
    }
}
