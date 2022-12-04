using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    public bool canMove = true;
    public GameObject bullet_prefab = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Test
    public void TableRotate(bool clockwise) // rotate table
    {
        if(!clockwise) GetComponent<Rigidbody2D>().rotation += 90f;
        else GetComponent<Rigidbody2D>().rotation -= 90f;
        transform.position -= new Vector3(0, transform.position.y/3.6f);;
    }

    // Level_1
    public void TankRotate(bool clockwise) // only rotate barrel
    {
        GameObject obj = transform.GetChild(0).gameObject;
        if(!clockwise && obj.GetComponent<Rigidbody2D>().rotation < 45f){
            obj.GetComponent<Rigidbody2D>().rotation += 15f;
            obj.transform.position += new Vector3(-0.02f, 0.08f, 0);
        }
        else if(clockwise && obj.GetComponent<Rigidbody2D>().rotation > -30f){
            obj.GetComponent<Rigidbody2D>().rotation -= 15f;
            obj.transform.position -= new Vector3(-0.02f, 0.08f, 0);
        }
    }
    public void TankShoot() // shoot bullet
    {
        GameObject obj = transform.GetChild(0).gameObject;
        Vector2 pos = new Vector2(obj.transform.position.x, obj.transform.position.y);
        Vector2 rot = new Vector2(obj.transform.rotation[3], obj.transform.rotation[2]*2.4f);

        GameObject bullet = Instantiate(bullet_prefab, pos+rot, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(rot * 200);
    }

    public void ClockRotate(bool clockwise) // rotate clock
    {
        if(!clockwise) GetComponent<Rigidbody2D>().rotation += 30f;
        else GetComponent<Rigidbody2D>().rotation -= 30f;
    }
    
    public void PulleyWheelRotate(bool clockwise) // only rotate barrel
    {
        Rigidbody2D plateRb = GameObject.Find("LeftPlate").GetComponent<Rigidbody2D>();
        if (clockwise) plateRb.velocity = new Vector2 (0, 1);
        else plateRb.velocity = new Vector2 (0, -1);
    }
}
