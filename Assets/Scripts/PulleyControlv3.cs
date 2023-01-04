using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyControlv3 : MonoBehaviour
{
    private Transform leftPlate;
    private Transform rightPlate;
    private float upperBound;
    private float lowerBound;
    private float lerpTime = 0.5f;

    private Coroutine leftRoutine;
    private Coroutine rightRoutine;

    // 0 : left up ; 1 : right up
    public bool toggle { get; private set; } = false;

    void Start()
    {
        leftPlate = transform.Find("LeftPlate");
        rightPlate = transform.Find("RightPlate");
        upperBound = leftPlate.position.y;
        lowerBound = rightPlate.position.y;
        // toggleThePlates();
    }

    public void toggleThePlates (){
        Vector3 rpos = rightPlate.position;
        Vector3 lpos = leftPlate.position;

        if (toggle) {
            rightRoutine = StartCoroutine(rightPlate.gameObject.GetComponent<PulleyPlateControl>().lerpPosition(rpos, new Vector3 (rpos.x, lowerBound, rpos.z), lerpTime * Mathf.Abs(rpos.y - lowerBound)));
            leftRoutine = StartCoroutine(leftPlate.gameObject.GetComponent<PulleyPlateControl>().lerpPosition(lpos, new Vector3 (lpos.x, upperBound, lpos.z), lerpTime * Mathf.Abs(rpos.y - lowerBound)));
        }
        else {
            rightRoutine = StartCoroutine(rightPlate.gameObject.GetComponent<PulleyPlateControl>().lerpPosition(rpos, new Vector3 (rpos.x, upperBound, rpos.z), lerpTime * Mathf.Abs(rpos.y - upperBound)));
            leftRoutine = StartCoroutine(leftPlate.gameObject.GetComponent<PulleyPlateControl>().lerpPosition(lpos, new Vector3 (lpos.x, lowerBound, lpos.z), lerpTime * Mathf.Abs(rpos.y - upperBound)));
        }
        toggle = !toggle;
    }

    public void forceToStop (){
        StopCoroutine(rightRoutine);
        StopCoroutine(leftRoutine);
    }

}
