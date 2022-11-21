using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A framework for script-based animation logic.
/// This class assumes that all animations are on layer 0 (base layer).
/// You are expected to override Init() and implement the animation logic
/// in Update(). See also: <see cref="AnimState"/>
/// </summary>
[RequireComponent(typeof(Animator))]
public abstract class ScriptAnimator : MonoBehaviour
{
    protected Animator animator;
    protected void Awake() {
        TryGetComponent<Animator>(out animator);
        Init();
    }
    /// <summary>
    /// Called during Awake.
    /// </summary>
    protected abstract void Init();
    /// <summary>
    /// Check if the animator has the states specified here. If any is missing,
    /// a warning is logged. 
    /// You can use nameof(AnimState) for convenience.
    /// </summary>
    /// <param name="states">The states the Animator are expected to have.</param>
    protected void RequireStates(params string[] states){
        foreach(string state in states){
            int stateHash = Animator.StringToHash(state);
            if(!animator.HasState(0, stateHash))
                Debug.LogWarning($"Animator is missing state {state}");
        }
    }
    /// <summary>
    /// Check if the animator is currently in the state.
    /// Use <see cref="AnimState"/>.
    /// </summary>
    /// <param name="stateHash">The hash of the state.</param>
    /// <returns></returns>
    protected bool IsInState(int stateHash){
        return animator.GetCurrentAnimatorStateInfo(0).shortNameHash == stateHash;
    }
    /// <summary>
    /// Check if the state the animator is in is done. Note that this
    /// assumes the state to be non-looping.
    /// </summary>
    /// <returns>True the current state is done. False otherwise.</returns>
    protected bool CurrentStateDone(){
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
    }
    /// <summary>
    /// Plays the given state immediately.
    /// Use <see cref="AnimState"/>.
    /// </summary>
    /// <param name="stateHash">The hash of the state.</param>
    /// <returns></returns>
    protected void PlayState(int stateHash){
        animator.Play(stateHash, 0);
    }
    /// <summary>
    /// Plays the given state only if the animator isn't in that state already.
    /// Use <see cref="AnimState"/>.
    /// </summary>
    /// <param name="stateHash">The hash of the state.</param>
    /// <returns></returns>
    protected void PlayStateIfNotInState(int stateHash){
        if(!IsInState(stateHash))
            PlayState(stateHash);
    }

}
