using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamToastControl : MonoBehaviour
{
    public Sprite jamToastImage;
    private SpriteRenderer renderer;
    private bool isJammed;
    // Start is called before the first frame update
    void Start()
    {
        isJammed = false;
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.name == "Strawberry(Clone)" && !isJammed){
            renderer.sprite = jamToastImage;
            // StartCoroutine(col.gameObject.GetComponent<StrawberryControl>().scheduleDestroyStrawberry());
            isJammed = true;
        }
    }

    // private IEnumerator scheduleDestroyStrawberry (GameObject Strawberry){
    //     Strawberry.GetComponent<Animator>().enabled = true;
    //     yield return new WaitForSeconds(0.15f);
    //     GameObject.Destroy(Strawberry);
    // }
}
