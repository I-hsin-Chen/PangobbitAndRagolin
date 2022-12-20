using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script plays the prologue story.

public class Prologue : MonoBehaviour
{
    private GameObject textObject;                      // The text GameObject in the Canvas
    private List<string> contents = new List<string>(); // The contents to be displayed sequentially
    private float fadeInTime;                           // The time it takes for the text to fade in
    private float duration;                             // The time the text is displayed
    private float fadeOutTime;                          // The time it takes for the text to fade out
    private float delay;                                // The time between the end of the text and the start of the next text

    // Start is called before the first frame update
    void Start()
    {
        // Find the text object
        textObject = GameObject.Find("Prologue/Canvas/Text");
        if (textObject != null) {
            Debug.Log("Text object found");
        }
        // Initialize the text to be empty and invisible
        textObject.GetComponent<TextMeshProUGUI>().text = "";
        textObject.GetComponent<TextMeshProUGUI>().alpha = 0.0f;
        // Initialize the contents, just for testing now
        contents.Add("Once upon a time, \nthere was a biotechnology laboratory that conducted inhumane experiments on many animals in order to study illegal drugs.");
        contents.Add("In these inhumane biological experiments, an accident occurred, and two animals evolved unimaginable extraordinary superpowers because of the drug and escaped from control.");
        // Initialize the fade in/out time and duration
        fadeInTime = 1.0f;
        duration = 2.0f;
        fadeOutTime = 1.0f;
        delay = 0.5f;

        // Start the prologue
        StartPrologue();
    }

    public void StartPrologue(){
        Debug.Log("Start Prologue");
        StartCoroutine(SchedulePrologue(0));
    }

    IEnumerator SchedulePrologue(int contents_index) {
        // If the index is out of range, stop the prologue and change to the next scene
        if (contents_index >= contents.Count) {
            Debug.Log("End of prologue");
            // Change to the next scene
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Test");
            yield return null;
        }

        float startTime;
        float endTime;
        float alpha;
        textObject.GetComponent<TextMeshProUGUI>().text = contents[contents_index];
        Debug.Log("Displaying contents " + contents_index);
        // Fade in the text
        startTime = Time.time;
        endTime = startTime + fadeInTime;
        while (Time.time < endTime) {
            alpha = (Time.time - startTime) / fadeInTime;
            textObject.GetComponent<TextMeshProUGUI>().alpha = alpha;
            yield return null;
        }
        // Display the text
        yield return new WaitForSeconds(duration);
        // Fade out the text
        startTime = Time.time;
        endTime = startTime + fadeOutTime;
        while (Time.time < endTime) {
            alpha = 1.0f - (Time.time - startTime) / fadeOutTime;
            textObject.GetComponent<TextMeshProUGUI>().alpha = alpha;
            yield return null;
        }
        // Delay before the next text
        yield return new WaitForSeconds(delay);
        // Start the next text
        StartCoroutine(SchedulePrologue(contents_index + 1));
    }
}
