using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool isEmpty;
    public Image inventorySlot;
    public Image inventorySlotEmpty;
    [HideInInspector]
    public string slotContents;


    void Start()
    {
        SetupImages();
        isEmpty = true;
        slotContents = null;
    }

    // assigns the correct 'empty' images
    void SetupImages()
    {
        Transform parentSlot = transform.Find("Empty Slot");
        Transform herbSlot = parentSlot.transform.Find("Selected Herb");

        inventorySlot = herbSlot.GetComponent<Image>();
        Image temporaryImage = GetComponent<Image>();
        Color temporaryColour = new Color(1,1,1,0);
        //temporaryColour.a = 0f;
        temporaryImage.color = temporaryColour;
        //temporaryImage.alpha = 0f;
        inventorySlotEmpty = temporaryImage;
        //inventorySlotEmpty = GetComponent<Image>();//inventorySlot;
        //Debug.Log("Image is set as " + inventorySlot.name);
    }

    // changes the image display to the sprite specified
    public void UpdateIcon(Sprite sprite)
    {
        inventorySlot.sprite = sprite;
        isEmpty = false;
    }

    public void UpdateContents(string contents)
    {
        if (contents == "x")
        {
            slotContents = null;
        }
        else
        {
            slotContents = contents;
            Debug.Log("Inventory slot holding " + slotContents + ".");
        }
    }

    // removes the herb icon and becomes empty again
    public void RemoveHerb()
    {
        inventorySlot.sprite = inventorySlotEmpty.sprite;
        Debug.Log("Removing herb");
        UpdateContents("x");
        isEmpty = true;
    }
}
