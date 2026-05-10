using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IDropHandler
{
    //private float uiScale;
    //private Canvas canvas;
    [HideInInspector]
    public List<InventorySlot> inventorySlots;
    
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

            //canvas = GetComponentInParent<Canvas>();
            //uiScale = canvas.scaleFactor;
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
            InventorySlot temp = child.GetComponent<InventorySlot>();       // switch to trygetcomponent?
            if (temp != null)
            {
                inventorySlots.Add(temp);
            }
        }
        //Debug.Log("There are currently " + inventorySlots.Count + "inventory slots.");
    }

    /*void GetImage(DraggableHerbs draggedHerb)
    {
        Image iconToUpdate = draggedHerb.cuttingImage;
        UpdateInventory(iconToUpdate);
    }
    
    void UpdateInventory(Image image)
    {
        Sprite spriteToUpdate = image.sprite;
        SearchAndUpdate(spriteToUpdate);
    }*/

    void GetImageAndUpdateInventory(DraggableHerbs draggedHerb)
    {
        Image iconToUpdate = draggedHerb.cuttingImage;
        Sprite spriteToUpdate = iconToUpdate.sprite;

        SearchAndUpdate(spriteToUpdate, draggedHerb);
        //UpdateInventory(iconToUpdate);
    }
    

    /*void SearchAndUpdate(Sprite spriteToUpdate)
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
    }*/

    void SearchAndUpdate(Sprite spriteToUpdate, DraggableHerbs draggedHerb)
    {
        foreach (InventorySlot i in inventorySlots)
        {
            if (i.isEmpty)
            {
                //Debug.Log("Slot located at " + inventorySlots[i])
                Debug.Log("Updating " + i.gameObject.name + " with " + spriteToUpdate.name);

                i.UpdateIcon(spriteToUpdate);
                i.UpdateContents(draggedHerb.herbType);
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
            //Debug.Log("This drop works!");
            //Image temp = eventData.pointerDrag.GetComponent<Image>();
            DraggableHerbs temp = eventData.pointerDrag.GetComponent<DraggableHerbs>();
            //GetImage(temp);
            GetImageAndUpdateInventory(temp);
        }
    }

    /*public List<string> CheckContents()
    {
        List<string> contentsList = new List<string>();

        foreach (InventorySlot i in inventorySlots)
        {
            if (!i.isEmpty)
            {
                Debug.Log(i.slotContents);
                contentsList.Add(i.slotContents);
                // would be read by the results checker
            }

            //if (contentsList.Count > 0)
            //{
                return new List<string> (contentsList);
            //}
            //else
            //return new List<string>
        }
    }*/

    //public void AddToInventory()
}
