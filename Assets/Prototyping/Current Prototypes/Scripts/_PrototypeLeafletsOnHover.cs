using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class _PrototypeLeafletsOnHover : MouseHover, IBeginDragHandler, IDragHandler, IEndDragHandler//, IPointerClickHandler//, IDropHandler// , Draggable
{
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
        
    private Vector2 smallestSize = new Vector2(150f, 90f);
    private Vector2 largestSize = new Vector2(600f, 360f);
    private Vector2 currentSize;
    private float timeToResize = 0.1f;
    
    private Vector2 moveDelta;
    public RectTransform rectTransform;
    
    private float boundingBoxMaxX;
    private float boundingBoxMaxY;
    private float boundingBoxMinX;
    private float boundingBoxMinY;
    
    private bool isDocked;
    // private bool justActivated;
    private bool defaultPositionsSet;
    
    public void InitialisePrototypeLeaflet()
    {
        rectTransform = GetComponent<RectTransform>();
        leafletImage = GetComponent<Image>();
            defaultLeafletColour = new Color (.65f, .65f, .65f);
            hoverLeafletColour = leafletImage.color;
            leafletImage.color = defaultLeafletColour;
        
        titleText = GetComponentInChildren<TMP_Text>();
            leafletTitleText = titleText.text;
            leafletDetailText = (tempLeafletPrefaceText + "<b>" + leafletTitleText + "</b>").ToString();
        
        currentSize = smallestSize;
        
        isDocked = true;
        // justActivated = false;
        defaultPositionsSet = false;
    }
    
    private Vector3 GetLeafletPosition()
    {
        Vector3 leafletPositionToSet = transform.position;
        return leafletPositionToSet;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        leafletImage.color = hoverLeafletColour;
        
        if (isDocked)
        {
            if (!defaultPositionsSet)
            {
                // sets the default positions for the leaflet and its hover destination
                defaultLeafletPosition = GetLeafletPosition();
                hoverLeafletPosition = new Vector3 (defaultLeafletPosition.x, defaultLeafletPosition.y + 30.0f,  defaultLeafletPosition.z);
                defaultPositionsSet = true;
            }
            
            // Begin raise animation here
            StartHoverAnimation(hoverLeafletPosition);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        leafletImage.color = defaultLeafletColour;
        
        if (isDocked)
        {
            // Begin lower animation here
            StartHoverAnimation(defaultLeafletPosition);
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        moveDelta = eventData.pressPosition - (Vector2)transform.position;
        
        // if in the default location, resizes to large
        if (isDocked)
        {
            isDocked = false;
            // justActivated = true;
            StartResize();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        var x = eventData.position.x / Screen.width;
        var y = eventData.position.y / Screen.height;
        //try to prevent dragging the element offscreen
        if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;

        transform.position = eventData.position - moveDelta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Get the location of the leaflet at the end of the drag
        currentLeafletPosition = GetLeafletPosition();
        
        // If inside the bounding box, resize and return to dock
        if (currentLeafletPosition.x >= boundingBoxMinX && currentLeafletPosition.x <= boundingBoxMaxX && currentLeafletPosition.y >= boundingBoxMinY && currentLeafletPosition.y <= boundingBoxMaxY)
        {
            StartResize();
        }
        
        // justActivated = false;
    }

    public void SetBoundingBox(float minX, float minY, float maxX, float maxY)
    {
        // would make more sense to just get these directly from the main controller script
        boundingBoxMinX = minX;
        boundingBoxMinY = minY;
        boundingBoxMaxX = maxX;
        boundingBoxMaxY = maxY;
        
        Debug.Log("Bounding box set. x axis coordinates are: " + boundingBoxMinX + " - " + boundingBoxMaxX + ". y axis coordinates are: " + boundingBoxMinY + " - " + boundingBoxMaxY + ".");
    }

    
    public void StartResize()
    {
        StopAllCoroutines();
        StartCoroutine(ResizeLeaflet());
    }

    public void StartHoverAnimation(Vector3 hoverDestination)
    {
        if ((hoverDestination == defaultLeafletPosition) || (hoverDestination == hoverLeafletPosition))
        {
            StopAllCoroutines();
            StartCoroutine(MoveLeafletToDestination(hoverDestination));
        }
    }
    
    IEnumerator ResizeLeaflet()
    {
        float elapsedTime = 0.0f;
        titleText.gameObject.SetActive(false);
        
        // sets the target size and text according to the current leaflet display
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
            
            titleText.GetComponent<RectTransform>().sizeDelta = new Vector2(targetSize.x - 200f, targetSize.y - 20f);
        }
        else
        {
            var heldPosition = currentLeafletPosition;
            var targetPosition = defaultLeafletPosition;
            
            while (elapsedTime < timeToResize)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime/timeToResize);
            
                rectTransform.sizeDelta = Vector2.Lerp(currentSize, targetSize, t);
                rectTransform.position = Vector2.Lerp(heldPosition, targetPosition, t);
                
                yield return null;
            }
            
            titleText.GetComponent<RectTransform>().sizeDelta = targetSize;
            isDocked = true;
        }
        
        // updates and shows the text again
        titleText.text = updatedTextDisplay;
        titleText.gameObject.SetActive(true);
        
        // ensures size is correct, and updates the currentSize vector to match
        rectTransform.sizeDelta = targetSize;
        currentSize = targetSize;
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
}
