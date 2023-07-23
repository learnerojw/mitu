using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void ResetTrigger(string triggerName)
    {
        anim.ResetTrigger(triggerName);
    }

    public void ResetAllTrigger()
    {

        anim.ResetTrigger("Attack1");
        anim.ResetTrigger("Attack_air1");
        anim.ResetTrigger("Attack_down");
        
    }
}
