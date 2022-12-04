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
    public AudioClip SE_Jump;
    public AudioClip SE_Possess;
    public AudioClip SE_Tower;
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
        // if key Z is pressed, play SE_Jump
        if (Input.GetKeyDown(KeyCode.Z)) {
            PlaySE_Jump();
        }
        // if key X is pressed, play SE_Possess
        if (Input.GetKeyDown(KeyCode.X)) {
            PlaySE_Possess();
        }
        // if key C is pressed, play SE_Tower
        if (Input.GetKeyDown(KeyCode.C)) {
            PlaySE_Tower();
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
}
