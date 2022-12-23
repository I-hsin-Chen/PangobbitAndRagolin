using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAreaControl : MonoBehaviour
{
    private bool tapOpened = false;
    private RabbitGetPinkControl pinkCtrl;
    private SpriteRenderer waterRenderer;

    public Sprite waterInitialSprite;
    // Start is called before the first frame update
    void Start()
    {
        pinkCtrl = GameObject.Find("Rabbit").GetComponent<RabbitGetPinkControl>();
        waterRenderer = GameObject.Find("/WaterTap/Water").GetComponent<SpriteRenderer>();
    }

    public void settapOpened(bool open){
        tapOpened = open;
        if (!open) waterRenderer.sprite = waterInitialSprite;
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.name == "Rabbit" && pinkCtrl.isPink && tapOpened){
            StartCoroutine(pinkCtrl.scheduleCleanAnimation());
        }
    }
}
