using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NoteSensor : MouseHover
{
    
    void Start()
    {
        //herbalistNote = 

        //RectTransform rectTransform = herbalistNoteCanvasGroup.GetComponent<RectTransform>();
        //noteWidth = rectTransform.width + notePadding;
        
    }

    void LocateSensorAndUpdateNote(GameObject hoverObject)
    {
        GameObject parentObject = hoverObject.transform.parent.gameObject;
        GameObject grandparentObject = parentObject.transform.parent.gameObject;

        if (grandparentObject.TryGetComponent<DrawerSensor>(out DrawerSensor drawerSensor))
        {
            // Sets the location for the note to show up
                //RectTransform drawerRect = grandparentObject.GetComponent<RectTransform>();
                //Vector2 rectPosition = drawerRect.anchoredPosition;
                //AllHerbsData.SetNoteLocation(rectPosition);


            //drawerSensor = d;
            //Debug.Log("Sensor found.");

            string herbToDisplay = drawerSensor.drawerContents;
            AllHerbsData.Instance.UpdateAndShowNote(herbToDisplay);
        }
        //drawerSensor = grandparentObject.GetComponent
    }

    void RemoveNote()
    {
        AllHerbsData.Instance.HideNote();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        GameObject activeHerb = eventData.pointerEnter;
        //AllHerbsData.UpdateNoteLocation(activeHerb);
        LocateSensorAndUpdateNote(activeHerb);
        //Debug.Log("Mouse registered - hovering over UI");
        //image.color = hoverColour;

        // invoke after 1 second?

        //herbalistNote = eventData.


        /*var x = eventData.position.x / Screen.width;
        var y = eventData.position.y / Screen.height;


            //try to prevent dragging the element offscreen
            if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;
        
            transform.position = eventData.position - delta;*/
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse registered - exited UI");
        //image.color = defaultColour;
        RemoveNote();
    }
}
