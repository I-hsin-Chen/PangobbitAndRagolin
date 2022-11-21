using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwiggyAnimator : ScriptAnimator
{
    private Runner runner;
    private Jumper jumper;
    private PlayerState playerState;
    private FacingDirection facingDirection;
    private Vector3 initialLocalScale;

    protected override void Init()
    {
        #if UNITY_EDITOR
        RequireStates(
            nameof(AnimState.Idle),
            nameof(AnimState.Jump),
            nameof(AnimState.Fall),
            nameof(AnimState.Run),
            nameof(AnimState.Victory)
        );
        #endif
        TryGetComponent<Runner>(out runner);
        TryGetComponent<Jumper>(out jumper);
        TryGetComponent<PlayerState>(out playerState);
        TryGetComponent<FacingDirection>(out facingDirection);
        initialLocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerState.hasWon){
            PlayStateIfNotInState(AnimState.Victory);
        }
        else if(jumper.isRising){
            PlayStateIfNotInState(AnimState.Jump);
        }
        else if(jumper.isFalling){
            PlayStateIfNotInState(AnimState.Fall);
        }
        else if(runner.isRunning){
            PlayStateIfNotInState(AnimState.Run);
        }
        else{
            PlayStateIfNotInState(AnimState.Idle);
        }
        if(facingDirection.isFacingAwayFromInitialDirection) transform.localScale 
        = new Vector3(-initialLocalScale.x, initialLocalScale.y, initialLocalScale.z);
        else transform.localScale = initialLocalScale;
    }
}
