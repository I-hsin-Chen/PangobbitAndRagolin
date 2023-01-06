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
    private AudioManager audiomanager;

    // for highlighing the object that has been raycasted
    private SpriteRenderer my_renderer;
    private Color highlightColor;

    private Object pangolinHint;
    private Object rabbitHint;

    // record bullet
    private List<GameObject> bullets = new List<GameObject>();

    // record stage_4 Gate and Marbel collision
    private bool[,] collidedGate = new bool[5, 5];
    private GameObject[,] marbelList = new GameObject[5, 5];
    private bool playing = false;
    private bool falling = false;
    private Coroutine coroutineCheckMarbels;
    private Coroutine coroutineCheckGearDown;

    private void Awake(){
        rabbitHint = Resources.Load("RabbitHint"); 
        pangolinHint = Resources.Load("PangolinHint"); 
        audiomanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<SpriteRenderer>(out my_renderer);
        renderers = GetComponentsInChildren<SpriteRenderer>();
        highlightColor = (Color)(new Color32(255, 172, 238, 255));

        // init stage_4 Gate and Marbel collision
        for(int i = 0; i < 5; i++)
            for(int j = 0; j < 5; j++)
                collidedGate[i, j] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (possessed) {
            if (gameObject.name != "ColorBox"){
                float y = GetComponent<Collider2D>().bounds.max.y;
                myHint.transform.position = new Vector3 (transform.position.x, y + 0.5f, transform.position.z);
            }
            else myHint.transform.position = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
    }

    // Stage_0
    public void TableRotate(bool clockwise) // rotate table
    {
        if(!clockwise) GetComponent<Rigidbody2D>().rotation += 90f;
        else GetComponent<Rigidbody2D>().rotation -= 90f;
        transform.position -= new Vector3(0, transform.position.y/3.6f);
    }
    // Stage_0 end ======================================================================

    // Stage_1
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
            if (clockwise) strength = -60.0f;
            else strength = 60.0f;
            var impulse = (strength * Mathf.Deg2Rad) * body.inertia;
            body.AddTorque(impulse, ForceMode2D.Impulse);
        }
    }
    // Stage_1 end ======================================================================

    // Stage_2
    public void RadioRotate(bool clockwise) // rotate radio
    {
        if(!clockwise) GetComponent<Rigidbody2D>().rotation += 90f;
        else GetComponent<Rigidbody2D>().rotation -= 90f;
        // transform.position -= new Vector3(0, transform.position.y/3.6f);
    }
    public void TankRotate(bool clockwise) // only rotate barrel
    {
        GameObject obj = transform.GetChild(0).gameObject;
        if(!clockwise && obj.GetComponent<Rigidbody2D>().rotation < 60f)
            obj.GetComponent<Rigidbody2D>().rotation += 5f; // 5f when GetKeyDown
        else if(clockwise && obj.GetComponent<Rigidbody2D>().rotation > -30f)
            obj.GetComponent<Rigidbody2D>().rotation -= 5f; // 5f when GetKeyDown
    }
    public void TankShoot() // shoot bullet
    {
        while(bullets.Count > 0 && bullets[0] == null)
            bullets.RemoveAt(0);

        if(bullets.Count > 3){
            audiomanager.PlaySE_Empty();
            return;
        }

        GameObject obj = transform.GetChild(0).gameObject;
        GameObject cannon = obj.transform.GetChild(0).gameObject;
        Vector2 pos = new Vector2(cannon.transform.position.x, cannon.transform.position.y);
        pos += new Vector2(cannon.GetComponent<SpriteRenderer>().bounds.size.x / 2, ((obj.GetComponent<Rigidbody2D>().rotation >= 0) ? 1 : -1) * cannon.GetComponent<SpriteRenderer>().bounds.size.y / 3);
        Vector2 rot = new Vector2(obj.transform.rotation[3], obj.transform.rotation[2]*2.4f);

        GameObject bullet = Instantiate(bullet_prefab, pos, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(rot * 240);
        audiomanager.PlaySE_Tower();

        bullets.Add(bullet);
    }

    public void ClockRotate(bool clockwise) // rotate clock
    {
        if(!GetComponent<ClockControl>().GetClockCanRotate()) return; // the door is already open

        if(!clockwise) GetComponent<Rigidbody2D>().rotation += 30f;
        else GetComponent<Rigidbody2D>().rotation -= 30f;
    }
    
    public void PulleyWheelRotate(bool clockwise) // only rotate barrel
    {
        Animator wheelAnimator = GetComponent<Animator>();
        PulleyControlv3 ctrl = GameObject.Find("Pulley").GetComponent<PulleyControlv3>();

        if (clockwise && !ctrl.toggle) ctrl.toggleThePlates();
        else if (!clockwise && ctrl.toggle) ctrl.toggleThePlates();
    }
    // Stage_2 end ======================================================================

    // Stage_3
    public void WaterTapRotate(){
        transform.Find("Head").GetComponent<Animator>().enabled = true;
        transform.Find("Water").GetComponent<Animator>().enabled = true;
        transform.Find("WaterArea").GetComponent<WaterAreaControl>().settapOpened(true);
    }

    public void WaterTapStopRotate(){
        transform.Find("Head").GetComponent<Animator>().enabled = false;
        transform.Find("Water").GetComponent<Animator>().enabled = false;
        transform.Find("WaterArea").GetComponent<WaterAreaControl>().settapOpened(false);
    }
    // Stage_3 end ======================================================================

    // Stage_4
    public void ServerRotate(bool clockwise) // rotate server
    {
        if(!clockwise) GetComponent<Rigidbody2D>().rotation += 90f;
        else GetComponent<Rigidbody2D>().rotation -= 90f;
    }

    public void GearRotate(bool clockwise) // rotate gear and move up
    {
        if(!clockwise && transform.position.y < 8f){
            GetComponent<Rigidbody2D>().rotation += 4.5f;
            transform.position += new Vector3(0, 0.0426f, 0);
        }
        else if(clockwise && transform.position.y > -9f){
            GetComponent<Rigidbody2D>().rotation -= 4.5f;
            transform.position -= new Vector3(0, 0.0426f, 0);
        }
    }

    public void GearDown(bool start) // move gear to buttom
    {
        if(start && !falling) coroutineCheckGearDown = StartCoroutine(CheckGearDown());
        else{
            if(falling && coroutineCheckGearDown != null){
                StopCoroutine(coroutineCheckGearDown);
                falling = false;
            }
        }
    }
    
    IEnumerator CheckGearDown()
    {
        yield return new WaitForSeconds(2f);
        falling = true;
        while(transform.position.y > -9f){
            GetComponent<Rigidbody2D>().rotation -= 4.5f;
            transform.position -= new Vector3(0, 0.0426f, 0);
            yield return new WaitForSeconds(0.05f);
        }
        falling = false;
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
    
    public void PhonographPlay(bool rotating)
    {
        if(rotating && !playing) coroutineCheckMarbels = StartCoroutine(CheckMarbels());
        else if(!rotating && playing){ 
            if(coroutineCheckMarbels != null){
                StopCoroutine(coroutineCheckMarbels);
                ClearAllMarbels();
            } 
            audiomanager.StopSE_Accompaniment();
            playing = false;
        }
    }
    
    IEnumerator CheckMarbels()
    {
        playing = true;
        audiomanager.PlaySE_Accompaniment();

        lightTheMarbels(0);
        if(collidedGate[0, 0]) audiomanager.PlaySE_Pitch1();
        if(collidedGate[0, 1]) audiomanager.PlaySE_Pitch2();
        if(collidedGate[0, 2]) audiomanager.PlaySE_Pitch3();
        if(collidedGate[0, 3]) audiomanager.PlaySE_Pitch4();
        if(collidedGate[0, 4]) audiomanager.PlaySE_Pitch5();
        yield return new WaitForSeconds(0.3515625f);
        unlightTheMarbels(0);

        lightTheMarbels(1);
        if(collidedGate[1, 0]) audiomanager.PlaySE_Pitch1();
        if(collidedGate[1, 1]) audiomanager.PlaySE_Pitch2();
        if(collidedGate[1, 2]) audiomanager.PlaySE_Pitch3();
        if(collidedGate[1, 3]) audiomanager.PlaySE_Pitch4();
        if(collidedGate[1, 4]) audiomanager.PlaySE_Pitch5();
        yield return new WaitForSeconds(0.3515625f);
        unlightTheMarbels(1);

        lightTheMarbels(2);
        if(collidedGate[2, 0]) audiomanager.PlaySE_Pitch1();
        if(collidedGate[2, 1]) audiomanager.PlaySE_Pitch2();
        if(collidedGate[2, 2]) audiomanager.PlaySE_Pitch3();
        if(collidedGate[2, 3]) audiomanager.PlaySE_Pitch4();
        if(collidedGate[2, 4]) audiomanager.PlaySE_Pitch5();
        yield return new WaitForSeconds(0.234375f);
        unlightTheMarbels(2);

        lightTheMarbels(3);
        if(collidedGate[3, 0]) audiomanager.PlaySE_Pitch1();
        if(collidedGate[3, 1]) audiomanager.PlaySE_Pitch2();
        if(collidedGate[3, 2]) audiomanager.PlaySE_Pitch3();
        if(collidedGate[3, 3]) audiomanager.PlaySE_Pitch4();
        if(collidedGate[3, 4]) audiomanager.PlaySE_Pitch5();
        yield return new WaitForSeconds(0.46875f);
        unlightTheMarbels(3);

        lightTheMarbels(4);
        if(collidedGate[4, 0]) audiomanager.PlaySE_Pitch1();
        if(collidedGate[4, 1]) audiomanager.PlaySE_Pitch2();
        if(collidedGate[4, 2]) audiomanager.PlaySE_Pitch3();
        if(collidedGate[4, 3]) audiomanager.PlaySE_Pitch4();
        if(collidedGate[4, 4]) audiomanager.PlaySE_Pitch5();
        yield return new WaitForSeconds(0.46875f);
        audiomanager.StopSE_Accompaniment();
        unlightTheMarbels(4);


        if(CheckStage4Pass())
            GetComponent<PhonographControl>().Stage4Pass();
        yield return new WaitForSeconds(0.5f);
        playing = false;
    }

    private void lightTheMarbels(int row){
        for (int i=0; i<= 4; i++){
            if (marbelList[row, i] != null)
            marbelList[row, i].transform.Find("Point Light").GetComponent<Light>().intensity = 1.3f ;
        } 
    }

    private void unlightTheMarbels(int row){
        for (int i=0; i<= 4; i++){
            if (marbelList[row, i] != null)
            marbelList[row, i].transform.Find("Point Light").GetComponent<Light>().intensity = 0.0f ;
        } 
    }

    public bool CheckStage4Pass()
    {
        for(int i = 0; i < 5; i++){
            for(int j = 0; j < 5; j++){
                if((i==0 && j==2) || (i==1 && j==3) || (i==2 && j==2) || (i==3 && j==4) || (i==4 && j==0)){
                    if(!collidedGate[i, j]) return false;
                }
                else{
                    if(collidedGate[i, j]) return false;
                }
            }
        }

        return true;
    }

    public void SetCollision(int i, int j)
    {
        collidedGate[i, j] = true;
    }

    public void ClearCollision(int i, int j)
    {
        collidedGate[i, j] = false;
    }

    public void SetMarbel(int i, int j, GameObject ball)
    {
        marbelList[i, j] = ball;
    }

    public void ClearMarbel(int i, int j)
    {
        marbelList[i, j] = null;
    }

    public void ClearAllMarbels()
    {
        for (int i=0; i<=4; i++) 
            for(int j=0; j<=4; j++){
                if (marbelList[i, j] != null)
                marbelList[i, j].transform.Find("Point Light").GetComponent<Light>().intensity = 0.0f ;
            }
    }

    // Stage_4 end ======================================================================


    // Draw possessing hint on top of objects ===========================================

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
