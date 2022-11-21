using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A static class that holds state name hashes in Animators.
/// Using this class means not using Animator's state transition graph,
/// and handling animator state transitions yourself through code.
/// </summary>
public static class AnimState
{
    public static readonly int Idle = Animator.StringToHash(nameof(Idle));
    public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    public static readonly int Jump = Animator.StringToHash(nameof(Jump));
    public static readonly int Fall = Animator.StringToHash(nameof(Fall));
    public static readonly int Run = Animator.StringToHash(nameof(Run));
    public static readonly int Victory = Animator.StringToHash(nameof(Victory));
}
