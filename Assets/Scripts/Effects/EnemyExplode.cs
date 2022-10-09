using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour
{
    AudioSource _expldoeSFX;

    private void Awake()
    {
        _expldoeSFX = GetComponent<AudioSource>();
        _expldoeSFX.clip = GameManager.Instance.DeathSounds[Random.Range(0, GameManager.Instance.DeathSounds.Length)];
        _expldoeSFX.pitch = (Random.Range(0.6f, .9f));
        _expldoeSFX.Play();

    }
}
