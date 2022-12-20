using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFollowControl : MonoBehaviour
{
    GameObject followObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followObject.transform.position;
    }

    public void setFollowObject(GameObject obj){
        followObject = obj;
    }
}
