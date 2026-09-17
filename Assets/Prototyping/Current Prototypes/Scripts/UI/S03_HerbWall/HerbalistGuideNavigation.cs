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
        UIManager.Instance.EnableUI(navigationLeftCanvasGroup);
        UIManager.Instance.EnableUI(navigationRightCanvasGroup);
        navigationLeft.interactable = true;
        navigationRight.interactable = true;
        
        // disables L/R buttons when target int leads outside of array's bounds
        // if (target == HerbalistGuidePages.Instance.pagesArray.Length - 1)
        if (target == HerbalistGuidePages.Instance.pagesList.Count - 1){
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
        
        // propertyName.text = HerbalistGuidePages.Instance.pagesArray[target]._treatmentCategoryName;
        // propertyDescription.text = HerbalistGuidePages.Instance.pagesArray[target]._treatmentCategoryDescription;
        propertyName.text = HerbalistGuidePages.Instance.pagesList[target]._treatmentCategoryName;
        propertyDescription.text = HerbalistGuidePages.Instance.pagesList[target]._treatmentCategoryDescription;

        currentPageNumber = target;
    }

    public void ResetHerbalistGuideNavigation()
    {
        GoToPage(0);
    }
}
