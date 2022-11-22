using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public Rigidbody2D Pl;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float horizontalmove = Input.GetAxisRaw("Horizontal");
        Pl.velocity = new Vector2(horizontalmove * speed, 0);
        if (horizontalmove != 0)
        {
            transform.localScale = new Vector3(horizontalmove, 1, 1);
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }
    }
}
