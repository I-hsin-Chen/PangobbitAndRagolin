using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_End : MonoBehaviour
{
    private Animator animator;
    private GameObject gameManager;
    public bool touched;
    

    private void Awake()
    {
        TryGetComponent<Animator>(out animator);
        gameManager = GameObject.Find("GameManager");
        touched = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Rabbit" || col.gameObject.name == "Pangolin")
        {
            animator.enabled = true;
            touched = true;
            print(touched);
        }
        
    }
}
