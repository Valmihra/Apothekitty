using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryBin : MonoBehaviour, IDropHandler
{
    /*
     
            TESTING DRAGGABLE INTERACTION INSTEAD!!
     
    private Button binButton;
    public bool canDelete;

    //public Button 
    // Start is called before the first frame update
    void Start()
    {
        binButton = GetComponent<Button>();
        canDelete = false;
        binButton.onClick.AddListener(delegate { ActivateBin(); });
    }

    // The player will now remove the next herb they click on from their inventory.
    void ActivateBin()
    {
        canDelete = true;
    }
    */
    
    // public GameObject parentObject;

    // private int timesTossed;
    // private int numToTossOnDesk = 3;


    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            // Debug.Log("drop is working");
            if (eventData.pointerDrag.TryGetComponent<InventorySlot>(out InventorySlot invSlot))
            {
                invSlot.RemoveHerb();
            }
        }
    }
}
