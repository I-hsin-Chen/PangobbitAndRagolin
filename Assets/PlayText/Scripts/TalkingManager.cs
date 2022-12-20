using System.Collections;
using System.Collections.Generic;
using GraphSpace;
using PlayTextSupport;
using UnityEngine;

/// <summary>
/// Here is a sample to demonstrate how to communicate with PlayText.
/// </summary>
public class TalkingManager : MonoBehaviour
{
    public DialogueGraph Graph;
    public bool LockE = false;

    void Start()
    {
        // prepared for disabling graph transition in game
        EventCenter.GetInstance().AddEventListener("LockKeyE", LockKeyE);
        EventCenter.GetInstance().AddEventListener("UnlockKeyE", UnlockKeyE);
    }

    private void Update()
    {
        // change the key to space instead of E
        if(Input.GetKeyDown(KeyCode.Space) && !LockE)
        {
            EventCenter.GetInstance().EventTriggered("PlayText.Play", Graph);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            EventCenter.GetInstance().EventTriggered("PlayText.OptionUp");
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            EventCenter.GetInstance().EventTriggered("PlayText.OptionDown");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            EventCenter.GetInstance().EventTriggered("PlayText.Stop");
        }
    }

    void LockKeyE()
    {
        Debug.Log("LockKeyE");
        LockE = true;
    }

    void UnlockKeyE()
    {
        Debug.Log("UnlockKeyE");
        LockE = false;
    }
}
