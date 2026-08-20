using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialBin : MonoBehaviour, IDropHandler
{
    public GameObject parentObject;
    private RectTransform rectTransform;
    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Debug.Log("reading");
    }


    public void OnDrop(PointerEventData eventData)
    {
       
       /* if(eventData.pointerDrag != null)
        {
            Debug.Log("This drop works!");
            //  Debug.Log("Drop Detected");
            //Image temp = eventData.pointerDrag.GetComponent<Image>();
            //DraggableHerbs temp = eventData.pointerDrag.GetComponent<DraggableHerbs>();
            //GetImage(temp);
            //GetImageAndUpdateInventory(temp);
        }*/
        
        if(eventData.pointerDrag != null)
        {
            Debug.Log("isn't null");

            if (eventData.pointerDrag.TryGetComponent<DraggableTutorialItem>(out DraggableTutorialItem draggableTutorialItem))
            {
                Debug.Log("check works");
                DialogueRunner.Instance.cleanDesk = true;
                Debug.Log(DialogueRunner.Instance.cleanDesk);
                DialogueRunner.Instance.GetDialogue("desk two");


                Destroy(draggableTutorialItem.gameObject);
                
                Destroy(parentObject);
            }
            
        }
    }
}
