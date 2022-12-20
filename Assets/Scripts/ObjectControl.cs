using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    public bool canMove = true;
    private bool hinted;
    private bool possessed;
    private GameObject myHint;
    public GameObject bullet_prefab = null;
    private SpriteRenderer[] renderers;

    // for highlighing the object that has been raycasted
    private SpriteRenderer my_renderer;
    private Color highlightColor;

    private Object pangolinHint;
    private Object rabbitHint;

    private void Awake(){
        rabbitHint = Resources.Load("RabbitHint"); 
        pangolinHint = Resources.Load("PangolinHint"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<SpriteRenderer>(out my_renderer);
        renderers = GetComponentsInChildren<SpriteRenderer>();
        highlightColor = (Color)(new Color32(255, 172, 238, 255));
        if(name == "Tank") GetComponent<Rigidbody2D>().freezeRotation = true;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (possessed && gameObject.name != "ColorBox") {
            float y = GetComponent<Collider2D>().bounds.max.y;
            myHint.transform.position = new Vector3 (transform.position.x, y + 0.5f, transform.position.z);
        }
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
        if(!clockwise && obj.GetComponent<Rigidbody2D>().rotation < 45f)
            obj.GetComponent<Rigidbody2D>().rotation += 5f;
        else if(clockwise && obj.GetComponent<Rigidbody2D>().rotation > -30f)
            obj.GetComponent<Rigidbody2D>().rotation -= 5f;
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
        PulleyControl ctrl = GameObject.Find("Pulley").GetComponent<PulleyControl>();
        // if (clockwise) plateRb.velocity = new Vector2 (0, 1);
        // else plateRb.velocity = new Vector2 (0, -1);
        if (ctrl.canRole()) {
            if (clockwise) plateRb.AddForce(new Vector2 (0, 2.0f));
            else plateRb.AddForce(new Vector2 (0, -2.0f));
        }
    }

    // level_3
    public void ServerRotate(bool clockwise) // rotate server
    {
        if(!clockwise) GetComponent<Rigidbody2D>().rotation += 90f;
        else GetComponent<Rigidbody2D>().rotation -= 90f;
    }

    public void GearRotate(bool clockwise) // rotate gear and move up
    {
        if(!clockwise){
            GetComponent<Rigidbody2D>().rotation += 1.5f;
            transform.position += new Vector3(0, 0.0142f, 0);
        }
        else{
            GetComponent<Rigidbody2D>().rotation -= 1.5f;
            transform.position -= new Vector3(0, 0.0142f, 0);
        }
    }

    public void GateClose(bool clockwise) // open/close the gate and rotate the gear
    {
        GameObject gate = transform.GetChild(0).gameObject;
        GameObject gear = transform.GetChild(1).gameObject;

        if(!clockwise && gate.transform.position.x > gear.transform.position.x - 0.8f){
            gate.transform.position -= new Vector3(0.01f, 0, 0);
            gear.GetComponent<Rigidbody2D>().rotation += 0.5f;
        }
        else if(clockwise && gate.transform.position.x < gear.transform.position.x + 0.8f){
            gate.transform.position += new Vector3(0.01f, 0, 0);
            gear.GetComponent<Rigidbody2D>().rotation -= 0.5f;
        }
    }

    public void ColorBoxRotate(bool clockwise) // only rotate barrel
    {
        if (!GetComponent<ColorBoxControl>().isDynamic){
            if (clockwise) GetComponent<Rigidbody2D>().rotation += 0.5f;
            else GetComponent<Rigidbody2D>().rotation -= 0.5f;
        }
        else {
            var body = GetComponent<Rigidbody2D>();

            if (Mathf.Abs(body.angularVelocity) > 200.0f) return;
            float strength;
            if (clockwise) strength = -22.0f;
            else strength = 22.0f;
            var impulse = (strength * Mathf.Deg2Rad) * body.inertia;
            body.AddTorque(impulse, ForceMode2D.Impulse);
        }
    }

    // ************** Draw possessing hint on top of objects ************** //

    public void highlightObject(bool isRabbit)
    {
        if (hinted) return;
        DrawHint(isRabbit, 0.6f);
        hinted = true;
    }

    public void unhighlightObject(){
        if (!hinted) return;
        GameObject delete = GameObject.Find("/hint_" + gameObject.name).gameObject;
        GameObject.Destroy(delete);
        hinted = false;
    }

    public void possessSign(bool isRabbit)
    {
        if (possessed) return;
        possessed = true;
        DrawHint(isRabbit, 1.0f);
    }

    public void unpossessSign()
    {
        if (!possessed) return;
        possessed = false;
        GameObject delete = GameObject.Find("/possesshint_" + gameObject.name).gameObject;
        GameObject.Destroy(delete);
    }

    private void DrawHint(bool isRabbit, float transparency){
        GameObject hint;
        if (isRabbit) hint = GameObject.Instantiate(rabbitHint, Vector3.zero, Quaternion.identity) as GameObject;
        else  hint = GameObject.Instantiate(pangolinHint, Vector3.zero, Quaternion.identity) as GameObject;

        float y = GetComponent<Collider2D>().bounds.max.y;
        hint.transform.position = new Vector3 (transform.position.x, y + 0.5f, transform.position.z);

        Color c = hint.GetComponent<SpriteRenderer>().color;
        hint.GetComponent<SpriteRenderer>().color = new Color(c.r, c.b, c.g, transparency);

        if (transparency == 0.6f) hint.name = "hint_" + gameObject.name;
        else {
            hint.name = "possesshint_" + gameObject.name;
            myHint = hint;
        }
    }

}
