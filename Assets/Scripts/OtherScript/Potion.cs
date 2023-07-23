using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum showText {npc, obj, blood }
public class Potion : MonoBehaviour
{
    public Transform canvas;
    public showText showText = showText.obj;
    private QuestTarget questTarget;
    private void Start()
    {
        canvas = transform.GetChild(0);
        questTarget = GetComponent<QuestTarget>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(true);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKey(KeyCode.F)&&collision.CompareTag("Player"))
        {
            PlayerMovement player_State = collision.GetComponent<PlayerMovement>();
            player_State.currentHp = (player_State.currentHp + 20) > player_State.maxHp ? player_State.maxHp : (player_State.currentHp + 20);
            EventCenter.EventTrigger("Add HP", 20);
            if(showText!=showText.npc)
            {
                if(questTarget!=null)
                {
                    questTarget.TaskCompleted();
                }
                Destroy(gameObject);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
