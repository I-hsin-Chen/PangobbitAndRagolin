using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private BoxCollider2D collider;

    private ContactFilter2D pressFilter;
    private List<ContactPoint2D> contactBuffer = new List<ContactPoint2D>();
    public bool pressed { get; private set; } = false;

    public Sprite pressedImage;
    public GameObject door;

    private void Awake(){
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<SpriteRenderer>(out renderer);
        TryGetComponent<BoxCollider2D>(out collider);

        pressFilter.useNormalAngle = true;
        pressFilter.minNormalAngle = -45.0f - 90.0f;
        pressFilter.maxNormalAngle = -45.0f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pressed){
            int groundHits = rb.GetContacts(pressFilter, contactBuffer);
            if (groundHits > 0){
                pressed = true;
                renderer.sprite = pressedImage;

                Vector2 size = collider.size;
                collider.size = new Vector2 (collider.size.x, collider.size.y/2);
                door.GetComponent<Animator>().enabled = true;
            }
        }
    }
}
