using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FSM_Bwitch_run : StateMachineBehaviour
{
    private Transform transform;
    private B_Witch b_Witch;
    private Rigidbody2D rb;
    private float createBatTime;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.GetComponent<Transform>();
        b_Witch = animator.GetComponent<B_Witch>();
        rb = animator.GetComponent<Rigidbody2D>();
        createBatTime = UnityEngine.Random.Range(3,7);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(b_Witch.targetPlayer!=null)
        {
            createBatTime -= Time.deltaTime;
            b_Witch.FlipTo(b_Witch.targetPlayer);
            if (Mathf.Abs(transform.position.x - b_Witch.targetPlayer.position.x) > b_Witch.attackArea.x)
            {
                //transform.position = Vector2.MoveTowards(transform.position,
                //                                     b_Witch.targetPlayer.position,
                //                                     b_Witch.moveSpeed * Time.deltaTime);
                Vector2 dir = (b_Witch.targetPlayer.position - transform.position).normalized;
                dir.y = 0;
                //dir = dir.normalized;
                rb.velocity = dir * b_Witch.moveSpeed * Time.deltaTime;
            }

            Collider2D collider2D = Physics2D.OverlapBox(b_Witch.attackPoint.position,
                                                         b_Witch.attackArea,
                                                         0,
                                                         b_Witch.attackLayer);
            
            if (collider2D != null)
            {
                //Debug.Log(collider2D.name);
                animator.SetTrigger("Attack");
                rb.velocity = Vector2.zero;
                return;
            }

            if(createBatTime<=0)
            {
                animator.SetTrigger("Charge");
            }
        }
        else
        {
            b_Witch.FlipTo(b_Witch.idelPoint);
            //transform.position = Vector2.MoveTowards(transform.position,
            //                                       b_Witch.idelPoint.position,
            //                                       b_Witch.moveSpeed * Time.deltaTime);
            Vector2 dir = (b_Witch.idelPoint.position - transform.position).normalized;
            dir.y = 0;
            //dir = dir.normalized;
            rb.velocity = dir * b_Witch.moveSpeed * Time.deltaTime;

            if(Mathf.Abs(transform.position.x-b_Witch.idelPoint.position.x)<1)
            {
                b_Witch.isRun = false;
            }
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
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
