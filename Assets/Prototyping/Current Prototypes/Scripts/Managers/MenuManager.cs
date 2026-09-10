using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Canvas Groups")]
    public CanvasGroup mainMenuCanvasGroup;
    public CanvasGroup pauseMenuCanvasGroup;
    public CanvasGroup popupMenuCanvasGroup;
    public CanvasGroup progressMenuCanvasGroup;
	public CanvasGroup newDayMenuCanvasGroup;
	public CanvasGroup endOfMVPMenuCanvasGroup;
    
        public List<CanvasGroup> allMenuCanvasGroupsList;
    
    // COLOUR CHANGES
    // <color=red>  <#8A1E1E>

    // PLACEHOLDER VECTORS FOR TUTORIAL POPUP LOCATIONS
    private Vector2 initialPopupMenuPosition;
    // private Vector2 movedDownPosition;
    // private Vector2 movedUpPosition;

    [Header("Popup Text References")]
    public TMP_Text popupTextBox;
    public TMP_Text popupPrompt;
    
    [Header("Progress Popup References")]
    public TMP_Text progressPromptText;
    public Button progressExitButton;
    public Button progressContinueButton;

    private string popupText;
    private string defaultPopupPromptText = "Click to close.";
    private string continuePopupPromptText = "Click to continue.";

    private bool diagnosisSheetPopupActive;
    private bool popupPositionsSet;
    //private bool deskPopupActive;

	// *TAG* - Trying to see if splitting up some of the longer messages would help::
	[SerializeField] private PopupSensor popupSensor;
	[SerializeField] private AnimatedTextEffectNewDay newDayAnimatedTextComponent;

	[SerializeField] private TMP_Text endScreenClientsSeen;
	[SerializeField] private TMP_Text endScreenClientsCured;
	[SerializeField] private TMP_Text endScreenCallToAction;
	

    private static MenuManager _instance;
    public static MenuManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        //if (_instance = null)         {
            _instance = this;

        InitialiseMenuManager();
    }

    // ---------------------------------
    //      BASIC SETUP
    // ---------------------------------

    // Performs all basic functions required to run MenuManager 
    void InitialiseMenuManager()
    {
        // Sets bool and default prompt text for popup
        popupPrompt.text = defaultPopupPromptText;
        diagnosisSheetPopupActive = false;
        popupPositionsSet = false;

        CreateMenuCanvasGroupList();
        CreatePopupMenuPositions();
    }
    
    // Creates a list of all menu canvases
    void CreateMenuCanvasGroupList()
    {
        allMenuCanvasGroupsList = new List<CanvasGroup>();
        allMenuCanvasGroupsList.Add(pauseMenuCanvasGroup);
        allMenuCanvasGroupsList.Add(popupMenuCanvasGroup);
            allMenuCanvasGroupsList.Add(mainMenuCanvasGroup);
            allMenuCanvasGroupsList.Add(progressMenuCanvasGroup);
			allMenuCanvasGroupsList.Add(newDayMenuCanvasGroup);
			allMenuCanvasGroupsList.Add(endOfMVPMenuCanvasGroup);
    }

    // Plots the relevant locations for the tutorial popup
    void CreatePopupMenuPositions()
    {
        if (!popupPositionsSet)
        {
            initialPopupMenuPosition = popupMenuCanvasGroup.transform.position;
            
	        // currently trying version without altered menu positions
            // movedDownPosition = new Vector2(initialPopupMenuPosition.x, initialPopupMenuPosition.y - 100f);
            // movedUpPosition = new Vector2(initialPopupMenuPosition.x, initialPopupMenuPosition.y + 100f);
            
            popupPositionsSet = true;
        }
        else
        {
            // Debug.Log("Set positions already exist.");
            return;
        }
    }

    // ---------------------------------
    //      MENU DISPLAY FUNCTIONS
    // ---------------------------------

    public void OpenMenu(CanvasGroup menuCanvasGroup)
    {
        // game is paused?
        UIManager.Instance.DisableInteraction(SceneManager.Instance.currentCanvasGroup);
        SceneManager.Instance.OnGameplaySuspended();

        UIManager.Instance.EnableUI(menuCanvasGroup);
        menuCanvasGroup.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void ExitMenu(CanvasGroup menuCanvasGroup)
    {
        UIManager.Instance.DisableUI(menuCanvasGroup);
        UIManager.Instance.EnableInteraction(SceneManager.Instance.currentCanvasGroup);
        SceneManager.Instance.OnReturnToGame();

        if (menuCanvasGroup == mainMenuCanvasGroup)
        {
            GameManager.Instance.onMainMenu = false;
        }
        // game is no longer paused?
    }

    public void HideAllMenuCanvases()
    {
        foreach (CanvasGroup c in allMenuCanvasGroupsList)
        {
            UIManager.Instance.DisableUI(c);
        }
    }

    /// ---------------------------------
    //      TUTORIAL POPUP FUNCTIONS
    // ---------------------------------

    public void OpenTutorialPopup(string popupType)
    {
        OpenMenu(popupMenuCanvasGroup);
        
        if (popupType == "initialTutorial")
        {
			// FOR DEBUGGING EOMVP MENU EARLY: EndOfMVPMenu();
            popupText = "Welcome to Apothekitty!\n\nAs the town healer, it's your job to carefully diagnose and treat your clients. \n\nClick on the <#8A1E1E>curtain</color> to receive your first client!";
        }
        else if (popupType == "startPrompts")
        {
            UIManager.Instance.HighlightCanvasElement("arrows");
            popupText = "You'll find the client form on your desk. Click the <#8A1E1E>arrows</color> in the bottom right to navigate between screens.\n\nIf you feel lost at any point, click the arrow by the quest log to see what you still need to do.";
        }
        else if (popupType == "initDeskPrompts")
        {
            popupText = "Papers pile up quickly, but you can always click and drag things on your desk to keep it organised. \n\nTry moving those old notes to the bin.";
        }
        else if (popupType == "grimoire")
        {
            // popupMenuCanvasGroup.transform.position = movedDownPosition;
			List <string> infoToSend = new List <string> {"Your Grimoire acts as your reference point for ailments.",  " Pay close attention to each ailment's description and compare it to your client's symptoms.", "Once you think you've found the correct diagnosis, click on the <#8A1E1E>ailment's picture</color> to select it!"};
            
			popupSensor.SetupLongPopupText(infoToSend);
			// UIManager.Instance.HighlightCanvasElement("ailmentIcon");   // could be better to set this as a small script attached to the actual object?
            // popupText = "Your Grimoire acts as your reference point for ailments. Pay close attention to each ailment's description and compare it to your client's symptoms.\n\nOnce you think you've found the correct diagnosis, click on the <#8A1E1E>ailment's picture</color> to select it!";
        }
        else if (popupType == "diagnosisSheet")
        {
            // popupMenuCanvasGroup.transform.position = movedUpPosition;
            UIManager.Instance.HighlightCanvasElement("modifiers");
            popupPrompt.text = continuePopupPromptText;
            diagnosisSheetPopupActive = true;
            popupText = "Scan the ailment's description for clues to make a suitable treatment plan.\n\nYou can click and drag papers on your desk if you need to see something that's being obscured.\n\nNot all ailments require two targets, but some may require a <#8A1E1E>modifier</color>.";
            
        }
        else if (popupType == "diagnosisSheetTwo")
        {
            popupPrompt.text = defaultPopupPromptText;
            diagnosisSheetPopupActive = false;
            popupText = "For extreme cases or large clients, you can strengthen the treatment with the <b>enhancer</b>. You can also choose the <b>inverter</b> to achieve the opposite effect, if the description calls for it.\n\nClick <b>submit treatment plan</b> when you're ready.";
        }
        else if (popupType == "toHerbWall")
        {
            popupText = "Now that you've chosen a treatment plan, you can access your herb stores to create your treatment.\n\nClick the new arrow in the bottom right to navigate to the <#8A1E1E>herb wall</color>.";
        }
		else if (popupType == "endOfMVP")
		{
			// popupMenuCanvasGroup.transform.position = movedDownPosition;
			List <string> infoToSend = new List <string> {"And that's all we've got right now!", "Thank you for taking the time to play the current version of our game. \n\nAs a reward for playing to the end, here's a review of your results!", "Click the exit button to go back to the main menu at any time"};
			
			popupSensor.SetupLongPopupText(infoToSend);
		}
		else if (popupSensor.readingLongPopup)
		{
			popupText = popupSensor.longPopupText;
			if (popupText.Contains("<#8A1E1E>"))
			{
				UIManager.Instance.HighlightCanvasElement("ailmentIcon");
			}
		}
        else
        {
            popupText = "If you forget your chosen treatment plan, you can click the arrow in the bottom right to navigate to the desk.\n\nOnce you're done, click <b>'submit treatment'</b> to hand your recipe to the client.\n\nMake sure you're 100% certain before submitting, as there's no going back!";// how to submit and view results
        }
        popupTextBox.text = popupText;

		// *TAG* - ADD A JOURNAL THAT RECORDS THESE STRINGS AND LETS PLAYERS COME BACK TO THEM IF THEY NEED IT!!		USEFUL SAUCE FOR LATER!

        // IF RED IS PRESENT, WAIT .5F AND THEN INVOKE CHANGE COLOUR ON THE OBJECT
        // CHANGE COLOUR (WAITS .5F AND THEN SETS BACK TO DEFAULT COLOUR?)
        // INVOKE IT 3X? EACH TIME PERFORMING TIMESCALLED++ UNTIL 3X
        // THEN RESET TO 0 AND STOP RUNNING CHANGE COLOUR.
    }

    // ---------------------------------
    //      ALERT MESSAGE FUNCTIONS
    // ---------------------------------

	public void NewDayMenu(string command)
	{
		if (command == "open")
		{
			HideAllMenuCanvases();
            OpenMenu(newDayMenuCanvasGroup);

            newDayAnimatedTextComponent.NewDayAnimation();
		}
		if (command == "close")
		{
			ExitMenu(newDayMenuCanvasGroup);
		}
	}

	public void EndOfMVPMenu()
	{
		// *TAG* - This group should have the client results, qr code, etc.
		int numberCuredPatients = GameManager.Instance.GetCuredPatients();//0;
		Debug.Log(numberCuredPatients);
		//GameManager.Instance.GetCuredPatients(numberCuredPatients);
		//Debug.Log(numberCuredPatients);
		endScreenClientsSeen.text = ("<b>Total patients seen:</b> " + ClientLetter.Instance.allClientsList.Count).ToString();
		endScreenClientsCured.text = ("<b>Total patients cured:</b> " + numberCuredPatients).ToString();
		
		if (numberCuredPatients == 0)
		{
			endScreenCallToAction.text = "You weren't able to cure any patients. \n\nThis shows us we need to do something to help clarify how the mechanics of our game work. We'd really appreciate your feedback.";
		}
		else
		{
			endScreenCallToAction.text = "You cured someone!! \n\nNot gonna lie, I'm kinda celebrating, because for a while there, nobody was really doing that lmao";
		}
		
		OpenMenu(endOfMVPMenuCanvasGroup);
	}

    public void DiagnosisSheetInvalidCombinationPopup()
    {
        popupTextBox.text = "This combination is invalid.\n\nPlease ensure you are choosing <b> at least </b> a single effect and a target to pair it with.";
        OpenMenu(popupMenuCanvasGroup);
    }

	public void DiagnosisSheetCategoryDoubleUpPopup()
	{
		popupTextBox.text = "You have already using this target or effect in your treatment plan.\n\nPlease choose another to continue.";
		OpenMenu(popupMenuCanvasGroup);
	}

    public void HerbWallNothingChosenToSubmitPopup()
    {
        popupTextBox.text = "Bro, you can't give the client <i>nothing</i>. They're a paying customer!!";
        OpenMenu(popupMenuCanvasGroup);
    }

    public void HerbWallOneHerbChosenToSubmitPopup()
    {
        popupTextBox.text = "Don't get stingy, you need to pick at least two herbs for a recipe to do something!!";
        OpenMenu(popupMenuCanvasGroup);
    }

	public void HerbWallDuplicatePopup()
	{
		popupTextBox.text = "You cannot create an effective treatment with two of the same herb. Try submitting something else!";
		OpenMenu(popupMenuCanvasGroup);
	}

    public void ProgressDayPopup()
    {
        string messageToPlayer; 
        if (ClientLetter.Instance.currentDayClientsList.Count > 1)
        {
            // setup popup for next client
			messageToPlayer = "Looks like someone else is heading in now... better keep going!";
            progressContinueButton.GetComponent<ProgressLevelButton>().clientsRemaining = true;
        }
        else
        {
			// setup popup for end of day + results screen
            messageToPlayer = "Seems like no one else is coming today... time to close up!";
            progressContinueButton.GetComponent<ProgressLevelButton>().clientsRemaining = false;
        }

		progressContinueButton.GetComponent<ProgressLevelButton>().UpdateProgressLevelButtonText();
        
        progressPromptText.text = messageToPlayer;
        OpenMenu(progressMenuCanvasGroup);
    }

    public void ExitGamePopup()
    {
        popupTextBox.text = "The game should close after you click this box.\n\nThank-a-you so much for to playing our game!";
        OpenMenu(popupMenuCanvasGroup);
    }

    public void ClosePopup()
    {
        // Tutorial-specific conditions to check for
        if (GameManager.Instance.runningTutorial)
        {
            if (diagnosisSheetPopupActive)
            {
                popupMenuCanvasGroup.transform.position = initialPopupMenuPosition;
                OpenTutorialPopup("diagnosisSheetTwo");
                return;
            }
            else if (popupTextBox.text.Contains("Pay close attention to each ailment's description"))
            {
                //AilmentIconColourController.Instance.ResetAilmentIconBackground();
                popupMenuCanvasGroup.transform.position = initialPopupMenuPosition;
                ExitMenu(popupMenuCanvasGroup);
                return;
            }
        }

        // Constant conditions to check for
        //else
        //{
        
            // Checks if trying to exit the game		-- should probably come before the other check ^^
            if (popupTextBox.text.Contains("Thank-a-you so much for to playing our game!"))
            {
                ExitMenu(popupMenuCanvasGroup);
                Invoke(nameof(Quit), 1f);
            }
            else
            {
                ExitMenu(popupMenuCanvasGroup);
            }
        // }
    }

    void Quit()
    {
        Debug.Log("got to the quit command");
        GameManager.Instance.ExitGame();
    }

}
