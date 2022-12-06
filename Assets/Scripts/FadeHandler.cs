using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeHandler : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject imageBlack;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        imageBlack = gameObject.transform.Find("ImageBlack").gameObject;
        StartFadeIn();
    }

    public void StartFadeIn(float duration = 1.0f){
        Debug.Log("StartFadeIn");
        StartCoroutine(FadeIn(duration));
    }

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
        gameManager.GetComponent<GameManager>().SetPlayerCanMove(true);
    }

    public void StartFadeOut(float duration = 1.0f){
        Debug.Log("StartFadeOut");
        StartCoroutine(FadeOut(duration));
    }

    public IEnumerator FadeOut(float duration){
        gameManager.GetComponent<GameManager>().SetPlayerCanMove(false);
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
