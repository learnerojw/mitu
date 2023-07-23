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
            Debug.Log(string.Format("����{0} �Ѿ�������", quest.questName));
        }
    }

    public void getRewards()
    {
        Player.instance.exp += quest.expRewards;
        Debug.Log(string.Format("������{0}�㾭��ֵ", quest.expRewards));

        Player.instance.gold += quest.goldRewards;
        Debug.Log(string.Format("������{0}�����", quest.goldRewards));
    }

}
