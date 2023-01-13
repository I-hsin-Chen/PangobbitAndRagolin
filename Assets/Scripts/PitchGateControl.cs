using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchGateControl : MonoBehaviour
{
    private GameObject objectControl;

    // Start is called before the first frame update
    void Start()
    {
        objectControl = GameObject.Find("Phonograph");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name.Length >= 6 && col.gameObject.name.Substring(0, 6) == "Marbel"){
            int i = transform.parent.gameObject.transform.parent.gameObject.name[10] - '1'; // Gate_Gear_j
            int j = transform.parent.gameObject.name[8] - '1'; // Pitch i
            objectControl.GetComponent<ObjectControl>().SetCollision(i, j);
            objectControl.GetComponent<ObjectControl>().SetMarbel(i, j, col.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.name.Length >= 6 && col.gameObject.name.Substring(0, 6) == "Marbel"){
            int i = transform.parent.gameObject.transform.parent.gameObject.name[10] - '1';
            int j = transform.parent.gameObject.name[8] - '1';
            objectControl.GetComponent<ObjectControl>().ClearCollision(i, j);
            objectControl.GetComponent<ObjectControl>().ClearMarbel(i, j);
        }
    }
}
