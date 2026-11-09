using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public RectTransform rectTransform;
        private Vector2 delta;

        private bool isGrimoire;
        private bool isdeskObject;

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            isGrimoire = false;
            isdeskObject = false;
            
            
            if (this.gameObject.CompareTag("Grimoire"))
            {
                isGrimoire = true;
            }

            if (this.gameObject.CompareTag("DeskObject"))
            {
                isdeskObject = true;
            }
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (isGrimoire || isdeskObject)
            {
                AudioClip audioToPlay = isGrimoire ? SoundManager.Instance.sfxDraggingGrimoire : SoundManager.Instance.sfxDraggingForms;
                SoundManager.Instance.PlayAudio(audioToPlay);
            }
            
            delta = eventData.pressPosition - (Vector2)transform.position;
            rectTransform.SetAsLastSibling();
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            var x = eventData.position.x / Screen.width;
            var y = eventData.position.y / Screen.height;
            //try to prevent dragging the element offscreen
            if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;
        
            transform.position = eventData.position - delta;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            if (isGrimoire || isdeskObject)
            {
                AudioClip audioToPlay = isGrimoire ? SoundManager.Instance.sfxDroppingGrimoire : SoundManager.Instance.sfxDroppingForms;
                SoundManager.Instance.PlayAudio(audioToPlay);
            }
        }

    }