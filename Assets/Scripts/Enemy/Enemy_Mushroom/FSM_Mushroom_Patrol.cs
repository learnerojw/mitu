using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Mushroom_Patrol : StateMachineBehaviour
{

    private Transform transform;
    private Enemy_MushRoom enemy_MushRoom;

    private int currentPatrolPoint;
    private Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.GetComponent<Transform>();
        enemy_MushRoom = animator.GetComponent<Enemy_MushRoom>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemy_MushRoom.targetPlayer==null)
        {
            enemy_MushRoom.FlipTo(enemy_MushRoom.PatrolPoints[currentPatrolPoint]);
            //transform.position = Vector2.MoveTowards(transform.position,
            //                                         enemy_MushRoom.PatrolPoints[currentPatrolPoint].position,
            //                                         enemy_MushRoom.patrolSpeed * Time.deltaTime);
            Vector2 dir = (enemy_MushRoom.PatrolPoints[currentPatrolPoint].position - transform.position).normalized;
            dir.y = 0;
            dir.x = dir.x >= 0 ? 1 : -1;
            rb.velocity = dir * enemy_MushRoom.patrolSpeed * Time.deltaTime;
            if (Mathf.Abs(transform.position.x - enemy_MushRoom.PatrolPoints[currentPatrolPoint].position.x) < 0.5f)
            {
                enemy_MushRoom.isPatrol = false;
            }
        }
        else
        {
            enemy_MushRoom.FlipTo(enemy_MushRoom.targetPlayer);
            if(Mathf.Abs(transform.position.x-enemy_MushRoom.targetPlayer.position.x)>0.7f)
            {
                //transform.position = Vector2.MoveTowards(transform.position,
                //                                     enemy_MushRoom.targetPlayer.position,
                //                                     enemy_MushRoom.patrolSpeed * Time.deltaTime);
                Vector2 dir = (enemy_MushRoom.targetPlayer.position - transform.position).normalized;
                dir.y = 0;
                dir.x = dir.x >= 0 ? 1 : -1;
                rb.velocity = dir * enemy_MushRoom.patrolSpeed * Time.deltaTime;

            }
            
            Collider2D collider2D = Physics2D.OverlapCircle(enemy_MushRoom.attackPoint.position,
                                                            enemy_MushRoom.attackRaduis,
                                                            enemy_MushRoom.attackLayer);
            if(collider2D!=null)
            {
                enemy_MushRoom.isPatrol = false;
                animator.SetTrigger("Attack1");
                rb.velocity = Vector2.zero;
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentPatrolPoint++;
        if(currentPatrolPoint>=enemy_MushRoom.PatrolPoints.Length)
        {
            currentPatrolPoint = 0;
        }

        animator.ResetTrigger("Attack1");
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
