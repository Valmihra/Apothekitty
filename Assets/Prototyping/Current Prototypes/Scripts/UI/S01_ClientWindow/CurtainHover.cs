using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CurtainHover : MouseHover, IPointerEnterHandler, IPointerExitHandler
{
    public Image curtainImage;
    public Color defaultCurtainColour;
    public Color hoverCurtainColour;
    
    void Awake()
    {
        curtainImage = GetComponent<Image>();

        defaultCurtainColour = new Color (.8f, .8f, .8f);
        hoverCurtainColour = curtainImage.color;
        curtainImage.color = defaultCurtainColour;
    }
    
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.canStartDay)
        {
            curtainImage.color = hoverCurtainColour;
        }
        
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.Instance.canStartDay)
        {
            curtainImage.color = defaultCurtainColour;
        }
    }
}
