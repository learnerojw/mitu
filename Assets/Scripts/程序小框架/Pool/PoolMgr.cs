using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMgr :SingletonMono<PoolMgr>
{
    public Dictionary<string, List<GameObject>> poolDic = new Dictionary<string, List<GameObject>>();

    private GameObject poolObj;
    public GameObject GetObj(string name)
    {
        GameObject obj = null;
        if(poolDic.ContainsKey(name)&&poolDic[name].Count>0)
        {
            obj = poolDic[name][0];
            poolDic[name].RemoveAt(0);
        }
        else
        {
            obj = Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;
        }
        obj.transform.parent = null;
        obj.SetActive(true);
        return obj;
    }

    public void Push(string name,GameObject obj)
    {
        if(poolObj!=null)
        {
            obj.transform.parent = poolObj.transform;
        }
        else
        {
            poolObj = new GameObject("Pool");
            obj.transform.parent = poolObj.transform;
        }
        obj.SetActive(false);

        if(poolDic.ContainsKey(name))
        {
            poolDic[name].Add(obj);
        }
        else
        {
            poolDic.Add(name,new List<GameObject>(){obj });
        }
    }

    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
