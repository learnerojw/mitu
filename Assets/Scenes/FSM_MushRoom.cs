using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum StateType
//{
//    Idel,Patrol,Chese,Attack1,Hit
//}

//[System.Serializable]
//public class parameter
//{
//    public Animator anim;

//    public float patrolSpeed;

//    public float idelTime;

//    public bool isPatrol;
    

//    public Transform[] patrolPoints;


//}

//public class FSM_MushRoom : MonoBehaviour
//{
//    private IState currentState;

//    private Dictionary<StateType, IState> States = new Dictionary<StateType, IState>();

//    public parameter parameter;


//    private void Awake()
//    {
//        parameter.anim = GetComponent<Animator>();
//    }
//    private void Start()
//    {
        
//        States.Add(StateType.Idel, new MushRoom_idel(this));
//        States.Add(StateType.Patrol, new MushRoom_Patrol(this));
//        States.Add(StateType.Hit, new MushRoom_Hit(this));
//        currentState = States[StateType.Idel];
//    }
//    private void Update()
//    {
//        Check();
//        parameter.anim.SetBool("Patrol", parameter.isPatrol);
//        currentState.OnUpdate();
//    }
//    public void TransitionState(StateType type)
//    {
//        if(currentState!=null)
//        {
//            currentState.OnExit();
//            currentState = States[type];
//            currentState.OnEnter();
//        }
        
//    }

//    public void FlipTo(Transform tran)
//    {
//        if(tran.position.x>transform.position.x)
//        {
//            transform.eulerAngles = new Vector3(0, 0, 0);
//        }
//        else if(tran.position.x<transform.position.x)
//        {
//            transform.eulerAngles = new Vector3(0, 180, 0);
//        }


//    }

//    private void Check()
//    {
//        foreach (var item in States)
//        {
//            if(item.Value==currentState)
//            {
//                print(item.Key.ToString());
//            }
//        }
//    }
//}
