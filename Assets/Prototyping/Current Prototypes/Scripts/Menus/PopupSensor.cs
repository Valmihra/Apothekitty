using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupSensor : MonoBehaviour, IPointerClickHandler
{
    public bool readingLongPopup = false;
    // private int timesClicked = 0;
    private int timesUntilClosePopup = 0;
    private int numberOfTextToAddInList = 0;
    public string longPopupText;
    private List<string> currentLongPopupStrings;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!readingLongPopup)
        {
            MenuManager.Instance.ClosePopup();
        }
        else
        {
            if (numberOfTextToAddInList != timesUntilClosePopup)
            {
                // longPopupText = (longPopupText + currentLongPopupStrings[numberOfTextToAddInList].ToString());
                longPopupText = currentLongPopupStrings[numberOfTextToAddInList];
                numberOfTextToAddInList++;
                MenuManager.Instance.OpenTutorialPopup(longPopupText);
            }
            else
            {
                ResetPopupSensor();
                MenuManager.Instance.ClosePopup();
                if (longPopupText.Contains("Click the exit button to go back to"))
                {
                    MenuManager.Instance.EndOfMVPMenu();
                }
            }
        }
    }
    
    public void ResetPopupSensor()
    {
        readingLongPopup = false;
        numberOfTextToAddInList = 0;
        timesUntilClosePopup = 0;
    }

    public void SetupLongPopupText(List<string> stringsForLongPopup)
    {
        readingLongPopup = true;
        currentLongPopupStrings = new List<string>(stringsForLongPopup);
        
        timesUntilClosePopup = currentLongPopupStrings.Count;

        longPopupText = currentLongPopupStrings[0];
        numberOfTextToAddInList++;
        MenuManager.Instance.OpenTutorialPopup(longPopupText);
    }
    //
}