using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IDropHandler
{
    [HideInInspector]
    public List<InventorySlot> inventorySlots;

    public bool draggingFromInventory;
    
    private static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        //if (_instance = null)
        //{
            _instance = this;

            //canvas = GetComponentInParent<Canvas>();
            //uiScale = canvas.scaleFactor;
        //}
    }

    public void InitialiseInventorySlots()
    {
        SetupInventorySlotsList();
        foreach (InventorySlot i in inventorySlots)
        {
            i.SetupImages();
        }
    }
    
    public void ResetInventory()
    {
        draggingFromInventory = false;
        foreach (InventorySlot i in inventorySlots)
        {
            i.ResetInventorySlot();
        }
    }
    
    void SetupInventorySlotsList()
    {
        inventorySlots = new List<InventorySlot>();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<InventorySlot>(out InventorySlot temp))
            {
                if (temp != null)
                 {
                     inventorySlots.Add(temp);
                 }
            }
        }
        // Debug.Log("Reset complete. Inventory has found " + inventorySlots.Count + " slots.");
    }

    void GetImageAndUpdateInventory(DraggableHerbs draggedHerb)
    {
        Image iconToUpdate = draggedHerb.cuttingImage;
        Sprite spriteToUpdate = iconToUpdate.sprite;

        CheckForDuplicates(spriteToUpdate, draggedHerb);
    }

    void CheckForDuplicates(Sprite spriteToCheck, DraggableHerbs draggedHerb)
    {
        bool duplicateFound = false;
        foreach (InventorySlot i in inventorySlots)
        {
            if (i.inventorySlotImage.sprite == spriteToCheck)
            {
                duplicateFound = true;
                MenuManager.Instance.HerbWallDuplicatePopup();
                break;
            }
            else
            {
                continue;
            }
        }

        if (!duplicateFound)
        {
            SearchAndUpdate(spriteToCheck, draggedHerb);
        }
    }

    void SearchAndUpdate(Sprite spriteToUpdate, DraggableHerbs draggedHerb)
    {
        foreach (InventorySlot i in inventorySlots)
        {
            if (i.isEmpty)
            {
                i.UpdateIcon(spriteToUpdate);
                i.UpdateContents(draggedHerb.herbType);
                return;
            }
            else
            {
                if (i.inventorySlotImage.sprite == spriteToUpdate)
                {
                    MenuManager.Instance.HerbWallDuplicatePopup();
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!draggingFromInventory)
        {
            if(eventData.pointerDrag != null)
             {
                 DraggableHerbs temp = eventData.pointerDrag.GetComponent<DraggableHerbs>();
                 GetImageAndUpdateInventory(temp);
             }
        }
    }
}
