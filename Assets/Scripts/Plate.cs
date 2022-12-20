using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public bool ifCollision;
    // Start is called before the first frame update
    void Start()
    {
        ifCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collsionObject)
    {
        print(collsionObject.gameObject.name);
        if  (collsionObject.gameObject.name == "Bulle(1)")
            ifCollision = true;
            
            
            

    }

    

}
