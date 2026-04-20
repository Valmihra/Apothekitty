using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrimoireNavigation : MonoBehaviour
{
    [Header("Navigation Buttons")]
        public Button navigationLeft;
        public Button navigationRight;
        public Button ailmentSelection;

    [Header("Page Display")]
        public TMP_Text ailmentName;
        public TMP_Text ailmentDescription;
        public Image ailmentIcon;
        public GameObject selectionIcon;

    [Header("Ailment Icons")]
        public Image ailmentIcon01;
        public Image ailmentIcon02;
        public Image ailmentIcon03;
        public Image ailmentIcon04;
        private Image[] ailmentIcons;
        //private List<Image> ailmentIcons;

    [Header("GameObject References")]
        public GameObject clientLetterObj;
        public GameObject diagnosisSheetObj;
        public GameObject navigationButtonsObj;
    
    [Header("Ailment Information")]
        public string selectedAilment;    // For display on the Diagnosis sheet later? may not be necessary if just use ... .text, ... .ailmentName etc.

    // Keeps track of page numbers
        private int currentPageNumber;
        private int mindTabPageNum;
        private int bodyTabPageNum;
        private int spiritTabPageNum;
    // Script holding information on all pages
        private GrimoirePagesData pageData;
        private UIManager uiManagerTemp;


        public bool ailmentChosen;
    
    void Start()
    {
        InitialiseScene();
        
        pageData = FindObjectOfType<GrimoirePagesData>();
        uiManagerTemp = FindObjectOfType<UIManager>();
            /*if (pageData != null)
            {
                Debug.Log("Found pageData!");
            }*/

        //InitialiseList();
        InitialiseArray();
        SetTabNumbers();

        // 0 is first num of array!     this whole setup section would later be replaced with better scene management.
        GoToPage(0);
        
        navigationLeft.onClick.AddListener(delegate { GoToPage(currentPageNumber -1); });
        navigationRight.onClick.AddListener(delegate { GoToPage(currentPageNumber +1); });
        ailmentSelection.onClick.AddListener(delegate {GetSelectedAilment (currentPageNumber); });

    }

    // Basic scene setup
    void InitialiseScene()
    {
        selectionIcon.SetActive(false);
        ailmentChosen = false;
        //diagnosisSheetObj.SetActive(false);
    }

    // Immediately jumps to one of three key page numbers depending on tab chosen
    // NOT CURRENTLY IMPLEMENTED, BUT BASIC PLAN TO LOOK AT:
    void JumpTab(int tabNum)
    {
        if (tabNum == 1)
        {
            GoToPage(mindTabPageNum);
        }
        if (tabNum == 2)
        {
            GoToPage(bodyTabPageNum);
        }
        if (tabNum == 3)
        {
            GoToPage(spiritTabPageNum);
        }
        else
        {
            Debug.Log("Error while attempting to switch between main tabs.");
        }
    }

    // Determines which directions you can turn pages, and alters the UI's information to imitate a page turn 
    void GoToPage(int target)
    {
        navigationRight.enabled = true;
        navigationLeft.enabled = true;
        
        // disables L/R buttons when target int leads outside of array's bounds
        if (target == pageData.pagesArray.Length - 1)
        {
            navigationRight.enabled = false;
        }
        if (target == 0)
        {
            navigationLeft.enabled = false;
        }
        
        ailmentName.text = pageData.pagesArray[target].ailmentName_;
        ailmentDescription.text = pageData.pagesArray[target].ailmentDescription_;
        ailmentIcon.sprite = ailmentIcons[target].sprite;
        
        currentPageNumber = target;
    }

    void GetSelectedAilment(int pageNum)
    {
        selectionIcon.SetActive(true);
        ailmentChosen = true;
        ///
        /// 
        /// 
        /// 
        selectedAilment = pageData.pagesArray[pageNum].ailmentName_;
        //Debug.Log("The selected ailment is " + selectedAilment + ".");
        //Debug.Log("Would open Diagnosis Sheet here.");

        // Updates UI on screen
        uiManagerTemp.SubmitAilment();
            // clientLetterObj.SetActive(false);
            // diagnosisSheetObj.SetActive(true);

            // navigationButtonsObj.SetActive(false);
    }

    /*void InitialiseList()
    {
        ailmentIcons = new List<Image>();

        ailmentIcons.Add(ailmentIcon01);
        ailmentIcons.Add(ailmentIcon02);
        ailmentIcons.Add(ailmentIcon03);
        ailmentIcons.Add(ailmentIcon04);
    }*/

    void InitialiseArray()
    {
        List<Image> icons = new List<Image>();

        icons.Add(ailmentIcon01);
        icons.Add(ailmentIcon02);
        icons.Add(ailmentIcon03);
        icons.Add(ailmentIcon04);

        ailmentIcons = icons.ToArray();
        //Debug.Log(ailmentIcons.Length + " icons registered.");
    }

    void SetTabNumbers()
    {
        mindTabPageNum = 1;
        bodyTabPageNum = 2;
        spiritTabPageNum = 4;
    }
}