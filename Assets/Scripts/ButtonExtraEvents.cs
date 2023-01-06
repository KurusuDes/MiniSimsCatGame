using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
public class ButtonExtraEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnClick;
    public UnityEvent OnRelease;
    public UnityEvent OnEnterHover;
    public UnityEvent OnExitHover;
    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnRelease.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnterHover.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExitHover.Invoke();
    }

}
