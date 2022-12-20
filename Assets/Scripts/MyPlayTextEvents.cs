using System.Collections;
using System.Collections.Generic;
using GraphSpace;
using PlayTextSupport;
using UnityEngine;


// This script contains all the events used in PlayText
// All functions are called by EventCenter
// Add events to EventCenter in Start()

// Outer Events:
// LockKeyE: Lock the key E
// UnlockKeyE: Unlock the key E
// PlayText.Play: Play/Continue the graph

public class MyPlayTextEvents : MonoBehaviour
{
    public DialogueGraph Graph;

    // Start is called before the first frame update
    void Start()
    {
        print("Adding PlayTextEvents");
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("PrintEvent", PrintEvent);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyX", WaitingForKeyX);
    }

    void PrintEvent(List<EventValueClass> Value)
    {
        Debug.Log("Print Event");
        Debug.Log(Value[0].intValue);
        Debug.Log(Value[1].floatValue);
        Debug.Log(Value[2].stringValue);
        Debug.Log(Value[3].boolValue);
    }

    void WaitingForKeyX(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForKeyX");
        StartCoroutine(SchduleWaitingForKeyX(Value));
    }

    IEnumerator SchduleWaitingForKeyX(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForKeyX");
        EventCenter.GetInstance().EventTriggered("LockKeyE");
        bool flag_X = false;
        while (!flag_X)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                flag_X = true;
            }
            yield return null;
        }
        EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph);
        EventCenter.GetInstance().EventTriggered("UnlockKeyE");
    }
}