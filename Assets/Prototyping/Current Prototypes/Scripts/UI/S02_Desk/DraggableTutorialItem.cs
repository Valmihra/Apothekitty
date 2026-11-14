using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableTutorialItem : Draggable
{
     private Vector2 editedDelta;
     
     private Vector2 draggableTutorialItemLargestSize;
     private Vector2 draggableTutorialItemSmallestSize;
     private Vector2 draggableTutorialItemCurrentSize;
     
     private Rect tutorialBinRectBounds;
     private Rect draggableTutorialItemRectBounds;
     private RectTransform draggableTutorialItemRectTransform;
     
     private float itemTimer = 0.05f;
     
     private bool resizingDraggableTutorialItem;
     private bool tutorialItemOverBin;
     private bool tutorialItemIsRaycastTarget;

     public void InitialiseTutorialItem(Rect rectToAssign)
     {
         draggableTutorialItemRectTransform = GetComponent<RectTransform>();
         
         this.gameObject.SetActive(false);
         
         CreateTutorialItemRect();
         tutorialBinRectBounds = rectToAssign;
     }
     
     private void CreateTutorialItemRect()
     {
         float offset = 50f;

         draggableTutorialItemRectBounds = new Rect(transform.position.x, transform.position.y, (draggableTutorialItemRectTransform.rect.width + offset), (draggableTutorialItemRectTransform.rect.height + offset));
         
         float width = draggableTutorialItemRectBounds.width;
         float height = draggableTutorialItemRectBounds.height;
         
         draggableTutorialItemLargestSize = new Vector2(width, height);
         draggableTutorialItemSmallestSize = new Vector2((width / 2), (height / 2));
         
         draggableTutorialItemCurrentSize = draggableTutorialItemLargestSize;
     }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxTutorialPageGrab);
        
        editedDelta = eventData.pressPosition - (Vector2)transform.position;
        draggableTutorialItemRectTransform.SetAsLastSibling();
        
        tutorialItemIsRaycastTarget = false;
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
        draggableTutorialItemRectBounds.center = transform.position;
        
        CheckTutorialItemPosition();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        CheckTutorialItemPosition();
        
        if (tutorialItemOverBin)
        {
            Invoke(nameof(TutorialItemFailsafeDestroy), itemTimer);
        }
        else
        {
            tutorialItemIsRaycastTarget = true;
            Invoke(nameof(SetTutorialItemRaycasts), itemTimer);
        }
        
        
    }

    void CheckTutorialItemPosition()
    {
        tutorialItemOverBin = draggableTutorialItemRectBounds.Overlaps(tutorialBinRectBounds);
        
        // If overlapping the bin, checks if ready to delete or not
        if (tutorialItemOverBin)
        {
            if (draggableTutorialItemCurrentSize != draggableTutorialItemSmallestSize && !resizingDraggableTutorialItem)
            {
                resizingDraggableTutorialItem = true;
                ResizeTutorialItem();
            }
        }
        else
        {
            if (draggableTutorialItemCurrentSize != draggableTutorialItemLargestSize && !resizingDraggableTutorialItem)
            {
                resizingDraggableTutorialItem = true;
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
        gameObject.GetComponent<Image>().raycastTarget = tutorialItemIsRaycastTarget;
    }


    private void ResizeTutorialItem()
    {
        StopAllCoroutines();
        StartCoroutine(TutorialItemResizer());
    }

    IEnumerator TutorialItemResizer()
    {
        float elapsedTime = 0.0f;
        Vector2 targetItemSize = draggableTutorialItemCurrentSize == draggableTutorialItemLargestSize ? draggableTutorialItemSmallestSize : draggableTutorialItemLargestSize;

        while (elapsedTime < itemTimer)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/itemTimer);
         
            draggableTutorialItemRectTransform.sizeDelta = Vector2.Lerp(draggableTutorialItemCurrentSize, targetItemSize, t);
         
            yield return null;
        }

        // Ensures size is correct at end of coroutine and resets the resize bool
        draggableTutorialItemRectTransform.sizeDelta = targetItemSize;
        draggableTutorialItemCurrentSize = targetItemSize;
        resizingDraggableTutorialItem = false;
    }
}
