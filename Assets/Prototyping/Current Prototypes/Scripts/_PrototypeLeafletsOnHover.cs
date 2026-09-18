using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class _PrototypeLeafletsOnHover : MouseHover, IBeginDragHandler, IDragHandler, IEndDragHandler//, IDropHandler// , Draggable
{
    // try add on click handler and put resize coroutine on click
    // OOOUH -- OR!!!
    // have them come out horizontally instead of upwards
    public Image leafletImage;
    public Color defaultLeafletColour;
    public Color hoverLeafletColour;
    
    private Vector3 defaultContainerPosition;
    public Vector3 defaultLeafletPosition;
    
        public RectTransform rectTransform;
        
        public Vector2 smallestSize = new Vector2(150f, 90f);
        public Vector2 largestSize = new Vector2(600f, 360f);
        public float timeToResize = 1f;
    
    private float boundingBoxMaxX;
    private float boundingBoxMaxY;
    private float boundingBoxMinX;
    private float boundingBoxMinY;
    
    [HideInInspector] public TMP_Text titleText;
    private string leafletDetailText;
    private string leafletTitleText;
    
    private Vector2 moveDelta;

    private bool docked;
    private bool initSetPosition;

    // vector for on mouse hover animation:
    // private Vector3 hoverLeafletPosition;
    
    public void GetInformation(Image incomingImage)
    {
        leafletImage = incomingImage;
        defaultLeafletColour = new Color (.8f, .8f, .8f);
        hoverLeafletColour = leafletImage.color;
        leafletImage.color = defaultLeafletColour;
        
        rectTransform = GetComponent<RectTransform>();
        titleText = GetComponentInChildren<TMP_Text>();
        leafletTitleText = titleText.text;

        string temporaryString =
            "This is just to prove the detail switching works.\n\nThis card has information about the following treatment category: ";
        leafletDetailText = (temporaryString + leafletTitleText).ToString();
        
        defaultLeafletPosition = rectTransform.position;
        var x = defaultLeafletPosition.x / Screen.width;
        var y = defaultLeafletPosition.y / Screen.height;
        Vector2 tempVector = new Vector2(x, y);
        defaultLeafletPosition = tempVector;
        Debug.Log(tempVector);
        Debug.Log((Vector2)transform.position);
        
        
        //delta = eventData.pressPosition - (Vector2)transform.position;
        // defaultContainerPosition = transform.position;
        // defaultLeafletPosition = rectTransform.gameObject.transform.position;
        docked = true;
        initSetPosition = false;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        // Begin raise animation
        leafletImage.color = hoverLeafletColour;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (docked)
        {
            // Begin lower animation
        }

        leafletImage.color = defaultLeafletColour;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!initSetPosition)
        {
            defaultLeafletPosition = (Vector2)transform.position;
            Debug.Log(defaultLeafletPosition);
        }
        // _PrototypeHerbGuideMockupController.
        moveDelta = eventData.pressPosition - (Vector2)transform.position;

        if (docked)
        {
            docked = false;
            // titleText.gameObject.SetActive(false);
            StartResize(largestSize);
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
        // if in the bounding box, shrink back to default size and return to position


        /*var x = eventData.position.x / Screen.width;
        var y = eventData.position.y / Screen.height;
        
        /*var dist = boundingBoxMaxX-boundingBoxMinX;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dist);
        dist = boundingBoxMaxY-boundingBoxMinY;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dist);
        /
        
        // if inside of bounding box, make small and return to position
        if (x >= boundingBoxMinX && x <= boundingBoxMaxX && y >= boundingBoxMinY && y <= boundingBoxMaxY)
        {
            // Debug.Log("Inside boundingBox");
            StartResize(smallestSize);
            HideInformation();
        }
        /*else
        {
            DisplayInformation();
        }*/
            // var x = eventData.position.x / Screen.width;
            // var y = eventData.position.y / Screen.height;
            // selfRectTransform.anchoredPosition = new Vector2(x, y);
    }

    /*public void OnDrop(PointerEventData eventData)
    {
        StartResize(smallestSize);
        HideInformation();
    }*/

    public void SetBoundingBox(float minX, float minY, float maxX, float maxY)
    {
        // would make more sense to just get these directly from the main controller script
        boundingBoxMinX = minX;
        boundingBoxMinY = minY;
        boundingBoxMaxX = maxX;
        boundingBoxMaxY = maxY;
    }

    /*void DisplayInformation()
    {
        // if (size != maxSize) make big
        // show information
        Debug.Log("info gets displayed here");
    }*/

    public void HideInformation()
    {
        // hide information
        // titleText.gameObject.SetActive(false);
        docked = true;
    }

    // Stops other coroutines and begins resize
    public void StartResize(Vector2 sizeToChangeTo)
    {
        StopAllCoroutines();
        StartCoroutine(ResizeLeaflet(sizeToChangeTo));
    }

    IEnumerator ResizeLeaflet(Vector2 sizeToChangeTo)
    {
        float elapsedTime = 0.0f;
        Vector2 targetSize = Vector2.zero;
        Vector2 currentSize = Vector2.zero;
        
        // hides the text so that it can change 
        titleText.gameObject.SetActive(false);
        
        if (sizeToChangeTo == largestSize)
        {
            targetSize = largestSize;
            currentSize = smallestSize;
            titleText.text = leafletDetailText;
        }
        else
        {
            targetSize = smallestSize;
            currentSize = largestSize;
            titleText.text = leafletTitleText;
        }

        while (elapsedTime < timeToResize)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/timeToResize);
            
            rectTransform.sizeDelta = Vector2.Lerp(currentSize, targetSize, t);
            /*if (shrinking)
            {
                transform.position = Vector2.Lerp(heldPosition, defaultLeafletPosition, t);
            }*/
            // titleText.GetCompo
            
            yield return null;
        }

        if (sizeToChangeTo == largestSize)
        {
            titleText.GetComponent<RectTransform>().sizeDelta = new Vector2(targetSize.x - 200f, targetSize.y - 20f); //- (20f, ;
        }
        else
        {
            titleText.GetComponent<RectTransform>().sizeDelta = targetSize;
        }
        
        titleText.gameObject.SetActive(true);
        rectTransform.sizeDelta = targetSize;
    }
    /*
    IEnumerator GrowLeaflet()
    {
        // makes bigger
        float elapsedTime = 0.0f;

        while (elapsedTime < timeToResize)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/timeToResize);
            
            rectTransform.sizeDelta = Vector2.Lerp(smallestSize, largestSize, t);
            
            yield return null;
        }
        
        rectTransform.sizeDelta = largestSize;
    }
    
    IEnumerator ShrinkLeaflet()
    {
        // makes bigger
        float elapsedTime = 0.0f;

        while (elapsedTime < timeToResize)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/timeToResize);
            
            rectTransform.sizeDelta = Vector2.Lerp(largestSize, smallestSize, t);
            
            yield return null;
        }
        
        rectTransform.sizeDelta = smallestSize;
    }*/

    public void StartHoverAnimation(Vector2 targetDestination)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToTarget(targetDestination));
    }

    IEnumerator MoveToTarget(Vector2 targetDestination)
    {
        //
    }
}
