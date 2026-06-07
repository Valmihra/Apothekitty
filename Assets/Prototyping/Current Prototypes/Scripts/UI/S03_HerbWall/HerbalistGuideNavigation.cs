using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HerbalistGuideNavigation : MonoBehaviour
{
    [Header("Navigation Buttons")]
        public Button navigationLeft;
        public Button navigationRight;
        private CanvasGroup navigationLeftCanvasGroup;
        private CanvasGroup navigationRightCanvasGroup;
        
    [Header("Page Display")]
        public TMP_Text propertyName;
        public TMP_Text propertyDescription;

    // Keeps track of page numbers
        private int currentPageNumber;
        

    void Start()
    {
        navigationLeftCanvasGroup = navigationLeft.GetComponent<CanvasGroup>();
        navigationRightCanvasGroup = navigationRight.GetComponent<CanvasGroup>();

        navigationLeft.onClick.AddListener(delegate { GoToPage(currentPageNumber -1); });
        navigationRight.onClick.AddListener(delegate { GoToPage(currentPageNumber +1); });

    }

    // Determines which directions you can turn pages, and alters the UI's information to imitate a page turn 
    void GoToPage(int target)
    {
        //if (target )
        
        /*navigationRight.enabled = true;
        navigationLeft.enabled = true;*/

        UIManager.Instance.EnableUI(navigationLeftCanvasGroup);
        UIManager.Instance.EnableUI(navigationRightCanvasGroup);
        navigationLeft.interactable = true;
        navigationRight.interactable = true;
        
        // disables L/R buttons when target int leads outside of array's bounds
        if (target == HerbalistGuidePages.Instance.pagesArray.Length - 1)
        {
            //navigationRight.enabled = false;
            UIManager.Instance.DisableUI(navigationRightCanvasGroup);
            navigationRight.interactable = false;
        }
        if (target == 0)
        {
            //navigationLeft.enabled = false;
            UIManager.Instance.DisableUI(navigationLeftCanvasGroup);
            navigationLeft.interactable = false;
        }
        
        propertyName.text = HerbalistGuidePages.Instance.pagesArray[target].propertyType_;
        propertyDescription.text = HerbalistGuidePages.Instance.pagesArray[target].propertyDescription_;
        
        currentPageNumber = target;
    }

    public void ResetHerbalistGuideNavigation()
    {
        // initialise
        // list/array

        // no tab numbers rn, but will eventually set up UI to represent 
        // all elements on cards that can be pulled out of slots on the wall 
        // in the same place as the guide icon is currently.

        GoToPage(0);
    }
}
