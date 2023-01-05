using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockControl : MonoBehaviour
{
    public GameObject door;
    public GameObject guard;
    public Sprite emptyImage;

    private SpriteRenderer renderer;
    private bool clockCanRotate;
    // Start is called before the first frame update
    void Start()
    {
        guard.TryGetComponent<SpriteRenderer>(out renderer);
        clockCanRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localEulerAngles.z < 1){
            door.GetComponent<Animator>().enabled = true;
            renderer.sprite = emptyImage;;
            clockCanRotate = false;
        }
    }

    public bool GetClockCanRotate(){
        return clockCanRotate;
    }
}
