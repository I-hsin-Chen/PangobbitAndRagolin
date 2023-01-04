using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayTextSupport;

// This script is for AudioManager
// AudioManager is a DontDestroyOnLoad GameObject that is used to control the audio
// All the audio related functions are in this script

public class AudioManager : MonoBehaviour
{
    // There are two audio sources to set volume independently
    private float BGMVolume = 0.5f;
    private float SEVolume = 0.5f;
    private AudioSource BGMPlayer;  // audio source for BGM, attached to GameManager
    private AudioSource SEPlayer;   // audio source for SE, attached to AudioManager
    // ===== add audio source here =====
    public AudioClip BGM;
    public AudioClip SE_Jump;
    public AudioClip SE_Possess;
    public AudioClip SE_Tower;

    // Stage_4 audio
    public AudioClip SE_Answer;
    public AudioClip SE_Pitch1;
    public AudioClip SE_Pitch2;
    public AudioClip SE_Pitch3;
    public AudioClip SE_Pitch4;
    public AudioClip SE_Pitch5;
    public AudioClip SE_Accompaniment;

    // =================================

    // Start is called before the first frame update
    void Start()
    {   
        DontDestroyOnLoad(this.gameObject);
        // Set up BGMPlayer here, BGMVolume may be changed in UIEventManager.cs
        BGMPlayer = GameObject.Find("GameManager").GetComponent<AudioSource>();
        BGMPlayer.volume = BGMVolume;
        BGMPlayer.mute = false;
        BGMPlayer.loop = true;
        BGMPlayer.clip = BGM;
        BGMPlayer.Play();
        // Set up SEPlayer here, SEVolume may be changed in UIEventManager.cs
        SEPlayer = gameObject.GetComponent<AudioSource>();
        SEPlayer.volume = SEVolume;
        SEPlayer.mute = false;
    }

    public void SetBGMVolume(float vol)
    {
        BGMVolume = vol;
        BGMPlayer.volume = BGMVolume;
    }

    public float GetBGMVolume()
    {
        return BGMVolume;
    }

    public void SetSEVolume(float vol)
    {
        SEVolume = vol;
        SEPlayer.volume = SEVolume;
        // also set the volume of PlayText typing sound
        EventCenter.GetInstance().EventTriggered("PlayText.SetVolume", SEVolume);
    }

    public float GetSEVolume()
    {
        return SEVolume;
    }

    public void FadeInBGM(float duration)
    {
        BGMPlayer.volume = 0;
        BGMPlayer.Play();
        StartCoroutine(ScheduleFadeInBGM(duration * 0.6f));
    }

    IEnumerator ScheduleFadeInBGM(float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        while (Time.time < endTime) {
            float alpha = (Time.time - startTime) / duration;
            BGMPlayer.volume = BGMVolume * alpha;
            yield return null;
        }
        BGMPlayer.volume = BGMVolume;
    }

    public void FadeOutBGM(float duration)
    {
        StartCoroutine(ScheduleFadeOutBGM(duration * 0.4f));
    }

    IEnumerator ScheduleFadeOutBGM(float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        while (Time.time < endTime) {
            float alpha = 1 - (Time.time - startTime) / duration;
            BGMPlayer.volume = BGMVolume * alpha;
            yield return null;
        }
        BGMPlayer.volume = 0;
    }

    public void PlaySE_Jump()
    {
        SEPlayer.PlayOneShot(SE_Jump);
    }

    public void PlaySE_Possess()
    {
        SEPlayer.PlayOneShot(SE_Possess);
    }

    public void PlaySE_Tower()
    {
        SEPlayer.PlayOneShot(SE_Tower);
    }

    // Stage_4 function
    public void PlaySE_Answer()
    {
        SEPlayer.PlayOneShot(SE_Answer);
    }

    public void PlaySE_Pitch1()
    {
        SEPlayer.PlayOneShot(SE_Pitch1);
    }

    public void PlaySE_Pitch2()
    {
        SEPlayer.PlayOneShot(SE_Pitch2);
    }

    public void PlaySE_Pitch3()
    {
        SEPlayer.PlayOneShot(SE_Pitch3);
    }

    public void PlaySE_Pitch4()
    {
        SEPlayer.PlayOneShot(SE_Pitch4);
    }

    public void PlaySE_Pitch5()
    {
        SEPlayer.PlayOneShot(SE_Pitch5);
    }

    public void PlaySE_Accompaniment()
    {
        SEPlayer.clip = SE_Accompaniment;
        SEPlayer.Play();
    }

    public void StopSE_Accompaniment()
    {
        SEPlayer.Stop();
    }
}
