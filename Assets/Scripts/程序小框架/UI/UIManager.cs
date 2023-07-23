using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class UIManager :BaseManager<UIManager>
{
    private Dictionary<string, BasePanel> dicUI;

    public Stack<BasePanel> stackPanel;

    private RectTransform canvas;
    public UIManager()
    {
        GameObject obj = ResMgr.GetInstance().Load<GameObject>("UI/Canvas");
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);

        GameObject obj2 = ResMgr.GetInstance().Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj2);

        dicUI = new Dictionary<string, BasePanel>();
        stackPanel = new Stack<BasePanel>();
    }

    public void PushPanel<T>(string name, UnityAction<T> callBack = null) where T : BasePanel
    {
        if(stackPanel.Count>0)
        {
            BasePanel topPanel = stackPanel.Peek();
            topPanel.OnPause();
        }

        if(dicUI.ContainsKey(name))
        {
            BasePanel targetPanel = dicUI[name];
            stackPanel.Push(targetPanel);
            targetPanel.OnResume();
            if (callBack != null)
            {
                callBack(targetPanel as T);
            }
            return;
        }

        ResMgr.GetInstance().LoadAsync<GameObject>("UI/" + name, (obj) =>
          {
              T targetPanel = obj.GetComponent<T>();
              obj.transform.SetParent(canvas);

              obj.transform.localPosition = Vector3.zero;
              obj.transform.localScale = Vector3.one;

              (obj.transform as RectTransform).anchorMin = Vector2.zero;
              (obj.transform as RectTransform).anchorMax = Vector2.one;


              if (callBack != null)
              {
                  callBack(targetPanel);
              }

              dicUI.Add(name, targetPanel);
              stackPanel.Push(targetPanel);
              targetPanel.OnEnter();

          });
    }

    public void PopPanel(string name)
    {
        if(dicUI.ContainsKey(name))
        {
            Debug.Log("�ر��˶Ի����");
            stackPanel.Peek().OnExit();
            stackPanel.Pop();
            if(stackPanel.Count>0)
            {
                //stackPanel.Push(stackPanel.Peek());
                stackPanel.Peek().OnResume();
            }
            //GameObject.Destroy(dicUI[name].gameObject);
            //dicUI.Remove(name);

        }
    }

    public T GetPanel<T>(string name) where T:BasePanel
    {
        if(dicUI.ContainsKey(name))
        {
            return dicUI[name] as T;
        }
        return null;
    }

    public void ClearPanel()
    {
        dicUI.Clear();
        while(stackPanel.Count>0)
        {
            BasePanel panel= stackPanel.Peek();
            panel.OnExit();
            stackPanel.Pop();
            //GameObject.Destroy(panel.gameObject);
        }
    }
}
