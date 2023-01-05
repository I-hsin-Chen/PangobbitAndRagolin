using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        if (GameObject.Find("FadeCanvas/ImageBlack") != null)
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

    // 0 for not showing, 1 for during showing, 2 for finished showing
    private int instructionState = 0;

    public void QuestionButtonClicked(string instruction)
    {
        Debug.Log("Question Button Clicked");
        if (instructionState == 0)
            ShowInstruction(instruction);
        else if (instructionState == 2)
            CloseInstruction();
    }

    public void ShowInstruction(string instruction)
    {
        instructionState = 1;
        Debug.Log("Show Instruction");
        StartCoroutine(ScheduleShowInstruction(instruction));
    }

    IEnumerator ScheduleShowInstruction(string instruction){
        GameObject Instruction = GameObject.Find("InstructionCanvas/Instruction");
        if (Instruction == null) {
            Debug.Log("Instruction Object not found");
            yield break;
        }
        string text = "";
        while (text.Length < instruction.Length) {
            text = text + instruction[text.Length];
            Instruction.GetComponent<TextMeshProUGUI>().text = text;
            yield return new WaitForSeconds(0.01f);
        }
        instructionState = 2;
    }

    public void CloseInstruction()
    {
        instructionState = 1;
        Debug.Log("Close Instruction");
        StartCoroutine(ScheduleCloseInstruction());
    }

    IEnumerator ScheduleCloseInstruction(){
        GameObject Instruction = GameObject.Find("InstructionCanvas/Instruction");
        if (Instruction == null) {
            Debug.Log("Instruction Object not found");
            yield break;
        }
        string text = Instruction.GetComponent<TextMeshProUGUI>().text;
        while (text.Length > 0) {
            text = text.Substring(0, text.Length - 1);
            Instruction.GetComponent<TextMeshProUGUI>().text = text;
            yield return new WaitForSeconds(0.01f);
        }
        instructionState = 0;
    }
}
