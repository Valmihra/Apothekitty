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
    public bool onMainMenu;
     
    public bool shopIsClosed;
    public bool canOpenShop;

        // tutorial marker
        public bool runningTutorial;

    // player progress markers
    public bool ailmentSubmitted;
    public bool diagnosisSubmitted;
    public bool herbsSubmitted;
    //public bool onLastClientOfDay;
    
    
    public DayTrigger curtainAccess;
    private DiagnosisSheetInteractables treatmentPlanInteractables;
    private ResultsCalculator resultsCalculator;

	private GameData _gameData;

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
        treatmentPlanInteractables = FindObjectOfType<DiagnosisSheetInteractables>();      // should only ever be one in the game, but might be better way to do this. maybe search for all components in scene instead and delete any not on Constant UI?
        resultsCalculator = FindObjectOfType<ResultsCalculator>();

        ResetGameStatusBools();
    }

    // GameManager should be first script to run
    void Start()
    {
        Debug.Log("Starting game at Main Menu.");

        FirstOpenGame();
        GoMainMenu();
        

    }

    // void Update()
    // {
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
    // }

    // ---------------------------------
    //      GAME MANAGEMENT
    // ---------------------------------


    void ResetGameStatusBools()
    {
        quitting = false;
		isMVP = true;
    }

    void InitialiseAllGameData()
    {
        // Creates all data required to run the game
        ClientLetter.Instance.InitialiseClientLetter();
        AilmentData.Instance.InitialiseAilmentData();
        DayManager.Instance.InitialiseDayManager();
        resultsCalculator.InitialiseResultsCalculator();
        treatmentPlanInteractables.InitialiseDiagnosisSheet();
        GrimoirePagesData.Instance.InitialiseGrimoirePagesData();
        ClientLetter.Instance.AssignClientsToGameDays();
        
        ResultsScreen.Instance.InitialiseResultsScreen();
        DialogueRunner.Instance.InitialiseDialogueRunner();
        
		if (runningTutorial)
		{
			TutorialItemController.Instance.InitialiseTutorialItemsDesk();
		}
		
    }

    void ResetGameElementsOnNewDay()
    {
        shopIsClosed = true;
        canOpenShop = false;
        curtainAccess.ResetCurtain();
        
        ClientLetter.Instance.SetCurrentDayClientsList();
        resultsCalculator.ResetDailyTreatedClientData();
		treatmentPlanInteractables.SetDiagnosisSheetConfiguration();
        ResultsScreen.Instance.HideResultsScreen();
    }

    void ResetInteractablesBetweenClients()
    {
        // Resets all elements the player may have altered in-game. Used before any new client arrives
        
        // Resets the markers used to check progress with a single client
        ailmentSubmitted = false;
        diagnosisSubmitted = false;
        
        // Resets Desk UI
        ClientLetter.Instance.ResetClientLetterPosition();
        GrimoirePagesData.Instance.ResetGrimoire();
        treatmentPlanInteractables.ResetDiagnosisSheet();
            
        // Resets Herb Wall UI
        HerbalistGuidePages.Instance.ResetHerbalistGuide();
        HerbDrawersController.Instance.ResetHerbDrawerIcons();
        Inventory.Instance.ResetInventory();


        DialogueRunner.Instance.ResetDialogueRunner();
        // DialogueRunner.Instance.FixDialogueBools();         //UIManager / ScreenNav - .ResetCanvasLocations         ? maybe ?
    }
    
    // Resets the scene entirely.
    void OnNewDay()
    {
        // Reset the data for a new day
        ResetGameElementsOnNewDay();
        
        // Reset game elements for a new client
        ResetInteractablesBetweenClients();

        // Reset the game scene
        SceneManager.Instance.ResetScene();
		MenuManager.Instance.NewDayMenu("open");
    }

    // ---------------------------------
    //      SWITCHING BETWEEN DAYS
    // ---------------------------------
	public void StartDay()
	{
		SceneManager.Instance.ResetScene();
		if (runningTutorial)
        {
            TutorialItemController.Instance.ResetTutorialItemsDesk();
            BeginTutorial();
        }
	}
    
    public void GoNextDay()
	{
		Debug.Log("Should be setting up for day " + (DayManager.Instance.currentDayNumber + 1).ToString());
        
        DayManager.Instance.GoNextGameDay();
        OnNewDay();
    }

    public void DebugJumpToDayNumber(int dayNumber)
    {
        CreateNewGameData();
        InitialiseAllGameData();
        DayManager.Instance.JumpToDayNumber(dayNumber);
        OnNewDay();
        
        if (runningTutorial)
        {
            BeginTutorial();
        }
        else
        {
            Debug.Log("Skipping tutorial for debug purposes.");
        }
    }
    
    

    
    
	
    // ---------------------------------
    //      GAME DATA RETRIEVAL
    // ---------------------------------
    private void CreateNewGameData()
    {
        _gameData = new GameData();
    }
    
	public void RecordCuredPatient()
	{
		_gameData.numberPatientsCured++;
	}

	public int GetCuredPatients()
	{
		int numberCuredPatients = _gameData.numberPatientsCured;
		Debug.Log("Number of cured patients is currently: " + numberCuredPatients);
		return numberCuredPatients;
	}
    
    // ---------------------------------
    //      ,,,
    // ---------------------------------
    
    public void SummonClient()
    {
        // Set separately because is also triggered by the curtain interaction at start of each day. Randomises client.
		_gameData.numberPatientsSeen++;
        ClientLetter.Instance.RandomiseIncomingClientLetter();
        // DialogueRunner.Instance.GetDialogue("clientArrive");    // *TAG* - MOVED FROM CLIENT LETTER
    }
    
    public void GoNextClient()
    {
        // updates the number of clients left to treat in the day
        ClientLetter.Instance.UpdateCurrentDayClientsList();
        
        ResetInteractablesBetweenClients();
        SceneManager.Instance.ResetScene();
        
        SummonClient();
            // *TAG* - DO I NEED TO DO ANYTHING ELSE HERE??
    }

	public void SubmitTreatmentToClient()
    {
        SceneManager.Instance.ReturnToClient();
		DialogueRunner.Instance.GetDialogue("submit herbs to client");
    }
    
    void BeginTutorial()
    {
        // Initiates the series of dialogue, checks, and popups related to the tutorial
        DialogueRunner.Instance.GetDialogue("tutorial");
    }
    
    // ---------------------------------
    //      GAME DATA RETRIEVAL
    // ---------------------------------
    
	public void GoResultsScreen()
	{
        // preparing for game data implementation. would pass more data here.
		resultsCalculator.UpdateAndShowResultsScreen();
	}

    

    void FirstOpenGame()
    {
        // for now set to run automatically, but will only go on tutorial lv when Jimmy is implemented
        // **   set separately so that you can toggle tutorial bool off in debug!!
        runningTutorial = true;
    }

    
    public void GoMainMenu()
    {
        onMainMenu = true;
        SceneManager.Instance.SetupMainMenu();
    }

    

	

    // ---------------------------------
    //      BASIC UTILITY FUNCTIONS
    // ---------------------------------
    
    public void NewGame()
    {
        Debug.Log("Setting up a new game...");
        CreateNewGameData();
        
        // Generate the clients and ailments and reset the scene for a new day
        InitialiseAllGameData();
        OnNewDay();

        Debug.Log("CURRENT DAY IS " + DayManager.Instance.currentDayNumber);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void RestartLevel()
    {
        // Reset the level so that the player can start the current day again
        Debug.Log("Resetting the current level...");
        OnNewDay();

        if (runningTutorial)
        {
            TutorialItemController.Instance.ResetTutorialItemsDesk();
            BeginTutorial();
        }
        
    }
}
