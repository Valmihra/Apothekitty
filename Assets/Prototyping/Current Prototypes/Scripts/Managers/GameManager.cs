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
    
    [SerializeField] private GameObject tutorialItemsPrefab;
    [SerializeField] private Canvas tutorialItemControllerParentCanvas;
    
    
    public DayTrigger curtainAccess;
    private TreatmentPlanInteractables treatmentPlanInteractables;
    // private ResultsCalculator resultsCalculator;


	private GameData _gameData;
    private GameData _tempDataStorage;

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
        treatmentPlanInteractables = FindObjectOfType<TreatmentPlanInteractables>();      // should only ever be one in the game, but might be better way to do this. maybe search for all components in scene instead and delete any not on Constant UI?
        // resultsCalculator = FindObjectOfType<ResultsCalculator>();

        ResetGameStatusBools();
    }

    // GameManager should be first script to run
    void Start()
    {
        Debug.Log("Starting game at Main Menu.");

        FirstOpenGame();
        GoMainMenu();
        
    }

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
		// HerbCompendiumController mockupController = FindObjectOfType<HerbCompendiumController>();
        
        // Creates all data required to run the game
        PatientData.Instance.InitialisePatientLetter();
        AilmentData.Instance.InitialiseAilmentData();
        AllHerbsData.Instance.InitialiseAllHerbsData();
        HerbCompendiumData.Instance.InitialiseHerbalistGuide();
            HerbCompendiumController.Instance.InitialiseCompendium();
        DayManager.Instance.InitialiseDayManager();
        ResultsCalculator.Instance.InitialiseResultsCalculator();
        treatmentPlanInteractables.InitialiseDiagnosisSheet();
        GrimoirePagesData.Instance.InitialiseGrimoirePagesData();
        PatientData.Instance.AssignPatientsToGameDays();
        Inventory.Instance.InitialiseInventorySlots();
        curtainAccess.InitialiseDayTrigger();
        
        ResultsScreen.Instance.InitialiseResultsScreen();
        DialogueRunner.Instance.InitialiseDialogueRunner();
        
		if (runningTutorial)
		{
            GameObject tutorialSpecificUI = Instantiate(tutorialItemsPrefab, tutorialItemControllerParentCanvas.transform);
            tutorialSpecificUI.GetComponent<RectTransform>().SetAsLastSibling();
			TutorialItemController.Instance.InitialiseTutorialItemsDesk();
		}
        /*else
        {
            TutorialItemController.Instance.DebugDestroyTutorialItems();
        }*/
        

    }

    void ResetGameElementsOnNewDay()
    {
        shopIsClosed = true;
        canOpenShop = false;
        curtainAccess.ResetCurtain();
        
        PatientData.Instance.SetCurrentDayPatientsList();
        ResultsCalculator.Instance.ResetDailyTreatedClientData();
		treatmentPlanInteractables.SetDiagnosisSheetConfiguration();
        ResultsScreen.Instance.HideResultsScreen();


        _gameData.seenPatientsInCurrentDay = new List<ResultsCalculator.FinishedPatient>();
    }

    void ResetInteractablesBetweenClients()
    {
        // Resets all elements the player may have altered in-game. Used before any new client arrives
        
        // Resets the markers used to check progress with a single client
        ailmentSubmitted = false;
        diagnosisSubmitted = false;
        
        // Resets Desk UI
        //PatientData.Instance.ResetPatientLetterPosition();
        PatientData.Instance.ResetPatientLetter();
        GrimoirePagesData.Instance.ResetGrimoire();
        treatmentPlanInteractables.ResetDiagnosisSheet();
            
        // Resets Herb Wall UI
        // HerbCompendiumData.Instance.ResetHerbalistGuide();           *** RESET LEAFLETS NOW!!!
        HerbCompendiumController.Instance.ResetCompendiumLeaflets();
        DrawerController.Instance.ResetHerbDrawerIcons();
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

        _gameData.savedDayNumber++;
        
        DayManager.Instance.GoNextGameDay();
        OnNewDay();
    }

    public void DebugJumpToDayNumber(int dayNumber)
    {
        runningTutorial = false;

        CreateNewGameData();
        InitialiseAllGameData();
        DayManager.Instance.JumpToDayNumber(dayNumber);
        OnNewDay();
    }
    
    

    
    
	
    // ---------------------------------
    //      GAME DATA RETRIEVAL
    // ---------------------------------
    private void CreateNewGameData()
    {
        _gameData = new GameData();
        _gameData.numberPatientsSeen = 0;
        _gameData.numberPatientsCured = 0;

        _gameData.savedDayNumber = 0;
        _gameData.seenPatientsInCurrentDay = new List<ResultsCalculator.FinishedPatient>();
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

    public void AddFinishedPatientToData(ResultsCalculator.FinishedPatient treatedPatient)
    {
        /*if (_gameData.seenPatientsInCurrentDay.Count == 0)
        {
            _gameData.seenPatientsInCurrentDay = new List<ResultsCalculator.FinishedPatient>();
        }*/
        
        _gameData.seenPatientsInCurrentDay.Add(treatedPatient);
    }
    
    // ---------------------------------
    //      ,,,
    // ---------------------------------
    
    public void SummonClient()
    {
        // Set separately because is also triggered by the curtain interaction at start of each day. Randomises client.
		// _gameData.numberPatientsSeen++;
        PatientData.Instance.RandomiseIncomingPatientData();
        // DialogueRunner.Instance.GetDialogue("patientArrive");    // *TAG* - MOVED FROM CLIENT LETTER
    }

    public void SetClientData()
    {
        _gameData.savedActivePatient = PatientData.Instance.activePatientData;
        Debug.Log("The current active patient is saved to game data as: " + _gameData.savedActivePatient._patientName);
    }
    
    public void GoNextClient()
    {
        _gameData.numberPatientsSeen++;
        
        // updates the number of clients left to treat in the day
        PatientData.Instance.UpdateCurrentDayPatientsList();
        
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
        ResultsCalculator.Instance.UpdateAndShowResultsScreen();
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
        SoundManager.Instance.PlayAudio(SoundManager.Instance.musicMainMenu);
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

    public void SaveGame()
    {
        Debug.Log("Saving game...");
        
        _tempDataStorage = _gameData;
        
        Debug.Log("Should record day number: " + _gameData.savedDayNumber + ". # patients seen total: " + _gameData.numberPatientsSeen + ". # patients seen today: " + _gameData.seenPatientsInCurrentDay.Count + ".");
        
        
        // string savedData = JsonUtility.ToJson(_gd);     // _gd.ToJson();
        // string filePathSuffix = "/sav.json";
        
        // File.WriteAllText(Application.streamingAssetsPath + filePathSuffix, savedData);
        
        
        
    }

    public void LoadGame()
    {
        
        if (_tempDataStorage != null)
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.menuSelect);
            
            
            _gameData = _tempDataStorage;

            DayManager.Instance.JumpToDayNumber(_gameData.savedDayNumber);
            
            if (_gameData.seenPatientsInCurrentDay.Count > 0)
            {
                shopIsClosed = true;
                canOpenShop = false;
                curtainAccess.ResetCurtain();
        
                PatientData.Instance.SetCurrentDayPatientsList();
                ResultsCalculator.Instance.ResetDailyTreatedClientData();
                treatmentPlanInteractables.SetDiagnosisSheetConfiguration();
                ResultsScreen.Instance.HideResultsScreen();

                
                foreach (ResultsCalculator.FinishedPatient patient in _gameData.seenPatientsInCurrentDay)
                {
                    for (int i = 0; i < PatientData.Instance.currentDayPatientsList.Count; i++)
                    {
                        if (patient._treatedPatientName == PatientData.Instance.currentDayPatientsList[i]._patientName)
                        {
                            ResultsCalculator.Instance.dailyTreatedPatientsList.Add(patient);

                            PatientData.Instance.SetActivePatient(PatientData.Instance.currentDayPatientsList[i]);
                            PatientData.Instance.UpdateCurrentDayPatientsList();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                PatientData.Instance.SetSpecificPatientData(_gameData.savedActivePatient);
                
                // Reset game elements for a new client
                ResetInteractablesBetweenClients();
                
                
                SoundManager.Instance.SwitchInstrumental(SoundManager.Instance.musicPatientWindow);
                MenuManager.Instance.ExitMenu(MenuManager.Instance.mainMenuCanvasGroup);
                
                
                // Reset the game scene
                SceneManager.Instance.ResetScene();
                MenuManager.Instance.NewDayMenu("open");
            }
            else
            {
                SoundManager.Instance.SwitchInstrumental(SoundManager.Instance.musicPatientWindow);
                MenuManager.Instance.ExitMenu(MenuManager.Instance.mainMenuCanvasGroup);
                
                
                OnNewDay();
            }
        }
        else
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.menuReturn);
            MenuManager.Instance.OpenTutorialPopup("No data");
            return;
        }
        // string filePath = //Path
        
        // string jsonString = //""
    
        // GameData _gameData = //JsonUtility.FromJson<GameData>()
        // Debug.Log("Loaded for day: " + _gameData.currentDayNumber + ". Has seen: " + _gameData.numberPatientsSeen + " total patients, and: " + _gameData.seenPatientsInCurrentDay + " patients in the current game day.");

        
        
        //
        // string fileData = 
        
        // GameData currentGameData = _gameData;
    }
}
