using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonographControl : MonoBehaviour
{
    public GameObject door;
    private AudioManager audiomanager;
    
    // Start is called before the first frame update
    void Start()
    {
        audiomanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Stage4Pass()
    {
        door.GetComponent<Animator>().enabled = true;
        audiomanager.FadeInBGM();
    }
}
