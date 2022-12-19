using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorBoxControl : MonoBehaviour
{
    private GameObject redStick;
    private GameObject yellowStick;
    private GameObject blueStick;
    private GameObject greenStick;
    private bool[] stickState;
    private List<GameObject> stickList;

    // order of sticks : red, yellow, blue, green
    private enum Colors {RED,YELLOW,GREEN,BLUE}
    private bool isCountingDown;
    private int remainingTime;
    private float challengeGapTime = 3.0f;

    [SerializeField]
    private AnimationCurve jumpCurve;
    [SerializeField][Min(0)]
    private float startTime;
    private float duration = 1.5f;
    private float initialYPos;

    public TMP_Text remainingTimeText;
    public TMP_Text colorHintText;

    void Start(){
        
        stickList = new List<GameObject>();
        stickList.Add(transform.Find("Red").gameObject);
        stickList.Add(transform.Find("Yellow").gameObject);
        stickList.Add(transform.Find("Green").gameObject);
        stickList.Add(transform.Find("Blue").gameObject);
        stickState = new bool[]{true, true, true, true};

        remainingTime = 6;
        isCountingDown = true;
        startTime = Time.fixedTime;
        initialYPos = transform.position.y;

        StartCoroutine(ScheduleColorDisappear());
        StartCoroutine(ScheduleCountDown());
    }

    void Update(){

        remainingTimeText.text = remainingTime.ToString();
    }

    void FixedUpdate(){
        float t = Time.fixedTime - startTime;
        if (t > duration) startTime = Time.fixedTime;
        else transform.position = new Vector3(transform.position.x, initialYPos + jumpCurve.Evaluate(t), transform.position.z);
    }

    public IEnumerator ScheduleCountDown(){
        while(true)
        {
            if (isCountingDown && remainingTime > 0) remainingTime = remainingTime - 1;
            yield return new WaitForSeconds(1.0f);
        }
    }

    public IEnumerator ScheduleColorDisappear(){

        // Challenge 1 : Green disappear //
        stickState[(int)Colors.GREEN] = false;
        yield return new WaitForSeconds(5.0f);
        setStickState();
        yield return new WaitForSeconds(challengeGapTime);
        resetStickState();

        // Challenge 2 : Yellow and Blue disappear //
        remainingTime = 6;
        stickState[(int)Colors.YELLOW] = false;
        stickState[(int)Colors.BLUE] = false;
        yield return new WaitForSeconds(5.0f);
        setStickState();
        yield return new WaitForSeconds(challengeGapTime);
        resetStickState();

        // Challenge 3 : Red Yellow and Green disappear //
        remainingTime = 6;
        stickState[(int)Colors.YELLOW] = false;
        stickState[(int)Colors.RED] = false;
        stickState[(int)Colors.GREEN] = false;
        yield return new WaitForSeconds(5.0f);
        setStickState();
        yield return new WaitForSeconds(challengeGapTime);
        resetStickState();

        // Challenge 4 : Red Blue and Green disappear //
        remainingTime = 6;
        stickState[(int)Colors.BLUE] = false;
        stickState[(int)Colors.RED] = false;
        stickState[(int)Colors.GREEN] = false;
        yield return new WaitForSeconds(5.0f);
        setStickState();
        yield return new WaitForSeconds(challengeGapTime);
        resetStickState();
    }

    private void setStickState(){
        remainingTime = 0;
        for(int i=0; i<=3; i++){
            stickList[i].SetActive(stickState[i]);
        }
        isCountingDown = false;
    }

    private void resetStickState(){
        for(int i=0; i<=3; i++){
            stickState[i] = true;
            stickList[i].SetActive(true);
        }
        isCountingDown = true;
    }

    public bool[] getStickState(){
        return stickState;
    }

}
