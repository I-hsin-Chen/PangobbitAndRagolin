using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyControlv2 : MonoBehaviour
{
    private PulleyPlateControl leftCtrl;
    private PulleyPlateControl rightCtrl;

    private bool isRolling = false;
    private float startForcingTime;
    private float previousDistance = 0.0f;
    private float leftStartForcingPosition;
    private float rightStartForcingPosition;

    private float lowerBound;

    private float plateMass = 8.0f;
    private int stopRollingCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        leftCtrl = transform.Find("LeftPlate").GetComponent<PulleyPlateControl>();
        rightCtrl = transform.Find("RightPlate").GetComponent<PulleyPlateControl>();
        lowerBound = rightCtrl.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // print(isRolling);
        if ((leftCtrl.isForced || rightCtrl.isForced ) && !isRolling) {
            isRolling = true;
            startForcingTime = Time.time;
            leftStartForcingPosition = leftCtrl.gameObject.transform.position.y;
            rightStartForcingPosition = rightCtrl.gameObject.transform.position.y;
        }
        else if (!(leftCtrl.isForced || rightCtrl.isForced ) && isRolling){
            stopRollingCounter += 1;
            if (stopRollingCounter >= 1){
                isRolling = false;
                stopRollingCounter = 0;
            }
        }
        
        if (leftCtrl.isForced || rightCtrl.isForced) stopRollingCounter = 0;
    }

    void FixedUpdate()
    {
        if (isRolling){

            float f = rightCtrl.force - leftCtrl.force;
            float a = f / plateMass;
            float deltaT = Time.time - startForcingTime;
            float v = a * deltaT;
            // float distance = 0.5f * a * deltaT * deltaT - previousDistance;
            float distance = 0.5f * a * Time.deltaTime - previousDistance;

            Vector3 lpos = leftCtrl.gameObject.transform.position;
            Vector3 rpos = rightCtrl.gameObject.transform.position;


            if (!((distance > 0 &&  rpos.y < lowerBound) || (distance < 0 &&  lpos.y < lowerBound))){
                // print(distance);
                // print(a);
                leftCtrl.gameObject.transform.position += new Vector3 (0, distance, 0);
                rightCtrl.gameObject.transform.position -= new Vector3 (0, distance, 0);

                // StartCoroutine(leftCtrl.lerpPosition(lpos, lpos + new Vector3 (0, distance, 0), Time.deltaTime));
                // StartCoroutine(rightCtrl.lerpPosition(rpos, rpos - new Vector3 (0, distance, 0), Time.deltaTime));
                
            }

            // if (!((distance > 0 &&  rpos.y < lowerBound) || (distance < 0 &&  lpos.y < lowerBound))){
            //     StartCoroutine(leftCtrl.lerpPosition(lpos, lpos + new Vector3 (0, distance, 0), Time.deltaTime));
            //     StartCoroutine(rightCtrl.lerpPosition(rpos, rpos - new Vector3 (0, distance, 0), Time.deltaTime));
            // }

            // leftCtrl.gameObject.transform.Translate(v * Vector3.up * Time.fixedDeltaTime);
            // rightCtrl.gameObject.transform.Translate(v * Vector3.down * Time.fixedDeltaTime);

            // if ((v > 0 &&  rpos.y < lowerBound) || (v < 0 &&  lpos.y < lowerBound)) {
            //     leftCtrl.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //     rightCtrl.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            // }
            // else {
            //     leftCtrl.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, v);
            //     rightCtrl.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -v);
            // }

            previousDistance = distance + previousDistance;
        }
        else {
            previousDistance = 0;
            // leftCtrl.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            // rightCtrl.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
