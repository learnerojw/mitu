using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum StateType
{
    Idle,
    Chase,
    Attack,
    Hit,
    Dead,
}
[Serializable]
public class BlackBoard
{
    
}


public class FSM
{
    public IState currentState;

    public Dictionary<StateType, IState> statesDic;

    public BlackBoard blackBoard;
    public FSM(BlackBoard blackBoard)
    {
        statesDic = new Dictionary<StateType, IState>();
        this.blackBoard = blackBoard;
    }

    public void AddState(StateType stateType,IState state)
    {
        if(statesDic.ContainsKey(stateType))
        {
            Debug.Log("�Ѿ���������״̬");
            return;
        }
        statesDic.Add(stateType, state);
    }

    public void SwitchState(StateType stateType)
    {
        if(!statesDic.ContainsKey(stateType))
        {
            Debug.Log("û�����״̬����ת��");
            return;
        }
        if(currentState!=null)
        currentState.OnExit();

        currentState = statesDic[stateType];
        currentState.OnEnter();
    }

    public void OnUpdate()
    {
        currentState.OnUpdate();
    }

    public void OnFixUpdate()
    {
        currentState.OnFixUpdate();
    }
}
