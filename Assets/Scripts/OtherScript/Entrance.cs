using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Entrance : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ScenesMgr.GetInstance().LoadSceneAsyn(sceneName, () =>
            {
                GameObject playerObj = other.gameObject;
                playerObj.transform.position = new Vector3(-12, 6, 0);

                ResManager.instance.Load<GameObject>("Camera/Main Camera").transform.position = playerObj.transform.position;
                ResManager.instance.Load<GameObject>("Camera/CM vcam1").transform.position = playerObj.transform.position;
                //ResManager.instance.Load<GameObject>("Prefab/Canvas");
            });
        }
    }
}
