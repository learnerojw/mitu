using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class MushRoom_idel : IState
//{
//    private float timer;

//    private FSM_MushRoom FSM;

//    private parameter parameter;
//    public MushRoom_idel(FSM_MushRoom FSM)
//    {
//        this.FSM = FSM;
//        this.parameter = FSM.parameter;
//    }

//    public void OnEnter()
//    {
        
//    }
//    public void OnUpdate()
//    {
//        timer += Time.deltaTime;

//        if(timer>parameter.idelTime)
//        {
//            FSM.parameter.isPatrol = true;
//            FSM.TransitionState(StateType.Patrol);
//        }
//    }
//    public void OnExit()
//    {
//        timer = 0;
//    }

    
//}


//public class MushRoom_Patrol : IState
//{
//    private FSM_MushRoom FSM;

//    private parameter parameter;

//    private int currentPatrolPointIndex;
//    public MushRoom_Patrol(FSM_MushRoom FSM)
//    {
//        this.FSM = FSM;
//        this.parameter = FSM.parameter;
//    }



//    public void OnEnter()
//    {
        
//    }
//    public void OnUpdate()
//    {
//        FSM.transform.position = Vector2.MoveTowards(FSM.transform.position,
//                                                     parameter.patrolPoints[currentPatrolPointIndex].position,
//                                                     Time.deltaTime*parameter.patrolSpeed);

//        FSM.FlipTo(parameter.patrolPoints[currentPatrolPointIndex]);

//        if (Mathf.Abs(FSM.transform.position.x-parameter.patrolPoints[currentPatrolPointIndex].position.x)<0.5f)
//        {
//            parameter.isPatrol = false;
//            FSM.TransitionState(StateType.Idel);
            
//        }

        
//    }
//    public void OnExit()
//    {
//        currentPatrolPointIndex++;
//        if(currentPatrolPointIndex>=parameter.patrolPoints.Length)
//        {
//            currentPatrolPointIndex = 0;
//        }
//    }

    
//}

//public class MushRoom_Hit : IState
//{
//    private FSM_MushRoom FSM;
//    private parameter parameter;


//    public MushRoom_Hit(FSM_MushRoom FSM)
//    {
//        this.FSM = FSM;
//        this.parameter = FSM.parameter;
//    }
//    public void OnEnter()
//    {
        
//    }
//    public void OnUpdate()
//    {
//        if(parameter.anim.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.95)
//        {
//            FSM.TransitionState(StateType.Idel);
//        }
//    }
//    public void OnExit()
//    {
        
//    }
////}