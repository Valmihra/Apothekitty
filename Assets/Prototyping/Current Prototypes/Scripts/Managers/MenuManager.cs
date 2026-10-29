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

        if (GameManager.Instance.runningTutorial)
		{
			if (popupType == "initialTutorial")
        	{
				// FOR DEBUGGING EOMVP MENU EARLY: EndOfMVPMenu();
            	// popupText = "Welcome to Apothekitty!\n\nAs the town healer, it's your job to carefully diagnose and treat your clients. \n\nClick on the <#8A1E1E>curtain</color> to receive your first client!";
				List <string> infoToSend = new List <string> {"Welcome to Apothekitty!", "As the town healer, your task is to diagnose and treat all clients you face. Click the <#8A1E1E>curtain</color> to receive your first client!"};
				popupSensor.SetupLongPopupText(infoToSend);
        	}
        	else if (popupType == "startPrompts")
        	{
         	   
            	// popupText = "You'll find the patient form on your desk. Click the <#8A1E1E>arrows</color> in the bottom right to navigate between screens.\n\nIf you feel lost at any point, click the arrow by the quest log to see what you still need to do.";
				// UIManager.Instance.HighlightCanvasElement("arrows");
				// popupText = "A patient information sheet and treatment plan are provided on your workdesk below. To navigate around the shop, click the <#8A1E1E>arrows</color> on the bottom right.";
				
				List <string> infoToSend = new List <string> {"A patient information sheet and treatment plan are provided on your workdesk below. To navigate around the shop, click the <#8A1E1E>arrows</color> on the bottom right.", "To keep track of your tasks as a shopkeeper, a quest log is available in the pause menu."};
				popupSensor.SetupLongPopupText(infoToSend);
        	}
        	else if (popupType == "initDeskPrompts")
        	{
            	// popupText = "Papers pile up quickly, but you can always click and drag things on your desk to keep it organised. \n\nTry moving those old notes to the bin.";
				popupText = "Click and drag objects to keep your space organised. Try placing the paper into the bin.";
        	}
        	else if (popupType == "grimoire")
        	{
				// List <string> infoToSend = new List <string> {"Your Grimoire acts as your reference point for ailments.",  " Pay close attention to each ailment's description and compare it to your client's symptoms.", "Once you think you've found the correct diagnosis, use the <#8A1E1E>stamp</color> on the page you want to choose."};
            	// *TAG* - WHAT IF ADD A NEW POPUP TYPE WITH IMAGES?
				List <string> infoToSend = new List <string> {"Your Grimoire acts as your library of various ailments. It is your duty to analyse your client’s symptoms to confirm a diagnosis.", "To diagnose your patient correctly, pay close attention to each ailment's description and compare it to the symptoms your patient is describing on the Patient Sheet.", "Once you believe you've found the correct diagnosis, use the stamp to select the current page."};
				popupSensor.SetupLongPopupText(infoToSend);
			
				}
        	/*else if (popupType == "diagnosisSheet")
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
        	}*/
        	else if (popupType == "toHerbWall")
        	{
            	// popupText = "Now that you've chosen a treatment plan, you can access your herb stores to create your treatment.\n\nClick the new arrow in the bottom right to navigate to the <#8A1E1E>herb wall</color>.";
				List <string> infoToSend = new List <string> {"You have completed your first treatment plan! Now, you need to collect herbs that best fit both the personal needs and ailment of your client.", "The new arrow on the bottom right will navigate you to the herb wall."};
				popupSensor.SetupLongPopupText(infoToSend);
        	}
			else if (popupType == "finalHerbWall")
			{
				List <string> infoToSend = new List <string> {"Select herbs by dragging and dropping them onto your inventory tray. To remove a selected herb, simply drag it to the bin", "Once you're done, click 'submit treatment' to hand your recipe to the patient.", "Make sure you’ve made the correct choice, as there's no going back once you hand it in!", "If you're feeling overwhelmed, and need a refresher, there are tutorials in available in the pause menu."};
				popupSensor.SetupLongPopupText(infoToSend);
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
        	else if (popupType == "No data")
        	{
	    	    popupText = "No game file exists that can be loaded.";
        	}
        	else
        	{
				// *TAG* - THIS IS A DOUBLE-UP NOW
            	popupText = "If you forget your chosen treatment plan, you can click the arrow in the bottom right to navigate to the desk.\n\nOnce you're done, click <b>'submit treatment'</b> to hand your recipe to the client.\n\nMake sure you're 100% certain before submitting, as there's no going back!";// how to submit and view results
        	}
		}
        else
		{
			// So far, only other text to popup like this is for treatment plan - extended, so:
			List <string> infoToSend = new List <string> {"As you've likely noticed, sometimes ailments can affect both the mind <i>and</i> the body. \n\nIn cases where the grimoire recommends treating for two things, you can choose a primary <i>and</i> secondary treatment.", "The secondary treatment works exactly the same as the primary one -- choose the desired effect and the area to target -- just keep in mind that you can only use each category once. \n\nFor example, if your primary treatment is <i>'ease mind'</i>, you cannot use <i>'ease'</i> or <i>'mind'</i> in your secondary treatment.", "From now on, there is a possibility that you may need to treat both areas for each patient, so stay vigilant!"};
			popupSensor.SetupLongPopupText(infoToSend);
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
		endScreenClientsSeen.text = ("<b>Total patients seen:</b> " + PatientData.Instance.allPatientsList.Count).ToString();
		endScreenClientsCured.text = ("<b>Total patients cured:</b> " + numberCuredPatients).ToString();
		
		if (numberCuredPatients == 0)
		{
			endScreenCallToAction.text = "You weren't able to cure any patients. \n\nHere, we'd say smth like 'This shows us we need to do something to help clarify how the mechanics of our game work. We'd really appreciate your feedback.'";
		}
		else
		{
			endScreenCallToAction.text = "You cured someone!! \n\nHere, we'd say smth to celebrate and then ask them to give feedback anyway lmao";
		}
		
		OpenMenu(endOfMVPMenuCanvasGroup);
	}


	public void AlertMessage(string messageType)
	{
		switch (messageType)
		{
			case "treatmentPlanInvalidCombo":
				popupTextBox.text = "This combination is invalid.\n\nPlease ensure you are choosing <b> at least </b> a single effect and a target to pair it with.";
        		OpenMenu(popupMenuCanvasGroup);
				break;

			case "treatmentPlanCategoryDoubleUp":
				popupTextBox.text = "You have already using this target or effect in your treatment plan.\n\nPlease choose another to continue.";
				OpenMenu(popupMenuCanvasGroup);
				break;

			case "herbWallNothingChosen":
				popupTextBox.text = "Bro, you can't give the client <i>nothing</i>. They're a paying customer!!";
        		OpenMenu(popupMenuCanvasGroup);
				break;

			case "herbWallOneHerbChosen":
				popupTextBox.text = "Don't get stingy, you need to pick at least two herbs for a recipe to do something!!";
        		OpenMenu(popupMenuCanvasGroup);
				break;

			case "herbWallDuplicateChosen":
				popupTextBox.text = "You cannot create an effective treatment with two of the same herb. Try submitting something else!";
				OpenMenu(popupMenuCanvasGroup);
				break;

			/*case "":
				//
				break;*/

			default:
                popupTextBox.text = "YOU HAVE GOOFED IT SEVERELY. CHECK YOUR CODE (GET GOOD) OR STOP USING SWITCH STATEMENTS.";
				OpenMenu(popupMenuCanvasGroup);
                break;
		}
	}

    /*public void DiagnosisSheetInvalidCombinationPopup()
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
	}*/

    public void ProgressDayPopup()
    {
        string messageToPlayer; 
        if (PatientData.Instance.currentDayPatientsList.Count > 1)
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
