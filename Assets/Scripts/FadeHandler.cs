using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is attached to FadeCanvas
// It is used to perform fade in and fade out effect

public class FadeHandler : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject imageBlack;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        imageBlack = gameObject.transform.Find("ImageBlack").gameObject;
        // Start fade in at the beginning in every scene
        StartFadeIn();
    }

    // Start fade in effect, default duration is 1 second
    // This function should be called at the beginning of every scene
    public void StartFadeIn(float duration = 1.0f){
        Debug.Log("StartFadeIn");
        StartCoroutine(FadeIn(duration));
    }

    // Fade in effect
    IEnumerator FadeIn(float duration){
        float startTime = Time.time;
        float endTime = startTime + duration;
        imageBlack.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        while (Time.time < endTime) {
            float alpha = 1 - (Time.time - startTime) / duration;
            imageBlack.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        imageBlack.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        // After fade in, if there is no story in the scene, player can do actions
        // otherwise, player cannot do actions until the story is finished
        // (playerCanMove, pangolinCanPossess, and rabbitCanPossess will be set to true in story Graph)
        if (GameObject.Find("PlayText_Follow") == null) {
            gameManager.GetComponent<GameManager>().SetPlayerCanMove(true);        
            gameManager.GetComponent<GameManager>().SetPangolinCanPossess(true);
            gameManager.GetComponent<GameManager>().SetRabbitCanPossess(true);
        }
        // After fade in, hide the black image to avoid blocking the UI
        imageBlack.SetActive(false);
    }

    // Start fade out effect, default duration is 1 second
    // This function should be called whenever there is a scene change
    public void StartFadeOut(float duration = 1.0f){
        Debug.Log("StartFadeOut");
        StartCoroutine(FadeOut(duration));
    }

    // Fade out effect, will also set playerCanMove, pangolinCanPossess, and rabbitCanPossess to false
    public IEnumerator FadeOut(float duration){
        // Before fade out, show the black image
        imageBlack.SetActive(true);
        // During fade out, player cannot move or possess
        gameManager.GetComponent<GameManager>().SetPlayerCanMove(false);
        gameManager.GetComponent<GameManager>().SetPangolinCanPossess(false);
        gameManager.GetComponent<GameManager>().SetRabbitCanPossess(false);
        float startTime = Time.time;
        float endTime = startTime + duration;
        imageBlack.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        while (Time.time < endTime) {
            float alpha = (Time.time - startTime) / duration;
            imageBlack.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        imageBlack.GetComponent<Image>().color = new Color(0, 0, 0, 1);
    }
}
