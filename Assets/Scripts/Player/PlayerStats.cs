using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;
    public static PlayerStats Instance => _instance;

    public TMPro.TextMeshProUGUI PLAYERHP_UI;
    public TMPro.TextMeshProUGUI PLAYERXP_UI;

    public System.Action OnLevelUp;

    public AudioSource LEVEL_UP_SFX;

    private void Awake()
    {
        _instance = this;

    }


   
    public Stats Stats;

    private void Update()
    {
        PLAYERHP_UI.text = $"HP: {Stats.HP}/{Stats.MaxHP}";

        PLAYERXP_UI.text = $"XP: {Stats.CurrentXP}/{Stats.NextLevelXP}";

        CheckLevelUp();
        CheckForUnalive();
    }

    private void CheckForUnalive()
    {
        if(Stats.HP <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    private void CheckLevelUp()
    {
        if(Stats.CurrentXP >= Stats.NextLevelXP)
        {
            OnLevelUp?.Invoke();
            LEVEL_UP_SFX.Play();
            Stats.CurrentXP = 0;
            Stats.NextLevelXP += 10;
        }
    }


}
