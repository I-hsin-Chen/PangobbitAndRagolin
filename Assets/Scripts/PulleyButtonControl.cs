using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyButtonControl : MonoBehaviour
{
    private SpriteRenderer _renderer;
    public bool pressed { get; private set; } = false;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private ContactFilter2D pressFilter;
    private List<ContactPoint2D> contactBuffer = new List<ContactPoint2D>();
    private Sprite unpressedImage;
    private PulleyControlv3 pulleyCtrl;

    private float startPosition = 3.5f;
    private float endPosition = 0.5f;
    private bool isMovingDown = true;

    public Sprite pressedImage;

    private void Awake(){
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<SpriteRenderer>(out _renderer);
        TryGetComponent<BoxCollider2D>(out collider);

        pressFilter.useNormalAngle = true;
        pressFilter.minNormalAngle = -45.0f;
        pressFilter.maxNormalAngle = -45.0f + 90.0f;
    }


    void Start()
    {
        unpressedImage = _renderer.sprite;
        pulleyCtrl = GameObject.Find("Pulley").GetComponent<PulleyControlv3>();
        StartCoroutine(schedulebuttonMove());
    }

    void Update()
    {
        if (!pressed){
            int groundHits = rb.GetContacts(pressFilter, contactBuffer);
            if (groundHits > 0) StartCoroutine(pressTheButton());
        }
    }

    private IEnumerator schedulebuttonMove(){

        while (true){
            Vector3 pos = transform.position;
            if (isMovingDown){
                if (pos.y > endPosition) transform.position = new Vector3 (pos.x, pos.y - 2 * Time.fixedDeltaTime, pos.z);
                else isMovingDown = false;
            }
            else {
                if (pos.y < startPosition) transform.position = new Vector3 (pos.x, pos.y + 2 * Time.fixedDeltaTime, pos.z);
                else isMovingDown = true;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator pressTheButton(){
        pressed = true;
        _renderer.sprite = pressedImage;
        Vector2 size = collider.size;
        collider.size = new Vector2 (collider.size.x, collider.size.y/2);
        pulleyCtrl.toggleThePlates();

        yield return new WaitForSeconds(0.8f);

        pressed = false;
        _renderer.sprite = unpressedImage;
        collider.size = size;
    }
}
