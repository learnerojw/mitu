using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestManager : BasePanel
{
    

    public GameObject[] questUIArray;

    private List<Quest> PlayerQuest;

    //public GameObject QuestPanel;
    

    private void Start()
    {
        PlayerQuest = Player.instance.questList;
        //QuestPanel.SetActive(false);
    }
    public void UpdateQuestList()
    {
        PlayerQuest = Player.instance.questList;
        for (int i = 0; i < PlayerQuest.Count; i++)
        {
            questUIArray[i].transform.GetChild(0).GetComponent<Text>().text = PlayerQuest[i].questName;
            questUIArray[i].transform.GetChild(1).GetComponent<Text>().text = PlayerQuest[i].questStatus.ToString();

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.GetInstance().PopPanel("Quest Panel");
        }
    }

    //public override void OnEnter()
    //{
    //    base.OnEnter();
    //    UpdateQuestList()
    //}
}
