using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D collsionObject)
    {
        
        //if(collsionObject.gameObject.name == "RightPlate")
        //    Destroy(gameObject);
    }
}
