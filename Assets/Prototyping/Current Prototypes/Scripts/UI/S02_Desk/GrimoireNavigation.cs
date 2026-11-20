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
    public int currentGrimoirePageNumber;
    
    // Vectors to store relevant positions for UI
    private Vector2 grimoireInitialPosition;
    private Vector2 buttonsPositionPage; 
    private Vector2 buttonsPositionCover;

	/*void Awake ()
	{
		buttonsPositionPage = navigationButtonsObject.transform.position;
        buttonsPositionCover = new Vector2((buttonsPositionPage.x + 100), buttonsPositionPage.y);
        grimoireInitialPosition = transform.position;
        
        grimoireDisplayedImage = GetComponent<Image>();
        
		// currentGrimoirePageNumber = 0;
        grimoireNavigationLeft.onClick.AddListener(delegate { GoToPage(currentGrimoirePageNumber--); });
        grimoireNavigationRight.onClick.AddListener(delegate { GoToPage(currentGrimoirePageNumber++); });
	}*/

    public void InitialiseGrimoireNavigation()
    {
        buttonsPositionPage = navigationButtonsObject.transform.position;
        buttonsPositionCover = new Vector2((buttonsPositionPage.x + 100), buttonsPositionPage.y);
        grimoireInitialPosition = transform.position;
        
        grimoireDisplayedImage = GetComponent<Image>();
        
		// currentGrimoirePageNumber = 0;
        grimoireNavigationLeft.onClick.AddListener(delegate { GoToPage(currentGrimoirePageNumber--); });
        grimoireNavigationRight.onClick.AddListener(delegate { GoToPage(currentGrimoirePageNumber++); });
        
        notOpenedGrimoire = true;
        resettingGrimoire = false;
        InitialiseGrimoireAilmentIconList();
    }

    
    public void ResetGrimoireNavigation()
    {
        // Basic scene setup
        resettingGrimoire = true;
        
        // Hides stamped ink and resets position on screen
        grimoireInkFromStampObject.SetActive(false);
        transform.position = grimoireInitialPosition;
        
        // Sets page to 0, thus closing book on GoToPage
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

        UIManager.Instance.SpriteShift(grimoireDisplayedImage, grimoireFrontCoverImage.sprite);
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
        // Plays sound if not called from reset function
        if (!resettingGrimoire)
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxGrimoirePageTurn);
        }
        
        grimoireNavigationLeft.gameObject.SetActive(true);
		grimoireNavigationRight.gameObject.SetActive(true);
        
        // Prevents navigating outside of the list's bounds
        if (target == GrimoirePagesData.Instance.grimoirePagesList.Count - 1)
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
            // *TAG* - Check here for null entries?
            string errorsFound = "Error entries detected: ";
            int errorNum = 0;
            
            foreach (GrimoirePagesData.SinglePage s in GrimoirePagesData.Instance.grimoirePagesList)
            {
                if (s._ailmentName == null)
                {
                    errorsFound = (errorsFound + s + ". ");
                    errorNum++;
                }
            }

            if (errorNum >= 1)
            {
                Debug.Log(errorsFound);
            }
            
            resettingGrimoire = false;
        }
    }

    void UpdateGrimoireDisplay(int target)
    {
        grimoireAilmentNameDisplay.text = GrimoirePagesData.Instance.grimoirePagesList[target]._ailmentName;
        grimoireAilmentDescriptionDisplay.text = GrimoirePagesData.Instance.grimoirePagesList[target]._ailmentDescription;
            
        UIManager.Instance.SpriteShift(grimoireAilmentIconDisplay, grimoireAilmentIconsList[target].sprite);
    }

    public void TempSelectionCheck()
    {
        GetSelectedAilment(currentGrimoirePageNumber);
    }
    
    void GetSelectedAilment(int pageNum)
    {
        grimoireInkFromStampObject.SetActive(true);
        GameManager.Instance.ailmentSubmitted = true;
        
        string selectedAilmentName = GrimoirePagesData.Instance.grimoirePagesList[pageNum]._ailmentName;
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
        // Allows grimoire object to act as a button, but only when closed
        if (grimoireIsClosed)
        {
            GoToPage(currentGrimoirePageNumber +1);
        }
    }
}