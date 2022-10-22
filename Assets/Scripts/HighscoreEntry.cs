using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighscoreEntry
{
    public List<RunStats> PastRuns = new List<RunStats>();

    public string GetJsonString()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadStats(string json)
    {
        HighscoreEntry tmp = JsonUtility.FromJson<HighscoreEntry>(json);
        if(tmp != null)
            PastRuns = tmp.PastRuns;
    }


}
