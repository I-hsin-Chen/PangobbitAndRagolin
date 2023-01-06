using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndStory : MonoBehaviour
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
        contents.Add("They escape.");
        contents.Add("Happ Ever After");
        // Initialize the fade in/out time and duration
        fadeInTime = 1.0f;
        duration = 3.0f;
        fadeOutTime = 1.0f;
        delay = 0.5f;

        StartEnd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartEnd()
    {
        StartCoroutine(ScheduleEnd(0));
    }
    IEnumerator ScheduleEnd(int contents_index)
    {
        if (contents_index >= contents.Count) {
            Debug.Log("End of prologue");
            // Change to the next scene
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Main_Scene");
            yield return null;
        }
        else
        {
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
            StartCoroutine(ScheduleEnd(contents_index + 1));
        }
    }
}
