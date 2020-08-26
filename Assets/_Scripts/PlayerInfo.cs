using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public PlayerStats playerStats;


    // Start is called before the first frame update
    void Start()
    {
        //(1)Creating an instance from the SO Script
        //playerStats = (PlayerStats)ScriptableObject.CreateInstance("PlayerStats");

        //(2)Loading the SO Asset //SO is persistent with changes
        //playerStats = Resources.Load("_ScriptableObjects/PlayerInitialStats") as PlayerStats;

        //(3)Load the SO, but create a new Instance keeping original
        PlayerStats baseSO = Resources.Load("_ScriptableObjects/PlayerInitialStats") as PlayerStats;
        playerStats = ScriptableObject.Instantiate(baseSO);

        Debug.Log("PlayerStats initial MaxHealth: " + playerStats.maxHealth);
        Debug.Log("PlayerStats initial    health: " + playerStats.health);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("PlayerStats initial    health: " + playerStats.health);
    }
}
