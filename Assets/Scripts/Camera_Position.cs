using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Position : MonoBehaviour
{
    public Rigidbody2D rabbit;
    public Rigidbody2D pangolin;
    private GameObject center;
    

    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.Find("Center");
        //Debug.Log(center);   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cen_pos = (rabbit.transform.position + pangolin.transform.position)*0.5f;
        print(cen_pos);
        cen_pos.y = cen_pos.y + 5f;
        center.transform.position = cen_pos;
    }
}
