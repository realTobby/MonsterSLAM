using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/FlyingHammer")]
public class FlyingHammer : Ability
{
    public GameObject PREFAB_FLYING_HAMMER;
    public int MaxBounces = 10;
    public float Speed;

    Stack<GameObject> AllCurrentGameObjects = new Stack<GameObject>();

    public override void InitBaseStats()
    {
        MaxBounces = 5;
        Damage = 1;
        Speed = 15;
        Luck = 1;
    }

    public override void RuneUpgrade()
    {
        Speed += 5f;
        MaxBounces+=10;
        base.CooldownTime -= .2f;
        if(base.CooldownTime <= 1)
        {
            base.CooldownTime = 1;
        }
    }

    public override void Execute(GameObject target)
    {
        var pos = new Vector3(target.transform.position.x, 2.32f, target.transform.position.z);
        var hammer = Instantiate(PREFAB_FLYING_HAMMER, pos, Quaternion.identity);
        hammer.GetComponent<FlyingHammerEntity>().Speed = Speed;
        hammer.GetComponent<FlyingHammerEntity>().MaxBounces = MaxBounces;
        AllCurrentGameObjects.Push(hammer);
    }

    public override void EndAbility(GameObject target)
    {
        var fh = AllCurrentGameObjects.Peek();
        AllCurrentGameObjects.Pop();
        Destroy(fh);
    }
}
