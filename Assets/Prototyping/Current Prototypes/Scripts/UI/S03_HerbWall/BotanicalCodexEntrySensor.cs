using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotanicalCodexEntrySensor : MouseHover
{
    void LocateSensorAndUpdateNote(GameObject hoverObject)
    {
        GameObject parentObject = hoverObject.transform.parent.gameObject;
        GameObject grandparentObject = parentObject.transform.parent.gameObject;

        if (grandparentObject.TryGetComponent<DrawerSensor>(out DrawerSensor drawerSensor))
        {
            string herbToDisplay = drawerSensor.drawerContents;
            AllHerbsData.Instance.UpdateAndShowNote(herbToDisplay);
        }
    }

    void RemoveNote()
    {
        AllHerbsData.Instance.HideNote();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        GameObject activeHerb = eventData.pointerEnter;
        LocateSensorAndUpdateNote(activeHerb);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        RemoveNote();
    }
}
