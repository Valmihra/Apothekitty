using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Game states
    public bool gameIsQuitting {get; private set;}
	public bool gameIsMVP {get; private set;}
    public bool gameIsOnMainMenu;
    
    // New day markers
    public bool shopIsClosed;
    public bool canOpenShop;

    // Tutorial marker
    public bool runningTutorial;

    // Player progress markers
    public bool ailmentSubmitted;
    public bool diagnosisSubmitted;
    
    // Tutorial item prefab and parent for instantiation
    [SerializeField] private GameObject tutorialItemsPrefab;
    [SerializeField] private Canvas tutorialItemControllerParentCanvas;
    
    // Other required script references
    [SerializeField] private DayTrigger curtainAccess;
    private TreatmentPlanInteractables treatmentPlanInteractables;

	/*[SerializeField] private CanvasGroup instantiationParentCanvas;

	[SerializeField] private GameObject patientWindowPrefab;
	[SerializeField] private GameObject deskPrefab;
	[SerializeField] private GameObject herbWallPrefab;
	[SerializeField] private GameObject resultsScreenPrefab;*/



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

    // ---------------------------------
    //      ON RUNTIME
    // ---------------------------------
    void Awake()
    {
        _instance = this;

        treatmentPlanInteractables = FindObjectOfType<TreatmentPlanInteractables>();
        ResetGameStatusBools();
    }
    
    void Start()
    {
        FirstOpenGame();
        GoMainMenu();
    }

    
    // ---------------------------------
    //      GAME STATE UPDATES
    // ---------------------------------
    private void ResetGameStatusBools()
    {
        gameIsQuitting = false;
		gameIsMVP = true;
    }

    private void InitialiseAllGameData()
    {
        // Creates all patient, ailment and herb data
        PatientData.Instance.InitialisePatientLetter();
        AilmentData.Instance.InitialiseAilmentData();
        AllHerbsData.Instance.InitialiseAllHerbsData();
        
        // Creates all days, and uses them to assign correct patients in PatientData 
        DayManager.Instance.InitialiseDayManager();
        PatientData.Instance.AssignPatientsToGameDays();
        
        GrimoirePagesData.Instance.InitialiseGrimoirePagesData();
        HerbCompendiumData.Instance.InitialiseHerbCompendium();
        treatmentPlanInteractables.InitialiseDiagnosisSheet();
        
        curtainAccess.InitialiseDayTrigger();
        DialogueRunner.Instance.InitialiseDialogueRunner();
        
        Inventory.Instance.InitialiseInventorySlots();
        ResultsCalculator.Instance.InitialiseResultsCalculator();
        
		if (runningTutorial)
        {
            InstantiateTutorialObjects();
        }
    }

    private void ResetGameElementsOnNewDay()
    {
        // Resets values for opening the shop
        shopIsClosed = true;
        canOpenShop = false;
        curtainAccess.ResetCurtain();
        
        // Updates daily patient trackers and updates treatment plan configuration
        PatientData.Instance.SetCurrentDayPatientsList();
        ResultsCalculator.Instance.ResetDailyTreatedClientData();
        
        if (DayManager.Instance.currentDayNumber == 0 || DayManager.Instance.currentDayNumber == 1)
        {
            // Checks 
            treatmentPlanInteractables.SetDiagnosisSheetConfiguration();
            // ResultsScreen.Instance.HideResultsScreen();/
        }
        
        // Creates a new list of seenPatients in _gameData to override previous one
        _gameData.seenPatientsInCurrentDay = new List<ResultsCalculator.FinishedPatient>();
    }

    private void ResetInteractablesBetweenClients()
    {
        // Resets the markers used to check progress with a single client
        ailmentSubmitted = false;
        diagnosisSubmitted = false;
        
        // Resets Desk UI
        PatientData.Instance.ResetPatientLetter();
        GrimoirePagesData.Instance.ResetGrimoire();
        treatmentPlanInteractables.ResetDiagnosisSheet();
            
        // Resets Herb Wall UI
        HerbCompendiumData.Instance.ResetCompendium();
        DrawerController.Instance.ResetHerbDrawerIcons();
        Inventory.Instance.ResetInventory();


        DialogueRunner.Instance.ResetDialogueRunner();
    }
    

    // ---------------------------------
    //      SWITCHING BETWEEN DAYS
    // ---------------------------------
	public void StartNewDay()
	{
		// SceneManager.Instance.ResetScene();
		if (runningTutorial)
        {
            TutorialItemController.Instance.ResetTutorialItemsDesk();
            BeginTutorial();
        }
	}
    
    public void GoNextDay()
	{
		// Debug.Log("Should be setting up for day " + (DayManager.Instance.currentDayNumber + 1).ToString());
        _gameData.savedDayNumber++;
        
        DayManager.Instance.GoNextGameDay();
        OnNewDay();
    }
    
    void OnNewDay() // Resets scene entirely
    {
        // Resets elements for a new day
        ResetGameElementsOnNewDay();
        
        // Resets interactables for a new client
        ResetInteractablesBetweenClients();

        // Reset the game scene
        SceneManager.Instance.ResetScene();
        MenuManager.Instance.NewDayMenu("showNewDayMenu");
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
    //      SWITCHING BETWEEN CLIENTS
    // ---------------------------------
    public void GetNewPatient()
    {
        PatientData.Instance.RandomiseIncomingPatientData();
    }
    
    public void GoNextPatient()
    {
        // Updates patient visit information
        _gameData.numberPatientsSeen++;
        PatientData.Instance.UpdateCurrentDayPatientsList();
        
        // Resets interactables and the scene
        ResetInteractablesBetweenClients();
        SceneManager.Instance.ResetScene();
        
        // Summons the next patient
        GetNewPatient();
    }

    public void SubmitTreatmentToClient()
    {
        SceneManager.Instance.ReturnToClient();
        DialogueRunner.Instance.GetDialogue("submit herbs to client");
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
    
    public void RecordActivePatient()
    {
        _gameData.savedActivePatient = PatientData.Instance.activePatientData;
    }

    public void RecordTreatedPatient(ResultsCalculator.FinishedPatient treatedPatient)
    {
        _gameData.seenPatientsInCurrentDay.Add(treatedPatient);

        if (treatedPatient._correctHerbs)
        {
            // Records a cured patient\
            _gameData.numberPatientsCured++;
        }
    }
    
    public int GetCuredPatients()
    {
        int numberCuredPatients = _gameData.numberPatientsCured;
        // Debug.Log("Number of cured patients is currently: " + numberCuredPatients);
        return numberCuredPatients;
    }
    
    
    // ---------------------------------
    //      KEY GAME FUNCTIONS
    // ---------------------------------
    public void GoMainMenu()
    {
        gameIsOnMainMenu = true;
        SoundManager.Instance.PlayAudio(SoundManager.Instance.musicMainMenu);
        SceneManager.Instance.SetupMainMenu();
    }
    
    public void GoResultsScreen()
    {
        // preparing for game data implementation. would pass more data here.
        ResultsCalculator.Instance.UpdateAndShowResultsScreen();
    }
    
    
    // ---------------------------------
    //      KEY TUTORIAL FUNCTIONS
    // ---------------------------------
    void FirstOpenGame()
    {
        // Set separately so that you can toggle tutorial bool off in debug!!
        runningTutorial = true;
    }
    
    void BeginTutorial()
    {
        DialogueRunner.Instance.GetDialogue("tutorial");
    }

    void InstantiateTutorialObjects()
    {
        GameObject targetController = GameObject.Find("Panel - Tutorial Objects(Clone)");
            
        if (targetController != null)
        {
            Debug.Log("Controller found when trying to reset. Deleting now.");
            Destroy(targetController);
        }
        
        GameObject tutorialSpecificUI = Instantiate(tutorialItemsPrefab, tutorialItemControllerParentCanvas.transform);
        tutorialSpecificUI.GetComponent<RectTransform>().SetAsLastSibling();
        TutorialItemController.Instance.InitialiseTutorialItemsDesk();
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
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void RestartLevel()
    {
        // Reset the level so that the player can start the current day again
        Debug.Log("Resetting the current level...");

        if (runningTutorial)
        {
            // DialogueRunner.Instance.InitialiseDialogueRunner();
            DialogueRunner.Instance.ResetDialogueRunner();

            // Tries to find instances of instantiated tutorial objects
            InstantiateTutorialObjects();
        }
        
        OnNewDay(); 
    }

    public void SaveGame()
    {
        // Debug.Log("Saving game...");
        
        _tempDataStorage = _gameData;
        Debug.Log("Saving as day number: " + _gameData.savedDayNumber + ". # patients seen total: " + _gameData.numberPatientsSeen + ". # patients seen today: " + _gameData.seenPatientsInCurrentDay.Count + ".");
        
        
        // string savedData = JsonUtility.ToJson(_gd);     // _gd.ToJson();
        // string filePathSuffix = "/sav.json";
        
        // File.WriteAllText(Application.streamingAssetsPath + filePathSuffix, savedData);
        
        
        
    }

    public void LoadGame()
    {
        if (_tempDataStorage != null)
        {
            // If there's a saved file, play correct sound, and get data
            SoundManager.Instance.PlayAudio(SoundManager.Instance.menuSelect);
            _gameData = _tempDataStorage;
            
            // Jump to the saved day and set the patients and interactables accordingly
            DayManager.Instance.JumpToDayNumber(_gameData.savedDayNumber);
            if (DayManager.Instance.currentDayNumber == 0)
            {
                runningTutorial = true;
                NewGame();
                return;
            }
            else
            {
                InitialiseAllGameData();
            
                treatmentPlanInteractables.SetDiagnosisSheetConfiguration();
                PatientData.Instance.SetCurrentDayPatientsList();
                ResultsCalculator.Instance.ResetDailyTreatedClientData();
                
                if (_gameData.seenPatientsInCurrentDay.Count > 0)
                {
                    // Updates any treated patients
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

                    // Sets the active patient
                    PatientData.Instance.SetSpecificPatientData(_gameData.savedActivePatient);
                
                    // Switch sounds 
                    SoundManager.Instance.SwitchInstrumental(SoundManager.Instance.musicPatientWindow);
                    // (and close the main menu)        // MenuManager.Instance.ExitMenu(MenuManager.Instance.mainMenuCanvasGroup);
                
                    // Reset interactables and the scene
                    ResetInteractablesBetweenClients();
                    SceneManager.Instance.ResetScene();
                    MenuManager.Instance.NewDayMenu("showNewDayMenu");
                }
                else
                {
                    SoundManager.Instance.SwitchInstrumental(SoundManager.Instance.musicPatientWindow);
                    MenuManager.Instance.ExitMenu(MenuManager.Instance.mainMenuCanvasGroup);
                
                
                    OnNewDay();
                }
            }
        }
        else
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.menuReturn);
            MenuManager.Instance.AlertMessage("noData");
            return;
        }
    }
}
