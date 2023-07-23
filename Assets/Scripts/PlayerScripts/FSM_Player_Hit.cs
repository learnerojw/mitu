using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FSM_Player_Hit : StateMachineBehaviour
{
    PlayerMovement playerMovement;
    Transform transform;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMovement = animator.GetComponent<PlayerMovement>();
        transform = animator.GetComponent<Transform>();
        playerMovement.stopInput = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playerMovement.enemy!=null)
        {
            if((playerMovement.enemy as Transform).position.x>transform.position.x)
            {
                playerMovement.moveH = -playerMovement.hitDistance * animator.GetFloat("HitVelocity");
            }
            else
            {
                playerMovement.moveH = playerMovement.hitDistance * animator.GetFloat("HitVelocity");
            }


        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMovement.stopInput = false;
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
