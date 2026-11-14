using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupSensor : MonoBehaviour, IPointerClickHandler
{
    public bool readingLongPopup = false;
    private int timesUntilClosePopup = 0;
    private int numberOfTextToAddInList = 0;
    public string longPopupText;
    private List<string> currentLongPopupStrings;

    [SerializeField] private GameObject standardPopup;
    [SerializeField] private GameObject imagePopup;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        /*GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        if (clickedObject == imagePopup)
        {
            Debug.Log("Image popup clicked");
        }

        if (clickedObject == standardPopup)
        {
            Debug.Log("Standard popup clicked");
        }*/
        
        
        
        if (!readingLongPopup)
        {
            // If the popup is one window only, closes immediately on click
            MenuManager.Instance.ClosePopup();
        }
        else
        {
            if (numberOfTextToAddInList != timesUntilClosePopup)
            {
                // If the number of lines read is not equal to the number of lines to display,
                // increment number in list to view, and open the next popup accordingly
                longPopupText = currentLongPopupStrings[numberOfTextToAddInList];

                        /*if (longPopupText == MenuManager.Instance.imagePopupCode)
                        {
                            // Increments again to skip the popup code, and then calls popup function on MenuManager
                            numberOfTextToAddInList++;
                        }*/
                
                numberOfTextToAddInList++;
                MenuManager.Instance.OpenTutorialPopup(longPopupText);
            }
            else
            {
                // If the number is equal to the display limit, reset and close popup
                ResetPopupSensor();
                MenuManager.Instance.ClosePopup();
                
                if (MenuManager.Instance.imagePopupActive)
                {
                    MenuManager.Instance.ResetTutorialPopup();
                }
                
                // Checks whether the last string was the one before the end of MVP
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

    public string GetNextString()
    {
        // int numberInList = currentLongPopupStrings.IndexOf(longPopupText);
        // numberInList++;

        // Increments the string like it would in the standard section.
        // numberOfTextToAddInList++;
        
        string textToReturn = currentLongPopupStrings[numberOfTextToAddInList];
        
        numberOfTextToAddInList++;
        return textToReturn;
    }
    
}