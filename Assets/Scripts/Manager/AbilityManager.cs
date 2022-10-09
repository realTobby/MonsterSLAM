using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityManager : MonoBehaviour
{
    public Ability ability;

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

    // Update is called once per frame
    void Update()
    {
        switch(CurrentState)
        {
            case AbilityState.Ready:
                if (Input.GetKeyDown(Key) && EventSystem.current.IsPointerOverGameObject() == false)
                {
                    ability.Execute(GameManager.Instance.PlayerGameObject);
                    CurrentState = AbilityState.Active;
                    activeTime = ability.ActiveTime;
                }
                break;
            case AbilityState.Active:
                if(activeTime > 0)
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
                }
                else
                {
                    CurrentState = AbilityState.Ready;
                    
                }
                break;
        }
    }
}
