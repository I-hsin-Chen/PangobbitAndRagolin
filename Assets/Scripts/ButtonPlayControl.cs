using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlayControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private BoxCollider2D collider;

    private ContactFilter2D pressFilter;
    private List<ContactPoint2D> contactBuffer = new List<ContactPoint2D>();
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
        CheckButtonPressed();
    }

    void CheckButtonPressed()
    {
        int groundHits = rb.GetContacts(pressFilter, contactBuffer);
        if (!pressed && groundHits > 0)
            StartCoroutine(ButtonPlayAnswer());
    }

    IEnumerator ButtonPlayAnswer()
    {
        pressed = true;
        renderer.sprite = pressedImage;
        collider.size = new Vector2 (collider.size.x, collider.size.y/2);

        audiomanager.PlaySE_Answer();
        yield return new WaitForSeconds(1.75f); // remain the button pressed when playing answer

        renderer.sprite = unpressedImage;
        collider.size = new Vector2 (collider.size.x, collider.size.y*2);
        yield return new WaitForSeconds(0.25f); // make the button rebound for a while
        pressed = false;
    }
}
