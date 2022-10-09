using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class Stats : ScriptableObject
{
    public float MaxHP = 10;
    public float HP = 10;
    public int Durability = 1;
    public float Speed = 15;
    public int Damage = 1;

    public void RestartStats()
    {
        MaxHP = 10;
        HP = 10;
        Durability = 1;
        Speed = 15;
        Damage = 1;
    }

}
