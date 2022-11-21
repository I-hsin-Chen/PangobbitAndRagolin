using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiJumper : Jumper
{
    // ! You can inherit from Jumper to implement Double Jump.
    // ! Remember to replace Jumper with MultiJumper when you're done.
    // ! (Or you can just modify Jumper directly. Your choice)

    // [SerializeField]
    // private int maxJumpCount = 2;
    // private int currentJumpCount = 0;
    
    // TODO: Define jump condition.
    // ! If it's just "base.canJump", then it's the same as the original Jumper (no double jump)
    private int jump_count = 0;
    private bool canJumpMul = true;

    public override bool canJump => base.canJump;
    protected override void DoJump(){

        if (jump_count == 2) canJumpMul = false;
        else {
            canJumpMul = true;
            jump_count ++ ;
        }
        if (canJumpMul) base.DoJump();
        // TODO: Increase jump count.
    }

    void Update(){
        if (canJump) jump_count = 0;
    }

    private void Start() {
        // TODO: Reset jump count on grounded.
    }
}
