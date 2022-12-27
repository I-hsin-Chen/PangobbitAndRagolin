using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlayControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private BoxCollider2D collider;

    private ContactFilter2D pressFilter;
    private List<Collider2D> contactBuffer = new List<Collider2D>();
    private bool pressed = false;

    public Sprite pressedImage;
    public Sprite unpressedImage;
    private AudioManager audiomanager;
    
    private void Awake() {
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<SpriteRenderer>(out renderer);
        TryGetComponent<BoxCollider2D>(out collider);

        audiomanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        pressFilter.useNormalAngle = true;
        pressFilter.minNormalAngle = -30.0f - 90.0f;
        pressFilter.maxNormalAngle = -30.0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int groundHits = rb.GetContacts(pressFilter, contactBuffer);
        if (!pressed){
            if (groundHits > 0){
                renderer.sprite = pressedImage;
                // collider.size = new Vector2 (collider.size.x, collider.size.y/2);
                // foreach (Collider2D col in contactBuffer)
                //     col.gameObject.transform.position -= new Vector3(0, collider.size.y, 0);

                audiomanager.PlaySE_Answer();
                pressed = true;
            }
        }
        else{
            if(groundHits <= 0){
                renderer.sprite = unpressedImage;
                // foreach (Collider2D col in contactBuffer)
                //     col.gameObject.transform.position += new Vector3(0, collider.size.y, 0);
                // collider.size = new Vector2 (collider.size.x, collider.size.y*2);

                pressed = false;
            }
        }
    }
}
