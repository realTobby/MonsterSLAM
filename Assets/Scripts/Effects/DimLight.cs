using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimLight : MonoBehaviour
{
    public Light vfxLight;

    public float DimDuration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(DimLightLevel));
    }

    IEnumerator DimLightLevel()
    {
        float startIntensity = vfxLight.intensity;
        float endIntensity = 0f;

        float timeElapsed = 0;
        while (timeElapsed < DimDuration)
        {
            vfxLight.intensity = Mathf.Lerp(startIntensity, endIntensity, timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        vfxLight.intensity = endIntensity;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}
