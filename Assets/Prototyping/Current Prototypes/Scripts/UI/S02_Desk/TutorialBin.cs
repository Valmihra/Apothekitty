using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialBin : MonoBehaviour, IDropHandler
{
    public GameObject parentObject;
    // private RectTransform rectTransform;

    private int timesTossed;
    private int numToTossOnDesk = 3;
    
    // void Awake()
    // {
    //     rectTransform = GetComponent<RectTransform>();
    // }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent<DraggableTutorialItem>(out DraggableTutorialItem draggableTutorialItem))
            {
                Destroy(draggableTutorialItem.gameObject);
                timesTossed++;

                if (timesTossed == numToTossOnDesk)
                {
                    DialogueRunner.Instance.deskIsClean = true;
                    DialogueRunner.Instance.GetDialogue("desk two");
                    
                    Destroy(parentObject);
                }
            }
        }
    }
}
