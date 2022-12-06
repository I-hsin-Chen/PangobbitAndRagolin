using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private Animator animator;
    private GameObject gameManager;

    private bool rabbitPass = false;
    private bool pangolinPass = false;
    
    private void Awake(){
        TryGetComponent<Animator>(out animator);
        gameManager = GameObject.Find("GameManager");
        rabbitPass = false;
        pangolinPass = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Rabbit"){
            rabbitPass = true;
            if(pangolinPass && animator.enabled == true)
                gameManager.GetComponent<GameManager>().Win(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (col.gameObject.name == "Pangolin"){
            pangolinPass = true;
            if(rabbitPass && animator.enabled == true)
                gameManager.GetComponent<GameManager>().Win(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Rabbit")
            rabbitPass = false;
        if (col.gameObject.name == "Pangolin")
            pangolinPass = false;
    }
}
