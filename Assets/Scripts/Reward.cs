using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public int MaxHits = 10;

    public int HITS = 0;

    public AudioSource HIT_SFX;
    public float pitch = 0.1f;

    public Material rewardMaterial;

    public float intensity = 0.3f;

    private void Awake()
    {
        rewardMaterial = this.transform.GetComponentInChildren<Renderer>().material;

        rewardMaterial.SetColor("_EmissionColor", new Color(2.55f, 0f, 2.55f, 0.3f)); // set the starting color to a low alpha 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("HammerHitbox") || other.transform.CompareTag("FlyingHammer"))
        {
            HITS++;
            HIT_SFX.pitch = pitch;
            HIT_SFX.Play();
            pitch += 0.1f;
            intensity += 0.2f;
            rewardMaterial.SetColor("_EmissionColor", new Color(2.55f, 0f, 2.55f, intensity)); // set the starting color to a low alpha 

        }
    }
    private void Update()
    {
        if(HITS >= MaxHits)
        {
            GameManager.Instance.UpgradeManager.OpenNewRune();
            GameManager.Instance.WaveManager.OnWaveEnd?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
