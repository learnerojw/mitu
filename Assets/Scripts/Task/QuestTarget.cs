using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTarget : MonoBehaviour
{
    public Quest quest;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            for (int i = 0; i < Player.instance.questList.Count; i++)
            {
                if (quest.questName == Player.instance.questList[i].questName && Player.instance.questList[i].questStatus==Quest.QuestStatus.Accepted)
                {
                    switch (quest.questType)
                    {
                        case Quest.QuestType.Gathering:
                            { 
                                Player.instance.requireAmount += 1;
                                if (Player.instance.requireAmount >= 3)
                                {
                                    questCompelted(i);
                                }
                                Destroy(gameObject);
                            }
                            break;

                        case Quest.QuestType.Reach:
                            {
                                questCompelted(i);
                                
                            }
                            break;
                    }
                }
            }
        }
        
    }
    public void TaskCompleted()
    {
        for (int i = 0; i < Player.instance.questList.Count; i++)
        {
            if (quest.questName == Player.instance.questList[i].questName && Player.instance.questList[i].questStatus == Quest.QuestStatus.Accepted)
            {
                questCompelted(i);
            }
        }
    }

    

    private void questCompelted(int i)
    {
        Player.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
        //QuestManager.instance.UpdateQuestList();
    }
}
