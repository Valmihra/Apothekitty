using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        private Vector2 delta;
    
        public void OnBeginDrag(PointerEventData eventData)
        {
            delta = eventData.pressPosition - (Vector2)transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var x = eventData.position.x / Screen.width;
            var y = eventData.position.y / Screen.height;
            //try to prevent dragging the element offscreen
            if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;
        
            transform.position = eventData.position - delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
        
        }
    }