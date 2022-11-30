using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{
    private GameObject gameManager;
    // ===== add canvas here ===== 
    private GameObject pauseCanvas;
    private GameObject exitCanvas;
    // ===========================

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        // try to find the canvas
        // it is okay if there is no corresponding canvas, but remember to check when using it
        pauseCanvas = GameObject.Find("PauseCanvas");
        exitCanvas = GameObject.Find("ExitCanvas");

        if (pauseCanvas != null) {
            print("PauseCanvas found");
            pauseCanvas.SetActive(false);
        }
        if (exitCanvas != null) {
            print("ExitCanvas found");
            exitCanvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ShowPauseCanvas();
        }
    }

    public void GoToScene(int idx)
    {
        Debug.Log("Go To Scene " + idx);
        // To make the game run smoothly, destroy some DontDestroyOnLoad objects
        // ==========
        GameObject toDestroy = GameObject.Find("PlayText.PlayTextSupport.EventCenter");
        if (toDestroy != null)
            Destroy(toDestroy);
        toDestroy = GameObject.Find("PlayText.PlayTextSupport.ResMgr");
        if (toDestroy != null)
            Destroy(toDestroy);
        toDestroy = GameObject.Find("PlayText.PlayTextSupport.AudioMgr");
        if (toDestroy != null)
            Destroy(toDestroy);
        // ==========
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(idx);
        if (idx == 0)
            Destroy(gameManager);
        Time.timeScale = 1;
    }

    public void ReloadScene()
    {
        Debug.Log("Reload Scene");
        // To make the game run smoothly, destroy some DontDestroyOnLoad objects
        // ==========
        GameObject toDestroy = GameObject.Find("PlayText.PlayTextSupport.EventCenter");
        if (toDestroy != null)
            Destroy(toDestroy);
        toDestroy = GameObject.Find("PlayText.PlayTextSupport.ResMgr");
        if (toDestroy != null)
            Destroy(toDestroy);
        toDestroy = GameObject.Find("PlayText.PlayTextSupport.AudioMgr");
        if (toDestroy != null)
            Destroy(toDestroy);
        // ==========
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ShowPauseCanvas()
    {
        if (pauseCanvas != null) {
            Debug.Log("Show Pause Canvas");
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
        }
    }

    public void ClosePauseCanvas()
    {
        if (pauseCanvas != null) {
            Debug.Log("Close Pause Canvas");
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ShowExitCanvas()
    {
        if (exitCanvas != null) {
            Debug.Log("Show Exit Canvas");
            exitCanvas.SetActive(true);
        }
    }

    public void CloseExitCanvas()
    {
        if (exitCanvas != null) {
            Debug.Log("Close Exit Canvas");
            exitCanvas.SetActive(false);
        }
    }

    public void CloseGame()
    {
        Debug.Log("Close Game");
        Application.Quit();
    }

    public void TestButton(string name)
    {
        Debug.Log("Test Button " + name);
    }
}
