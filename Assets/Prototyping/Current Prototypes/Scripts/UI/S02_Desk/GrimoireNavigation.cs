using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GrimoireNavigation : MonoBehaviour, IPointerClickHandler
{
    [Header("Image Display")]
        public Image grimoireFrontCoverImage;
        public Image grimoireStandardPageImage;
        private Image grimoireDisplayedImage;

    [Header("Navigation Buttons")]
        public Button grimoireNavigationLeft;
        public Button grimoireNavigationRight;
        public GameObject navigationButtonsObject;

    [Header("Page Display")]
        public TMP_Text grimoireAilmentNameDisplay;
        public TMP_Text grimoireAilmentDescriptionDisplay;
        public Image grimoireAilmentIconDisplay;
        
        public GameObject grimoireInkFromStampObject;
        private List<Image> grimoireAilmentIconsList;

        private bool resettingGrimoire;
        private bool grimoireIsClosed;
        
        

        // Key stats to track:
        private bool notOpenedGrimoire;
        public int currentGrimoirePageNumber;       // *TAG* - private it again once I've got this check working !!!
        
        // Vectors to store relevant positions for UI
        private Vector2 grimoireInitialPosition;
        private Vector2 buttonsPositionPage; 
        private Vector2 buttonsPositionCover;

    public void InitialiseGrimoireNavigation()
    {
        buttonsPositionPage = navigationButtonsObject.transform.position;
        buttonsPositionCover = new Vector2((buttonsPositionPage.x + 100), buttonsPositionPage.y);
        grimoireInitialPosition = transform.position;
        
        grimoireDisplayedImage = GetComponent<Image>();
        
        grimoireNavigationLeft.onClick.AddListener(delegate { GoToPage(currentGrimoirePageNumber -1); });
        grimoireNavigationRight.onClick.AddListener(delegate { GoToPage(currentGrimoirePageNumber +1); });
        
        notOpenedGrimoire = true;
        resettingGrimoire = false;
        InitialiseGrimoireAilmentIconList();
        ResetGrimoireNavigation();
    }

    // Basic scene setup
    public void ResetGrimoireNavigation()
    {
        resettingGrimoire = true;
        
        // grimoireAilmentSelectionButton.interactable = true;
        grimoireInkFromStampObject.SetActive(false);

        // reset position on screen
        transform.position = grimoireInitialPosition;
        
        currentGrimoirePageNumber = 0;
        GoToPage(currentGrimoirePageNumber);
    }

    void CloseGrimoire()
    {
        // Hides text components
        grimoireAilmentNameDisplay.enabled = false;
        grimoireAilmentDescriptionDisplay.enabled = false;
        
        // Hides icons, stamp ink, and navigation arrows
        grimoireInkFromStampObject.SetActive(false);
        grimoireAilmentIconDisplay.gameObject.SetActive(false);
        navigationButtonsObject.SetActive(false);
        // grimoireNavigationLeft.gameObject.SetActive(false);

        UIManager.Instance.SpriteShift(grimoireDisplayedImage, grimoireFrontCoverImage.sprite);
        // Moves navigation object. instead hide it and turn on open button object?
        // navigationButtonsObject.transform.position = buttonsPositionCover;
        
        grimoireIsClosed = true;
    }
    
    void OpenGrimoire()
    {
        grimoireIsClosed = false;
        
        // Shows text components
        grimoireAilmentNameDisplay.enabled = true;
        grimoireAilmentDescriptionDisplay.enabled = true;
        
        // Shows icon and navigation arrows
        grimoireAilmentIconDisplay.gameObject.SetActive(true);
        navigationButtonsObject.SetActive(true);
        
        UIManager.Instance.SpriteShift(grimoireDisplayedImage, grimoireStandardPageImage.sprite);
        
        if (notOpenedGrimoire)
        {
            RunGrimoireTutorial();
        }
    }

    void RunGrimoireTutorial()
    {
        notOpenedGrimoire = false;
        if (GameManager.Instance.runningTutorial)
        {
            MenuManager.Instance.OpenTutorialPopup("grimoire");
        }
    }


    // Determines which directions you can turn pages, and alters the UI's information to imitate a page turn 
    void GoToPage(int target)
    {
        if (!resettingGrimoire)
        {
            // Plays audio if not resetting          could probably remove check by calling differently on reset?
            SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxGrimoirePageTurn);
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
            CloseGrimoire();
        }
        else
        {
            if (currentGrimoirePageNumber == 0 && target == 1)
            {
                OpenGrimoire();
            }
            
            UpdateGrimoireDisplay(target);
        }
        
        currentGrimoirePageNumber = target;
        
        if (resettingGrimoire)
        {
            resettingGrimoire = false;
        }
    }

    void UpdateGrimoireDisplay(int target)
    {
        grimoireAilmentNameDisplay.text = GrimoirePagesData.Instance.grimoirePagesArray[target]._ailmentName;
        grimoireAilmentDescriptionDisplay.text = GrimoirePagesData.Instance.grimoirePagesArray[target]._ailmentDescription;
            
        UIManager.Instance.SpriteShift(grimoireAilmentIconDisplay, grimoireAilmentIconsList[target].sprite);
    }

    public void TempSelectionCheck()
    {
        GetSelectedAilment(currentGrimoirePageNumber);
    }
    
            // *TAG* - Private it again once stamp interaction is working!!!!
    void GetSelectedAilment(int pageNum)
    {
        grimoireInkFromStampObject.SetActive(true);
        GameManager.Instance.ailmentSubmitted = true;
        
        string selectedAilmentName = GrimoirePagesData.Instance.grimoirePagesArray[pageNum]._ailmentName;
        SceneManager.Instance.UpdateAilment(selectedAilmentName);
        SceneManager.Instance.SubmitAilment();

        DialogueRunner.Instance.GetDialogue("ailmentSubmitted");
    }

    void InitialiseGrimoireAilmentIconList()
    {
        grimoireAilmentIconsList = new List<Image>();
		Transform ailmentIconHolder = GameObject.Find("[Ailment Icon Storage]").transform;

		foreach (Transform child in ailmentIconHolder)
		{
			grimoireAilmentIconsList.Add(child.GetComponent<Image>());
		}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (grimoireIsClosed)
        {
            GoToPage(currentGrimoirePageNumber +1);
        }
    }
}