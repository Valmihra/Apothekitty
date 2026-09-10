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
            //Debug.Log ("yarh");
            _instance = this;

            //canvas = GetComponentInParent<Canvas>();
            //uiScale = canvas.scaleFactor;
        //}
    }

    void Start()
    {
        //SetupList();
    }

    void SetupList()
    {
        inventorySlots = new List<InventorySlot>();
        // Debug.Log("Inventory is resetting, currently contains " + inventorySlots.Count + " slots.");
        foreach (Transform child in transform)
        {
            InventorySlot temp = child.GetComponent<InventorySlot>();       // switch to trygetcomponent?
            if (temp != null)
            {
                inventorySlots.Add(temp);
            }
        }
        // Debug.Log("Reset complete. Inventory has found " + inventorySlots.Count + " slots.");
    }

    public void ResetInventory()
    {
        SetupList();
        foreach (InventorySlot i in inventorySlots)
        {
            i.ResetInventorySlot();
        }
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

        CheckForDuplicates(spriteToUpdate, draggedHerb);
        // SearchAndUpdate(spriteToUpdate, draggedHerb);
        //UpdateInventory(iconToUpdate);
    }

    void CheckForDuplicates(Sprite spriteToCheck, DraggableHerbs draggedHerb)
    {
        bool duplicateFound = false;
        foreach (InventorySlot i in inventorySlots)
        {
            if (i.inventorySlot.sprite == spriteToCheck)
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
                // Debug.Log("Updating " + i.gameObject.name + " with " + spriteToUpdate.name);

                i.UpdateIcon(spriteToUpdate);
                i.UpdateContents(draggedHerb.herbType);
                return;
            }
            else
            {
                if (i.inventorySlot.sprite == spriteToUpdate)
                {
                    MenuManager.Instance.HerbWallDuplicatePopup();
                }
                else
                {
                    continue;
                }
            }
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
