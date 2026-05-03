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
        
    [Header("Page Display")]
        public TMP_Text propertyName;
        public TMP_Text propertyDescription;

    // Keeps track of page numbers
        private int currentPageNumber;
        

    void Start()
    {
        InitialiseGuideNav();
        
        navigationLeft.onClick.AddListener(delegate { GoToPage(currentPageNumber -1); });
        navigationRight.onClick.AddListener(delegate { GoToPage(currentPageNumber +1); });

    }

    // Basic scene setup
    void InitialiseGuideNav()
    {
        GoToPage(0);
    }

    // Determines which directions you can turn pages, and alters the UI's information to imitate a page turn 
    void GoToPage(int target)
    {
        navigationRight.enabled = true;
        navigationLeft.enabled = true;
        
        // disables L/R buttons when target int leads outside of array's bounds
        if (target == HerbalistGuidePages.Instance.pagesArray.Length - 1)
        {
            navigationRight.enabled = false;
        }
        if (target == 0)
        {
            navigationLeft.enabled = false;
        }
        
        propertyName.text = HerbalistGuidePages.Instance.pagesArray[target].propertyType_;
        propertyDescription.text = HerbalistGuidePages.Instance.pagesArray[target].propertyDescription_;
        
        currentPageNumber = target;
    }
}
