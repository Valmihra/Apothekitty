using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : Draggable//MonoBehaviour 
{
    [HideInInspector]
    public string slotContents;
    public bool isEmpty;
    public Image inventorySlotImage;
    public Image inventorySlotEmpty;
    
    private Vector2 dragDelta;
    private Transform herbSlot;
    private GameObject herbDraggedFromInventory;
    
    
    public void SetupImages()
    {
        // finds the correct object to assign the herb images to
        Transform parentSlot = transform.Find("Empty Slot");
        herbSlot = parentSlot.transform.Find("Selected Herb");
        inventorySlotImage = herbSlot.GetComponent<Image>();
        
        // makes a transparent sprite for the starting inventory image, based on a preexisting sprite in the scene
        GameObject temporaryObject = GameObject.Find("Empty Inventory Icon");
        inventorySlotEmpty = temporaryObject.GetComponent<Image>();
        
        // finds and assigns the shared draggable display object
        Transform tempTransform = transform.parent.transform.Find("Panel - Herb Wall - Dragged Herb Display");
        herbDraggedFromInventory = tempTransform.gameObject;
        if (herbDraggedFromInventory.activeInHierarchy)
        {
            herbDraggedFromInventory.SetActive(false);
        }
    }

    public void ResetInventorySlot()
    {
        RemoveHerb();
    }

    // changes the image display to the sprite specified
    public void UpdateIcon(Sprite sprite)
    {
        UIManager.Instance.SpriteShift(inventorySlotImage, sprite);
        isEmpty = false;
    }

    public void UpdateContents(string contents)
    {
        if (contents == "emptyInventorySlot")
        {
            slotContents = null;
        }
        else
        {
            slotContents = contents;
            // Debug.Log("Inventory slot holding " + slotContents + ".");
        }
    }

    // removes the herb icon and becomes empty again
    public void RemoveHerb()
    {
        UIManager.Instance.SpriteShift(inventorySlotImage, inventorySlotEmpty.sprite);
        UpdateContents("emptyInventorySlot");
        isEmpty = true;
    }
    
    
    public override void OnBeginDrag(PointerEventData eventData)
    {
        Inventory.Instance.draggingFromInventory = true;
        dragDelta = eventData.pressPosition - (Vector2)transform.position;
        
        herbDraggedFromInventory.transform.position = herbSlot.position;
        UIManager.Instance.SpriteShift(herbDraggedFromInventory.GetComponent<Image>(), inventorySlotImage.sprite);
        
        herbSlot.gameObject.SetActive(false);
        herbDraggedFromInventory.SetActive(true);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        var x = eventData.position.x / Screen.width;
        var y = eventData.position.y / Screen.height;
            //try to prevent dragging the element offscreen
            if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;

        // make the herb icon follow cursor
        herbSlot.transform.position = eventData.position - dragDelta;
        herbDraggedFromInventory.transform.position = eventData.position - dragDelta;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        Inventory.Instance.draggingFromInventory = false;
        
        herbSlot.gameObject.SetActive(true);
        herbDraggedFromInventory.SetActive(false);
    }
}
