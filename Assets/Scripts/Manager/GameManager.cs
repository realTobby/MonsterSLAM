using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public TMPro.TextMeshProUGUI UI_PLAYERSCORE;
    public TMPro.TextMeshProUGUI UI_MONSTER_BONUS;

    public WaveManager WaveManager;
    public UpgradeManager UpgradeManager;

    public AbilityManager MainAbility;
    public AbilityManager SecondaryAbility;

    public RoomSpawner DungeonRoomSpawner;

    public int CurrentMonsterChain = 0;
    public int PlayerScore = 0;

    public AudioClip[] DeathSounds;

    public GameObject PlayerGameObject;

    public GameObject PREFAB_XPGEM;

    public RunStats CurrentRunStats;

    public bool IsGamePaused = false;

    public void PauseGame()
    {
        IsGamePaused = true;
    }

    public void UnpauseGame()
    {
        IsGamePaused = false;
    }

    private void Awake()
    {
        _instance = this;

        CurrentRunStats = new RunStats();


        PlayerStats.Instance.OnLevelUp += PauseGame;

        PlayerGameObject = GameObject.FindGameObjectWithTag("Hammer");

        WaveManager = GetComponent<WaveManager>();
        UpgradeManager = GetComponent<UpgradeManager>();
        //AbilityManager = GetComponent<AbilityManager>();

        InvokeRepeating("PlayBonusDecreaseAnimation", 0f, 1f);
    }

    private void PlayBonusDecreaseAnimation()
    {
        if(UI_MONSTER_BONUS.IsActive() && IsGamePaused == false)
            UI_MONSTER_BONUS.gameObject.GetComponent<Animator>().Play("monsterChainTimerDecrease");
    }

    private void Update()
    {
        UpdateMonsterChainBonus();

        if(UI_PLAYERSCORE != null)
        {
            if(PlayerScore > 0)
            {
                UI_PLAYERSCORE.gameObject.SetActive(true);
            }
            else
            {
                UI_PLAYERSCORE.gameObject.SetActive(false);
            }
            UI_PLAYERSCORE.text = "Score: " + PlayerScore.ToString();
        }

    }

    float currentChainTime = 0f;
    float maxChainTime = 5f;

    public bool ChainIsRunning = false;

    IEnumerator ChainTimer()
    {
        ChainIsRunning = true;

        while (currentChainTime > 0)
        {
            if(IsGamePaused == false)
            {
                currentChainTime -= Time.deltaTime;
            }
            yield return null;
        }

        currentChainTime = maxChainTime;
        CurrentMonsterChain = 0;
        StartCoroutine(nameof(ChainTimer));
    }

    public void EndRun()
    {
        PlayerPrefs.SetString("LastRunStatsJSON", CurrentRunStats.GetJsonString());
        PlayerPrefs.Save();

        // save all shit to PlayerPrefs
        // load unalive screen
        SceneManager.LoadScene(3);
        // show stats
        // let user pick a name
        // save that shit to PlayerPrefs

        //show highscores in mainmenu
    }

    public void GainChainBonus()
    {
        CurrentMonsterChain++;
        UpdateMonsterChainBonus();

        currentChainTime = maxChainTime;

        if (ChainIsRunning == false)
        {
            StartCoroutine(nameof(ChainTimer));
        }

        UI_MONSTER_BONUS.gameObject.GetComponent<Animator>().Play("monsterChainGain");
    }

    private void UpdateMonsterChainBonus()
    {
        CurrentRunStats.MonstersKilled += 1;
        UI_MONSTER_BONUS.text = "x" + CurrentMonsterChain.ToString();

        if (CurrentMonsterChain > 0)
        {
            UI_MONSTER_BONUS.gameObject.SetActive(true);
        }
        else
        {
            UI_MONSTER_BONUS.gameObject.SetActive(false);
        }
    }

    public void GainScore(int score)
    {
        PlayerScore += score * CurrentMonsterChain > 0 ? CurrentMonsterChain : 1;
        CurrentRunStats.Score = PlayerScore;
    }

    

}
