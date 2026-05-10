using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryClickSensor : MonoBehaviour, IPointerClickHandler
{
    private InventoryBin binReference;
    private InventorySlot inventorySlot;

    void Awake ()
    {
        inventorySlot = GetComponent<InventorySlot>();
        FindBin();
        //var bin = 
        //binReference = 
    }

    void FindBin()
    {
        //var bin = 
        var parent = transform.parent;
        var bin = parent.transform.Find("Button - Herb Wall - Inventory Bin");

        if (bin.TryGetComponent<InventoryBin>(out InventoryBin foundBin))
        {
            binReference = foundBin;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (binReference.canDelete)
        {
            inventorySlot.RemoveHerb();

            binReference.canDelete = false;



            // still needs to update the contents of the inventory?
        }
    }
}