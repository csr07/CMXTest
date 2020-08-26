using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats", order = 51)]
public class PlayerStats : ScriptableObject
{
    [SerializeField] public int maxHealth = 200;
    [SerializeField] public int health = 100;
}
