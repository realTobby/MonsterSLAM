using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public HammerStart _hammerStart;

    public GameObject PREFAB_HIGHSCORE_ENTRY;

    public GameObject UI_HIGHSCORE_CONTENT;

    public void OpenCredits()
    {
        _hammerStart.OpenScene(2);
        
    }

    public GameObject UI_HIGHSCORE_WINDOW;

    public void LoadHighscore()
    {
        //HighscoreEntry pastRuns = new HighscoreEntry();
        //pastRuns.LoadStats(PlayerPrefs.GetString("PastRunsJSON"));

        //foreach(var item in pastRuns.PastRuns)
        //{
        //    Debug.Log("Past run!");
            
        //    var newElement = Instantiate(PREFAB_HIGHSCORE_ENTRY, this.transform.position, Quaternion.identity);
        //    newElement.transform.parent = UI_HIGHSCORE_CONTENT.transform;
        //    newElement.GetComponent<HighscoreElement>().Init(item.PlayerName, item.Score);
        //    UI_HIGHSCORE_WINDOW.SetActive(true);
        //}

        //// read json <list>
        //// iterate
        //// instantiate PREFAB
        //// parent to highscoreScrollview
    }

    private void Awake()
    {
        LoadHighscore();
    }




}
