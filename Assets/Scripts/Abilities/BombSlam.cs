using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BombSlam")]
public class BombSlam : Ability
{
    public override void InitBaseStats()
    {
        Damage = 1;
        Luck = 1;
    }

    public override void RuneUpgrade()
    {
        Damage += 2;
    }

    public override void Execute(GameObject target)
    {
        target.GetComponent<Hammer>().IsAttackingWithBombs = true;

        target.GetComponent<Hammer>()._hammerHitBox.gameObject.SetActive(true);
        target.GetComponent<Hammer>()._hammerAnimator.Play("HammerHit");

    }

    public override void EndAbility(GameObject target)
    {
        target.GetComponent<Hammer>().IsAttackingWithBombs = false;
        target.GetComponent<Hammer>()._hammerHitBox.gameObject.SetActive(false);
    }
}
