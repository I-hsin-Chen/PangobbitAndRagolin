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
        print (col.gameObject.name);
        if (col.gameObject.name == "Strawberry(Clone)"){
            renderer.sprite = jamToastImage;
            GameObject.Destroy(col.gameObject);
            isJammed = true;
        }
    }
}
