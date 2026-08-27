using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrimoireNavigation : MonoBehaviour
{
    [Header("Image Display")]
        public Image grimoireFrontCoverImage;
        public Image grimoireStandardPageImage;
        private Image grimoireDisplayedImage;

    [Header("Navigation Buttons")]
        public Button grimoireNavigationLeft;
        public Button grimoireNavigationRight;
        public Button grimoireAilmentSelectionButton;

    [Header("Page Display")]
        public TMP_Text grimoireAilmentNameDisplay;
        public TMP_Text grimoireAilmentDescriptionDisplay;
        public Image grimoireAilmentIconDisplay;
        public GameObject grimoireAilmentSelectionObject;
        // public CanvasGroup grimoireAilmentIconCanvasGroup;
        // might be able to remove canvasgroup? idk,,

        public Color defaultGrimoireTextColour;
        public Color hiddenGrimoireTextColour;
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
        private List<Image> grimoireAilmentIconsList;

    [Header("GameObject References")]
        // public GameObject clientLetterObj;
        // public GameObject diagnosisSheetObj;
        public GameObject navigationButtonsObject;

        
    
    [Header("Ailment Information")]
        public string selectedAilment;    // For display on the Diagnosis sheet later? may not be necessary if just use ... .text, ... .grimoireAilmentNameDisplay etc.

        // Key stats to track:
        public bool ailmentChosen;
        private bool notOpenedGrimoire;
        private int currentGrimoirePageNumber;
        
        // Vectors to store relevant positions for UI
        private Vector2 grimoireInitialPosition;
        private Vector2 buttonsPositionPage; 
        private Vector2 buttonsPositionCover;

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    public void InitialiseGrimoireNavigation()
    {
        buttonsPositionPage = navigationButtonsObject.transform.position;
        buttonsPositionCover = new Vector2((buttonsPositionPage.x + 100), buttonsPositionPage.y);
        grimoireInitialPosition = transform.position;
        
        grimoireDisplayedImage = GetComponent<Image>();
        
        defaultGrimoireTextColour = new Color(0,0,0);
        hiddenGrimoireTextColour= new Color(0,0,0,0);
        
        grimoireNavigationLeft.onClick.AddListener(delegate { GoToPage(currentGrimoirePageNumber -1); });
        grimoireNavigationRight.onClick.AddListener(delegate { GoToPage(currentGrimoirePageNumber +1); });
        grimoireAilmentSelectionButton.onClick.AddListener(delegate {GetSelectedAilment (currentGrimoirePageNumber); });
        
        
        InitialiseGrimoireAilmentIconList();
        ResetGrimoireNavigation();
        // SetTabNumbers();

        
    }

    // Basic scene setup
    public void ResetGrimoireNavigation()
    {
        notOpenedGrimoire = true;

        grimoireAilmentSelectionButton.interactable = true;
        grimoireAilmentSelectionObject.SetActive(false);
        ailmentChosen = false;
        //diagnosisSheetObj.SetActive(false);

        // reset position on screen
        transform.position = grimoireInitialPosition;

        // 0 is first num of array!     this whole setup section would later be replaced with better scene management.
        GoToPage(0);
    }


    // Determines which directions you can turn pages, and alters the UI's information to imitate a page turn 
    void GoToPage(int target)
    {
        if (target != 0 && notOpenedGrimoire)
        {
            MenuManager.Instance.OpenTutorialPopup("grimoire");
            notOpenedGrimoire = false;
        }
        grimoireNavigationRight.enabled = true;
        grimoireNavigationLeft.enabled = true;
        
        // disables L/R buttons when target int leads outside of array's bounds
        if (target == GrimoirePagesData.Instance.grimoirePagesArray.Length - 1)
        {
            grimoireNavigationRight.enabled = false;
        }

        if (target == 0)
        {
            // turns details transparent
            grimoireAilmentNameDisplay.color = hiddenGrimoireTextColour;
            grimoireAilmentDescriptionDisplay.color = hiddenGrimoireTextColour;
            // UIManager.Instance.DisableUI(grimoireAilmentIconCanvasGroup);
            grimoireAilmentSelectionObject.SetActive(false);
			grimoireAilmentIconDisplay.gameObject.SetActive(false);

            // hides left navigation and sets sprite icon
            grimoireNavigationLeft.enabled = false;
            grimoireDisplayedImage.sprite = grimoireFrontCoverImage.sprite;

            navigationButtonsObject.GetComponent<RectTransform>().transform.position = buttonsPositionCover;
            // close book option.enabled = true?
        }
        else
        {
            grimoireAilmentNameDisplay.color = defaultGrimoireTextColour;
            grimoireAilmentDescriptionDisplay.color = defaultGrimoireTextColour;
            // UIManager.Instance.EnableUI(grimoireAilmentIconCanvasGroup);
            // grimoireAilmentSelectionObject.SetActive(true);
			grimoireAilmentIconDisplay.gameObject.SetActive(true);

            grimoireDisplayedImage.sprite = grimoireStandardPageImage.sprite;

            grimoireAilmentNameDisplay.text = GrimoirePagesData.Instance.grimoirePagesArray[target]._ailmentName;
            grimoireAilmentDescriptionDisplay.text = GrimoirePagesData.Instance.grimoirePagesArray[target]._ailmentDescription;
            grimoireAilmentIconDisplay.sprite = grimoireAilmentIconsList[target].sprite;

            navigationButtonsObject.GetComponent<RectTransform>().transform.position = buttonsPositionPage;
        }
        //if

        
        
        currentGrimoirePageNumber = target;
    }
    
    // if target 0, show hidden arrow to open/close book

    void GetSelectedAilment(int pageNum)
    {
        grimoireAilmentSelectionObject.SetActive(true);
        ailmentChosen = true;
        ///
        grimoireAilmentSelectionButton.interactable = false;

        selectedAilment = GrimoirePagesData.Instance.grimoirePagesArray[pageNum]._ailmentName;

        SceneManager.Instance.UpdateAilment(selectedAilment);
        SceneManager.Instance.SubmitAilment();

        DialogueRunner.Instance.GetDialogue("ailmentSubmitted");
        //Debug.Log("The selected ailment is " + selectedAilment + ".");
        //Debug.Log("Would open Diagnosis Sheet here.");
    }

    void InitialiseGrimoireAilmentIconList()
    {
        //List<Image> icons = new List<Image>();
        grimoireAilmentIconsList = new List<Image>();

        grimoireAilmentIconsList.Add(tempIcon00);
        grimoireAilmentIconsList.Add(ailmentIcon01);
        grimoireAilmentIconsList.Add(ailmentIcon02);
        grimoireAilmentIconsList.Add(ailmentIcon03);
        grimoireAilmentIconsList.Add(ailmentIcon04);
        grimoireAilmentIconsList.Add(ailmentIcon05);
        grimoireAilmentIconsList.Add(ailmentIcon06);
        grimoireAilmentIconsList.Add(ailmentIcon07);
        grimoireAilmentIconsList.Add(ailmentIcon08);
        grimoireAilmentIconsList.Add(ailmentIcon09);
        grimoireAilmentIconsList.Add(ailmentIcon10);
        //grimoireAilmentIconsList.Add(ailmentIcon11);
        //grimoireAilmentIconsList.Add(ailmentIcon12);

        //grimoireAilmentIconsList = icons.ToArray();
        //Debug.Log(grimoireAilmentIconsList.Length + " icons registered.");
    }

    /*void SetTabNumbers()
    {
        mindTabPageNum = 1;
        bodyTabPageNum = 2;
        spiritTabPageNum = 4;
    }*/



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