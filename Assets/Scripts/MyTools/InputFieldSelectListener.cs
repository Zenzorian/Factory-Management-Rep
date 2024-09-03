using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputFieldSelectListener : MonoBehaviour, IPointerClickHandler
{
    public Action onSelectAction;

    public void OnPointerClick(PointerEventData eventData)
    {
        onSelectAction?.Invoke();
    }   
}
