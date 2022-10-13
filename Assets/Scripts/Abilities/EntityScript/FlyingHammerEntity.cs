using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyingHammerEntity : MonoBehaviour
{
    public int MaxBounces = 10;

    List<Transform> AllMonsterTransforms;

    Rigidbody flyingHammerRigidbody;

    Vector3 moveDir;
    Vector3 _moveAmount;
    Vector3 _smoothMoveVeleocity;

    private Transform CurrentTarget;

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
        flyingHammerRigidbody = GetComponent<Rigidbody>();
        AllMonsterTransforms = GameObject.FindGameObjectsWithTag("SpawnedMonster").ToList().Select(x=>x.transform).ToList();
        Destroy(gameObject, 5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        FindNextTarget();
    }

    void FindNextTarget()
    {
        flyingHammerRigidbody.velocity = Vector3.zero;

        if (AllMonsterTransforms.Count > 0)
        {
            CurrentTarget = AllMonsterTransforms[Random.Range(0, AllMonsterTransforms.Count)];
            if(CurrentTarget != null)
            {
                flyingHammerRigidbody.velocity = Vector3.zero;
            }
            
        }
        
    }

    Collider myCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("SpawnedMonster"))
        {
            AllMonsterTransforms.Remove(other.transform);
            flyingHammerRigidbody.velocity = Vector3.zero;
            //Physics.IgnoreCollision(collision.collider, myCollider, true);
            if (MaxBounces > 0)
            {
                FindNextTarget();
                MaxBounces--;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void Update()
    {
        if(CurrentTarget != null)
        {
            moveDir = (CurrentTarget.position - transform.position).normalized;
            _moveAmount = moveDir * PlayerStats.Instance.Stats.Speed;

            transform.LookAt(CurrentTarget);
        }
        
        if(AllMonsterTransforms.Count <= 0)
        {
            Destroy(this.gameObject);
        }

        if(MaxBounces <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.IsGamePaused == false)
        {
            Vector3 moveForce = _moveAmount * (100 * 2 * Time.fixedDeltaTime);
            flyingHammerRigidbody.AddForce(moveForce);
        }
        else
        {
            flyingHammerRigidbody.velocity = Vector3.zero;
        }
        

        //flyingHammerRigidbody.velocity = _moveAmount + transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime;
    }

    //Transform GetClosestEnemy()
    //{
    //    Transform tMin = null;
    //    float minDist = Mathf.Infinity;
    //    Vector3 currentPos = transform.position;
    //    foreach (Transform t in AllMonsterTransforms)
    //    {
    //        float dist = Vector3.Distance(t.position, currentPos);
    //        if (dist < minDist)
    //        {
    //            tMin = t;
    //            minDist = dist;
    //        }
    //    }
    //    return tMin;
    //}

    private void OnDrawGizmos()
    {
        if(CurrentTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, CurrentTarget.transform.position);
        }
        
    }
}
