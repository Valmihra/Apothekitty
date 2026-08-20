using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrimoireNavigation : MonoBehaviour
{
    [Header("Image Display")]
        public Image frontCover;
        public Image standardPage;
        private Image displayImage;

    [Header("Navigation Buttons")]
        public Button navigationLeft;
        public Button navigationRight;
        public Button ailmentSelection;

    [Header("Page Display")]
        public TMP_Text ailmentName;
        public TMP_Text ailmentDescription;
        public Image ailmentIcon;
        public GameObject selectionIcon;
        public CanvasGroup ailmentIconCanvasGroup;

        public Color defaultTextColour;
        public Color hiddenTextColour;
        //public CanvasGroup pageContents;

    [Header("Ailment Icons")]
    public Image tempIcon00;
        public Image ailmentIcon01;
        public Image ailmentIcon02;
        public Image ailmentIcon03;
        public Image ailmentIcon04;
        public Image ailmentIcon05;
        public Image ailmentIcon06;
        public Image ailmentIcon07;
        public Image ailmentIcon08;
        public Image ailmentIcon09;
        public Image ailmentIcon10;
        //public Image ailmentIcon11;
        //public Image ailmentIcon12;
        //private Image[] ailmentIcons;
        private List<Image> ailmentIcons;

    [Header("GameObject References")]
        public GameObject clientLetterObj;
        public GameObject diagnosisSheetObj;
        public GameObject navigationButtonsObj;

        private Vector2 buttonsPositionPage;
        private Vector2 buttonsPositionCover;
    
    [Header("Ailment Information")]
        public string selectedAilment;    // For display on the Diagnosis sheet later? may not be necessary if just use ... .text, ... .ailmentName etc.

    // Keeps track of page numbers
        private int currentPageNumber;
        private int mindTabPageNum;
        private int bodyTabPageNum;
        private int spiritTabPageNum;

        public bool ailmentChosen;
        private bool notOpened;
        
        // Vector used to reset draggable objects
        private Vector2 startingPosition;

    void Awake()
    {
        startingPosition = transform.position;
        displayImage = GetComponent<Image>();
    }

    void Start()
    {
        navigationLeft.onClick.AddListener(delegate { GoToPage(currentPageNumber -1); });
        navigationRight.onClick.AddListener(delegate { GoToPage(currentPageNumber +1); });
        ailmentSelection.onClick.AddListener(delegate {GetSelectedAilment (currentPageNumber); });
    }

    public void ResetGrimoireNavigation()
    {
        InitialiseScene();
        //InitialiseList();
        InitialiseArray();
        SetTabNumbers();

        // 0 is first num of array!     this whole setup section would later be replaced with better scene management.
        GoToPage(0);
    }

    // Basic scene setup
    void InitialiseScene()
    {
        notOpened = true;

        ailmentSelection.interactable = true;
        selectionIcon.SetActive(false);
        ailmentChosen = false;
        //diagnosisSheetObj.SetActive(false);

        // reset position on screen
        transform.position = startingPosition;

        buttonsPositionPage = navigationButtonsObj.transform.position;
        buttonsPositionCover = new Vector2((buttonsPositionPage.x + 100), buttonsPositionPage.y);

        defaultTextColour = new Color(0,0,0);
        hiddenTextColour= new Color(0,0,0,0);
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
        if (target != 0 && notOpened)
        {
            MenuManager.Instance.TutorialPopup("grimoire");
            notOpened = false;
        }
        navigationRight.enabled = true;
        navigationLeft.enabled = true;
        
        // disables L/R buttons when target int leads outside of array's bounds
        if (target == GrimoirePagesData.Instance.pagesArray.Length - 1)
        {
            navigationRight.enabled = false;
        }

        if (target == 0)
        {
            // turns details transparent
            ailmentName.color = hiddenTextColour;
            ailmentDescription.color = hiddenTextColour;
            // UIManager.Instance.DisableUI(ailmentIconCanvasGroup);
            ailmentIconCanvasGroup.gameObject.SetActive(false);

            // hides left navigation and sets sprite icon
            navigationLeft.enabled = false;
            displayImage.sprite = frontCover.sprite;

            navigationButtonsObj.GetComponent<RectTransform>().transform.position = buttonsPositionCover;
            // close book option.enabled = true?
        }
        else
        {
            ailmentName.color = defaultTextColour;
            ailmentDescription.color = defaultTextColour;
            // UIManager.Instance.EnableUI(ailmentIconCanvasGroup);
            ailmentIconCanvasGroup.gameObject.SetActive(true);

            displayImage.sprite = standardPage.sprite;

            ailmentName.text = GrimoirePagesData.Instance.pagesArray[target]._ailmentName;
            ailmentDescription.text = GrimoirePagesData.Instance.pagesArray[target]._ailmentDescription;
            ailmentIcon.sprite = ailmentIcons[target].sprite;

            navigationButtonsObj.GetComponent<RectTransform>().transform.position = buttonsPositionPage;
        }
        //if

        
        
        currentPageNumber = target;
    }

    void GetSelectedAilment(int pageNum)
    {
        selectionIcon.SetActive(true);
        ailmentChosen = true;
        ///
        ailmentSelection.interactable = false;

        selectedAilment = GrimoirePagesData.Instance.pagesArray[pageNum]._ailmentName;

        SceneManager.Instance.UpdateAilment(selectedAilment);
        SceneManager.Instance.SubmitAilment();

        DialogueRunner.Instance.GetDialogue("ailmentSubmitted");
        //Debug.Log("The selected ailment is " + selectedAilment + ".");
        //Debug.Log("Would open Diagnosis Sheet here.");
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
        //List<Image> icons = new List<Image>();
        ailmentIcons = new List<Image>();

        ailmentIcons.Add(tempIcon00);
        ailmentIcons.Add(ailmentIcon01);
        ailmentIcons.Add(ailmentIcon02);
        ailmentIcons.Add(ailmentIcon03);
        ailmentIcons.Add(ailmentIcon04);
        ailmentIcons.Add(ailmentIcon05);
        ailmentIcons.Add(ailmentIcon06);
        ailmentIcons.Add(ailmentIcon07);
        ailmentIcons.Add(ailmentIcon08);
        ailmentIcons.Add(ailmentIcon09);
        ailmentIcons.Add(ailmentIcon10);
        //ailmentIcons.Add(ailmentIcon11);
        //ailmentIcons.Add(ailmentIcon12);

        //ailmentIcons = icons.ToArray();
        //Debug.Log(ailmentIcons.Length + " icons registered.");
    }

    void SetTabNumbers()
    {
        mindTabPageNum = 1;
        bodyTabPageNum = 2;
        spiritTabPageNum = 4;
    }



    /*
    void OpenBook()
    {
        // hides the book cover OR changes the icon of the background? WHICH TO DO??

        uiManagerTemp.EnableUI(pageContents);
        GoToPage(0);

        // enable buttons
        // disable cover button
    }

    void CloseBook()
    {
        // Hides the page contents and main navigation buttons, enables the cover navigation button
        uiManagerTemp.DisableUI(pageContents);
        // disable buttons
        // enable cover button
    }
    */
}