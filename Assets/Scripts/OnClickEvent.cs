using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnClickEvent : MonoBehaviour
{
    [Header("Extra Events")]

    [SerializeField]
    public UnityEvent OnClick;

    [SerializeField]
    public UnityEvent OnRelease;

    [SerializeField]
    public UnityEvent OnEnter;

    [SerializeField]
    public UnityEvent OnExit;

    public void OnMouseDown()
    {
        //print("Click");
        OnClick.Invoke();
    }
    public void OnMouseUp()
    {
        OnRelease.Invoke();
    }
    public void OnMouseOver()
    {
        //print("hover" + gameObject.name);
    }
    public void OnMouseEnter()
    {
        OnEnter.Invoke();
    }
    public void OnMouseExit()
    {
        OnExit.Invoke();
    }

}
