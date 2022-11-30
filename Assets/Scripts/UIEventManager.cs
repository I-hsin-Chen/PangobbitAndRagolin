using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{
    private GameObject gameManager;
    // ===== add canvas here ===== 
    private GameObject pauseCanvas;
    private GameObject instructionCanvas;
    private GameObject settingCanvas;
    private GameObject exitCanvas;
    // ===========================

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        // try to find the canvas
        // it is okay if there is no corresponding canvas, but remember to check when using it
        pauseCanvas = GameObject.Find("PauseCanvas");
        instructionCanvas = GameObject.Find("InstructionCanvas");
        settingCanvas = GameObject.Find("SettingCanvas");
        exitCanvas = GameObject.Find("ExitCanvas");

        if (pauseCanvas != null) {
            print("PauseCanvas found");
            pauseCanvas.SetActive(false);
        }
        if (instructionCanvas != null) {
            print("InstructionCanvas found");
            instructionCanvas.SetActive(false);
        }
        if (settingCanvas != null) {
            print("SettingCanvas found");
            settingCanvas.SetActive(false);
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
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(idx);
        if (idx == 0)
            Destroy(gameManager);
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

    public void ShowInstructionCanvas()
    {
        if (instructionCanvas != null) {
            Debug.Log("Show Instruction Canvas");
            instructionCanvas.SetActive(true);
        }
    }

    public void CloseInstructionCanvas()
    {
        if (instructionCanvas != null) {
            Debug.Log("Close Instruction Canvas");
            instructionCanvas.SetActive(false);
        }
    }

    public void ShowSettingCanvas()
    {
        if (settingCanvas != null) {
            Debug.Log("Show Setting Canvas");
            settingCanvas.SetActive(true);
        }
    }

    public void CloseSettingCanvas()
    {
        if (settingCanvas != null) {
            Debug.Log("Close Setting Canvas");
            settingCanvas.SetActive(false);
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
