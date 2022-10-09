using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class OnClickEvent : UnityEvent { }

public class UpgradeOptionInput : MonoBehaviour, IPointerClickHandler
{
    public OnClickEvent OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
