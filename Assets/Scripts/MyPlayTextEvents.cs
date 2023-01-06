using System.Collections;
using System.Collections.Generic;
using GraphSpace;
using PlayTextSupport;
using UnityEngine;


// This script contains all customized the events used in PlayText
// Most functions should be triggered by EventCenter, but not directly called
// Add events to EventCenter in Start()

// Outer Events:
// LockConversation: Lock Conversation before certain event
// UnLockConversation: Unlock Conversation after certain event
// PlayText.Play: Play/Continue the graph
// PlayText.ForceNext: Force to go to next node even if the current node is not finished
// PlayText.SetVolume: Set the volume of PlayText audioMgr
// PlayText.Stop: Stop the graph

public class MyPlayTextEvents : MonoBehaviour
{
    // all used graphs should be assigned in the inspector
    public DialogueGraph Graph_Stage_0 = null;
    public DialogueGraph Graph_Stage_1 = null;
    public DialogueGraph Graph_Stage_2 = null;
    public DialogueGraph Graph_Stage_3 = null;
    public DialogueGraph Graph_Stage_4 = null;
    public DialogueGraph Graph_Tank_Rabbit = null;
    public DialogueGraph Graph_Tank_Pangolin = null;
    public DialogueGraph Graph_Phonograph = null;
    public DialogueGraph Graph_IamStuck_Rabbit = null;
    public DialogueGraph Graph_IamStuck_Pangolin = null;
    private GameObject gameManager;
    private GameObject audioManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        audioManager = GameObject.Find("AudioManager");

        // set the typing volume (same with the SE volume)
        // typing volume will be handle by AudioManager in the rest of the game
        EventCenter.GetInstance().EventTriggered("PlayText.SetVolume", audioManager.GetComponent<AudioManager>().GetSEVolume());

