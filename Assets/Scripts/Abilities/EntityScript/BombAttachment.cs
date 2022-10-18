using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttachment : MonoBehaviour
{
    public GameObject BOMB_PREFAB;

    public GameObject EXPLOSION_VFX;

    SphereCollider triggerZone;

    //public float ExplosionTimer = 2f;

    private void Awake()
    {
        triggerZone = GetComponent<SphereCollider>();
        triggerZone.enabled = false;
        Gizmos.color = Color.red;
    }

    public void Explode()
    {
        StartCoroutine(nameof(ExplodeBomb));
    }

    IEnumerator ExplodeBomb()
    {
        Gizmos.color = Color.green;
        triggerZone.enabled = true;
        yield return new WaitForSeconds(.2f);
        Instantiate(EXPLOSION_VFX, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.DrawWireSphere(this.transform.position, triggerZone.radius);
    }


}
