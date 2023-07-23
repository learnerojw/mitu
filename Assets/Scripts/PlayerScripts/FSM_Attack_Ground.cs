using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Attack_Ground : StateMachineBehaviour
{
    private PlayerMovement playerMovement;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        playerMovement = animator.GetComponent<PlayerMovement>();
        playerMovement.stopInput = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playerMovement.transform.localScale.x>0)
        {
            playerMovement.moveH = animator.GetFloat("AttackVelocity")*2;
        }
        else if(playerMovement.transform.localScale.x<0)
        {
            playerMovement.moveH = -2*animator.GetFloat("AttackVelocity");
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
