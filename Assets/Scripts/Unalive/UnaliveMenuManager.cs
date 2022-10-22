using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UnaliveMenuManager : MonoBehaviour, IPointerClickHandler
{
    private void Awake()
    {
        RunStats lastStats = new RunStats();
        lastStats.LoadStats(PlayerPrefs.GetString("LastRunStatsJSON"));
        lastStats.PlayerName = "Kordesii";

        PlayerPrefs.SetString("LastRunStatsJSON", lastStats.GetJsonString());

        HighscoreEntry pastRuns = new HighscoreEntry();
        pastRuns.LoadStats(PlayerPrefs.GetString("PastRunsJSON"));
        pastRuns.PastRuns.Add(lastStats);
        PlayerPrefs.SetString("PastRunsJSON", pastRuns.GetJsonString());

        PlayerPrefs.Save();

        StartCoroutine(nameof(WaitTime));
    }

    public bool CanClick = false;

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        CanClick = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(CanClick == true)
            SceneManager.LoadScene(0);
    }
}
