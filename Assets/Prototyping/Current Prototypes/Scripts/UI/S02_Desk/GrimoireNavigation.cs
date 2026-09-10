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

    [Header("Page Display")]
        public TMP_Text grimoireAilmentNameDisplay;
        public TMP_Text grimoireAilmentDescriptionDisplay;
        public Image grimoireAilmentIconDisplay;
        
        public Button grimoireAilmentSelectionButton;
        public GameObject grimoireStampObject;

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
        public GameObject navigationButtonsObject;

        
    
    [Header("Ailment Information")]
        // private string selectedAilmentName;    // For display on the Diagnosis sheet later? may not be necessary if just use ... .text, ... .grimoireAilmentNameDisplay etc.

        // Key stats to track:
        // public bool ailmentSubmitted;
        private bool notOpenedGrimoire;
        private int currentGrimoirePageNumber;
        
        // Vectors to store relevant positions for UI
        private Vector2 grimoireInitialPosition;
        private Vector2 buttonsPositionPage; 
        private Vector2 buttonsPositionCover;

    /*void Awake()
    {
        
    }

    void Start()
    {
        
    }*/

    public void InitialiseGrimoireNavigation()
    {
        buttonsPositionPage = navigationButtonsObject.transform.position;
        buttonsPositionCover = new Vector2((buttonsPositionPage.x + 100), buttonsPositionPage.y);
        grimoireInitialPosition = transform.position;
        
        grimoireDisplayedImage = GetComponent<Image>();
        
        grimoireNavigationLeft.onClick.AddListener(delegate { GoToPage(currentGrimoirePageNumber -1); });
        grimoireNavigationRight.onClick.AddListener(delegate { GoToPage(currentGrimoirePageNumber +1); });
        grimoireAilmentSelectionButton.onClick.AddListener(delegate {GetSelectedAilment (currentGrimoirePageNumber); });
        
        notOpenedGrimoire = true;
        InitialiseGrimoireAilmentIconList();
        ResetGrimoireNavigation();
        // SetTabNumbers();

        
    }

    // Basic scene setup
    public void ResetGrimoireNavigation()
    {
        grimoireAilmentSelectionButton.interactable = true;
        grimoireStampObject.SetActive(false);
        // ailmentSubmitted = false;

        // reset position on screen
        transform.position = grimoireInitialPosition;

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
        
        grimoireNavigationLeft.gameObject.SetActive(true);
		grimoireNavigationRight.gameObject.SetActive(true);

        // disables L/R buttons when target int leads outside of array's bounds
        if (target == GrimoirePagesData.Instance.grimoirePagesArray.Length - 1)
        {
			grimoireNavigationRight.gameObject.SetActive(false);
        }

        if (target == 0)
        {
            grimoireAilmentNameDisplay.enabled = false;
            grimoireAilmentDescriptionDisplay.enabled = false;
            
            grimoireStampObject.SetActive(false);
			grimoireAilmentIconDisplay.gameObject.SetActive(false);
            grimoireNavigationLeft.gameObject.SetActive(false);
            
			grimoireDisplayedImage.sprite = grimoireFrontCoverImage.sprite;
            navigationButtonsObject.transform.position = buttonsPositionCover;
        }
        else
        {
            if (target == 1)
            {
                grimoireAilmentNameDisplay.enabled = true;
                grimoireAilmentDescriptionDisplay.enabled = true;
                grimoireAilmentIconDisplay.gameObject.SetActive(true);
                
                navigationButtonsObject.transform.position = buttonsPositionPage;
                grimoireDisplayedImage.sprite = grimoireStandardPageImage.sprite;
            }
            
            grimoireAilmentNameDisplay.text = GrimoirePagesData.Instance.grimoirePagesArray[target]._ailmentName;
            grimoireAilmentDescriptionDisplay.text = GrimoirePagesData.Instance.grimoirePagesArray[target]._ailmentDescription;
            
            grimoireAilmentIconDisplay.sprite = grimoireAilmentIconsList[target].sprite;        // could maybe try to link in the same way I had been linking patient info and patient icons??
        }
        
        currentGrimoirePageNumber = target;
    }

    void GetSelectedAilment(int pageNum)
    {
        grimoireStampObject.SetActive(true);
        grimoireAilmentSelectionButton.interactable = false;
        GameManager.Instance.ailmentSubmitted = true;
        
        string selectedAilmentName = GrimoirePagesData.Instance.grimoirePagesArray[pageNum]._ailmentName;
        SceneManager.Instance.UpdateAilment(selectedAilmentName);
        SceneManager.Instance.SubmitAilment();

        DialogueRunner.Instance.GetDialogue("ailmentSubmitted");
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