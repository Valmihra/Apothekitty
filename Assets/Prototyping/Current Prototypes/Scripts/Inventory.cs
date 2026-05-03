using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IDropHandler
{
    private float uiScale;
    private Canvas canvas;

    private List<InventorySlot> inventorySlots;
    
    /*private static Inventory reference;
    public static Inventory Reference
    {
        get
        {
            return reference;
        }
    }*/

    void Awake()
    {
        //if (reference = null)
        //{
            //Debug.Log ("yarh");
            //reference = this;

            canvas = GetComponentInParent<Canvas>();
            uiScale = canvas.scaleFactor;
        //}
    }

    void Start()
    {
        SetupList();
    }

    void SetupList()
    {
        inventorySlots = new List<InventorySlot>();
        foreach (Transform child in transform)
        {
            InventorySlot temp = child.GetComponent<InventorySlot>();
            if (temp != null)
            {
                inventorySlots.Add(temp);
            }
        }
        //Debug.Log("There are currently " + inventorySlots.Count + "inventory slots.");
    }

    void GetImage(DraggableHerbs draggedHerb)
    {
        Image iconToUpdate = draggedHerb.cuttingImage;
        UpdateInventory(iconToUpdate);
    }
    
    void UpdateInventory(Image image)
    {
        Sprite spriteToUpdate = image.sprite;
        SearchAndUpdate(spriteToUpdate);
    }

    void SearchAndUpdate(Sprite spriteToUpdate)
    {
        foreach (InventorySlot i in inventorySlots)
        {
            if (i.isEmpty)
            {
                Debug.Log("Updating " + i.gameObject.name + " with " + spriteToUpdate.name);
                i.UpdateIcon(spriteToUpdate);
                return;
            }
            else
            continue;
        }

        // UPDATE CURRENT INVENTORY CONTENTS HERE
            // NEEDS LIST OF HERBS TO REFERENCE AND COMPARE NAMES WITH ICON
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            Debug.Log("This drop works!");
            //Image temp = eventData.pointerDrag.GetComponent<Image>();
            DraggableHerbs temp = eventData.pointerDrag.GetComponent<DraggableHerbs>();
            GetImage(temp);
        }
    }
}
