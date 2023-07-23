using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventCenter 
{
    private static Dictionary<string, UnityAction<object>> eventDic = new Dictionary<string, UnityAction<object>>();
    
    public static void AddEventListener(string name,UnityAction<object> action)
    {
        if(eventDic.ContainsKey(name))
        {
            eventDic[name] += action;
        }
        else
        {
            eventDic.Add(name, action);
        }
    }
    public static void RemoveEventListener(string name,UnityAction<object> action)
    {
        if(eventDic.ContainsKey(name))
        {
            eventDic[name] -= action;
        }
    }


    public static void EventTrigger(string name,object info)
    {
        if(eventDic.ContainsKey(name))
        {
            eventDic[name].Invoke(info);
        }
    }


    public static void Clear()
    {
        eventDic.Clear();
    }
    
}
