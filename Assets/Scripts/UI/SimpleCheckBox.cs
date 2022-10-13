using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleCheckBox : MonoBehaviour, IPointerClickHandler
{
    public Image CheckBoxImage;

    public bool IsChecked = false;

    public Sprite emtyBox;
    public Sprite checkedBox;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Maybe I clicked the CheckBox...");
        IsChecked = !IsChecked;
        //GameManager.Instance.WaveManager.AutoStart = IsChecked;
        RepaintCheckbox();
    }

    private void RepaintCheckbox()
    {
        if(IsChecked)
        {
            CheckBoxImage.sprite = checkedBox;
        }
        else
        {
            CheckBoxImage.sprite = emtyBox;
        }
    }

}
