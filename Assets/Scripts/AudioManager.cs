using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float BGMVolume = 0.5f;
    private float SEVolume = 0.5f;
    private AudioSource BGMPlayer;
    private AudioSource SEPlayer;
    // ===== add audio source here =====
    public AudioClip BGM;
    public AudioClip SE1;
    public AudioClip SE2;
    // =================================

    // Start is called before the first frame update
    void Start()
    {   
        DontDestroyOnLoad(this.gameObject);
        BGMPlayer = GameObject.Find("GameManager").GetComponent<AudioSource>();
        BGMPlayer.volume = BGMVolume;
        BGMPlayer.mute = false;
        BGMPlayer.loop = true;
        BGMPlayer.clip = BGM;
        BGMPlayer.Play();
        SEPlayer = gameObject.GetComponent<AudioSource>();
        SEPlayer.volume = SEVolume;
        SEPlayer.mute = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if key Z is pressed, play SE1
        if (Input.GetKeyDown(KeyCode.Z)) {
            PlaySE1();
        }
        // if key X is pressed, play SE2
        if (Input.GetKeyDown(KeyCode.X)) {
            PlaySE2();
        }
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
    }

    public float GetSEVolume()
    {
        return SEVolume;
    }

    public void PlaySE1()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(SE1);
    }

    public void PlaySE2()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(SE2);
    }
}
