using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLengthUpdate : MonoBehaviour
{
    public GameObject plate;
    private float initialScale;
    private float initialLength;
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
        transform.localScale = new Vector2 (scale.x, (transform.position.y - plate.transform.position.y) / initialLength * initialScale);
    }
}
