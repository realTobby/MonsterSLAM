using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject UI_UPGRADE_WINDOW;
    public GameObject UI_BUTTON_UPGRADES;
    public TMPro.TextMeshProUGUI PointsToSpendText;
    public int PointsToSpend = 0;

    public int WaveRewardThreshhold = 5;

    private void Awake()
    {
        GameManager.Instance.WaveManager.OnWaveStart += OnWaveStart;
        GameManager.Instance.WaveManager.OnWaveEnd += OnWaveEnd;
    }

    private void Update()
    {
        PointsToSpendText.text = "Points to Spend: " + System.Environment.NewLine + PointsToSpend;
    }

    private void OnWaveStart()
    {
        CloseUpgradeWindow();
        UI_BUTTON_UPGRADES.SetActive(false);
    }

    private void OnWaveEnd()
    {
        // check if current wave is equal to a wave-threshhold to gain Points for BaseStats!
        WaveRewardThreshhold--;
        if(WaveRewardThreshhold == 0)
        {
            WaveRewardThreshhold = 5;
            PointsToSpend += 1;
        }

        if(PointsToSpend > 0)
        {
            UI_BUTTON_UPGRADES.SetActive(true);
        }
    }

    public void OpenUpgradeWindow()
    {
        UI_UPGRADE_WINDOW.SetActive(true);
    }

    public void CloseUpgradeWindow()
    {
        UI_UPGRADE_WINDOW.SetActive(false);
    }

}
