using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // ===== add audio source here =====
    public AudioClip SE1;
    // =================================

    // Start is called before the first frame update
    void Start()
    {   
        // if AudioManager become another object
        DontDestroyOnLoad(this.gameObject);
    }


    public void PlaySE1()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(SE1);
    }
}
