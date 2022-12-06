using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // player cannot move when false
    private bool playerCanMove;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerCanMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            StartCoroutine(ScheduleChangeScene(1.0f, 1));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            StartCoroutine(ScheduleChangeScene(1.0f, 2));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            StartCoroutine(ScheduleChangeScene(1.0f, 3));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            StartCoroutine(ScheduleChangeScene(1.0f, 4));
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            StartCoroutine(ScheduleChangeScene(1.0f, 5));
        }
    }

    // call this public function when win
    // this function will load next scene with fade out effect
    // don't forget to set the index of next scene
    public void Win(int nextSceneIndex)
    {
        Debug.Log("Win");
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
        float fadeOutTime = 1.0f;
        StartCoroutine(ScheduleChangeScene(fadeOutTime, nextSceneIndex));
    }

    IEnumerator ScheduleChangeScene(float fadeOutTime, int idx){
        GameObject fadeCanvas = GameObject.Find("FadeCanvas");
        fadeCanvas.GetComponent<FadeHandler>().StartFadeOut(fadeOutTime);
        yield return new WaitForSeconds(fadeOutTime);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(idx);
        Time.timeScale = 1;
    }
    
    public void SetPlayerCanMove(bool playerCanMove)
    {
        this.playerCanMove = playerCanMove;
    }

    public bool GetPlayerCanMove()
    {
        return playerCanMove;
    }
}
