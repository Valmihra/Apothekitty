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


    void Awake()
    {
        SetupImages();
        //isEmpty = true;
        //slotContents = null;
    }

    public void ResetInventorySlot()
    {
        RemoveHerb();
        //SetupImages();  // ResetImages();
        //isEmpty = true;
        //slotContents = null;
    }

    /*void FindParents()
    {
        /
    }*/

    // assigns the correct 'empty' images
    void SetupImages()
    {
        Transform parentSlot = transform.Find("Empty Slot");
        Transform herbSlot = parentSlot.transform.Find("Selected Herb");

        inventorySlot = herbSlot.GetComponent<Image>();
        
        //Image temporaryImage = GetComponent<Image>();
        // makes a transparent sprite for the starting inventory image, based on a preexisting sprite in the scene
        GameObject temporaryObject = GameObject.Find("Empty Inventory Icon");
        inventorySlotEmpty = temporaryObject.GetComponent<Image>();

            // have instead made sprite transparent in unity
        // Color temporaryColour = new Color(1,1,1,0);
        // temporaryImage.color = temporaryColour;

        // assigns the blank sprite to the empty slot
        //inventorySlotEmpty.sprite = temporaryImage.sprite;

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
            // Debug.Log("Inventory slot holding " + slotContents + ".");
        }
    }

    // removes the herb icon and becomes empty again
    public void RemoveHerb()
    {
        inventorySlot.sprite = inventorySlotEmpty.sprite;
        // Debug.Log("Removing herb");
        UpdateContents("x");
        isEmpty = true;
    }
}
