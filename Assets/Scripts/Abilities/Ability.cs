using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Ability : ScriptableObject
{
    public Sprite icon;

    public string Name;
    public float CooldownTime;
    public float ActiveTime;

    public int Damage;
    public int Luck;

    public virtual void InitBaseStats() { }

    public virtual void Execute(GameObject target) { }

    public virtual void EndAbility(GameObject target) { }

    public virtual void RuneUpgrade() { }

}
