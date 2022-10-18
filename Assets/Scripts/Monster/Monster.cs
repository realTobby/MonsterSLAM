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
        walkable = GetComponent<Walkable>() != null ? GetComponent<Walkable>() : null;
    }

    public void InitMonster(int mMaxHP)
    {
        MaxHP = mMaxHP;
        HP = MaxHP;
    }

    private bool IsAttacking = false;
    public float AttackDelay = 1;

    public bool LeftPlayerTrigger = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Hammer"))
        {
            LeftPlayerTrigger = false;
            StartCoroutine(nameof(AttackPlayer));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Hammer"))
        {
            LeftPlayerTrigger = true;
        }
    }

    public GameObject BOMB_PREFAB;

    public GameObject AttachedBomb = null;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.IsGamePaused == false)
        {
            // explosion effect, attach bomb when exploded
            if (other.transform.CompareTag("Bomb"))
            {
                AttachedBomb = Instantiate(BOMB_PREFAB, transform.position, Quaternion.identity);
                AttachedBomb.transform.parent = this.transform;

                HP -= GameManager.Instance.SecondaryAbility.ability.Damage;
            }

            //Debug.Log("Collision with: " + other.transform.tag);
            if (other.transform.CompareTag("HammerHitbox") || other.transform.CompareTag("FlyingHammer"))
            {
                HP -= GameManager.Instance.MainAbility.ability.Damage;
                //var vfx = Instantiate(ExplodeEffect, this.transform.position, Quaternion.identity);

                if(other.transform.GetComponent<HammerHitBox>()._hammer.IsAttackingWithBombs)
                {
                    AttachedBomb = Instantiate(BOMB_PREFAB, other.transform.position, Quaternion.identity);
                    AttachedBomb.transform.parent = this.transform;
                }

            }

            if(other.transform.CompareTag("FlyingHammer"))
            {
                var vfx = Instantiate(ExplodeEffect, this.transform.position, Quaternion.identity);
            }

            if (other.transform.CompareTag("Knockback"))
            {
                Vector3 direction = ((transform.position - other.transform.position).normalized);
                direction.y = 0;

                walkable.rigidbody.AddForce(direction * 6000);

                //var vfx = Instantiate(ExplodeEffect, this.transform.position, Quaternion.identity);

                HP -= GameManager.Instance.SecondaryAbility.ability.Damage;
            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("HammerHitbox"))
        {
            LeftPlayerTrigger = true;
        }
    }

    IEnumerator AttackPlayer()
    {
        while(LeftPlayerTrigger == false)
        {
            IsAttacking = true;
            yield return new WaitForSeconds(AttackDelay);
            PlayerStats.Instance.Stats.HP -= 1;
            IsAttacking = false;
        }
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
            if (AttachedBomb != null)
            {
                AttachedBomb.transform.parent = null;
                AttachedBomb.GetComponent<BombAttachment>().Explode();
            }

            // lucky roll for xpgem

            int luckyRerolls = GameManager.Instance.MainAbility.ability.Luck;

            bool luckyThreshhold = Random.Range(0, 100) >= 60 ? true : false;

            if (luckyThreshhold)
            {
                var xpobj = Instantiate(GameManager.Instance.PREFAB_XPGEM, new Vector3(this.transform.position.x, 4, this.transform.position.z), Quaternion.Euler(-90, 0, 90));
                xpobj.GetComponent<XPGEM>().XPVALUE = luckyRerolls * 2;
            }

            var vfx = Instantiate(ExplodeEffect, this.transform.position, Quaternion.identity);
            GameManager.Instance.GainScore(1);
            GameManager.Instance.GainChainBonus();

            

            Destroy(this.gameObject);
        }
        
    }
}
