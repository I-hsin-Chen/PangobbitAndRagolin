using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for GameManager
// GameManager is a DontDestroyOnLoad GameObject that is used to control the game

public class GameManager : MonoBehaviour
{
    // player cannot move (left, right, up) when false
    private bool playerCanMove;
    // player cannot possess when false
    // two players are independent for teaching purpose
    private bool pangolinCanPossess;
    private bool rabbitCanPossess;

    private float changeSceneDelay = 1.0f;
    private GameObject audioManager;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerCanMove = false;
        audioManager = GameObject.Find("AudioManager");
    }

    // Update is called once per frame
    void Update()
    {
        // just for development, press 0-6 to change scene
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            ChangeSceneTo(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeSceneTo(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeSceneTo(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ChangeSceneTo(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ChangeSceneTo(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            ChangeSceneTo(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            ChangeSceneTo(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            ChangeSceneTo(9);
        }
    }

    // call this public function when win
    // this function will load next scene with fade out effect
    // don't forget to set the index of next scene
    public void Win(int nextSceneIndex)
    {
        Debug.Log("Win");
        ChangeSceneTo(nextSceneIndex);
    }

    // call this public function when you want to change scene
    public void ChangeSceneTo(int idx)
    {
        Debug.Log("ChangeSceneTo:" + idx);
        StartCoroutine(ScheduleChangeScene(changeSceneDelay, idx));
    }

    // Schedule to change scene after fade out
    // fadeOutTime: time to fade out
    // idx: index of scene to load
    IEnumerator ScheduleChangeScene(float fadeOutTime, int idx){
        // To make the game run smoothly, destroy some DontDestroyOnLoad objects
        // ==========
        GameObject toDestroy;
        toDestroy = GameObject.Find("PlayText.PlayTextSupport.EventCenter");
        if (toDestroy != null)
            Destroy(toDestroy);
        toDestroy = GameObject.Find("PlayText.PlayTextSupport.ResMgr");
        if (toDestroy != null)
            Destroy(toDestroy);
        toDestroy = GameObject.Find("PlayText.PlayTextSupport.AudioMgr");
        if (toDestroy != null)
            Destroy(toDestroy);
        // ==========
        if (idx == 1)
            audioManager.GetComponent<AudioManager>().FadeOutBGM(fadeOutTime);
        GameObject fadeCanvas = GameObject.Find("FadeCanvas");
        fadeCanvas.GetComponent<FadeHandler>().StartFadeOut(fadeOutTime);
        yield return new WaitForSeconds(fadeOutTime);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(idx);
        if (idx == 1)
            audioManager.GetComponent<AudioManager>().FadeInBGM(fadeOutTime);
        Time.timeScale = 1;
    }
    
    // Function to set playerCanMove
    public void SetPlayerCanMove(bool playerCanMove)
    {
        this.playerCanMove = playerCanMove;
    }

    // Function to get playerCanMove
    public bool GetPlayerCanMove()
    {
        return playerCanMove;
    }

    // Function to set pangolinCanPossess
    public void SetPangolinCanPossess(bool pangolinCanPossess)
    {
        this.pangolinCanPossess = pangolinCanPossess;
    }

    // Function to get pangolinCanPossess
    public bool GetPangolinCanPossess()
    {
        return pangolinCanPossess;
    }

    // Function to set rabbitCanPossess
    public void SetRabbitCanPossess(bool rabbitCanPossess)
    {
        this.rabbitCanPossess = rabbitCanPossess;
    }

    // Function to get rabbitCanPossess
    public bool GetRabbitCanPossess()
    {
        return rabbitCanPossess;
    }
}
