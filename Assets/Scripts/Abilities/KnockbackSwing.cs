using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/KnockbackSwing")]
public class KnockbackSwing : Ability
{

    public override void InitBaseStats()
    {
        CooldownTime = 5;
        ActiveTime = .1f;
        Damage = 1;
        Luck = 1;
    }

    public override void RuneUpgrade()
    {
        Damage += 2;
        Luck += 1;

        base.CooldownTime -= .2f;
        if (base.CooldownTime <= 1)
        {
            base.CooldownTime = 1;
        }
    }

    public override void Execute(GameObject target)
    {
        target.GetComponent<Hammer>().KnockbackHitbox.gameObject.SetActive(true);
        target.GetComponent<Hammer>()._hammerAnimator.Play("hammerKnockback");
    }

    public override void EndAbility(GameObject target)
    {
        target.GetComponent<Hammer>().KnockbackHitbox.gameObject.SetActive(false);
    }

}
