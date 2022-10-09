using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{

    private const float ForcePower = 10f;

    public new Rigidbody rigidbody;

    public float speed = 2f;
    public float force = 2f;

    private Vector3 direction;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void MoveTo(Vector3 direction)
    {
        this.direction = direction;
    }

    public void Stop()
    {
        MoveTo(Vector3.zero);
    }


    private void FixedUpdate()
    {
        if(GameManager.Instance.IsGamePaused == false)
        {
            var desiredVelocity = direction * speed;
            var deltaVelocity = desiredVelocity - rigidbody.velocity;
            Vector3 moveForce = deltaVelocity * (force * ForcePower * Time.fixedDeltaTime);
            rigidbody.AddForce(moveForce);
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
        
    }



}
