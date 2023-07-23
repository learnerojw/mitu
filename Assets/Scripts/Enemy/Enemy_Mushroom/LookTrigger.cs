using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTrigger : MonoBehaviour
{
    public Enemy_State enemy_State;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            enemy_State.targetPlayer = collision.transform;
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy_State.targetPlayer = null;
        }
    }
}
