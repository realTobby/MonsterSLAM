using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2500)]
public class UpgradeManager : MonoBehaviour
{
    public GameObject UI_UPGRADE_WINDOW;

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
        GameManager.Instance.SecondaryAbility.ability.Damage += 2;
    }

    public void UpgradeHP()
    {
        PlayerStats.Instance.Stats.HP = PlayerStats.Instance.Stats.MaxHP;
    }

    public void UpgradeLuck()
    {
        GameManager.Instance.MainAbility.ability.Luck += 2;
        GameManager.Instance.SecondaryAbility.ability.Luck += 2;
    }

}
