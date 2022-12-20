using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script contains all the functions that are called by UI elements
// Attach this script to the anywhere if needed

public class UIEventManager : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject audioManager;
    // ===== add canvas here ===== 
    private GameObject pauseCanvas;
    private GameObject exitCanvas;
    private GameObject fadeCanvas;
    // ===========================

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        audioManager = GameObject.Find("AudioManager");

        // try to find the canvas
        // it is okay if there is no corresponding canvas, but remember to check when using it
        pauseCanvas = GameObject.Find("PauseCanvas");
        exitCanvas = GameObject.Find("ExitCanvas");
        fadeCanvas = GameObject.Find("FadeCanvas");

        // Set up pauseCanvas
        if (pauseCanvas != null) {
            Debug.Log("PauseCanvas found");
            GameObject BarRed = pauseCanvas.transform.Find("Background Frame/Fill Bar Red").gameObject;
            GameObject BarGreen = pauseCanvas.transform.Find("Background Frame/Fill Bar Green").gameObject;
            // Set the value of the slider
            BarRed.GetComponent<Slider>().value = audioManager.GetComponent<AudioManager>().GetBGMVolume();
            BarGreen.GetComponent<Slider>().value = audioManager.GetComponent<AudioManager>().GetSEVolume();
            pauseCanvas.SetActive(false);
        }
        // Set up exitCanvas
        if (exitCanvas != null) {
            Debug.Log("ExitCanvas found");
            exitCanvas.SetActive(false);
        }
        // Set up fadeCanvas
        if (fadeCanvas != null) {
            Debug.Log("FadeCanvas found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            ShowPauseCanvas();
        }
    }

    // Only for testing the function of buttons
    public void TestButton(string name)
    {
        Debug.Log("button clicked: " + name);
    }

    public void GoToScene(int idx)
    {
        Debug.Log("GoToScene " + idx);
        ClosePauseCanvas();
        gameManager.GetComponent<GameManager>().ChangeSceneTo(idx);
    }

    public void ReloadScene()
    {
        Debug.Log("Reload Scene");
        ClosePauseCanvas();
        gameManager.GetComponent<GameManager>().ChangeSceneTo(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowPauseCanvas()
    {
        // if player cannot move, it should not be able to pause the game also
        if (gameManager.GetComponent<GameManager>().GetPlayerCanMove() == false)
            return;
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
        Debug.Log("Close Game. Bye~");
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
