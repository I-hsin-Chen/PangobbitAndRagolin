using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbelControl : MonoBehaviour
{
    private bool inhole = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetInHole()
    {
        return inhole;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "hole") inhole = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "hole") inhole = false;
    }
}
