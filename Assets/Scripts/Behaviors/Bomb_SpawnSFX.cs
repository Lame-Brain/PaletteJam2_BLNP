using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_SpawnSFX : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.PLAYER.GetComponent<AudioSource>().clip = GameManager.PLAYER.FindSound("Bomb_Spawn");
        GameManager.PLAYER.GetComponent<AudioSource>().Play();
    }
}
