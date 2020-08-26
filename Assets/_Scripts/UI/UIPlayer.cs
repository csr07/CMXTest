using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    public GameObject player;    
    private RectTransform rt;

    private void Start()
    {      
        
    }


    void Update()
    {
        //Update the bar with the value of Player's health      
        //rt.sizeDelta = new Vector2(player.GetComponent<PlayerInfo>().playerStats.health, rt.rect.height);
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        float health = player.GetComponent<PlayerInfo>().playerStats.health;

        rt.sizeDelta = new Vector2(health, rt.rect.height);

        if (health > 50)
        {
            gameObject.GetComponent<Image>().color = Color.green;
        }
        else if (health > 25 && health <= 50)
        {
            gameObject.GetComponent<Image>().color = Color.yellow;
        }
        if (health <= 25)
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
    }
}
