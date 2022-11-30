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
        if(clockwise) GetComponent<Rigidbody2D>().rotation += 90f;
        else GetComponent<Rigidbody2D>().rotation -= 90f;
        transform.position -= new Vector3(0, transform.position.y/3);;
    }
    
    public void TurretRotate(bool clockwise) // only rotate barrel
    {
        if(clockwise) transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().rotation += 30f;
        else transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().rotation -= 30f;
    }
}
