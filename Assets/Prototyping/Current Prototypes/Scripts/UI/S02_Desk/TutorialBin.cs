using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialBin : MonoBehaviour, IDropHandler
{
    public GameObject parentObject;

    private int timesTossed;
    private int numToTossOnDesk = 3;

    public Rect CreateRectBounds()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        return new Rect(transform.position.x, transform.position.y, rectTransform.rect.width, rectTransform.rect.height);
    }

    public void TutorialBinTossItem(DraggableTutorialItem itemToToss)
    {
        // Destroys the game object and increments the number of tossed objects
        Destroy(itemToToss.gameObject);
        timesTossed++;
        
        // If at the set number of items to toss, player can advance to next tutorial section
        if (timesTossed == numToTossOnDesk)
        {
            DialogueRunner.Instance.deskIsClean = true;
            DialogueRunner.Instance.GetDialogue("desk two");
                    
            Destroy(parentObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent<DraggableTutorialItem>(out DraggableTutorialItem draggableTutorialItem))
            {
                TutorialBinTossItem(draggableTutorialItem);
            }
        }
    }
}
