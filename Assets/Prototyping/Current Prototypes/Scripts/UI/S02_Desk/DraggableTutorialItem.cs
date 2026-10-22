using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableTutorialItem : Draggable
{
     private Vector2 editedDelta;
     
     private Vector2 largestItemSize;
     private Vector2 smallestItemSize;
     private Vector2 currentItemSize;
     
     private Rect tutorialBinRectBounds;
     private Rect selfRectBounds;
     private RectTransform itemRectTransform;
     
     private float itemTimer = 0.05f;
     
     private bool resizing;
     private bool overBin;
     private bool itemIsRaycastTarget;

     public void InitialiseTutorialItem(Rect rectToAssign)
     {
         itemRectTransform = GetComponent<RectTransform>();
         
         this.gameObject.SetActive(false);
         
         CreateTutorialItemRect();
         tutorialBinRectBounds = rectToAssign;
     }
     
     private void CreateTutorialItemRect()
     {
         float offset = 50f;

         selfRectBounds = new Rect(transform.position.x, transform.position.y, (itemRectTransform.rect.width + offset), (itemRectTransform.rect.height + offset));
         
         float width = selfRectBounds.width;
         float height = selfRectBounds.height;
         //Debug.Log("Self rect bounds is: " + width + " wide, and " + height + " high.");
         
         largestItemSize = new Vector2(width, height);
         smallestItemSize = new Vector2((width / 2), (height / 2));
         
         currentItemSize = largestItemSize;
     }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxTutorialPageGrab);
        
        // *TAG* - PROTOTYPE CURSOR EVENT
        // _PrototypeCursor.Instance.BeginLongClick();
        
        editedDelta = eventData.pressPosition - (Vector2)transform.position;
        itemRectTransform.SetAsLastSibling();
        
        itemIsRaycastTarget = false;
        SetTutorialItemRaycasts();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        var x = eventData.position.x / Screen.width;
        var y = eventData.position.y / Screen.height;
        //try to prevent dragging the element offscreen
        if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;
        
        // Updates the position of the item and its associated rect
        transform.position = eventData.position - editedDelta;
        selfRectBounds.center = transform.position;
        
        CheckTutorialItemPosition();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        // *TAG* - PROTOTYPE CURSOR EVENT
        // _PrototypeCursor.Instance.EndLongClick();
        
        CheckTutorialItemPosition();
        
        if (overBin)
        {
            Invoke(nameof(TutorialItemFailsafeDestroy), itemTimer);
        }
        else
        {
            itemIsRaycastTarget = true;
            Invoke(nameof(SetTutorialItemRaycasts), itemTimer);
        }
        
        
    }

    void CheckTutorialItemPosition()
    {
        overBin = selfRectBounds.Overlaps(tutorialBinRectBounds);
        
        // If overlapping the bin, checks if ready to delete or not
        if (overBin)
        {
            if (currentItemSize != smallestItemSize && !resizing)
            {
                resizing = true;
                ResizeTutorialItem();
            }
        }
        else
        {
            if (currentItemSize != largestItemSize && !resizing)
            {
                resizing = true;
                ResizeTutorialItem();
            }
        }
    }
    
    private void TutorialItemFailsafeDestroy()
    {
        // TECHNICALLY don't even need the bin script if I'm using this right,,, :S welp :S
        TutorialItemController.Instance.DestroyTutorialItem(this);
    }
    
    private void SetTutorialItemRaycasts()
    {
        gameObject.GetComponent<Image>().raycastTarget = itemIsRaycastTarget;
    }


    private void ResizeTutorialItem()
    {
        StopAllCoroutines();
        StartCoroutine(TutorialItemResizer());
    }

    IEnumerator TutorialItemResizer()
    {
        float elapsedTime = 0.0f;
        Vector2 targetItemSize = currentItemSize == largestItemSize ? smallestItemSize : largestItemSize;

        while (elapsedTime < itemTimer)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/itemTimer);
         
            itemRectTransform.sizeDelta = Vector2.Lerp(currentItemSize, targetItemSize, t);
         
            yield return null;
        }

        // Ensures size is correct at end of coroutine and resets the resize bool
        itemRectTransform.sizeDelta = targetItemSize;
        currentItemSize = targetItemSize;
        resizing = false;
    }
}
