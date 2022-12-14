using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintControl : MonoBehaviour
{
    private GameObject objToFollow;
    private float yOffset;
    public bool enable;
    // Start is called before the first frame update
    void Start()
    {
        enable = false;
        objToFollow = null;
    }

    // Update is called once per frame
    void Update()
    {
        // print(enable);
        if (objToFollow != null ){
            // print("456");
            Vector3 objPos = objToFollow.transform.position;
            this.transform.position = new Vector3 (objPos.x, objPos.y + yOffset, objPos.z);
        }
    }

    public void SetEnable(GameObject obj, float y){
        objToFollow = obj;
        print(objToFollow.name);
        yOffset = y;
        enable = true;
    }
}
