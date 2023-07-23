using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePanel : BasePanel
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        EventCenter.AddEventListener("Hit", (obj) =>
        {
            PlayerMovement playerMovement = obj as PlayerMovement;
            slider.value = (playerMovement.maxHp - playerMovement.currentHp) / playerMovement.maxHp;
        });

        EventCenter.AddEventListener("Add HP", (amount) =>
        {
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            slider.value = (playerMovement.maxHp - playerMovement.currentHp) / playerMovement.maxHp;

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
