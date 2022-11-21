using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseController : MonoBehaviour
{
    private bool reloading = false;
    public Image Fade_img;
    private Color c;

    public AudioClip lose_se;
    private AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
        audiosource = GameObject.Find("/AudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -3.5f && !reloading ){
            reloading = true;
            audiosource.PlayOneShot(lose_se);
            StartCoroutine(Reload());
            StartCoroutine(Fade());
        }
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator Fade(){

        c = Fade_img.color;
        for (float alpha = 0; alpha <= 1.0f; alpha += 0.01f){
            c.a = alpha;
            Fade_img.color = c;
            yield return new WaitForSeconds(0.01f);
            // yield return null;
        }
    }

    public IEnumerator FadeIn(){

        c = Fade_img.color;
        for (float alpha = 1.0f; alpha >= 0; alpha -= 0.01f){
            c.a = alpha;
            Fade_img.color = c;
            yield return new WaitForSeconds(0.01f);
            // yield return null;
        }
    }



    public IEnumerator Reload_Win()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator Fade_Win(){

        yield return new WaitForSeconds(1);
        c = Fade_img.color;
        for (float alpha = 0; alpha <= 1.0f; alpha += 0.01f){
            c.a = alpha;
            Fade_img.color = c;
            yield return new WaitForSeconds(0.01f);
            // yield return null;
        }
    }

    public void Win(){
        if (reloading) return;
        reloading = true;
        StartCoroutine(Reload_Win());
        StartCoroutine(Fade_Win());
    }

}
