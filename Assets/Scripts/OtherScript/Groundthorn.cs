using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundthorn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //collision.transform.GetComponent<ITakeDamage>().TakeDamage(5);
            collision.transform.position = collision.GetComponent<Player_State>().respawnPoint;
        }


    }
}
