using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRope : MonoBehaviour
{
    public GameObject plate;
    private float initialScale;
    private float initialLength;
    private float maxScale = 1.8f;
    private float minScale = 0.74f;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale.y;
        initialLength = transform.position.y - plate.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 platePos = plate.transform.position;
        Vector2 scale = transform.localScale;
        if(scale.y < maxScale && scale.y > minScale)
            //print(scale);
            transform.localScale = new Vector2 (scale.x, (transform.position.y - plate.transform.position.y) / initialLength * initialScale);
    }
}
