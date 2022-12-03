using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
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
        GameObject fadeCanvas = GameObject.Find("FadeCanvas");
        fadeCanvas.GetComponent<FadeHandler>().StartFadeOut(fadeOutTime);
        StartCoroutine(ScheduleChangeScene(fadeOutTime, nextSceneIndex));
    }

    IEnumerator ScheduleChangeScene(float fadeOutTime, int idx){
        yield return new WaitForSeconds(fadeOutTime);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(idx);
        Time.timeScale = 1;
    }
    
}
