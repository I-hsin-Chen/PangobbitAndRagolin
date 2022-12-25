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

public class MyPlayTextEvents : MonoBehaviour
{
    public DialogueGraph Graph;
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        // adding events to EventCenter
        print("Adding PlayTextEvents");
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("PrintEvent", PrintEvent);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("SetPlayerCanMove", SetPlayerCanMove);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("SetPangolinCanPossess", SetPangolinCanPossess);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("SetRabbitCanPossess", SetRabbitCanPossess);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyAD", WaitingForKeyAD);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyJL", WaitingForKeyJL);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyW", WaitingForKeyW);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyI", WaitingForKeyI);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForPangolinPossessTable", WaitingForPangolinPossessTable);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForRabbitPossessTable", WaitingForRabbitPossessTable);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyQ", WaitingForKeyQ);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyU", WaitingForKeyU);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("DestroyPlayText_Follow", DestroyPlayText_Follow);

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

    // set playerCanMove in event way
    void SetPlayerCanMove(List<EventValueClass> Value)
    {
        Debug.Log("SetPlayerCanMove");
        gameManager.GetComponent<GameManager>().SetPlayerCanMove(Value[0].boolValue);
    }

    // set pangolinCanPossess in event way
    void SetPangolinCanPossess(List<EventValueClass> Value)
    {
        Debug.Log("SetPangolinCanPossess");
        gameManager.GetComponent<GameManager>().SetPangolinCanPossess(Value[0].boolValue);
    }

    // set rabbitCanPossess in event way
    void SetRabbitCanPossess(List<EventValueClass> Value)
    {
        Debug.Log("SetRabbitCanPossess");
        gameManager.GetComponent<GameManager>().SetRabbitCanPossess(Value[0].boolValue);
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
        while (!W_pressed || Input.GetKey(KeyCode.W))
        {
            if (Input.GetKeyDown(KeyCode.W))
                W_pressed = true;
            yield return null;
        }
        // unlock conversation after W is pressed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
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
        while (!I_pressed || Input.GetKey(KeyCode.I))
        {
            if (Input.GetKeyDown(KeyCode.I))
                I_pressed = true;
            yield return null;
        }
        // unlock conversation after I is pressed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
    }

    // lock conversation and start waiting for Pangolin to possess a table
    void WaitingForPangolinPossessTable(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForPangolinPossessTable");
        StartCoroutine(SchduleWaitingForPangolinPossessTable(Value));
    }

    // coroutine to wait for Pangolin to possess a table
    IEnumerator SchduleWaitingForPangolinPossessTable(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForPangolinPossessTable");
        // lock conversation before waiting
        EventCenter.GetInstance().EventTriggered("LockConversation");
        // TODO: check if pangolin have possessed a table
        // currently, just wait for keyQ
        bool Q_pressed = false;
        // wait for player to press KeyQ
        while (!Q_pressed || Input.GetKey(KeyCode.Q))
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Q_pressed = true;
            yield return null;
        }
        // unlock conversation after table is possessed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
    }

    // lock conversation and start waiting for Rabbit to possess a table
    void WaitingForRabbitPossessTable(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForRabbitPossessTable");
        StartCoroutine(SchduleWaitingForRabbitPossessTable(Value));
    }

    // coroutine to wait for Rabbit to possess a table
    IEnumerator SchduleWaitingForRabbitPossessTable(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForRabbitPossessTable");
        // lock conversation before waiting
        EventCenter.GetInstance().EventTriggered("LockConversation");
        // TODO: check if rabbit have possessed a table
        // currently, just wait for keyU
        bool U_pressed = false;
        // wait for player to press KeyU
        while (!U_pressed || Input.GetKey(KeyCode.U))
        {
            if (Input.GetKeyDown(KeyCode.U))
                U_pressed = true;
            yield return null;
        }
        // unlock conversation after table is possessed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
    }

    // lock conversation and start waiting for player to press KeyQ
    void WaitingForKeyQ(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForKeyQ");
        StartCoroutine(SchduleWaitingForKeyQ(Value));
    }

    // coroutine to wait for player to press KeyQ
    IEnumerator SchduleWaitingForKeyQ(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForKeyQ");
        // lock conversation before waiting
        EventCenter.GetInstance().EventTriggered("LockConversation");
        bool Q_pressed = false;
        // wait for player to press KeyQ
        while (!Q_pressed || Input.GetKey(KeyCode.Q))
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Q_pressed = true;
            yield return null;
        }
        // unlock conversation after Q is pressed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
    }

    // lock conversation and start waiting for player to press KeyU
    void WaitingForKeyU(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForKeyU");
        StartCoroutine(SchduleWaitingForKeyU(Value));
    }

    // coroutine to wait for player to press KeyU
    IEnumerator SchduleWaitingForKeyU(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForKeyU");
        // lock conversation before waiting
        EventCenter.GetInstance().EventTriggered("LockConversation");
        bool U_pressed = false;
        // wait for player to press KeyU
        while (!U_pressed || Input.GetKey(KeyCode.U))
        {
            if (Input.GetKeyDown(KeyCode.U))
                U_pressed = true;
            yield return null;
        }
        // unlock conversation after U is pressed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
    }

    // destroy PlayText_Follow
    void DestroyPlayText_Follow(List<EventValueClass> Value)
    {
        Debug.Log("DestroyPlayText_Follow");
        gameManager.GetComponent<GameManager>().SetPlayerCanMove(true);
        gameManager.GetComponent<GameManager>().SetPangolinCanPossess(true);
        gameManager.GetComponent<GameManager>().SetRabbitCanPossess(true);
        GameObject to_destroy = GameObject.Find("PlayText_Follow");
        if (to_destroy != null)
            Destroy(GameObject.Find("PlayText_Follow"));
    }
}