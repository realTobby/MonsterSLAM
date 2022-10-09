using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Ability : ScriptableObject
{
    public string Name;
    public float CooldownTime;
    public float ActiveTime;

    public virtual void Execute(GameObject target) { }

    public virtual void EndAbility(GameObject target) { }
}
