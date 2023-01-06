using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class EndStory : MonoBehaviour
{
    private GameObject textObject;                      // The text GameObject in the Canvas
    private List<string> contents = new List<string>(); // The contents to be displayed sequentially
    private float fadeInTime;                           // The time it takes for the text to fade in
    private float duration;                             // The time the text is displayed
    private float fadeOutTime;                          // The time it takes for the text to fade out
    private float delay;                                // The time between the end of the text and the start of the next text

    public Image forest;
    public Image sea;
    public Image thrid;
    public Image fourth;

    
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
        contents.Add("They finally escape.");
        contents.Add("They travel around the world, get to see beautiful things they haven't seen before.");
        contents.Add("Although the horrible memories still haunt they sometimes.");
        contents.Add("But they know they can conquer everything with each other's company.");
        // Initialize the fade in/out time and duration
        fadeInTime = 1.0f;
        duration = 3.0f;
        fadeOutTime = 1.0f;
        delay = 0.5f;

        forest.enabled = true;
        sea.enabled = false;
        thrid.enabled = false;
        fourth.enabled = false;

        
        
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
            //Change Image

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

            //if(contents_index == 0)
            //{
            //    forest.enabled = true;
            //}
            if(contents_index == 0)
            {
                forest.enabled = false;
                sea.enabled = true;
            }
            if(contents_index == 1)
            {
                forest.enabled = false;
                thrid.enabled = true;
            }
            if(contents_index == 2)
            {
                thrid.enabled = false;
                fourth.enabled = true;
            }
            

        }
    }
}
