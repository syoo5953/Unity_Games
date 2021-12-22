using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimation : StateMachineBehaviour
{
    public int ClipCount = 4;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.SetInteger("idleRandom", Random.Range(0, ClipCount));
    }
}
