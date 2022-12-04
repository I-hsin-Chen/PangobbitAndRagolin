using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockControl : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localEulerAngles.z < 1)
            door.GetComponent<Animator>().enabled = true;
    }
}
