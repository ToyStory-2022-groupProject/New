using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Text btnText;

    public void OnPointerDown(PointerEventData eventData)
    {
        btnText.color = Color.gray;
    }
    
    public void OnPointerUp (PointerEventData eventData)
    {
        btnText.color = Color.yellow;
    }
    
}
