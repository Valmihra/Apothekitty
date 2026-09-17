using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class _PrototypeLeafletsOnHover : MouseHover, IBeginDragHandler, IDragHandler, IEndDragHandler//, IDropHandler// , Draggable
{
    public Image leafletImage;
    public Color defaultLeafletColour;
    public Color hoverLeafletColour;
    
    private Vector3 defaultContainerPosition;
    private Vector3 defaultLeafletPosition;
    
        private RectTransform rectTransform;
        // private RectTransform selfRectTransform;
        
        // private Vector2 smallestSize = new Vector2(140f, 150f);
        // private Vector2 largestSize = new Vector2(560f, 600f);
        
        public Vector2 smallestSize = new Vector2(150f, 90f);
        private Vector2 largestSize = new Vector2(600f, 360f);
        public float timeToResize = 1f;
    
    private float boundingBoxMaxX;
    private float boundingBoxMaxY;
    private float boundingBoxMinX;
    private float boundingBoxMinY;
    
    private TMP_Text titleText;
    private string leafletDetailText;
    private string leafletTitleText;
    
    private Vector2 moveDelta;

    private bool docked;
    // private Vector3 hoverLeafletPosition;
    
    public void GetInformation(Image incomingImage)
    {
        leafletImage = incomingImage;
        defaultLeafletColour = new Color (.8f, .8f, .8f);
        hoverLeafletColour = leafletImage.color;
        leafletImage.color = defaultLeafletColour;
                
                // rectTransform = incomingImage.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        titleText = GetComponentInChildren<TMP_Text>();
        leafletTitleText = titleText.text;

        string temporaryString =
            "This is just to prove the detail switching works.\n\nThis card has information about the following treatment category: ";
        leafletDetailText = (temporaryString + leafletTitleText).ToString();
        
        // selfRectTransform = GetComponent<RectTransform>();
        // defaultContainerPosition = selfRectTransform.anchoredPosition;
        defaultLeafletPosition = rectTransform.anchoredPosition;
        // defaultContainerPosition = transform.position;
        // defaultLeafletPosition = rectTransform.gameObject.transform.position;
        docked = true;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        leafletImage.color = hoverLeafletColour;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        leafletImage.color = defaultLeafletColour;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // _PrototypeHerbGuideMockupController.
        moveDelta = eventData.pressPosition - (Vector2)transform.position;

        if (docked)
        {
            docked = false;
            // titleText.gameObject.SetActive(false);
            StartResize(largestSize);
        }
        
        /*Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        var minX = corners[0].x;
        var maxX = corners[2].x;
        var dist = maxX - minX;
        
        if (dist != largestSize.x)
        {
            titleText.gameObject.SetActive(false);
            StartResize(largestSize);
        }*/
        
        // rectTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var x = eventData.position.x / Screen.width;
        var y = eventData.position.y / Screen.height;
        //try to prevent dragging the element offscreen
        if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;

        transform.position = eventData.position - moveDelta;
        // selfRectTransform.anchoredPosition = eventData.position - moveDelta;
        // GetComponent<RectTransform>().anchoredPosition = eventData.position - moveDelta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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
        boundingBoxMinX = minX;
        boundingBoxMinY = minY;
        boundingBoxMaxX = maxX;
        boundingBoxMaxY = maxY;
        // Debug.Log(boundingBoxMaxX + " is max X in boundingBox");
    }

    void DisplayInformation()
    {
        // if (size != maxSize) make big
        // show information
        Debug.Log("info gets displayed here");
    }

    public void HideInformation()
    {
        // make small
        // hide information
        // Debug.Log("info gets hidden here");

        // selfRectTransform.anchoredPosition = defaultContainerPosition;
        rectTransform.anchoredPosition = defaultLeafletPosition;
        docked = true;
        
        // transform.position = defaultContainerPosition;
        // rectTransform.position = defaultLeafletPosition;
            //defaultLeafletPosition;
        
    }

    public void StartResize(Vector2 sizeToChangeTo)    // could probs just reference the declared size vector instead
    {
        StopAllCoroutines();
        StartCoroutine(ResizeLeaflet(sizeToChangeTo));
    }

    IEnumerator ResizeLeaflet(Vector2 sizeToChangeTo)
    {
        float elapsedTime = 0.0f;
        Vector2 targetSize = Vector2.zero;
        Vector2 currentSize = Vector2.zero;
        
        // bool shrinking = false;
        // var heldPosition = rectTransform.position;
        
        titleText.gameObject.SetActive(false);
        
        if (sizeToChangeTo == largestSize)
        {
            targetSize = largestSize;
            currentSize = smallestSize;
            titleText.text = leafletDetailText;
        }
        else
        {
            // shrinking = true;
            // HideInformation();
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

        /*if (sizeToChangeTo == largestSize)
        {
            DisplayInformation();
        }
        else
        {
            titleText.gameObject.SetActive(true);
        }*/
        titleText.GetComponent<RectTransform>().sizeDelta = targetSize;
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
}
