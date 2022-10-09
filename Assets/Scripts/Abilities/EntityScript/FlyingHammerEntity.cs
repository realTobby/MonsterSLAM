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
    }

    // Start is called before the first frame update
    void Start()
    {
        FindNextTarget();
    }

    void FindNextTarget()
    {
        if(AllMonsterTransforms.Count > 0)
        {
            CurrentTarget = GetClosestEnemy(); //AllMonsterTransforms[Random.Range(0, AllMonsterTransforms.Count)];
            if(CurrentTarget != null)
            {
                AllMonsterTransforms.Remove(CurrentTarget);
                moveDir = (CurrentTarget.position - transform.position).normalized;
                _moveAmount = moveDir * PlayerStats.Instance.Stats.Speed;
            }
            
        }
        
    }

    Collider myCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("SpawnedMonster"))
        {
            Physics.IgnoreCollision(collision.collider, myCollider, true);
            if (MaxBounces > 0)
            {
                FindNextTarget();
                MaxBounces--;
            }
        }
    }

    private void Update()
    {
        transform.LookAt(CurrentTarget);

        if(MaxBounces <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveForce = _moveAmount * (100 * 2 * Time.fixedDeltaTime);
        flyingHammerRigidbody.AddForce(moveForce);

        //flyingHammerRigidbody.velocity = _moveAmount + transform.TransformDirection(_moveAmount) * Time.fixedDeltaTime;
    }

    Transform GetClosestEnemy()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in AllMonsterTransforms)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
