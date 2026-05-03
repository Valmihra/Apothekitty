using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool isEmpty;
    public Image inventorySlot;
    public Image inventorySlotEmpty;


    void Start()
    {
        SetupImages();
        isEmpty = true;    
    }

    void SetupImages()
    {
        Transform parentSlot = transform.Find("Empty Slot");
        Transform herbSlot = parentSlot.transform.Find("Selected Herb");

        inventorySlot = herbSlot.GetComponent<Image>();
        inventorySlotEmpty = inventorySlot;
        //Debug.Log("Image is set as " + inventorySlot.name);
    }

    public void UpdateIcon(Sprite sprite)
    {
        inventorySlot.sprite = sprite;
        isEmpty = false;
    }

    public void RemoveHerb()
    {
        inventorySlot.sprite = inventorySlotEmpty.sprite;
        isEmpty = true;
    }
}
