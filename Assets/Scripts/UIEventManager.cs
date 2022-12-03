using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventManager : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject audioManager;
    // ===== add canvas here ===== 
    private GameObject pauseCanvas;
    private GameObject exitCanvas;
    // ===========================

    // set the index of mainMenu to help destroy objects
    private int mainMenuSceneIndex = 2;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        audioManager = GameObject.Find("AudioManager");

        // try to find the canvas
        // it is okay if there is no corresponding canvas, but remember to check when using it
        pauseCanvas = GameObject.Find("PauseCanvas");
        exitCanvas = GameObject.Find("ExitCanvas");

        if (pauseCanvas != null) {
            Debug.Log("PauseCanvas found");
            GameObject BarRed = pauseCanvas.transform.Find("Background Frame/Fill Bar Red").gameObject;
            GameObject BarGreen = pauseCanvas.transform.Find("Background Frame/Fill Bar Green").gameObject;
            BarRed.GetComponent<Slider>().value = audioManager.GetComponent<AudioManager>().GetBGMVolume();
            BarGreen.GetComponent<Slider>().value = audioManager.GetComponent<AudioManager>().GetSEVolume();
            pauseCanvas.SetActive(false);
        }
        if (exitCanvas != null) {
            Debug.Log("ExitCanvas found");
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

    public void TestButton(string name)
    {
        Debug.Log("button clicked: " + name);
    }

    public void GoToScene(int idx)
    {
        Debug.Log("Go To Scene " + idx);
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
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(idx);
        if (idx == mainMenuSceneIndex) {
            Destroy(gameManager);
            Destroy(audioManager);
        }
        Time.timeScale = 1;
    }

    public void ReloadScene()
    {
        Debug.Log("Reload Scene");
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
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ShowPauseCanvas()
    {
        if (pauseCanvas == null)
            pauseCanvas = GameObject.Find("PauseCanvas");
        if (pauseCanvas != null) {
            Debug.Log("Show Pause Canvas");
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
        }
    }

    public void ClosePauseCanvas()
    {
        if (pauseCanvas == null)
            pauseCanvas = GameObject.Find("PauseCanvas");
        if (pauseCanvas != null) {
            Debug.Log("Close Pause Canvas");
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ShowExitCanvas()
    {
        if (exitCanvas == null)
            exitCanvas = GameObject.Find("ExitCanvas");
        if (exitCanvas != null) {
            Debug.Log("Show Exit Canvas");
            exitCanvas.SetActive(true);
        }
    }

    public void CloseExitCanvas()
    {
        if (exitCanvas == null)
            exitCanvas = GameObject.Find("ExitCanvas");
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

    public void ModifyBGMVolume(float vol)
    {
        Debug.Log("Modify BGM Volume: " + vol);
        if (audioManager == null)
            audioManager = GameObject.Find("AudioManager");
        audioManager.GetComponent<AudioManager>().SetBGMVolume(vol);
    }

    public void ModifySEVolume(float vol)
    {
        Debug.Log("Modify SE Volume: " + vol);
        if (audioManager == null)
            audioManager = GameObject.Find("AudioManager");
        audioManager.GetComponent<AudioManager>().SetSEVolume(vol);
    }
}
