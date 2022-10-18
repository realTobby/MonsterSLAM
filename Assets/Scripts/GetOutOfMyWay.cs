using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOutOfMyWay : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Wall"))
        {
            Destroy(collision.gameObject);
        }
    }
}
