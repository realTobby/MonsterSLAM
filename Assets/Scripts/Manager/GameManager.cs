using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public TMPro.TextMeshProUGUI UI_PLAYERSCORE;
    public TMPro.TextMeshProUGUI UI_MONSTER_BONUS;

    public WaveManager WaveManager;
    public UpgradeManager UpgradeManager;
    public AbilityManager AbilityManager;
    
    public int CurrentMonsterChain = 0;
    public int PlayerScore = 0;

    public AudioClip[] DeathSounds;

    public GameObject PlayerGameObject;

    private void Awake()
    {
        _instance = this;

        PlayerGameObject = GameObject.FindGameObjectWithTag("Hammer");

        WaveManager = GetComponent<WaveManager>();
        UpgradeManager = GetComponent<UpgradeManager>();
        AbilityManager = GetComponent<AbilityManager>();

        InvokeRepeating("PlayBonusDecreaseAnimation", 0f, 1f);
    }

    private void PlayBonusDecreaseAnimation()
    {
        if(UI_MONSTER_BONUS.IsActive())
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
            currentChainTime -= Time.deltaTime;
            yield return null;
        }

        currentChainTime = maxChainTime;
        CurrentMonsterChain = 0;
        StartCoroutine(nameof(ChainTimer));
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
    }

    

}
