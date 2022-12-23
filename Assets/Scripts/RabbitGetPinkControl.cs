using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitGetPinkControl : MonoBehaviour
{
    public bool isPink { get; private set; } = false;
    private float pinkAnimLength = 0.433f;
    private int getPinkState;
    private int getCleanState;
    private Animator animator;
    private PlayerControl rabbitCtrl;

    // Start is called before the first frame update
    void Start()
    {
        isPink = false;
        rabbitCtrl = GetComponent<PlayerControl>();
        animator = GetComponent<Animator>();
        getPinkState = Animator.StringToHash("Base Layer.GetPink");
        getCleanState = Animator.StringToHash("Base Layer.Pink_GetClean");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.name == "Strawberry(Clone)" && !isPink){
            isPink = true;
            StartCoroutine(schedulePinkAnimation());
        }
    }

    private IEnumerator schedulePinkAnimation(){
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        rabbitCtrl.enabled = false;
        animator.Play(getPinkState, 0);
        rabbitCtrl.moveState = Animator.StringToHash("Base Layer.Pink_Move");
        rabbitCtrl.idleState = Animator.StringToHash("Base Layer.Pink_Idle");
        rabbitCtrl.shrinkState = Animator.StringToHash("Base Layer.Pink_Shrink");
        rabbitCtrl.jumpState = Animator.StringToHash("Base Layer.Pink_Jump");
        yield return new WaitForSeconds(pinkAnimLength);
        rabbitCtrl.enabled = true;
        yield break;
    }

    public IEnumerator scheduleCleanAnimation(){
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        isPink = false;
        rabbitCtrl.enabled = false;
        animator.Play(getCleanState, 0);
        rabbitCtrl.moveState = Animator.StringToHash("Base Layer.Move");
        rabbitCtrl.idleState = Animator.StringToHash("Base Layer.Idle");
        rabbitCtrl.shrinkState = Animator.StringToHash("Base Layer.Shrink");
        rabbitCtrl.jumpState = Animator.StringToHash("Base Layer.Jump");
        yield return new WaitForSeconds(pinkAnimLength);
        rabbitCtrl.enabled = true;
        yield break;
    }



}
