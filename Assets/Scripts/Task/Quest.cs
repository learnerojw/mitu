using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Quest 
{
    public enum QuestType { Gathering,Talk,Reach, Fight };
    public enum QuestStatus { Waitting,Accepted,Completed};

    public string questName;
    public QuestType questType;
    public QuestStatus questStatus;

    public int expRewards;
    public int goldRewards;

}
