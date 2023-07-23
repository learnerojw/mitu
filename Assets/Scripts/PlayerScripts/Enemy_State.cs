using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_State :MonoBehaviour
{
    public float maxHp;
    public float currentHp;
    public Transform targetPlayer;
    public float damage;


    public void FlipTo(Transform tran)
    {
        if (tran.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (tran.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }


    }
}
