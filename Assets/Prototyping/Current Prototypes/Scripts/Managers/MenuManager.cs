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
	
    [Header("Popup GameObjects")]
    public GameObject standardPopupObject;
    public GameObject imagePopupObject;
    [SerializeField] private CanvasGroup imagePopupCanvasGroup;
    public bool imagePopupActive {get; private set;}
    
    [Header("Popup Text References")]
    public TMP_Text popupTextBox;
    public TMP_Text popupPrompt;
    [Space]
    public TMP_Text imagePopupTextBox;
    
    [Header("Progress Popup References")]
    public TMP_Text progressPromptText;
    public Button progressExitButton;
    public Button progressContinueButton;

    private string popupText;
    private string defaultPopupPromptText = "Click to continue.";		//"Click to close.";
    public string imagePopupCode { get; private set; }
    
    private bool diagnosisSheetPopupActive;

	// *TAG* - Trying to see if splitting up some of the longer messages would help::
	[SerializeField] private PopupSensor popupSensor;
	[SerializeField] private AnimatedTextEffectNewDay newDayAnimatedTextComponent;
	[SerializeField] private _PrototypePopupHandler prototypePopupHandler;

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
    void InitialiseMenuManager()
    {
        // Sets bool and default prompt text for popup
        popupPrompt.text = defaultPopupPromptText;
        diagnosisSheetPopupActive = false;
        imagePopupCode = "Switch to image popup.";


        CreateMenuCanvasGroupList();
        ResetTutorialPopup();
    }
    
    
    void CreateMenuCanvasGroupList()
    {
	    // Creates a list of all menu canvases
        allMenuCanvasGroupsList = new List<CanvasGroup>();
        allMenuCanvasGroupsList.Add(pauseMenuCanvasGroup);
        allMenuCanvasGroupsList.Add(popupMenuCanvasGroup);
            allMenuCanvasGroupsList.Add(mainMenuCanvasGroup);
            allMenuCanvasGroupsList.Add(progressMenuCanvasGroup);
			allMenuCanvasGroupsList.Add(newDayMenuCanvasGroup);
			allMenuCanvasGroupsList.Add(endOfMVPMenuCanvasGroup);
    }


    // ---------------------------------
    //      MENU DISPLAY FUNCTIONS
    // ---------------------------------
    public void OpenMenu(CanvasGroup menuCanvasGroup)
    {
	    // Disables interaction on the current CanvasGroup being displayed and pauses game
	    UIManager.Instance.DisableInteraction(SceneManager.Instance.currentCanvasGroup);
	    SceneManager.Instance.OnGameplaySuspended();

	    /*if (menuCanvasGroup == popupMenuCanvasGroup)
	    {
		    if (menuCanvasGroup.interactable)
		    {
			    SoundManager.Instance.PlayAudio(SoundManager.Instance.menuNavigation);
		    }
		    else
		    {
			    Debug.Log("opening!");
			    SoundManager.Instance.PlayAudio(SoundManager.Instance.menuReturn);
		    }
	    }*/
	    
	    // Enables the CanvasGroup specified and brings it to the front
	    UIManager.Instance.EnableUI(menuCanvasGroup);
	    menuCanvasGroup.GetComponent<RectTransform>().SetAsLastSibling();
        
    }

    public void ExitMenu(CanvasGroup menuCanvasGroup)
    {
        UIManager.Instance.DisableUI(menuCanvasGroup);
        UIManager.Instance.EnableInteraction(SceneManager.Instance.currentCanvasGroup);
        SceneManager.Instance.OnReturnToGame();
		
        /*if (menuCanvasGroup == popupMenuCanvasGroup)
        {
	        
	        SoundManager.Instance.PlayAudio(SoundManager.Instance.menuNavigation);
	        
        }*/ 
        
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
    public void ResetTutorialPopup()
    {
	    prototypePopupHandler.ResetImagePopup();
	    // imagePopupObject.SetActive(false);
	    UIManager.Instance.DisableUI(imagePopupCanvasGroup);
	    imagePopupActive = false;
	    
	    standardPopupObject.SetActive(true);
	    
	    
    }
    
    public void OpenTutorialPopup(string popupType)
    {
	    // SoundManager.Instance.PlayAudio(SoundManager.Instance.menuReturn);
        OpenMenu(popupMenuCanvasGroup);
        
        if (popupType == "endOfMVP")
        {
	        List<string> infoToSend = new List<string> { "And that's all we've got right now!", "Thank you for taking the time to play the current version of our game. \n\nAs a reward for playing to the end, here's a review of your results!", "Click the exit button to go back to the main menu at any time" };

	        popupSensor.SetupLongPopupText(infoToSend);
        }
        
        if (popupSensor.readingLongPopup)
        {
	        popupText = popupSensor.longPopupText;
        }
        
        if (popupType == "treatmentPlanExpanded")
        {
	        Debug.Log("Expanded treatment plan tutorial displaying. Should only happen on the first proper day in-game.");
	        List <string> infoToSend = new List <string> {"As you've likely noticed, sometimes ailments can affect both the mind <i>and</i> the body. \n\nIn cases where the grimoire recommends treating for two things, you can choose a primary <i>and</i> secondary treatment.", "The secondary treatment works exactly the same as the primary one -- just keep in mind that you can only use each category once. \n\nFor example, if your primary treatment is <i>'ease mind'</i>, you cannot use <i>'ease'</i> or <i>'mind'</i> in your secondary treatment.", "From now on, there is a possibility that you may need to treat both areas for each patient, so stay vigilant!"};
	        popupSensor.SetupLongPopupText(infoToSend);
        }
        

        if (GameManager.Instance.runningTutorial)
        {
            if (popupType == "initialTutorial")
            {
	            // IMAGE TUTORIAL POPUP
                List<string> infoToSend = new List<string> { "Welcome to Apothekitty! \n\nAs the town healer, your task is to diagnose and treat all clients you face.", imagePopupCode, "Click the <#8A1E1E>curtain</color> to receive your first client!" };
                popupSensor.SetupLongPopupText(infoToSend);
            }
            else if (popupType == "startPrompts")
            {
                List<string> infoToSend = new List<string> { "A patient information sheet and treatment plan are provided on your workdesk below. To navigate around the shop, click the <#8A1E1E>arrows</color> on the bottom right.", "To keep track of your tasks as a shopkeeper, a quest log is available in the pause menu." };
                popupSensor.SetupLongPopupText(infoToSend);
            }
            else if (popupType == "initDeskPrompts")
            {
	            // IMAGE TUTORIAL POPUP
	            List<string> infoToSend = new List<string> { "Things can get a bit messy on the desk. Lucky you can click and drag on objects to keep the space organised!", imagePopupCode, "Try placing the paper into the bin." };
	            popupSensor.SetupLongPopupText(infoToSend);
                // popupText = "Click and drag objects to keep your space organised. Try placing the paper into the bin.";
            }
            else if (popupType == "grimoire")
            {
	            // IMAGE TUTORIAL POPUP
                List<string> infoToSend = new List<string> { "Your Grimoire acts as your library of various ailments. It is your duty to analyse your client’s symptoms to confirm a diagnosis.", "To diagnose your patient correctly, pay close attention to each ailment's description and compare it to the symptoms your patient is describing on the Patient Sheet.", imagePopupCode, "Once you believe you've found the correct diagnosis, use the stamp to select the current page." };
                popupSensor.SetupLongPopupText(infoToSend);

            }
            
            else if (popupType == "toHerbWall")
            {
                List<string> infoToSend = new List<string> { "You have completed your first treatment plan! Now, you need to collect herbs that best fit both the personal needs and ailment of your client.", "The new arrow on the bottom right will navigate you to the herb wall." };
                popupSensor.SetupLongPopupText(infoToSend);
            }
            
            else if (popupType == "finalHerbWall")
            {
	            // IMAGE TUTORIAL POPUP
                List<string> infoToSend = new List<string>
                {
	                "On the Herb Wall, you must figure out which of the available herbs can be used to treat your patient.",
	                "Using your treatment recipe as a guide, you must find one herb to use for each category you have selected. \n\nTo figure out how each herb can be used, pull the roller door down to access your Compendium.",
	                imagePopupCode,
	                "The Compendium will help you to figure out if a herb can heal, ease, or fortify something, as well as if it targets the body or mind.",
	                "But your patient also has specific needs of their own! It's important that you choose herbs that are safe for their <b>size</b> and <b>diet</b>.", 
	                imagePopupCode,
	                "To check the specific information about a herb, hover the mouse over it to pull up its Botanical Codex entry.",
	                "Once you think you've found a good herb, drag it into your inventory tray. If you change your mind about it, simply drag it to the bin. \n\nWhen you're happy with your combination, click 'submit' to give the herbs to your patient.",
	                // "Over time, you will be able to visually determine which treatment categories a herb belongs to, but the specific information about each herb is harder to remember. " +
	                "Make good use of the Codex and Compendium to give your patients the best chance at recovering!"
                };
                
                popupSensor.SetupLongPopupText(infoToSend);
            }
            
            
            
            
            /*else
            {
                // *TAG* - THIS IS A DOUBLE-UP NOW
                popupText = "If you forget your chosen treatment plan, you can click the arrow in the bottom right to navigate to the desk.\n\nOnce you're done, click <b>'submit treatment'</b> to hand your recipe to the client.\n\nMake sure you're 100% certain before submitting, as there's no going back!";// how to submit and view results
            }*/
            
            
        }
        
        // popupTextBox.text = popupText;
        UpdatePopupText();

        // *TAG* - ADD A JOURNAL THAT RECORDS THESE STRINGS AND LETS PLAYERS COME BACK TO THEM IF THEY NEED IT!!		USEFUL SAUCE FOR LATER!

        // IF RED IS PRESENT, WAIT .5F AND THEN INVOKE CHANGE COLOUR ON THE OBJECT
        // CHANGE COLOUR (WAITS .5F AND THEN SETS BACK TO DEFAULT COLOUR?)
        // INVOKE IT 3X? EACH TIME PERFORMING TIMESCALLED++ UNTIL 3X
        // THEN RESET TO 0 AND STOP RUNNING CHANGE COLOUR.
    }


    void UpdatePopupText()
    {
	    bool usingImage = false;
	    
	    // Checks if text to display requires an image popup or not
	    if (popupText == imagePopupCode)
	    {
		    usingImage = true;
		    prototypePopupHandler.ResetImagePopup();
		    
		    // Gets the next string in the list, and switches from standard to image popup object
		    popupText = popupSensor.GetNextString();

		    if (standardPopupObject.activeInHierarchy)
		    {
			    standardPopupObject.SetActive(false);
		    }

		    if (!imagePopupCanvasGroup.interactable) //imagePopupObject.activeInHierarchy)
		    {
			    prototypePopupHandler.SearchForVideoPrefab(popupText);

			    UIManager.Instance.EnableUI(imagePopupCanvasGroup);
			    
			    // imagePopupObject.SetActive(true);
			    imagePopupActive = true;
		    }
	    }

	    if (!usingImage)
	    {
		    popupTextBox.text = popupText;

		    if (imagePopupCanvasGroup.interactable) //(imagePopupObject.activeInHierarchy)
		    {
			    ResetTutorialPopup();
		    }
	    }
	    else
	    {
		    imagePopupTextBox.text = popupText;
	    }
	    
	    
	    // Gets correct canvas to show
	    
	    
	    // Checks if need to highlight UI elements onscreen or not
	    if (popupText.Contains("<#8A1E1E>"))
	    {
		    Debug.Log("Highlight colour detected. Would highlight canvas element here.");
		    // UIManager.Instance.HighlightCanvasElement("ailmentIcon");
	    }
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
			case "noData":
				popupTextBox.text = "No game file exists that can be loaded.";
				OpenMenu(popupMenuCanvasGroup);
				break;
				
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
			

			default:
                popupTextBox.text = "YOU HAVE GOOFED IT SEVERELY. CHECK YOUR CODE (GET GOOD) OR STOP USING SWITCH STATEMENTS.";
				OpenMenu(popupMenuCanvasGroup);
                break;
		}
	}

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

    public void GetImagePopup()
    {
	    //
    }

    void Quit()
    {
        Debug.Log("got to the quit command");
        GameManager.Instance.ExitGame();
    }

}
