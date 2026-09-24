using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class _PrototypeLeafletsOnHover : MouseHover, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    /*  CURRENT ISSUE IS THAT LEAFLETS DO NOT SETASLASTSIBLING ON DRAG/POINTERDOWN,
     WHICH MEANS THEY ARE HARD TO KEEP ORGANISED. 
     
     COULD POTENTIALLY FIX THIS WITH AN INSTANTIATE PREFAB ON POINTERDOWN, HIDE THE
     REAL LEAFLET OBJECT AT SAME TIME, AND DRAG THE FAKE ONE AROUND? 
     
     WOULD NEED A GAMEOBJECT PARENT LOCATION TO SHARE WITH THE OTHER LEAFLETS -- AND
     PROBABLY ALSO SOMTHING IN PLACE TO MAKE SURE THEY RETURN WELL. ALSO, CLICKING 
     AND DRAGGING ON THEM WOULD HAVE TO BE FIXED SOMEHOW, SINCE THE OG SCRIPT WOULD
     STILL BE IN THE DOCK. HMMMMMMRRRMMMHHH,,,,
     
     */
    
    
    // Leaflet display properties
    private Image leafletImage;
    private Color defaultLeafletColour;
    private Color hoverLeafletColour;
    
    private TMP_Text titleText;
    private string leafletDetailText;
    private string leafletTitleText;
    private string tempLeafletPrefaceText = "This is just to prove the detail switching works.\n\nThis card has information about the following treatment category: ";
    
    // Leaflet positions on screen
    private Vector3 hoverLeafletPosition;
    private Vector3 defaultLeafletPosition;
    private Vector3 currentLeafletPosition;
    [SerializeField] private float hoverLeafletOffset = 30f;
    
    // Leaflet size and movement variables
    private RectTransform rectTransform;
    private Rect hiddenPanelRectBounds;
    private Rect leafletRectBounds;
    private Vector2 moveDelta;
    
    private Vector2 smallestSize = new Vector2(150f, 90f);
    private Vector2 largestSize = new Vector2(600f, 360f);
    private Vector2 currentSize;
    private float timeToResize = 0.1f;
    
    // Leaflet status checks
    private bool isDocked;
    private bool draggingFromDock;
    private bool goingBackToDock;
    private bool currentlyResizing;
    private bool defaultPositionsSet;
    
    public void InitialisePrototypeLeaflet()
    {
        defaultLeafletColour = new Color (.65f, .65f, .65f, 1f);
        
        rectTransform = GetComponent<RectTransform>();
        leafletImage = GetComponent<Image>();
        
        hoverLeafletColour = leafletImage.color;
        leafletImage.color = defaultLeafletColour;
        
        titleText = GetComponentInChildren<TMP_Text>();
        leafletTitleText = titleText.text;
        leafletDetailText = (tempLeafletPrefaceText + "<b>" + leafletTitleText + "</b>").ToString();
        
        // Default bool values should be:
        isDocked = true;
        goingBackToDock = false;
        draggingFromDock = false;
        currentlyResizing =  false;
        defaultPositionsSet = false;
        
        currentSize = smallestSize;
    }

    public void AssignRect(Rect rect)
    {
        hiddenPanelRectBounds = rect;
    }

    void CreateSelfRect()
    {
        // Uses rectTransform to set the rect for this leaflet
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        
        Vector2 min = corners[0];
        Vector2 max = corners[2];
        Vector2 size = max - min;

        leafletRectBounds = new Rect(min, size);
    }
    
    private Vector3 GetPositionFromTransform()
    {
        // Uses the current transform to set a position vector
        Vector3 leafletPositionToSet = transform.position;
        return leafletPositionToSet;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        // Always lights up if mouse is on the object
        leafletImage.color = hoverLeafletColour;
        
        if (isDocked)
        {
            // Sets default positions if they do not exist
            if (!defaultPositionsSet)
            {
                CreateSelfRect();
                defaultLeafletPosition = GetPositionFromTransform();
                hoverLeafletPosition = new Vector3(defaultLeafletPosition.x, defaultLeafletPosition.y + hoverLeafletOffset,  defaultLeafletPosition.z);
                defaultPositionsSet = true;
            }
            
            // Lifts up to hoverLeafletPosition inside the dock
            StartHoverAnimation(hoverLeafletPosition);
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
     {
         // Enlarges the leaflet if it is docked
         if (isDocked)
         {
             isDocked = false;
             draggingFromDock = true;
             
             if (currentSize != largestSize && !currentlyResizing)
             {
                 StartResize();
             }
         }
     }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        moveDelta = eventData.pressPosition - (Vector2)transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var x = eventData.position.x / Screen.width;
        var y = eventData.position.y / Screen.height;
        //try to prevent dragging the element offscreen
        if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;
        
        // Updates the position of the leaflet and its associated rect
        transform.position = eventData.position - moveDelta;
        leafletRectBounds.center = transform.position;
        
        // If not over hidden panel, checks size and drag status
        if (!leafletRectBounds.Overlaps(hiddenPanelRectBounds))
        {
            if (draggingFromDock)
            {
                draggingFromDock = false;
            }
            
            if (currentSize != largestSize && !currentlyResizing)
            {
                StartResize();
            }
        }
        
        // If not the initial drag from dock, leaflet shrinks when over hidden panel
        if (leafletRectBounds.Overlaps(hiddenPanelRectBounds) && !draggingFromDock)
        {
            if (currentSize != smallestSize && !currentlyResizing)
            {
                StartResize();
            }
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        CheckForCoroutineCompletion("OnPointerUp");

        if (currentSize == largestSize && leafletRectBounds.Overlaps(hiddenPanelRectBounds))
        {
            StartResize();
        }

        // Ensures that the drag check is always reset
        if (draggingFromDock)
        {
            draggingFromDock = false;
        }
    }
    
    public override void OnPointerExit(PointerEventData eventData)
    {
        leafletImage.color = defaultLeafletColour;
        
        // Returns to defaultLeafletPosition in the dock
        if (isDocked)
        {
            StartHoverAnimation(defaultLeafletPosition);
        }
    }
    
    
    void CheckForCoroutineCompletion(string caller)
    {
        // If leaflet is small or coroutine is still running when mouse lifts
        if ((currentlyResizing || currentSize == smallestSize) || (currentSize == smallestSize && !isDocked && goingBackToDock))
        {
            Debug.Log("Check from: " + caller + " was called before the resize coroutine could finish completely. Setting values and returning to dock.");
            currentLeafletPosition = GetPositionFromTransform();
            goingBackToDock = true;
            ReturnToDock();
        }
    }
    
    public void StartResize()
    {
        currentlyResizing = true;
        StopAllCoroutines();
        StartCoroutine(ResizeLeaflet());
    }
    
    IEnumerator ResizeLeaflet() 
    {
        float elapsedTime = 0.0f;
        titleText.gameObject.SetActive(false);
         
        // Uses currentSize to determine targetSize and titleText to determine updatedText
        Vector2 targetSize = currentSize == smallestSize ? largestSize : smallestSize;
        string updatedTextDisplay = titleText.text == leafletDetailText ? leafletTitleText : leafletDetailText;

        // Separate operations depending on resize value
        if (targetSize == largestSize)
        {
            while (elapsedTime < timeToResize)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime/timeToResize);
             
                rectTransform.sizeDelta = Vector2.Lerp(currentSize, targetSize, t);
             
                yield return null;
            }
             // Updates the text component's rectTransform
             float textRectOffset = 200f;
             titleText.GetComponent<RectTransform>().sizeDelta = new Vector2(targetSize.x - textRectOffset, targetSize.y - (textRectOffset / 10));
        }
        else
        {
            while (elapsedTime < timeToResize)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime/timeToResize);

                rectTransform.sizeDelta = Vector2.Lerp(currentSize, targetSize, t);
         
                yield return null;
            }
         
            titleText.GetComponent<RectTransform>().sizeDelta = targetSize;
        }

        // updates and shows the text again
        titleText.text = updatedTextDisplay;
        titleText.gameObject.SetActive(true);

        // ensures size is correct
        rectTransform.sizeDelta = targetSize;
        currentSize = targetSize;
        currentlyResizing = false;
    }
    
    public void StartHoverAnimation(Vector3 hoverDestination)
    {
        if ((hoverDestination == defaultLeafletPosition) || (hoverDestination == hoverLeafletPosition))
        {
            StopAllCoroutines();
            StartCoroutine(MoveLeafletToDestination(hoverDestination));
        }
    }
    
    IEnumerator MoveLeafletToDestination(Vector3 destination)
    {
        float elapsedTime = 0.0f;
        Vector3 target = destination == defaultLeafletPosition ? defaultLeafletPosition : hoverLeafletPosition;
        Vector3 curentPosition = target == defaultLeafletPosition ? hoverLeafletPosition : defaultLeafletPosition;

        while (elapsedTime < timeToResize)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/timeToResize);
            
            rectTransform.position = Vector3.Lerp(curentPosition, target, t);
            
            yield return null;
        }
        
        rectTransform.position = target;
    }
    
    public void ReturnToDock()
     {
         goingBackToDock = false;
         StopAllCoroutines();
         StartCoroutine(MoveLeafletToDock());
     }

    IEnumerator MoveLeafletToDock()
    {
        // In case shrink coroutine was not completed before mouse up
        if (currentSize != smallestSize || titleText.text != leafletTitleText || !titleText.gameObject.activeInHierarchy)
        {
            Debug.Log("Previous coroutine incomplete. Setting values before continuing.");
            rectTransform.sizeDelta = smallestSize;
            currentSize = smallestSize;
            titleText.text = leafletTitleText;
            titleText.gameObject.SetActive(true);
            currentlyResizing = false;
        }
        
        float elapsedTime = 0.0f;
        var heldPosition = currentLeafletPosition;
        var targetPosition = defaultLeafletPosition;
                
        while (elapsedTime < timeToResize)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/timeToResize);
            
            rectTransform.position = Vector2.Lerp(heldPosition, targetPosition, t);
                
            yield return null;
        }
        
        isDocked = true;
        goingBackToDock = false;
        
        // Ensures locators are set to correct position at end of coroutine
        rectTransform.position = targetPosition;
        leafletRectBounds.center = targetPosition;
        currentSize = smallestSize;
    }
}