        // adding events to EventCenter by stage
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("PrintEvent", PrintEvent);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("SetPlayerCanMove", SetPlayerCanMove);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("SetPangolinCanPossess", SetPangolinCanPossess);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("SetRabbitCanPossess", SetRabbitCanPossess);
        EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("FinishConversation", FinishConversation);
        string curStage = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Debug.Log("Adding PlayTextEvents in " + curStage);
        if (curStage == "Stage_0") {
            // Stage_0 events here
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyAD", WaitingForKeyAD);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyJL", WaitingForKeyJL);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyW", WaitingForKeyW);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyI", WaitingForKeyI);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForPangolinPossessTable", WaitingForPangolinPossessTable);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForRabbitPossessTable", WaitingForRabbitPossessTable);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyQ", WaitingForKeyQ);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyU", WaitingForKeyU);
        }
        else if (curStage == "Stage_1") {
            // Stage_1 events here
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("StartColorBoxChallenge0", StartColorBoxChallenge0);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("StartColorBoxChallenge1", StartColorBoxChallenge1);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("ResetStickState", ResetStickState);
        }
        else if (curStage == "Stage_2") {
            // Stage_2 events here
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyEorQ", WaitingForKeyEorQ);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyOorU", WaitingForKeyOorU);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("AdjustBubbleOffsetY", AdjustBubbleOffsetY);
        }
        else if (curStage == "Stage_3") {
            // Stage_3 events here
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyEorQ", WaitingForKeyEorQ);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyOorU", WaitingForKeyOorU);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("AdjustBubbleOffsetY", AdjustBubbleOffsetY);
        }
        else if (curStage == "Stage_4") {
            // Stage_4 events here
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("FadeOutBGM", FadeOutBGM);
            EventCenter.GetInstance().AddEventListener<List<EventValueClass>>("WaitingForKeyWorQ", WaitingForKeyWorQ);
        }

        // play graphs
        if (curStage == "Stage_0") {
            // play Graph_Stage_0
            if (Graph_Stage_0 == null)
                Debug.LogError("Graph_Stage_0 is not assigned!");
            EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph_Stage_0);
        }
        else if (curStage == "Stage_1") {
            // play Graph_Stage_1
            if (Graph_Stage_1 == null)
                Debug.LogError("Graph_Stage_1 is not assigned!");
            EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph_Stage_1);
        }
        else if (curStage == "Stage_2") {
            // play Graph_Stage_2
            if (Graph_Stage_2 == null)
                Debug.LogError("Graph_Stage_2 is not assigned!");
            EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph_Stage_2);
        }
        else if (curStage == "Stage_3") {
            // play Graph_Stage_3
            if (Graph_Stage_3 == null)
                Debug.LogError("Graph_Stage_3 is not assigned!");
            EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph_Stage_3);
        }
        else if (curStage == "Stage_4") {
            // play Graph_Stage_4
            if (Graph_Stage_4 == null)
                Debug.LogError("Graph_Stage_4 is not assigned!");
            EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph_Stage_4);
        }
    }

    // for testing purpose
    void PrintEvent(List<EventValueClass> Value)
    {
        Debug.Log("Hello World!");
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
        bool pangolinIsPossessing = false;
        // wait for Pangolin to possess a table
        while (!pangolinIsPossessing || Input.GetKey(KeyCode.Q))
        {
            if (GameObject.Find("Table").GetComponent<PlayerControl>().isPangolin ||
                GameObject.Find("Table_0").GetComponent<PlayerControl>().isPangolin ) {
                pangolinIsPossessing = true;
            }
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
        bool rabbitIsPossessing = false;
        // wait for Rabbit to possess a table
        while (!rabbitIsPossessing || Input.GetKey(KeyCode.U))
        {
            if (GameObject.Find("Table").GetComponent<PlayerControl>().isRabbit ||
                GameObject.Find("Table_0").GetComponent<PlayerControl>().isRabbit ) {
                rabbitIsPossessing = true;
            }
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

    // lock conversation and start waiting for player to press KeyE or keyQ
    void WaitingForKeyEorQ(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForKeyEorQ");
        StartCoroutine(SchduleWaitingForKeyEorQ(Value));
    }

    // coroutine to wait for player to press KeyE or keyQ
    IEnumerator SchduleWaitingForKeyEorQ(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForKeyEorQ");
        // lock conversation before waiting
        EventCenter.GetInstance().EventTriggered("LockConversation");
        bool E_pressed = false;
        bool Q_pressed = false;
        // wait for player to press KeyE or keyQ
        while ( (!E_pressed || Input.GetKey(KeyCode.E)) && (!Q_pressed || Input.GetKey(KeyCode.Q)) )
        {
            if (Input.GetKeyDown(KeyCode.E))
                E_pressed = true;
            if (Input.GetKeyDown(KeyCode.Q))
                Q_pressed = true;
            yield return null;
        }
        // unlock conversation after E or Q is pressed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
    }

    // lock conversation and start waiting for player to press KeyO or keyU
    void WaitingForKeyOorU(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForKeyOorU");
        StartCoroutine(SchduleWaitingForKeyOorU(Value));
    }

    // coroutine to wait for player to press KeyO or keyU
    IEnumerator SchduleWaitingForKeyOorU(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForKeyOorU");
        // lock conversation before waiting
        EventCenter.GetInstance().EventTriggered("LockConversation");
        bool O_pressed = false;
        bool U_pressed = false;
        // wait for player to press KeyO or keyU
        while ( (!O_pressed || Input.GetKey(KeyCode.O)) && (!U_pressed || Input.GetKey(KeyCode.U)) )
        {
            if (Input.GetKeyDown(KeyCode.O))
                O_pressed = true;
            if (Input.GetKeyDown(KeyCode.U))
                U_pressed = true;
            yield return null;
        }
        // unlock conversation after O or U is pressed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
    }

    // Start the color box challenge 0 for story
    void StartColorBoxChallenge0(List<EventValueClass> Value)
    {
        Debug.Log("StartFirstColorBoxChallenge");
        EventCenter.GetInstance().EventTriggered("LockConversation");
        GameObject.Find("ColorBox").GetComponent<ColorBoxControl>().StartChallenge(0);
        StartCoroutine(WaitForChallenge0());
    }

    IEnumerator WaitForChallenge0()
    {
        yield return new WaitForSeconds(4.0f);
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
    }

    // Reset the color box status
    void ResetStickState(List<EventValueClass> Value)
    {
        Debug.Log("ResetStickState");
        GameObject.Find("ColorBox").GetComponent<ColorBoxControl>().resetStickState();
    }

    // Start the color box challenge 1
    void StartColorBoxChallenge1(List<EventValueClass> Value)
    {
        Debug.Log("StartColorBoxChallenge1");
        GameObject.Find("ColorBox").GetComponent<ColorBoxControl>().StartChallenge(1);
    }

    // Finish the conversation
    void FinishConversation(List<EventValueClass> Value)
    {
        Debug.Log("FinishConversation");
        gameManager.GetComponent<GameManager>().SetPlayerCanMove(true);
        gameManager.GetComponent<GameManager>().SetPangolinCanPossess(true);
        gameManager.GetComponent<GameManager>().SetRabbitCanPossess(true);
        EventCenter.GetInstance().EventTriggered("PlayText.Stop");
    }

    void FadeOutBGM(List<EventValueClass> Value)
    {
        Debug.Log("FadeOutBGM");
        GameObject.Find("AudioManager").GetComponent<AudioManager>().FadeOutBGM();
    }

    void AdjustBubbleOffsetY(List<EventValueClass> Value)
    {
        int offset = Value[0].intValue;
        Debug.Log("AdjustBubbleOffsetY: " + offset);
        GameObject talkingManager = GameObject.Find("MyPlayText_Follow/TalkingManager");
        if (talkingManager != null)
        {
            talkingManager.GetComponent<PlayText>().BubblePositionOffset += new Vector2(0, offset);
            talkingManager.GetComponent<PlayText>().PointerOffset += new Vector2(0, offset);
        }
    }

    // lock conversation and start waiting for player to press KeyW or keyQ
    void WaitingForKeyWorQ(List<EventValueClass> Value)
    {
        Debug.Log("WaitingForKeyWorQ");
        StartCoroutine(SchduleWaitingForKeyWorQ(Value));
    }

    // coroutine to wait for player to press KeyW or keyQ
    IEnumerator SchduleWaitingForKeyWorQ(List<EventValueClass> Value)
    {
        Debug.Log("SchduleWaitingForKeyWorQ");
        // lock conversation before waiting
        EventCenter.GetInstance().EventTriggered("LockConversation");
        bool W_pressed = false;
        bool Q_pressed = false;
        // wait for player to press KeyO or keyU
        while ( (!W_pressed || Input.GetKey(KeyCode.W)) && (!Q_pressed || Input.GetKey(KeyCode.Q)) )
        {
            if (Input.GetKeyDown(KeyCode.W))
                W_pressed = true;
            if (Input.GetKeyDown(KeyCode.Q))
                Q_pressed = true;
            yield return null;
        }
        // unlock conversation after O or U is pressed
        EventCenter.GetInstance().EventTriggered("UnLockConversation");
        EventCenter.GetInstance().EventTriggered("PlayText.ForceNext");
    }

    // ===== Functions that can be called directly from other scripts =====

    private bool tankPossessed_Rabbit = false;
    private bool tankPossessed_Pangolin = false;
    // Call this function when Tank is possessed
    // who: "Rabbit" or "Pangolin"
    public void PlayGraphTank(string who)
    {
        switch (who)
        {
            case "Rabbit":
                if (tankPossessed_Rabbit)
                    return;
                Debug.Log("Play Graph_Tank_Rabbit");
                tankPossessed_Rabbit = true;
                EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph_Tank_Rabbit);
                break;
            case "Pangolin":
                if (tankPossessed_Pangolin)
                    return;
                Debug.Log("Play Graph_Tank_Pangolin");
                tankPossessed_Pangolin = true;
                EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph_Tank_Pangolin);
                break;
            default:
                Debug.LogError("PlayGraphTank: who is invalid");
                break;
        }
    }

    private bool phonographPossessed_Pangolin = false;
    public void PlayGraphPhonograph()
    {
        if (phonographPossessed_Pangolin)
            return;
        Debug.Log("Play Graph_Phonograph_Pangolin");
        phonographPossessed_Pangolin = true;
        EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph_Phonograph);
    }

    // Call this function when someone is stuck
    // who: "Rabbit" or "Pangolin"
    public void PlayGraphIamStuck(string who)
    {
        switch (who)
        {
            case "Rabbit":
                Debug.Log("Play Graph_IamStuck_Rabbit");
                EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph_IamStuck_Rabbit);
                break;
            case "Pangolin":
                Debug.Log("Play Graph_IamStuck_Pangolin");
                EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph_IamStuck_Pangolin);
                break;
            default:
                Debug.LogError("PlayGraphIamStuck: who is invalid");
                break;
        }
    }

}