using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HammerHitBox : MonoBehaviour
{
    public Hammer _hammer;

    public GameObject HitEffect;

    AudioSource _hammerSFX;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(!collision.transform.CompareTag("Hammer"))
    //    {
    //        // something was hit for sure!
    //        // shake camera
    //        // place coo dust particle system
    //        // also kill enemies
    //        Debug.Log("Hit :D");
    //        gameObject.SetActive(false);
    //    }
    //}

    private void Awake()
    {
        _hammer = transform.parent.GetComponent<Hammer>();

        _hammerSFX = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Hammer"))
        {
            Vector3 vfxSpawnPos = new Vector3(other.transform.position.x, 1.53f, other.transform.position.z);

            if(!other.transform.CompareTag("SpawnedMonster"))
            {
                Instantiate(HitEffect, vfxSpawnPos, Quaternion.identity);
            }

            
            CameraShaker.Shake(.4f, 8f);
            _hammerSFX.pitch = (Random.Range(0.6f, .9f));
            _hammerSFX.Play();
        }
    }
}
