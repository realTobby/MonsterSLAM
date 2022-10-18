using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UnaliveMenuManager : MonoBehaviour, IPointerClickHandler
{
    private void Awake()
    {

        StartCoroutine(nameof(WaitTime));
    }

    public bool CanClick = false;

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(5);
        CanClick = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(CanClick == true)
            SceneManager.LoadScene(0);
    }
}
