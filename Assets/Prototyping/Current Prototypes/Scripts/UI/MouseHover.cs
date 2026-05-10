using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Color defaultColour;
    public Color hoverColour;
    
    void Awake()
    {
        image = GetComponent<Image>();

        defaultColour = new Color (.8f, .8f, .8f);              // image.color;
        hoverColour = image.color;                              //new Color (defaultColour.x, defaultColour.y, defaultColour.z, 0.5f);
        image.color = defaultColour;                            //defaultColour = new Color (1f,1f,1f,0f);
    }
    
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse registered - hovering over UI");
        image.color = hoverColour;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse registered - exited UI");
        image.color = defaultColour;
    }
}
