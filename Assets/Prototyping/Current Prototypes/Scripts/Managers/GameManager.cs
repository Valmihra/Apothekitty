using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // game states
    //private bool isPaused;
    public bool quitting {get; private set;}
	public bool isMVP {get; private set;}
    public bool seenFirstClient;
    public bool onMainMenu;
     
    public bool beginningDay;
    public bool canStartDay;
    public bool reloaded;

        // tutorial marker
        public bool runningTutorial;

    // player progress markers
    public bool ailmentChosen;
    public bool diagnosisSubmitted;
    public bool herbsSubmitted;
    
    public bool lastClientOfDay;
    //private bool debugging;
    
    // public Button resetSceneButton;
    public DayTrigger curtainAccess;
    private DiagnosisSheetInteractables diagnosisSheetInteractables;
    private ResultsCalculator resultsCalculator;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    /*  GAME MANAGER NEEDS
        ------------------
        
        PLAY GAME:
        ----------
            NEW GAME 
                STARTS WITH TUTORIAL
            LOAD GAME / CONTINUE
                READS FROM JSON (NOWHERE NEAR IMPLEMENTED YET OOF)
                (on load, start at beginning of that day? or by client??)
        
        PAUSE GAME
        ----------
            PAUSE MENU

        SAVE GAME
        ----------
            (at end of days? (thinking on result screen? what does everyone else think?))

        QUIT GAME
        ----------
            ,, quits
        
     */

    void Awake()
    {
        _instance = this;
        

        // links the manager to the diagnosis sheet script
        diagnosisSheetInteractables = FindObjectOfType<DiagnosisSheetInteractables>();      // should only ever be one in the game, but might be better way to do this. maybe search for all components in scene instead and delete any not on Constant UI?
        resultsCalculator = FindObjectOfType<ResultsCalculator>();
            // Assigns function to the associated button
            // resetSceneButton.onClick.AddListener(delegate { ResetScene(); });

        // sets bools
        ResetBools();
            //quitting = false;
            //isPaused = false;
            //reloaded = false;

        // for now set to run automatically, but will only go on tutorial lv when Jimmy is implemented
        //runningTutorial = true;
    }

    // GameManager should be first script to run
    void Start()
    {
        Debug.Log("Starting game at Main Menu.");

        FirstOpenGame();
        GoMainMenu();
        

    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pausing");
            if (isPaused)
            {
                MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenuCanvasGroup);
            }
            else
            {
                MenuManager.Instance.OpenMenu(MenuManager.Instance.pauseMenuCanvasGroup);
            }
            isPaused = !isPaused;
        }*/
        /*if (quitting)
        {
            Application.Quit();
        }*/
    }

    // ---------------------------------
    //      GAME MANAGEMENT
    // ---------------------------------


    void ResetBools()
    {
        quitting = false;
		isMVP = true;
        // isPaused = false;
        reloaded = false;

        // for now set to run automatically, but will only go on tutorial lv when Jimmy is implemented
        // runningTutorial = true;
    }
    
    // Resets all basic information in the scene
    void ResetBasicInformation()
    {
        SceneManager.Instance.ResetScene();             // Quest Log also resets in this SceneManager function
            beginningDay = true;
            canStartDay = false;
            ResetClientProgress();
    }

    void InitialiseAllGameData()
    {
        // Creates all data required to run the game
        ClientLetter.Instance.InitialiseClientLetter();
        AilmentData.Instance.InitialiseAilmentData();
        DayManager.Instance.InitialiseDayManager();
        resultsCalculator.InitialiseResultsCalculator();
        diagnosisSheetInteractables.InitialiseDiagnosisSheet();
        GrimoirePagesData.Instance.InitialiseGrimoirePagesData();
        ClientLetter.Instance.AssignClientsToGameDays();
        // Resets the game to day 0 and sets client list accordingly
        DayManager.Instance.ResetGameDays();
        ClientLetter.Instance.SetCurrentDayClientsList();
		if (runningTutorial)
		{
			TutorialItemController.Instance.InitialiseTutorialItemsDesk();
		}
		
    }

    // Resets all elements the player may have altered in-game
    void ResetInteractableGameElements()
    {
        // Resets Client Window UI
        curtainAccess.ResetCurtain();

        // Resets Desk UI
		if (runningTutorial)
		{
			TutorialItemController.Instance.ResetTutorialItemsDesk();
		}
        ClientLetter.Instance.ResetClientLetterPosition();
        GrimoirePagesData.Instance.ResetGrimoire();                 //AilmentIconColourController.Instance.ResetAilmentIconBackground();
        diagnosisSheetInteractables.ResetDiagnosisSheet();
            
        // Resets Herb Wall UI
        HerbalistGuidePages.Instance.ResetHerbalistGuide();
        HerbDrawersController.Instance.ResetHerbDrawerIcons();
        Inventory.Instance.ResetInventory();

        // Resets Extras
        ResultsScreen.Instance.ResetResultsScreen();
        DialogueRunner.Instance.ResetDialogueRunner();
		
		// Resets clients in calculator for the day
		resultsCalculator.ResetTreatedClientData();
    }

    // Resets the scene entirely.
    public void FullResetScene()
    {
        Debug.Log("Resetting scene for a new game...");

        // Generate the clients and ailments
        InitialiseAllGameData();

        // Reset the game scene
        ResetBasicInformation();
        ResetInteractableGameElements();

        // Debug options. Build will only require BeginTutorial!
        if (runningTutorial)
        {
            BeginTutorial();
        }
        else
        {
            Debug.Log("Skipping tutorial for debug purposes.");
        }
        // BEGINS TUTORIAL DIALOGUE
        // BeginTutorial();
        //SceneManager.Instance.SetupInitialScene();


                // for positions, it might be easier to add a reset function to the 
                // draggable components? then search for all of them and reset? idk.
    }

    public void DebugJumpToDayNumber(int dayNumber)
    {
        InitialiseAllGameData();

        ResetBasicInformation();
        ResetInteractableGameElements();

        DayManager.Instance.currentDayNumber = dayNumber;
        diagnosisSheetInteractables.InitialiseDiagnosisSheet();
        ClientLetter.Instance.SetCurrentDayClientsList();
        
        


        if (runningTutorial)
        {
            BeginTutorial();
        }
        else
        {
            Debug.Log("Skipping tutorial for debug purposes.");
        }
    }

    // similar to full reset, but it maintains the day number and sets up accordingly
    public void ResetLevel()
    {
        // int dayNumber = DayManager.Instance.currentDayNumber;       // will eventually add gamedata for this stuff!!
        Debug.Log("Resetting the current level...");

        // Reset the game scene
        ResetBasicInformation();
        ResetInteractableGameElements();

        if (runningTutorial)
        {
            BeginTutorial();
        }
        
    }

    // Initiates the series of dialogue, checks, and popups related to the tutorial
    void BeginTutorial()
    {
        DialogueRunner.Instance.GetDialogue("tutorial");
    }

    public void PrepareForNextClient()
    {
        Debug.Log("preparing for the next client...");
        // resultsCalculator.
        MenuManager.Instance.ProgressDayPopup();//
    }
    public void NextClient()
    {
        ResetClientProgress();
        DialogueRunner.Instance.FixDialogueBools();
        //UIManager / ScreenNav - .ResetCanvasLocations         ? maybe ?
    }
    
    void ResetClientProgress()
    {
        ailmentChosen = false;
        diagnosisSubmitted = false;
        
        // Debug.Log("Client Progress reset.");
    }

    // Triggered by the curtain interaction.
    public void SummonClient()
    {
        ClientLetter.Instance.RandomiseIncomingClientLetter();
        //DialogueRunner.Instance.GetDialogue("patientArrive");
        // randomises the client and reads DayData to set the relevant information?
            // once randomised, mimic movement onto the screen? or just fade in?
        // Spawn Client once randomised (invoke 2.0f) 
            // Client/PatientData::
            // SpawnClient
    }

    public void GoNextClient()
    {
        ClientLetter.Instance.UpdateCurrentDayClientsList();
        Debug.Log("There are now " + ClientLetter.Instance.currentDayClientsList.Count + " clients remaining today.");

        
        /*int index = ClientLetter.Instance.allClientsList.FindIndex(ClientLetter.Instance.activeClientData);

        if (ClientLetter.Instance.allClientsList[index] == ClientLetter.Instance.activeClientData)
        {
            ClientLetter.Instance.allClientsList.Remove(ClientLetter.Instance.activeClientData);
        }
        if (ClientLetter.Instance.clientIconList[index] == ClientLetter.Instance.displayedClientIcon)
        {
            ClientLetter.Instance.clientIconList.Remove(ClientLetter.Instance.displayedClientIcon);
        }
        */

        // ClientLetter.Instance.allClientsList.Remove(ClientLetter.Instance.activeClientData);
        // ClientLetter.Instance.treatedClientsList.Add(activeClientData);


        

        // Resets without altering bools
        SceneManager.Instance.ResetScene();
        ResetClientProgress();

        // Resets Desk UI
        ClientLetter.Instance.ResetClientLetterPosition();
        GrimoirePagesData.Instance.ResetGrimoire();                     //AilmentIconColourController.Instance.ResetAilmentIconBackground();
        diagnosisSheetInteractables.ResetDiagnosisSheet();
            
        // Resets Herb Wall UI
        HerbalistGuidePages.Instance.ResetHerbalistGuide();
        HerbDrawersController.Instance.ResetHerbDrawerIcons();
        Inventory.Instance.ResetInventory();

        // Resets Extras
        ResultsScreen.Instance.ResetResultsScreen();
        DialogueRunner.Instance.ResetDialogueRunner();

        // Calls in the next client
        SummonClient();
    }

	public void SubmitTreatmentToClient()
    {
        SceneManager.Instance.ReturnToClient();
		DialogueRunner.Instance.GetDialogue("submit herbs to client");
        
        
    }

	public void GoResultsScreen()
	{
		resultsCalculator.GoResultsScreen();
		// ResultsScreen.Instance.
	}


    // STILL NEED SOMETHING TO SUBMIT THE FULL AILMENT WITH!!

    // maybe trigger on submission to patient?
    /*public void SetClientAilment()
    {
        string client;
        foreach (Ailment a in AilmentData.Global.allAilmentsList)
        {
            if (a._affectedClientName == ClientData.Instance.activeClientData.name)
            {
                client = a._affectedClientName;
                GameData.CalculateResultFor(client);
            }
            else
            continue;
        }
    }*/

    public void ExitGame()
    {
        //Debug.Log("got to the final exit command");
        //Invoke(nameof(QuitApplication), 1f);
        //QuitApplication();
        Application.Quit();
    }

    /*void QuitApplication()
    {
        //Application.Quit();
        //quitting = true;
        Application.Quit();
        /*#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif\/
    }*/

    void FirstOpenGame()
    {
        // for now set to run automatically, but will only go on tutorial lv when Jimmy is implemented
        runningTutorial = true;
        seenFirstClient = false;
        Debug.Log("SFC is: " + seenFirstClient);
    }

    //
    public void GoMainMenu()
    {
        onMainMenu = true;
        SceneManager.Instance.SetupMainMenu();
    }

    public void NewGame()
    {
        FullResetScene();
    }

	public void GoNextDay()
	{
		Debug.Log("Beginning a new day!");
		Debug.Log("Should be setting up for day " + (DayManager.Instance.currentDayNumber + 1).ToString());
		DayManager.Instance.currentDayNumber++;

		//ResetBasicInformation();
		//ResetInteractableGameElements();

		DebugJumpToDayNumber(DayManager.Instance.currentDayNumber);
	}
}
