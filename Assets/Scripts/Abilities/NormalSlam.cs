using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/NormalSlam")]
public class NormalSlam : Ability
{
    public override void Execute(GameObject target)
    {
        target.GetComponent<Hammer>()._hammerHitBox.gameObject.SetActive(true);
        target.GetComponent<Hammer>()._hammerAnimator.Play("HammerHit");
    }

    public override void EndAbility(GameObject target)
    {
        target.GetComponent<Hammer>()._hammerHitBox.gameObject.SetActive(false);
    }

}
