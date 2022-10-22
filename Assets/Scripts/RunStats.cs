using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RunStats
{
    public string PlayerName;
    public int Score;
    public string SecondaryRuneName;
    public int MonstersKilled;
    public int RoomsCleared;
    public int HighestCombo;
    public int Damage;
    public int HP;
    public int Luck;

    public string GetJsonString()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadStats(string json)
    {
        RunStats tmp = JsonUtility.FromJson<RunStats>(json);
        if(tmp != null)
        {
            PlayerName = tmp.PlayerName;
            Score = tmp.Score;
            SecondaryRuneName = tmp.SecondaryRuneName;
            MonstersKilled = tmp.MonstersKilled;
            RoomsCleared = tmp.RoomsCleared;
            HighestCombo = tmp.HighestCombo;
            Damage = tmp.Damage;
            HP = tmp.HP;
            Luck = tmp.Luck;
        }
        
    }

}
