using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeScene : MonoBehaviour
{
    public PlayerControl pc;
    private bool isChanges = false;
    public GameObject icon;
    // Start is called before the first frame update
    void Start()
    {
        
    }   

    // Update is called once per frame
    void Update()
    {
        if((pc.isRabbit||pc.isPangolin) && !isChanges)
        {
            isChanges = true;
            print("change");
            if (icon.name == "Icon_test")
                GameObject.Find("GameManager").GetComponent<GameManager>().ChangeSceneTo(2);
            if (icon.name == "Icon1")
                GameObject.Find("GameManager").GetComponent<GameManager>().ChangeSceneTo(3);
            if (icon.name == "Icon2")
                GameObject.Find("GameManager").GetComponent<GameManager>().ChangeSceneTo(4);
            if (icon.name == "Icon3")
                GameObject.Find("GameManager").GetComponent<GameManager>().ChangeSceneTo(5);
            if (icon.name == "Icon4")
                GameObject.Find("GameManager").GetComponent<GameManager>().ChangeSceneTo(6);
        }
            
        
    }
}
