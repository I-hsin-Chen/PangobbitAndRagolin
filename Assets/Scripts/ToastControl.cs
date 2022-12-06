using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastControl : MonoBehaviour
{ 
    private bool destroyCoolDown;
    private CollisionState collisionState;

    void Start()
    {
        TryGetComponent<CollisionState>(out collisionState);
        destroyCoolDown = true;
        StartCoroutine(coolDownCntDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (!destroyCoolDown && collisionState.grounded) StartCoroutine(scheduleDestroy());
    }

    private IEnumerator coolDownCntDown(){
        yield return new WaitForSeconds(1.0f);
        destroyCoolDown = false;
    }

    private IEnumerator scheduleDestroy(){
        yield return new WaitForSeconds(1.0f);
        GameObject.Destroy(gameObject);
        yield break;
    }

    
}
