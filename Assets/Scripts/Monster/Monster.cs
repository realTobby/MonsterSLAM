using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform target;
    public Walkable walkable;

    public GameObject ExplodeEffect;

    public int MaxHP;
    public int HP;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Hammer").transform;
        walkable = GetComponent<Walkable>();
    }

    public void InitMonster(int mMaxHP)
    {
        MaxHP = mMaxHP;
        HP = MaxHP;
    }

    private bool IsAttacking = false;
    private float AttackDelay = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.IsGamePaused == false)
        {
            if (other.transform.CompareTag("HammerHitbox"))
            {
                HP -= PlayerStats.Instance.Stats.Damage;
                var vfx = Instantiate(ExplodeEffect, this.transform.position, Quaternion.identity);
            }

            if (other.transform.CompareTag("Knockback"))
            {


                Vector3 direction = ((transform.position - other.transform.position).normalized);
                direction.y = 0;

                walkable.rigidbody.AddForce(direction * 6000);

                var vfx = Instantiate(ExplodeEffect, this.transform.position, Quaternion.identity);

                HP -= PlayerStats.Instance.Stats.Damage;
            }

            if (other.transform.CompareTag("Hammer"))
            {
                if (IsAttacking == false)
                {
                    StartCoroutine(nameof(AttackPlayer));
                }
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        IsAttacking = true;
        yield return new WaitForSeconds(AttackDelay);
        PlayerStats.Instance.Stats.HP -= 1;
        IsAttacking = false;
    }



    private void Update()
    {
        if (GameManager.Instance.IsGamePaused == false)
        {
            CheckForDeath();

            var targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);
            transform.LookAt(target);

            var directionTowardsTarget = (target.position - this.transform.position).normalized;
            walkable.MoveTo(directionTowardsTarget);
        }
    }

    private void CheckForDeath()
    {
        if(HP <= 0)
        {
            // lucky roll for xpgem
            int luckyThreshhold = Random.Range(0, 100);

            if(luckyThreshhold >= 60)
            {
                Instantiate(GameManager.Instance.PREFAB_XPGEM, this.transform.position, Quaternion.Euler(-90, 0, 90));
            }


            var vfx = Instantiate(ExplodeEffect, this.transform.position, Quaternion.identity);
            GameManager.Instance.GainScore(1);
            GameManager.Instance.GainChainBonus();
            Destroy(this.gameObject);
        }
        
    }
}
