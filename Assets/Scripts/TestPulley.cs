using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPulley : MonoBehaviour
{
    private Rigidbody2D right_rb;
    private Rigidbody2D left_rb;
    private Animator wheelAnimator;
    private float maxHeight;

    public Plate plate;

    public float ropeDragForceLeft { get; private set; } = 9.8f;
    public float ropeDragForceRight { get; private set; } = 9.8f;

    private void Awake() {
        left_rb = GameObject.Find("LeftPlate").GetComponent<Rigidbody2D>();
        right_rb = GameObject.Find("RightPlate").GetComponent<Rigidbody2D>();
        wheelAnimator = GameObject.Find("Wheel").GetComponent<Animator>();
        wheelAnimator.enabled = false;
        // TryGetComponent<Animator>(out animator);
    }

    void Start()
    {
        ropeDragForceRight = right_rb.mass * 9.8f;
        ropeDragForceLeft = left_rb.mass * 9.8f;
        maxHeight = left_rb.gameObject.transform.position.y;
        // print(ropeDragForceLeft);
    }

    void FixedUpdate()
    {
        //print(left_rb.mass);
        //print(right_rb.mass);
        print(plate.ifCollision);
        if(left_rb.mass > 0)
        {
            left_rb.AddForce(new Vector2 (0, ropeDragForceLeft));
            Vector2 left_v = left_rb.velocity;
            if (left_v.y != 0) {
                right_rb.velocity = new Vector2 (0, -left_v.y);
                if (Mathf.Abs(left_v.y) > 0.1f) wheelAnimator.enabled = true;
            }
            else {
                right_rb.velocity = new Vector2 (0, 0);
                wheelAnimator.enabled = false;
            }
        }
            
        if(right_rb.mass > 0)
        {    
            right_rb.AddForce(new Vector2 (0, ropeDragForceRight));
            Vector2 right_v = right_rb.velocity;
            if (right_v.y != 0) {
                left_rb.velocity = new Vector2 (0, -right_v.y);
                if (Mathf.Abs(right_v.y) > 0.1f) wheelAnimator.enabled = true;
            }
            else {
                right_rb.velocity = new Vector2 (0, 0);
                wheelAnimator.enabled = false;
            }
        }
        

        if (canRole() == false) {
            //print(left_rb.gameObject.transform.position.y);
            right_rb.velocity = new Vector2 (0, 0);
            left_rb.velocity = new Vector2 (0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRightForce(float f){
        ropeDragForceRight = f;
    }

    public bool canRole (){
        if ((left_rb.gameObject.transform.position.y > maxHeight && left_rb.velocity.y > 0) || (right_rb.gameObject.transform.position.y > maxHeight && right_rb.velocity.y > 0)) 
            return false;
        return true;
    }
    
}