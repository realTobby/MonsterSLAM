using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPGEM : MonoBehaviour
{
    public int XPVALUE;

    public bool WasMagneticTriggered = false;

    Rigidbody gemRigidibody;

    Vector3 moveDir;
    Vector3 _moveAmount;
    Vector3 _smoothMoveVeleocity;

    private void Awake()
    {
        gemRigidibody = GetComponent<Rigidbody>();

        GameManager.Instance.WaveManager.OnWaveEnd += BeMagnetic;

    }

    public void BeMagnetic()
    {
        WasMagneticTriggered = true;
        Destroy(this.gameObject, 15f);
    }

    private void Update()
    {
        if(WasMagneticTriggered)
        {
            
            moveDir = (GameManager.Instance.PlayerGameObject.transform.position - transform.position).normalized;
            _moveAmount = moveDir * 4f;
        }
    }

    private void FixedUpdate()
    {
        if(WasMagneticTriggered)
        {
            if (GameManager.Instance.IsGamePaused == false)
            {
                gemRigidibody.velocity = Vector3.zero;
                Vector3 moveForce = _moveAmount * (100 * 2 * Time.fixedDeltaTime);
                gemRigidibody.AddForce(moveForce);
            }

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.Instance.IsGamePaused == false)
        {
            if (collision.transform.CompareTag("Hammer"))
            {
                PlayerStats.Instance.Stats.CurrentXP += XPVALUE;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("MagneticRadius"))
        {
            WasMagneticTriggered = true;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.WaveManager.OnWaveEnd -= BeMagnetic;
    }
}
