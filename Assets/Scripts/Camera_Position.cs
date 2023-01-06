using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Position : MonoBehaviour
{
    private GameObject rabbitFollow;
    private GameObject pangolinFollow;
    private GameObject center;
    

    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.Find("Center");
        rabbitFollow = GameObject.Find("RabbitFollow");
        pangolinFollow = GameObject.Find("PangolinFollow");
        //Debug.Log(center);   
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 cen_pos = (rabbitFollow.transform.position + pangolinFollow.transform.position)*0.5f;
        // print(cen_pos);
        //cen_pos.y = cen_pos.y + 5f;
        cen_pos.y = 0.5f;
        center.transform.position = cen_pos;
        //print(cen_pos);
    }
}
