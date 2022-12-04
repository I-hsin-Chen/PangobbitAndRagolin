using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TableRotate(bool clockwise) // rotate table
    {
        if(!clockwise) GetComponent<Rigidbody2D>().rotation += 90f;
        else GetComponent<Rigidbody2D>().rotation -= 90f;
        transform.position -= new Vector3(0, transform.position.y/3.6f);;
    }

    public void TankRotate(bool clockwise) // only rotate barrel
    {
        GameObject obj = transform.GetChild(0).gameObject;
        if(!clockwise && obj.GetComponent<Rigidbody2D>().rotation < 45f){
            obj.GetComponent<Rigidbody2D>().rotation += 15f;
            obj.transform.position += new Vector3(-0.02f, 0.08f, 0);
        }
        else if(clockwise && obj.GetComponent<Rigidbody2D>().rotation > -30f){
            obj.GetComponent<Rigidbody2D>().rotation -= 15f;
            obj.transform.position -= new Vector3(-0.02f, 0.08f, 0);
        }
    }

    public void PulleyWheelRotate(bool clockwise) // only rotate barrel
    {
        Rigidbody2D plateRb = GameObject.Find("LeftPlate").GetComponent<Rigidbody2D>();
        PulleyControl ctrl = GameObject.Find("Pulley").GetComponent<PulleyControl>();
        // if (clockwise) plateRb.velocity = new Vector2 (0, 1);
        // else plateRb.velocity = new Vector2 (0, -1);
        if (ctrl.canRole()) {
            if (clockwise) plateRb.AddForce(new Vector2 (0, 2.0f));
            else plateRb.AddForce(new Vector2 (0, -2.0f));
        }
    }
}
