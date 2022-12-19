using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSquareUIControl : MonoBehaviour
{
    private bool[] stickState;
    private ColorBoxControl ctrl;
    private List<Image> squareList;
    private enum Colors {RED,YELLOW,GREEN,BLUE}
    // Start is called before the first frame update
    void Start()
    {
        squareList = new List<Image>();
        squareList.Add(transform.Find("RedSquare").gameObject.GetComponent<Image>());
        squareList.Add(transform.Find("YellowSquare").gameObject.GetComponent<Image>());
        squareList.Add(transform.Find("GreenSquare").gameObject.GetComponent<Image>());
        squareList.Add(transform.Find("BlueSquare").gameObject.GetComponent<Image>());
        ctrl = GameObject.Find("ColorBox").GetComponent<ColorBoxControl>();
    }

    // Update is called once per frame
    void Update()
    {
        stickState = ctrl.getStickState();
        for (int i=0; i<=3; i++){
            Color c = squareList[i].color;
            if (stickState[i]) squareList[i].color = new Color(c.r, c.g, c.b, 1.0f);
            else squareList[i].color = new Color(c.r, c.g, c.b, 0);
        }
    }
}
