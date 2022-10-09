using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/KnockbackSwing")]
public class KnockbackSwing : Ability
{
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
