using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Attach this component to the base panel of a UI element you want to be draggable in the scene.
    [Header("Location Reference")]
    public RectTransform rectTransform;
    //public CanvasGroup canvas;
    private Canvas canvas;
    private Outline outline;
    //private float 
    private Vector2 initialPositionOnDrag;
    private LayerMask deskBorder;

    private Image image;
    private Color defaultColour;
    private Color hoverColour;
    //private Color

    private float maxX;
    private float minX;
    private float maxY;
    private float minY;

    private float uiScale;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        outline = GetComponent<Outline>();

        canvas = GetComponentInParent<Canvas>();
        uiScale = canvas.scaleFactor;

        defaultColour = new Color (.8f, .8f, .8f);              // image.color;
        hoverColour = image.color;                              //new Color (defaultColour.x, defaultColour.y, defaultColour.z, 0.5f);
        image.color = defaultColour;                            //defaultColour = new Color (1f,1f,1f,0f);
        
            //SetupColours();
    }

    /*void SetupColours()
    {
        /*defaultColour = image.color;
        defaultColour.a = 0.5f;

        hoverColour = defaultColour;

        defaultColour.a = 0f;/
        Debug.Log("Would set up UI highlight shader here.");
    }*/

    // Defines the area within which it is safe for the player to drag the UI elements
    void DefineDragBoundaries()
    {
        // maxX = 
        // minX = 
        // maxY = 
        // minY = 
    }

    public void OnPointerEnter(PointerEventData mouse)
    {
        Debug.Log("Mouse registered - hovering over UI");
        // Might want to add shader so that can enable/disable 
        // when hovering over the draggable element? for now 
        // just adding debug so I know it works

        // outline.color = hoverColour;
        image.color = hoverColour;
    }

    public void OnPointerExit(PointerEventData mouse)
    {
        Debug.Log("Mouse registered - exited UI");
        //
        //outline.color = defaultColour;
        image.color = defaultColour;
    }

    // Sets the initial position of the UI prior to movement and brings the selected panel to the front on the screen
    public void OnBeginDrag(PointerEventData mouse)
    {
        Debug.Log("Drag working"); 
        rectTransform.SetAsLastSibling();
        initialPositionOnDrag = rectTransform.anchoredPosition;
    }

    // Updates the UI position according to the mouse's movement
    public void OnDrag(PointerEventData mouse)
    {
        //Debug.Log("Dragging"); 
        //
        rectTransform.anchoredPosition += mouse.delta / uiScale;


    }

    // Checks to see if the UI is within the set boundaries, and places it accordingly
    public void OnEndDrag(PointerEventData mouse)
    {
        Debug.Log("Drag complete");


        // if end position is outside of the desk boundaries, the drag does not count
        // else, the new position becomes the position at the end of the drag
                // if (this.GetComponent)
            
            //Vector2 alteredPosition = rectTransform.position.x >= maxX ? initialPositionOnDrag : rectTransform.position.x <= minX ? initialPositionOnDrag : rectTransform.position.y >= maxY ? initialPositionOnDrag : rectTransform.position.y <= minY ? initialPositionOnDrag : rectTransform.position;
            //rectTransform.anchoredPosition = alteredPosition;
        
    }



    // CAN INHERIT FROM THIS FOR DRAGGABLE INGREDIENTS? THEN USE OVERRIDE
    // FOR THE MIN AND MAX X, Y COORDS, AS WELL AS THE LAYERMASK -- CHANGE
    // FROM THE DESK TO THE INVENTORY SLOT.

}
