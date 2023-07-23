using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questable : MonoBehaviour
{
    public Quest quest;

    public bool isgetRewards;
    public void DelegateQuest()
    {
        if(quest.questStatus==Quest.QuestStatus.Waitting)
        {
            Player.instance.questList.Add(quest);
            quest.questStatus = Quest.QuestStatus.Accepted;
            //UIManager.GetInstance().GetPanel<QuestManager>("Quest Panel").UpdateQuestList();
            //QuestManager.instance.UpdateQuestList();
        }
        else
        {
            Debug.Log(string.Format("任务：{0} 已经被接受", quest.questName));
        }
    }

    public void getRewards()
    {
        Player.instance.exp += quest.expRewards;
        Debug.Log(string.Format("你获得了{0}点经验值", quest.expRewards));

        Player.instance.gold += quest.goldRewards;
        Debug.Log(string.Format("你获得了{0}个金币", quest.goldRewards));
    }

}
