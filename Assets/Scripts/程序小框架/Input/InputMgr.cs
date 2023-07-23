using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : SingletonMono<InputMgr>
{
    private bool isStart = false;

    private void CheckKeyCode(KeyCode key)
    {
        if(Input.GetKeyDown(key))
        {
            EventCenter.EventTrigger("ĳ������",key);
        }
        if(Input.GetKeyUp(key))
        {
            EventCenter.EventTrigger("ĳ��̧��", key);
        }

    }

    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }

    private void Update()
    {
        if(!isStart)
        {
            return;
        }
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.D);



    }

}
