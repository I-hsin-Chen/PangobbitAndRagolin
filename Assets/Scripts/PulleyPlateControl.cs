using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyPlateControl : MonoBehaviour
{
    private CollisionState colState;
    private bool bulletTouch = false;
    private bool characterTouch = false;
    private bool otherTouch = false;

    public virtual bool bulletForce => bulletTouch && colState.touchingCeiling;
    public virtual bool characterForce => characterTouch && colState.touchingCeiling;
    public virtual bool isForced => bulletForce || characterForce;
    private float bulletMass = 5.0f;
    private float characterMass = 1.0f;
    private float g = 9.8f;
    public float force { get; private set; } = 0.0f;


    void Awake() {
        TryGetComponent<CollisionState>(out colState);
    }

    public IEnumerator lerpPosition(Vector3 StartPos, Vector3 EndPos, float LerpTime)
    {
        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;
 
        while(Time.time < EndTime)
        {
            if (colState.grounded && EndPos.y < StartPos.y && (transform.position.y - EndPos.y) < 2.0f){
                transform.parent.gameObject.GetComponent<PulleyControlv3>().forceToStop();
            } 
            float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
            transform.position = Vector3.Lerp(StartPos, EndPos, timeProgressed);
            yield return new WaitForFixedUpdate();
        }
        
    }


    // void Update(){
    //     if (!colState.touchingCeiling){
    //         characterTouch = false;
    //         bulletTouch = false;
    //     }
    //     int bt = bulletForce ? 1 : 0 ;
    //     int ct = characterForce ? 1 : 0;
    //     force = (bt * bulletMass + ct * characterMass) * g;
    //     // print (gameObject.name + " " + force.ToString());
    // }

    // void OnCollisionStay2D(Collision2D col) {
    //     // print(col.contacts[0].normal.y);
    //     // ignore collisions that are not colliding from the ceiling side
    //     if (col.contacts[0].normal.y < 0) {
    //         // print (col.gameObject.name.StartsWith("Bullet"));
    //         if (col.gameObject.name.StartsWith("Bullet")) bulletTouch = true; 
    //         else if (col.gameObject.name.StartsWith("Rabbit") || col.gameObject.name.StartsWith("Pangolin")) characterTouch = true; 
    //         else otherTouch = true;
    //     }
    //     else {
    //         if (col.gameObject.name.StartsWith("Bullet")) bulletTouch = false; 
    //         else if (col.gameObject.name.StartsWith("Rabbit") || col.gameObject.name.StartsWith("Pangolin")) characterTouch = false; 
    //         else otherTouch = false;
    //     }
    // }

    // void OnCollisionExit2D(Collision2D col){
    //     if (col.gameObject.name.StartsWith("Bullet")) bulletTouch = false; 
    //     else if (col.gameObject.name.StartsWith("Rabbit") || col.gameObject.name.StartsWith("Pangolin")) characterTouch = false; 
    //     else otherTouch = false;
    // }


}
