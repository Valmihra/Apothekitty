using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class _PrototypeCursor : MonoBehaviour// , IPointerDownHandler, IPointerUpHandler
{
    private Color defaultColour;
    private Color clickColour;
    
    private GameObject mouseObject;
    private Image mouseImage;
    private Vector3 mousePosition;

    public bool holdingSomething;
    private bool uniqueCursorActive;

    private string colourCode = "#E0BF9E";
    
    private static _PrototypeCursor _instance;
    public static _PrototypeCursor Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        InitialiseCursor();
    }
    
    public void InitialiseCursor()
    {
        mouseObject = this.gameObject;
        mouseImage = GetComponent<Image>();

        clickColour = new Color(1f, 1f, 1f);
        if (ColorUtility.TryParseHtmlString(colourCode, out defaultColour))
        {
            mouseImage.color = defaultColour;
        }

        HideUniqueCursor();
        
        holdingSomething = false;
        uniqueCursorActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (uniqueCursorActive)
        {
            if (!holdingSomething)
            {
                mousePosition = Input.mousePosition;
                mouseObject.transform.position = mousePosition;
            }
        }
        
    }

    public void ToggleCursorType()
    {
        Debug.Log("is cursor visible?" + IsCursorVisible());
        
        if (IsCursorVisible())
        {
            Cursor.visible = false;
            ShowUniqueCursor();
        }
        else
        {
            Cursor.visible = true;
            HideUniqueCursor();
        }
    }
    
    void HideUniqueCursor()
    {
        mouseObject.SetActive(false);
        uniqueCursorActive = false;
    }

    void ShowUniqueCursor()
    {
        mouseObject.SetActive(true);
        uniqueCursorActive = true;
    }

    /*public void OnClick(PointerEventData eventData)
    {
        if (!hidden)
        {
            Debug.Log("OnPointerDown");
            mouseImage.color = clickColour;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!hidden)
        {
            Debug.Log("OnPointerUp");
            mouseImage.color = defaultColour;
        }
    }
    */
    
    // *TAG* - PROTOTYPE CURSOR EVENT
    public void QuickClick()
    {
        BeginLongClick();
        Invoke(nameof(EndLongClick), 0.1f);
    }

    public void BeginLongClick()
    {
        Debug.Log("OnPointerDown");
        mouseImage.color = clickColour;
    }

    public void EndLongClick()
    {
        Debug.Log("OnPointerUp");
        mouseImage.color = defaultColour;
    }
    
    private bool IsCursorVisible()
    {
        bool cursorCheck = Cursor.visible == true ? true : false; //Cursor.lockState == CursorLockMode.None ? true : false;
        return cursorCheck;
    }
}
