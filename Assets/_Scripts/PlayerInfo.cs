using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerInfo : MonoBehaviour
{
    public PlayerStats playerStats;


    // Start is called before the first frame update
    public void Start()
    {
        //(1)Creating an instance from the SO Script
        //playerStats = (PlayerStats)ScriptableObject.CreateInstance("PlayerStats");

        //(2)Loading the SO Asset //SO is persistent with changes
        //playerStats = Resources.Load("_ScriptableObjects/PlayerInitialStats") as PlayerStats;

        //(3)Load the SO, but create a new Instance keeping original
        PlayerStats baseSO = Resources.Load("_ScriptableObjects/PlayerInitialStats") as PlayerStats;
        playerStats = ScriptableObject.Instantiate(baseSO);
        //playerStats.health = 50;

        //(4)Load the SO, but create a new Instance keeping original, new instance is also saved 
        //as the current Player Stats for access from UI for example
        //PlayerStats baseSO = Resources.Load("_ScriptableObjects/PlayerInitialStats") as PlayerStats;
        //playerStats = ScriptableObject.Instantiate(baseSO);
        //AssetDatabase.CreateAsset(playerStats, "Assets/Resources/_ScriptableObjects/PlayerGameStats.asset");
        //playerStats = Resources.Load("_ScriptableObjects/PlayerGameStats") as PlayerStats;

        Debug.Log("PlayerStats initial MaxHealth: " + playerStats.maxHealth);
        Debug.Log("PlayerStats initial    health: " + playerStats.health);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("PlayerStats initial    health: " + playerStats.health);

        if (playerStats.health <= 0)
        {
            TP_Controller.Instance.Die();
        }
    }
}
