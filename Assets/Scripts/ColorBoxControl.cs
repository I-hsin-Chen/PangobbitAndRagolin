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
    private WaterLevelControl waterCtrl;

    // order of sticks : red, yellow, blue, green
    private enum Colors {RED,YELLOW,GREEN,BLUE}
    private bool isCountingDown;
    public bool isDynamic = false;
    private int remainingTime;
    private float challengeGapTime = 2.5f;

    [SerializeField]
    private AnimationCurve jumpCurve;
    [SerializeField][Min(0)]
    private float startTime;
    private float duration = 1.5f;
    private float yPos;
    private float waterOffset;

    public TMP_Text remainingTimeText;
    public TMP_Text colorHintText;

    void Start(){
        stickList = new List<GameObject>();
        stickList.Add(transform.Find("Red").gameObject);
        stickList.Add(transform.Find("Yellow").gameObject);
        stickList.Add(transform.Find("Green").gameObject);
        stickList.Add(transform.Find("Blue").gameObject);
        stickState = new bool[]{true, true, true, true};
        waterCtrl = GameObject.Find("Water").GetComponent<WaterLevelControl>();

        remainingTime = 6;
        isCountingDown = true;
        startTime = Time.fixedTime;
        yPos = transform.position.y;
        waterOffset = transform.position.y - waterCtrl.getWaterLevel();

        StartCoroutine(ScheduleColorDisappear());
        StartCoroutine(ScheduleCountDown());
    }

    void Update(){

        remainingTimeText.text = remainingTime.ToString();
    }

    void FixedUpdate(){

        float t = Time.fixedTime - startTime;
        if (t > duration) {
            startTime = Time.fixedTime;
            t = 0;
        }

        if (waterCtrl.getWaterLevel() > -1.7f) transform.position = new Vector3(transform.position.x, waterCtrl.getWaterLevel() + waterOffset + jumpCurve.Evaluate(t), transform.position.z);
        else if (!isDynamic) {
            isDynamic = true;
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            transform.GetComponent<Rigidbody2D>().gravityScale = 3.0f;
        }
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

        // Challenge 5 : Yellow Blue and Green disappear //
        remainingTime = 4;
        stickState[(int)Colors.YELLOW] = false;
        stickState[(int)Colors.BLUE] = false;
        stickState[(int)Colors.GREEN] = false;
        yield return new WaitForSeconds(3.0f);
        setStickState();
        yield return new WaitForSeconds(challengeGapTime);
        resetStickState();

        // Challenge 6 : Yellow Blue and Red disappear //
        remainingTime = 4;
        stickState[(int)Colors.RED] = false;
        stickState[(int)Colors.BLUE] = false;
        stickState[(int)Colors.YELLOW] = false;
        yield return new WaitForSeconds(3.0f);
        setStickState();
        yield return new WaitForSeconds(challengeGapTime);
        resetStickState();

        // After passing all challenges, the water starts to disappear
        remainingTime = 6;
        stickState[(int)Colors.BLUE] = false;
        WaterLevelControl waterCtrl = GameObject.Find("Water").GetComponent<WaterLevelControl>();
        waterCtrl.startFalling();
        yield return new WaitForSeconds(5.0f);
        setStickState();
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
