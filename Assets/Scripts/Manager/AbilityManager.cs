using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public Ability ability;

    public Image UI_ABILITY_IMAGE;

    public float cooldownTime;
    public float activeTime;

    enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    AbilityState CurrentState = AbilityState.Ready;

    public KeyCode Key;

    private void Awake()
    {
        if(ability != null)
            ability.InitBaseStats();

        if(UI_ABILITY_IMAGE != null)
        {
            UI_ABILITY_IMAGE.fillAmount = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(ability != null && UI_ABILITY_IMAGE != null)
        {
            UI_ABILITY_IMAGE.sprite = ability.icon;
            if(UI_ABILITY_IMAGE.gameObject.active == false)
            {
                UI_ABILITY_IMAGE.gameObject.SetActive(true);
            }
        }

        if(ability == null && UI_ABILITY_IMAGE != null)
        {
            UI_ABILITY_IMAGE.gameObject.SetActive(false);
        }

        if (GameManager.Instance.IsGamePaused == false)
        {
            switch (CurrentState)
            {
                case AbilityState.Ready:
                    if (Input.GetKeyDown(Key) && EventSystem.current.IsPointerOverGameObject() == false && ability != null)
                    {
                        ability.Execute(GameManager.Instance.PlayerGameObject);
                        //GameManager.Instance.SecondaryAbility.ability.Execute(GameManager.Instance.PlayerGameObject);
                        CurrentState = AbilityState.Active;
                        activeTime = ability.ActiveTime;

                        if(UI_ABILITY_IMAGE != null)
                        {
                            UI_ABILITY_IMAGE.fillAmount = 0;
                        }
                    }
                    break;
                case AbilityState.Active:
                    if (activeTime > 0)
                    {
                        activeTime -= Time.deltaTime;
                    }
                    else
                    {
                        CurrentState = AbilityState.Cooldown;
                        ability.EndAbility(GameManager.Instance.PlayerGameObject);
                        cooldownTime = ability.CooldownTime;
                    }
                    break;
                case AbilityState.Cooldown:
                    if (cooldownTime > 0)
                    {
                        cooldownTime -= Time.deltaTime;

                        if (UI_ABILITY_IMAGE != null)
                        {
                            UI_ABILITY_IMAGE.fillAmount += 1/cooldownTime * Time.deltaTime;
                        }
                    }
                    else
                    {
                        CurrentState = AbilityState.Ready;
                        if(UI_ABILITY_IMAGE != null)
                        {
                            UI_ABILITY_IMAGE.fillAmount = 1;
                        }
                        
                    }
                    break;
            }
        }
        
    }
}
