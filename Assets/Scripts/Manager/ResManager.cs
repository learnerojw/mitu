using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Object = UnityEngine.Object;

public class ResManager : MonoBehaviour
{
    public static ResManager instance;
    
    private void Awake()
    {
        
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
            
        }
        DontDestroyOnLoad(gameObject);
    }

    public T Load<T>(string name) where T:Object
    {
        T res = Resources.Load<T>(name);
        if(res is GameObject)
        {
            return Instantiate(res);
        }
        else
        {
            return res;
        }
        

    }

    

    public void LoadAsync<T>(string name,Action<T> callback) where T:Object
    {
        StartCoroutine(ReallyLoadAsync<T>(name, callback));
        
    }
    private IEnumerator ReallyLoadAsync<T>(string name,Action<T> callback) where T: Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);

        yield return r;

        if(r.asset is GameObject)
        {
            callback(Instantiate(r.asset as T));
        }
        else
        {
            callback(r.asset as T);
        }
    }


}
