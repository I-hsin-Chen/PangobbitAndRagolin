using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDummyColliderControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Rabbit" || col.gameObject.name == "Pangolin" || col.gameObject.name == "Marbel"){
            col.gameObject.transform.position -= new Vector3(0, 0.3f, 0);
        }
    }
}
