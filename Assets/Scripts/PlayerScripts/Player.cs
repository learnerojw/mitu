using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public int exp;
    public int gold;

    public List<Quest> questList = new List<Quest>();

    public int requireAmount=0;
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
}
