using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableTutorialItem : Draggable
{
     private Vector2 editedDelta;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        editedDelta = eventData.pressPosition - (Vector2)transform.position;
        rectTransform.SetAsLastSibling();
        gameObject.GetComponent<Image>().raycastTarget = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        var x = eventData.position.x / Screen.width;
        var y = eventData.position.y / Screen.height;
        //try to prevent dragging the element offscreen
        if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;
    
        transform.position = eventData.position - editedDelta;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        Invoke(nameof(Reset), 0.5f);
    }

    void Reset()
    {
        // Makes the object accept raycasts again
        gameObject.GetComponent<Image>().raycastTarget = true;
    }
}
