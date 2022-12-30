using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelControl : MonoBehaviour
{
    private float waterLevel;
    private float drownLevel;
    private float targetLevel = -5.5f;
    private bool isFalling = false;

    void Start()
    {
        waterLevel = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFalling && waterLevel > targetLevel ) waterLevel = waterLevel - 0.012f;
        else if (isFalling && waterLevel <= targetLevel ) GameObject.Find("Door").GetComponent<Animator>().enabled = true;

        drownLevel = waterLevel - 1.5f;
        transform.position = new Vector3 (transform.position.x, waterLevel, transform.position.z);
    }

    public void startFalling (){
        isFalling = true;
    }

    public float getDrowningLevel(){
        return drownLevel;
    }

    public float getWaterLevel(){
        return waterLevel;
    }
}
