using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLightControl : MonoBehaviour
{
    private Light light;
    private float timer;
    public Door_End door;
    private bool loadEndScene;
    public float fadeSpeed;
    public SpriteRenderer rabbit;
    public SpriteRenderer pangolin;

    private bool isDisappear = false;

    //private float int;
    void Awake()
    {
        //rabbit = GetComponent<SpriteRenderer>();
        //pangolin = GetComponent<SpriteRenderer>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light>();
        timer = 0;
        light.intensity = 0;
        light.range = 0;
        loadEndScene = false;

        //StartCoroutine(Fade());
    }

    // Update is called once per frame
    void Update()
    {
        if(door.touched)
        {
            timer += Time.deltaTime;
            print(timer);
            if(timer < 4.0f)
            {
                light.intensity = light.intensity + 0.5f;
                light.range = light.range + 0.5f;
                if (!isDisappear) StartCoroutine(Fade());
            }
            else
            {
                if(!loadEndScene)
                {
                    loadEndScene = true;
                    GameObject.Find("GameManager").GetComponent<GameManager>().ChangeSceneTo(10);
                }
                
            }
            //light.intensity = 100;
        }
    }
    IEnumerator Fade()
    {
        isDisappear = true;
        while(rabbit.color.a > 0)
        {
            rabbit.color = new Color(rabbit.color.r, rabbit.color.g, rabbit.color.b, rabbit.color.a - fadeSpeed);
            pangolin.color = new Color(pangolin.color.r, pangolin.color.g, pangolin.color.b, pangolin.color.a - fadeSpeed);
            yield return new WaitForFixedUpdate();
        }
    }
}
