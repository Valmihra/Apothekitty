using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableHerbs : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Location Reference")]
    public RectTransform rectTransform;
    private  CanvasGroup movableCutting;

    private Canvas canvas;
    private Outline outline;
    
    private Vector2 initialPositionOnDrag;
    private LayerMask deskBorder;

    private Image image;
    public Image cuttingImage;
    private Color defaultColour;
    private Color hoverColour;

    private float maxX;
    private float minX;
    private float maxY;
    private float minY;

    private float uiScale;
    
    

    void Awake()
    {
        LocateRect();
        AssignCanvasGroup();
        

        image = GetComponent<Image>();
        outline = GetComponent<Outline>();

        canvas = GetComponentInParent<Canvas>();
        uiScale = canvas.scaleFactor;

        defaultColour = new Color (.8f, .8f, .8f);              // image.color;
        hoverColour = image.color;                              //new Color (defaultColour.x, defaultColour.y, defaultColour.z, 0.5f);
        image.color = defaultColour;                            //defaultColour = new Color (1f,1f,1f,0f);
    }

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
        image.color = hoverColour;
    }

    public void OnPointerExit(PointerEventData mouse)
    {
        image.color = defaultColour;
    }

    // Sets the initial position of the UI prior to movement and brings the selected panel to the front on the screen
    public void OnBeginDrag(PointerEventData mouse)
    {
        initialPositionOnDrag = rectTransform.anchoredPosition;
        UIManager.Instance.EnableUI(movableCutting);

        UIManager.Instance.DisableInteraction(SceneManager.Instance.herbDrawers);
    }

    // Updates the UI position according to the mouse's movement
    public void OnDrag(PointerEventData mouse)
    {
        rectTransform.anchoredPosition += mouse.delta / uiScale;
    }

    // Checks to see if the UI is within the set boundaries, and places it accordingly
    public void OnEndDrag(PointerEventData mouse)
    {
        /*Vector2 herbPosition = rectTransform.anchoredPosition / uiScale;
        if ((herbPosition.x <= Inventory.Reference.maxX) && (herbPosition.x >= Inventory.Reference.minX))
        {
            Debug.Log("Caught");
            if ((herbPosition.y <= Inventory.Reference.maxY) && (herbPosition.y >= Inventory.Reference.minY))
            {
                //Debug.Log("Caught");
                Image herbIcon = movableCutting.gameObject.GetComponent<Image>();
            }
        }*/
            UIManager.Instance.DisableUI(movableCutting);
            rectTransform.anchoredPosition = initialPositionOnDrag;
            UIManager.Instance.EnableInteraction(SceneManager.Instance.herbDrawers);        
    }

    void LocateRect()
    {
        RectTransform r = GetComponent<RectTransform>();
        RectTransform[] findChild = GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < findChild.Length; i++)
        {
            if (findChild[i].gameObject != gameObject)
            {
                rectTransform = findChild[i];
                //Debug.Log(rectTransform.gameObject.name);
                return;
            }
        }
    }

    void AssignCanvasGroup()
    {
        //if (rectTransform != null)
        //{
            //Debug.Log(rectTransform.gameObject.name);
        //}
        
        CanvasGroup[] getCanvasGroup = GetComponentsInChildren<CanvasGroup>();
        if (getCanvasGroup[0].gameObject != gameObject)
        {
            movableCutting = getCanvasGroup[0];
            cuttingImage = movableCutting.gameObject.GetComponent<Image>();
            //Debug.Log(movableCutting.gameObject.name);
            //UIManager.Instance.DisableUI(this.movableCutting);
            return;
        }
    }
}
