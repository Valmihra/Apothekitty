using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // game states
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
    
    [SerializeField] private GameObject tutorialItemsPrefab;
    [SerializeField] private Canvas tutorialItemControllerParentCanvas;
    
    
    public DayTrigger curtainAccess;
    private TreatmentPlanInteractables treatmentPlanInteractables;


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
        
        // ResultsScreen.Instance.InitialiseResultsScreen();
        DialogueRunner.Instance.InitialiseDialogueRunner();
        
		if (runningTutorial)
        {
            InstantiateTutorialObjects();
        }
        // else check and delete?
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
        // Resets the markers used to check progress with a single client
        ailmentSubmitted = false;
        diagnosisSubmitted = false;
        
        // Resets Desk UI
        PatientData.Instance.ResetPatientLetter();
        GrimoirePagesData.Instance.ResetGrimoire();
        treatmentPlanInteractables.ResetDiagnosisSheet();
            
        // Resets Herb Wall UI
        HerbCompendiumController.Instance.ResetCompendiumLeaflets();
        DrawerController.Instance.ResetHerbDrawerIcons();
        Inventory.Instance.ResetInventory();


        DialogueRunner.Instance.ResetDialogueRunner();
    }
    

    // ---------------------------------
    //      SWITCHING BETWEEN DAYS
    // ---------------------------------
	public void StartNewDay()
	{
        // Called from the animated text component on the new day object.
        // Is that messy? maybe.
		SceneManager.Instance.ResetScene();
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

    public void DebugJumpToDayNumber(int dayNumber)
    {
        runningTutorial = false;

        CreateNewGameData();
        InitialiseAllGameData();
        DayManager.Instance.JumpToDayNumber(dayNumber);
        OnNewDay();
    }
    
    void OnNewDay() // Resets scene entirely
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
    //      SWITCHING BETWEEN CLIENTS
    // ---------------------------------
    public void SummonClient()
    {
        // Called at the start of the day on the curtain, and whenever spawning the next patient.
        PatientData.Instance.RandomiseIncomingPatientData();
    }
    
    public void GoNextClient()
    {
        // Updates patient visit information
        _gameData.numberPatientsSeen++;
        PatientData.Instance.UpdateCurrentDayPatientsList();
        
        // Resets interactables and the scene
        ResetInteractablesBetweenClients();
        SceneManager.Instance.ResetScene();
        
        // Summons the client
        SummonClient();
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
    
    
    // ---------------------------------
    //      UPDATING GAME DATA
    // ---------------------------------
    public void SetClientData()
    {
        _gameData.savedActivePatient = PatientData.Instance.activePatientData;
        Debug.Log("The current active patient is saved to game data as: " + _gameData.savedActivePatient._patientName);
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
        _gameData.seenPatientsInCurrentDay.Add(treatedPatient);
    }
    
    public void GoResultsScreen()
    {
        // preparing for game data implementation. would pass more data here.
        ResultsCalculator.Instance.UpdateAndShowResultsScreen();
    }
    
    
    // ---------------------------------
    //      KEY GAME FUNCTIONS
    // ---------------------------------
    void FirstOpenGame()
    {
        // Set separately so that you can toggle tutorial bool off in debug!!
        runningTutorial = true;
    }
    
    public void GoMainMenu()
    {
        onMainMenu = true;
        SoundManager.Instance.PlayAudio(SoundManager.Instance.musicMainMenu);
        SceneManager.Instance.SetupMainMenu();
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

        Debug.Log("CURRENT DAY IS " + DayManager.Instance.currentDayNumber);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void RestartLevel()
    {
        // Maybe void GetInformationForDay(DayManager.Instance.currentDayNumber) ?
        
        // Reset the level so that the player can start the current day again
        Debug.Log("Resetting the current level...");
        // *TAG* - doesn't currently account for all elements,
        // since dialogue and popups aren't aligning on reset!!

        if (runningTutorial)
        {
            DialogueRunner.Instance.InitialiseDialogueRunner();
            DialogueRunner.Instance.ResetDialogueRunner();

            // Tries to find instances of instantiated tutorial objects
            InstantiateTutorialObjects();
        }
        
        OnNewDay(); 
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
            MenuManager.Instance.AlertMessage("noData");
            return;
        }
    }
}
