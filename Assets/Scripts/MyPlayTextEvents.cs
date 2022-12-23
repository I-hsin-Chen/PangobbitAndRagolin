using System.Collections;
using System.Collections.Generic;
using GraphSpace;
using PlayTextSupport;
using UnityEngine;


// This script contains all the events used in PlayText
// All functions are called by EventCenter
// Add events to EventCenter in Start()

// Outer Events:
// LockConversation: Lock Conversation before certain event
// UnLockConversation: Unlock Conversation after certain event
// PlayText.Play: Play/Continue the graph
// PlayText.ForceNext: Force to go to next node even if the current node is not finished

public enum STATE
{
    OFF,
    TYPING,
    PAUSED
}

public class MyPlayTextEvents : MonoBehaviour
{
    public DialogueGraph Graph;

    // Start is called before the first frame update
    void Start()
    {
        print("Adding PlayTextEvents");
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("PrintEvent", PrintEvent);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyAD", WaitingForKeyAD);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyJL", WaitingForKeyJL);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyW", WaitingForKeyW);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyI", WaitingForKeyI);

        // start the graph
        EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph);
    }

    // for testing purpose
    void PrintEvent(List<EventValueClass> Value)
    {
        Debug.Log("Print Event");
        Debug.Log(Value[0].intValue);
        Debug.Log(Value[1].floatValue);
        Debug.Log(Value[2].stringValue);
        Debug.Log(Value[3].boolValue);
    }

    // lock conversation and start waiting for player to press KeyA and KeyD
    void WaitingForKeyAD(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForKeyAD");
        StartCoroutine(SchduleWaitingForKeyAD(Value));
    }

    // coroutine to wait for player to press KeyA and KeyD
    IEnumerator SchduleWaitingForKeyAD(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForKeyAD");
        // lock conversation before waiting
        EventCenter.GetInstance().EventTriggered("LockConversation");
        bool A_pressed = false;
        bool D_pressed = false;
        // wait for player to press KeyA and KeyD
        while (!A_pressed || !D_pressed || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
                A_pressed = true;
            if (Input.GetKey(KeyCode.D))
                D_pressed = true;
            yield return null;
        }
        // unlock conversation after A and D are pressed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        // EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph);
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
    }

    // lock conversation and start waiting for player to press KeyJ and KeyL
    void WaitingForKeyJL(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForKeyJL");
        StartCoroutine(SchduleWaitingForKeyJL(Value));
    }

    // coroutine to wait for player to press KeyJ and KeyL
    IEnumerator SchduleWaitingForKeyJL(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForKeyJL");
        // lock conversation before waiting
        EventCenter.GetInstance().EventTriggered("LockConversation");
        bool J_pressed = false;
        bool L_pressed = false;
        // wait for player to press KeyJ and KeyL
        while (!J_pressed || !L_pressed || Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
        {
            if (Input.GetKey(KeyCode.J))
                J_pressed = true;
            if (Input.GetKey(KeyCode.L))
                L_pressed = true;
            yield return null;
        }
        // unlock conversation after J and L are pressed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        // EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph);
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
    }

    // lock conversation and start waiting for player to press KeyW
    void WaitingForKeyW(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForKeyW");
        StartCoroutine(SchduleWaitingForKeyW(Value));
    }

    // coroutine to wait for player to press KeyW
    IEnumerator SchduleWaitingForKeyW(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForKeyW");
        // lock conversation before waiting
        EventCenter.GetInstance().EventTriggered("LockConversation");
        bool W_pressed = false;
        // wait for player to press KeyW
        while (!W_pressed)
        {
            if (Input.GetKeyDown(KeyCode.W))
                W_pressed = true;
            yield return null;
        }
        // unlock conversation after W is pressed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph);
    }

    // lock conversation and start waiting for player to press KeyI
    void WaitingForKeyI(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForKeyI");
        StartCoroutine(SchduleWaitingForKeyI(Value));
    }

    // coroutine to wait for player to press KeyI
    IEnumerator SchduleWaitingForKeyI(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForKeyI");
        // lock conversation before waiting
        EventCenter.GetInstance().EventTriggered("LockConversation");
        bool I_pressed = false;
        // wait for player to press KeyI
        while (!I_pressed)
        {
            if (Input.GetKeyDown(KeyCode.I))
                I_pressed = true;
            yield return null;
        }
        // unlock conversation after I is pressed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph);
    }
}