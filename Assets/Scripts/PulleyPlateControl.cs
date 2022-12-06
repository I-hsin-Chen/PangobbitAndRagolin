using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyPlateControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private float DragForce;

    private void Awake() {

        TryGetComponent<Rigidbody2D>(out rb);
        // if (name == "LeftPlate") DragForce = GameObject.Find("Pulley").GetComponent<PulleyControl>().ropeDragForceLeft;
        // else if (name == "RightPlate") DragForce = GameObject.Find("Pulley").GetComponent<PulleyControl>().ropeDragForceRight;
        // TryGetComponent<Animator>(out animator);
    }

    void OnCollisionStay2D(Collision2D col) {

        Vector3 normal = col.contacts[0].normal;
        if (name == "RightPlate" && normal == new Vector3(0, -1, 0))
            GameObject.Find("Pulley").GetComponent<PulleyControl>().ChangeRightForce(rb.mass * 9.8f + col.gameObject.GetComponent<Rigidbody2D>().mass * 9.8f );
        
    }

    // void OnCollisionExit2D(Collision2D col) {

    //     if (name == "RightPlate")
    //         GameObject.Find("Pulley").GetComponent<PulleyControl>().ChangeRightForce(rb.mass * 9.8f);
    // } 



}
