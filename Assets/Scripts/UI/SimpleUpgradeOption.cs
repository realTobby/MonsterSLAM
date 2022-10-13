using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeOptionType
{
    Health,
    Durability,
    Damage,
    Speed
}

public class SimpleUpgradeOption : MonoBehaviour
{
    public UpgradeOptionType OptionType;

    public TMPro.TextMeshProUGUI OptionText;

    public void IncreaseOption()
    {
        //switch (OptionType)
        //{
        //    case UpgradeOptionType.Health:
        //        PlayerStats.Instance.Stats.MaxHP += 1;
        //        break;
        //    case UpgradeOptionType.Durability:
        //        PlayerStats.Instance.Stats.Durability += 1;
        //        GameManager.Instance.UpgradeManager.PointsToSpend--;
        //        break;
        //    case UpgradeOptionType.Damage:
        //        PlayerStats.Instance.Stats.Damage += 1;
        //        GameManager.Instance.UpgradeManager.PointsToSpend--;
        //        break;
        //    case UpgradeOptionType.Speed:
        //        PlayerStats.Instance.Stats.Speed += 1;
        //        GameManager.Instance.UpgradeManager.PointsToSpend--;
        //        break;
        //}

        
    }

    public void DecreaseOption()
    {
        //switch (OptionType)
        //{
        //    case UpgradeOptionType.Health:
        //        if(PlayerStats.Instance.Stats.MaxHP > 1)
        //        {
        //            PlayerStats.Instance.Stats.MaxHP -= 1;
        //            GameManager.Instance.UpgradeManager.PointsToSpend++;
        //        }
        //        break;
        //    case UpgradeOptionType.Durability:
        //        if (PlayerStats.Instance.Stats.Durability > 1)
        //        {
        //            PlayerStats.Instance.Stats.Durability -= 1;
        //            GameManager.Instance.UpgradeManager.PointsToSpend++;
        //        }
                
        //        break;
        //    case UpgradeOptionType.Damage:
        //        if (PlayerStats.Instance.Stats.Damage > 1)
        //        {
        //            PlayerStats.Instance.Stats.Damage -= 1;
        //            GameManager.Instance.UpgradeManager.PointsToSpend++;
        //        }
                
        //        break;
        //    case UpgradeOptionType.Speed:
        //        if (PlayerStats.Instance.Stats.Speed > 1)
        //        {
        //            PlayerStats.Instance.Stats.Speed -= 1;
        //            GameManager.Instance.UpgradeManager.PointsToSpend++;
        //        }
                
        //        break;
        //}

        

    }

    private void Update()
    {
        switch (OptionType)
        {
            case UpgradeOptionType.Health:
                OptionText.text = "Health" + System.Environment.NewLine + PlayerStats.Instance.Stats.MaxHP;
                break;
            case UpgradeOptionType.Durability:
                OptionText.text = "Durability" + System.Environment.NewLine + PlayerStats.Instance.Stats.Durability;
                break;
            case UpgradeOptionType.Damage:
                OptionText.text = "Damage" + System.Environment.NewLine + PlayerStats.Instance.Stats.Damage;
                break;
            case UpgradeOptionType.Speed:
                OptionText.text = "Speed" + System.Environment.NewLine + PlayerStats.Instance.Stats.Speed;
                break;
        }
    }



}
