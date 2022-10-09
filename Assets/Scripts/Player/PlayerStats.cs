using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;
    public static PlayerStats Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    public Stats Stats;
}
