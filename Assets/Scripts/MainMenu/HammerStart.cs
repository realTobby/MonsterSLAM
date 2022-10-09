using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HammerStart : MonoBehaviour
{
    AudioSource _hammerStartSFX;

    public Material startMaterial;
    public Material glowMaterial;

    private void Awake()
    {
        _hammerStartSFX = GetComponent<AudioSource>();
    }

    private void OnMouseEnter()
    {
        this.GetComponent<Renderer>().sharedMaterial = glowMaterial;
    }

    private void OnMouseExit()
    {
        if(IsStartedGame == false)
        {
            this.GetComponent<Renderer>().sharedMaterial = startMaterial;
        }
        
    }

    private void OnMouseDown()
    {
        if(IsStartedGame == false)
        {
            StartCoroutine(nameof(MainMenuFadeOut));
            _hammerStartSFX.Play();
        }
        
    }

    public Image MainMenuFadeOutImage;

    public bool IsStartedGame = false;
    Color ImageStartColor;

    [SerializeField]
    float lerpDuration = 2.2f;

    IEnumerator MainMenuFadeOut()
    {
        IsStartedGame = true;

        Color endColor = Color.black;

        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            MainMenuFadeOutImage.color = Color.Lerp(ImageStartColor, endColor, timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        MainMenuFadeOutImage.color = endColor;

        //yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);

    }


}
