using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler
{
    [SerializeField, HeaderAttribute("ゲームマネージャ")]
    GameManager gameManager = null;

    Coroutine coStayClickEvent = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (coStayClickEvent != null) return;

        coStayClickEvent = StartCoroutine("StayClickEvent");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(coStayClickEvent);
        coStayClickEvent = null;
    }

    IEnumerator StayClickEvent()
    {
        while (true)
        {
            gameManager.OnRotationStay();

            yield return null;
        }
    }
}
