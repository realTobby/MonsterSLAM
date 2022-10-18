using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(2500)]
public class UpgradeManager : MonoBehaviour
{
    public GameObject UI_UPGRADE_WINDOW;

    public GameObject UI_NEW_RUNE_WINDOW;

    public GameObject PREFAB_XPGEM;

    private void Awake()
    {
        PlayerStats.Instance.OnLevelUp += OpenUpgradeWindow;
        GameManager.Instance.WaveManager.OnWaveStart += OnWaveStart;
    }

    private void Update()
    {
        //PointsToSpendText.text = "Points to Spend: " + System.Environment.NewLine + PointsToSpend;
    }

    private void OnWaveStart()
    {
        CloseUpgradeWindow();
        UI_UPGRADE_WINDOW.SetActive(false);
    }

    public void OpenUpgradeWindow()
    {
        UI_UPGRADE_WINDOW.SetActive(true);
    }

    public void CloseUpgradeWindow()
    {
        UI_UPGRADE_WINDOW.SetActive(false);
        GameManager.Instance.UnpauseGame();
    }


    public void UpgradeDamage()
    {
        GameManager.Instance.MainAbility.ability.Damage += 2;
        if(GameManager.Instance.SecondaryAbility.ability != null)
            GameManager.Instance.SecondaryAbility.ability.Damage += 2;
    }

    public void UpgradeHP()
    {
        PlayerStats.Instance.Stats.MaxHP += 5;
        PlayerStats.Instance.Stats.HP = PlayerStats.Instance.Stats.MaxHP;
    }

    public void UpgradeLuck()
    {
        GameManager.Instance.MainAbility.ability.Luck += 2;
        if (GameManager.Instance.SecondaryAbility.ability != null)
            GameManager.Instance.SecondaryAbility.ability.Luck += 2;
    }

    public TMPro.TextMeshProUGUI UI_NEWRUNE_NAME;

    public TMPro.TextMeshProUGUI UI_RUNE_BUTTON_TEXT;

    public enum Abilities
    {
        FlyingHammer = 0,
        KnockbackSwing = 1,
        BackwardsSlam = 2
    }

    public bool IsTake = false;
    public bool IsSwap = false;
    public bool IsUpgrade = false;

    public void OpenNewRune()
    {
        IsTake = false;
        IsSwap = false;
        IsUpgrade = false;

        int runeIndex = Random.Range(0, 3);

        Abilities ability = (Abilities)runeIndex;

        NewRune = Resources.Load<Ability>("Abilities/" + ability.ToString());
        NewRune.InitBaseStats();
        UI_NEWRUNE_NAME.text = NewRune.Name;
        UI_RUNE_ICON.sprite = NewRune.icon;
        // Take Rune // if secondary is empty

        if(GameManager.Instance.SecondaryAbility.ability == null)
        {
            UI_RUNE_BUTTON_TEXT.text = "Take rune!";
            IsTake = true;
        }
        else
        {
            // Swap Rune // if secondary is a different ability
            if (GameManager.Instance.SecondaryAbility.ability.GetType() != NewRune.GetType())
            {
                UI_RUNE_BUTTON_TEXT.text = "Replace rune!";
                IsSwap = true;
            }

            // Upgrade Rune // if secondary is same
            if (GameManager.Instance.SecondaryAbility.ability.GetType() == NewRune.GetType())
            {
                UI_RUNE_BUTTON_TEXT.text = "Upgrade rune!";
                IsUpgrade = true;
            }
        }
        UI_NEW_RUNE_WINDOW.SetActive(true);
    }
    public Image UI_RUNE_ICON;
    public Ability NewRune;

    public void TakeRune()
    {
        if(IsTake || IsSwap)
        {
            GameManager.Instance.SecondaryAbility.ability = NewRune;
        }

        if(IsUpgrade)
        {
            GameManager.Instance.SecondaryAbility.ability.RuneUpgrade();
        }
        UI_NEW_RUNE_WINDOW.SetActive(false);
    }

    // Dismiss option
    public void CloseRuneWindow()
    {
        // spawn a LOT of gems for the player in the current room
        int gems = Random.Range(5, 200);
        for(int i = 0; i < gems; i++)
        {
            var pos = GameManager.Instance.WaveManager.RandomPositionInsideArena(GameManager.Instance.DungeonRoomSpawner.LastRoom.GetComponent<DungeonRoom>());
            pos.y = 4;
            var xp = Instantiate(PREFAB_XPGEM, pos, Quaternion.identity);
            xp.GetComponent<XPGEM>().XPVALUE = 5;
            xp.transform.rotation = Random.rotation;
            xp.GetComponent<XPGEM>().BeMagnetic();
        }

        UI_NEW_RUNE_WINDOW.SetActive(false);
    }

}
