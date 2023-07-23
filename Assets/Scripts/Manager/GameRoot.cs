using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot instance;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameObject playerObj = ResManager.instance.Load<GameObject>("Prefab/Player");
        playerObj.transform.position = new Vector3(-167, -43, 0);

        ResManager.instance.Load<GameObject>("Camera/Main Camera").transform.position = playerObj.transform.position;
        ResManager.instance.Load<GameObject>("Camera/CM vcam1").transform.position = playerObj.transform.position;
        UIManager.GetInstance().PushPanel<GamePanel>("GamePanel");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (UIManager.GetInstance().stackPanel.Count == 1)
            {
                UIManager.GetInstance().PushPanel<QuestManager>("Quest Panel", (questManager) =>
                {
                    questManager.UpdateQuestList();
                });
                
            }
        }
    }
}
