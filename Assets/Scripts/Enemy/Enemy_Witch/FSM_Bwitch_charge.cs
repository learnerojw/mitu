using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Bwitch_charge : StateMachineBehaviour
{
    private Transform transform;
    private B_Witch b_Witch;
    private Rigidbody2D rb;
    private bool isCreate;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        b_Witch = animator.GetComponent<B_Witch>();
        rb = animator.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        isCreate = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isCreate)
        {
            Vector3 pos = transform.position + new Vector3(0, 2, 0);
            ResManager.instance.Load<GameObject>("Prefab/Bat-null").transform.position=pos;
            //ResManager.instance.Load<GameObject>("Prefab/Bat-null").transform.position = pos;
            isCreate = false;
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
