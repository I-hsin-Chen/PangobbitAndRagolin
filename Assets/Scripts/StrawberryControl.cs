using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryControl : MonoBehaviour
{
    private bool destroyCoolDown;
    private bool isDestroy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destroyCoolDown = true;
        isDestroy = false;
        StartCoroutine(coolDownCntDown());
    }

    private IEnumerator coolDownCntDown(){
        yield return new WaitForSeconds(0.2f);
        destroyCoolDown = false;
    }

    private void OnCollisionStay2D(Collision2D col){
        if (!destroyCoolDown && !isDestroy) StartCoroutine(scheduleDestroyStrawberry());
    }

    public IEnumerator scheduleDestroyStrawberry (){
        isDestroy = true;
        GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.15f);
        GameObject.Destroy(gameObject);
    }
}
