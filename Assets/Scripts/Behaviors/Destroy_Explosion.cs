using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Explosion : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject _go = animator.gameObject.transform.parent.gameObject;
        Destroy(_go, stateInfo.length);
    }
}
