using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [SerializeField] private bool isEntered;
    [TextArea(1, 3)]
    public string[] lines;

    public Questable questable;

    public QuestTarget questTarget;

    public string[] congratulateLines;
    public string[] normalLines;

    private bool changeLines;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = false;
        }
    }

    private void Update()
    {
        if(isEntered&&Input.GetKeyDown(KeyCode.T))
        {
            if(questable!=null)
            {
                checkQuest();
            }
            UIManager.GetInstance().PushPanel<DialogueManager>("Dialogue Panel", (panel) => {
                panel.ShowDialogue(lines);
                panel.currentQuestable = questable;
                panel.questTarget = questTarget;
            });
            

            
        }
    }

    public void checkQuest()
    {
        foreach (Quest item in Player.instance.questList)
        {
            if(item.questName==questable.quest.questName)
            {
                questable.quest.questStatus = item.questStatus;
            }

        }
        if (questable.quest.questStatus == Quest.QuestStatus.Completed&&!changeLines)
        {
            lines = congratulateLines;
            changeLines = true;
        }
        else if(questable.quest.questStatus == Quest.QuestStatus.Completed && changeLines)
        {
            lines = normalLines;
        }
    }

    
}
