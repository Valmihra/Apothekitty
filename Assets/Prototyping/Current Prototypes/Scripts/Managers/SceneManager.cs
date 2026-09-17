using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    // SHOULD CONTAIN EVERY SWITCHABLE MAIN CANVASGROUP IN THE GAME
    [Header("Main Canvas Groups")]
    public CanvasGroup mainCanvasGroupDesk;
    public CanvasGroup mainCanvasGroupHerbWall;
    public CanvasGroup mainCanvasGroupPatientWindow;
    // public CanvasGroup mainCanvasGroupResultsScreen;         // *TAG* - Should probably add this here for consistency, since it's not necessarily a menu.
        private List<CanvasGroup> allMainCanvasGroups;

    [Header("Determinant Canvas Groups")]   // *TAG* - for future cleanliness:: if child canvasgroup tagged DETERMINANT, maybe check what should be displayed(??)
    [SerializeField] private CanvasGroup canvasGroupPatientLetter;
    [SerializeField] private CanvasGroup canvasGroupTreatmentPlan;
    [SerializeField] private CanvasGroup canvasGroupGrimoire;
    [SerializeField] private CanvasGroup canvasGroupGrimoireNavigationArrows;
    [SerializeField] private CanvasGroup canvasGroupSubmitHerbCombinationButton;

    public CanvasGroup canvasGroupHerbDrawers;
    // public CanvasGroup clientWindowClientIcon;
    // public CanvasGroup canvasGroupHerbDrawers;          // MIGHT NEED TO SORT OUT A CHECK SOON FOR DAY + NUMBER DRAWERS TO DISPLAY TO PLAYER
    
        // Lists for canvasGroups when more detailed scene switches.
        private List<CanvasGroup> allCanvasesClientWindow;      //clientWindow;
        private List<CanvasGroup> allCanvasesHerbWall;          //herbWall;
        private List<CanvasGroup> allCanvasesDesk;              //allCanvasesDesk;

    [SerializeField] private GameObject patientWindowPatientObject;

    [Header("Scene Navigation Reference")]
    [SerializeField] private CanvasGroup canvasGroupSceneNavigationArrows;

    [Header("Navigation Button References")]
    public Button switchDeskHerbButton;
    public Button switchDeskClientButton;
    public Image iconArrowLeft;
    public Image iconArrowRight;
    public Image iconArrowUp;
    public Image iconArrowDown;
    
    // Movement vectors
    private Vector2 randomisedOrigin;
    private Vector2 defaultPatientImagePosition;
    
    
    // Information for the current display
    public CanvasGroup currentCanvasGroup;
    public Image clientImage;

    public bool onDesk;
    private bool herbWallActive;
    private bool firstVisitHerbWall;
    private bool firstVisitDesk;

    public string selectedAilment;

    private QuestLog questLog;

    private static SceneManager _instance;
    public static SceneManager Instance
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

        switchDeskHerbButton.onClick.AddListener(delegate {SwitchSceneDeskHerb(); });
        switchDeskClientButton.onClick.AddListener(delegate {SwitchSceneDeskClient(); });
        questLog = FindObjectOfType<QuestLog>();

        InitialiseSceneManager();
    }

    
    void Start()
    {       
            //
    }

    // update function here to handle 'animated camera movement'        DO WE WANT THAT?    QUIERES??
    // if (switching scenes)
    /*void Update()
    {
        if (switchingScenes)
        {
            if (currentCanvasGroup == mainCanvasGroupDesk)
            {

            }
            else if (currentCanvasGroup == mainCanvasGroupPatientWindow)
            {
                ,,
            }
            else
            {
                ,,
            }

            set up screens next to each other in the scene and have
            camera   physically   move towards target location!!
        }
    }*/

    void InitialiseSceneManager()
    {
        InitialiseLists();
        defaultPatientImagePosition = patientWindowPatientObject.transform.position;
    }

    public void SetupMainMenu()
    {
        HideAllCanvases();                              // may not even be necessary, but trying for now just in case,,
        currentCanvasGroup = mainCanvasGroupPatientWindow;   // preemptive fix. temporary.
        MenuManager.Instance.OpenMenu(MenuManager.Instance.mainMenuCanvasGroup);
    }
    
    public void ResetScene()
    {
        questLog.ResetQuestLog();

        SetInitialBools();
        SetupInitialGameScene();
        patientWindowPatientObject.transform.position = defaultPatientImagePosition;
    }

    // Sets the initial activation values for key elements in the game
    void SetInitialBools()
    {
        onDesk = false;
        herbWallActive = false;
        
        if (GameManager.Instance.runningTutorial)
        {
            firstVisitDesk = true;
            firstVisitHerbWall = true;
        }
        // else
        // {
        //     Debug.Log("Debug- bools set.");
        // }
    }

    // Determines the correct position for each canvas group to be enabled at during the setup phase
        // Useful later on, maybe letters arrive on the desk in a certain area. Maybe the clients pass
        // them over and the location is randomised slightly within an area radius? Could mimic sliding
        // the sheets over a desk? 
    /*void PlaceUI(CanvasGroup canvasGroup)
    {
        int placementNumber = allMainCanvasGroups.IndexOf(canvasGroup);
            canvasGroup.GetComponent<RectTransform>().anchoredPosition = canvasSpawnPoints[placementNumber];
            return;
       
    }*/

    /*void MovePosition()
    {
        position = Vector2.Lerp(randomisedOrigin, diagnosisSheetSpawnPoint, Random.value);
    }

    void SetPosition()
    {
        diagnosisSheet.GetComponent<RectTransform>().anchoredPosition = diagnosisSheetSpawnPoint;
    }*/



    // Hides the navigation buttons on the grimoire and enables the diagnosis sheet.
    public void SubmitAilment()
    {
        // prevents further navigation in grimoire and brings out diagnosis sheet
        UIManager.Instance.DisableUI(canvasGroupGrimoireNavigationArrows);
        questLog.UpdateQuestLog();

        if (!GameManager.Instance.runningTutorial)
        {
            GetDiagnosisSheet();
        }


    }

    // Enables the diagnosis sheet, fills it with the relevant information, and sends it to the front of the screen.
    public void GetDiagnosisSheet()
    {
        UIManager.Instance.EnableUI(canvasGroupTreatmentPlan);

        canvasGroupTreatmentPlan.GetComponent<TreatmentPlanInteractables>().FillDiagnosisSheet();
        canvasGroupTreatmentPlan.GetComponent<RectTransform>().SetAsLastSibling();

        // MenuManager.Instance.OpenTutorialPopup("diagnosisSheet");
    }

    public void SubmitDiagnosis()
    {
        Debug.Log("Treatment plan has been submitted.");
        questLog.UpdateQuestLog();

        if (GameManager.Instance.runningTutorial)
        {
            DialogueRunner.Instance.GetDialogue("treatmentPlanSubmitted");
        }
        else
        {
            UnlockHerbWall();
        }
    }

    /*public void SubmitHerbs()
    {
        questLog.FinishQuestLog();
    }*/

    // TODO: Fully change the submit treatment to send to clientwindow and show popup for next client/next day.
        // separate the ResultsScreen from the
    
        
    /*public void ResultsScreenPrep()
    {
        questLog.FinishQuestLog();
        UIManager.Instance.DisableUI(questLog.GetComponent<CanvasGroup>());
        UIManager.Instance.DisableUI(canvasGroupSceneNavigationArrows);
        UIManager.Instance.DisableUI(canvasGroupSubmitHerbCombinationButton);

        if (PatientData.Instance.currentDayPatientsList.Count == 1)
        {
            ResultsScreen.Instance.UpdateResultsScreenButtonText(true);
        }
        else
        {
            ResultsScreen.Instance.UpdateResultsScreenButtonText(false);
        }
    }*/

    // Sets up basic lists to use when resetting scenes
    void InitialiseLists()
    {
        allMainCanvasGroups = new List<CanvasGroup>();
        allMainCanvasGroups.Add(mainCanvasGroupDesk);             // 0
        allMainCanvasGroups.Add(mainCanvasGroupHerbWall);         // 1
        allMainCanvasGroups.Add(mainCanvasGroupPatientWindow);     // 2

        SetupDetailedLists();
    }
    
    // LIST SETUP FOR MORE CONTROL OVER WHAT GETS DISPLAYED WHEN THE SCENE SWITCHES
    void SetupDetailedLists()
    {
        CanvasGroup[] tempCanvasGroups = mainCanvasGroupDesk.GetComponentsInChildren<CanvasGroup>();
        allCanvasesDesk = new List<CanvasGroup>(tempCanvasGroups);
        
        tempCanvasGroups = mainCanvasGroupPatientWindow.GetComponentsInChildren<CanvasGroup>();
        allCanvasesClientWindow = new List<CanvasGroup>(tempCanvasGroups);
            tempCanvasGroups = null;
            
        allCanvasesHerbWall = new List<CanvasGroup>();
        allCanvasesHerbWall.Add(mainCanvasGroupHerbWall);
        allCanvasesHerbWall.Add(canvasGroupSubmitHerbCombinationButton);

                //tempCanvasGroups = mainCanvasGroupHerbWall.GetComponentsInChildren<CanvasGroup>();
                //allCanvasesHerbWall = new List<CanvasGroup>(tempCanvasGroups);
    }
    
    // Enables the UI associated with the correct scene
    public void SwitchSceneDeskHerb()
    {
        if (onDesk)
        {
            if (GameManager.Instance.runningTutorial)
            {
                if (firstVisitHerbWall)
                {
                    DialogueRunner.Instance.GetDialogue("onHerbWall");
                    //UIManager.Instance.EnableUI(canvasGroupSubmitHerbCombinationButton);
                    firstVisitHerbWall = false;
                }
            }
            //else if 
            UIManager.Instance.DisableUI(switchDeskClientButton.GetComponent<CanvasGroup>());
            // switchDeskClientButton.gameObject.enabled = false;
            UIManager.Instance.SpriteShift(switchDeskHerbButton.GetComponent<Image>(), iconArrowLeft.sprite);

            SetupUI(allCanvasesHerbWall);
        }
        else
        {
            UIManager.Instance.EnableUI(switchDeskClientButton.GetComponent<CanvasGroup>());
            // switchDeskClientButton.SetActive(true);
            UIManager.Instance.SpriteShift(switchDeskHerbButton.GetComponent<Image>(), iconArrowRight.sprite);

            SetupUI(allCanvasesDesk);
        }
        onDesk = !onDesk;
    }

    public void SwitchSceneDeskClient()
    {
        if (onDesk)
        {
            UIManager.Instance.DisableUI(switchDeskHerbButton.GetComponent<CanvasGroup>());
            // switchDeskHerbButton.SetActive(false);
            UIManager.Instance.SpriteShift(switchDeskClientButton.GetComponent<Image>(), iconArrowDown.sprite);

            SetupUI(allCanvasesClientWindow);
        }
        else
        {
            if (herbWallActive)
            {
                UIManager.Instance.EnableUI(switchDeskHerbButton.GetComponent<CanvasGroup>());
                // switchDeskHerbButton.SetActive(true);
            }
            else
            {
                UIManager.Instance.DisableUI(switchDeskHerbButton.GetComponent<CanvasGroup>());
                // switchDeskHerbButton.SetActive(false);
            }
            
            UIManager.Instance.SpriteShift(switchDeskClientButton.GetComponent<Image>(), iconArrowUp.sprite);
            SetupUI(allCanvasesDesk);

                if (firstVisitDesk)
                {
                    DialogueRunner.Instance.GetDialogue("desk");
                    firstVisitDesk = false;
                }
        }
        onDesk = !onDesk;
    }
    
    public void SetupInitialGameScene()
    {
        SetupUI(allCanvasesClientWindow);

        // Prevents navigating to other screens and hides irrelevant UI
        UIManager.Instance.DisableUI(canvasGroupSceneNavigationArrows);
        HideClient();
        CleanupScene();
    }

    void HideClient()
    {
        patientWindowPatientObject.SetActive(false);
        // clientWindowClientIcon.alpha = 0;
    }

    void CleanupScene()
    {
        // Resets direction of arrows
        UIManager.Instance.SpriteShift(switchDeskClientButton.GetComponent<Image>(), iconArrowDown.sprite);
        UIManager.Instance.SpriteShift(switchDeskHerbButton.GetComponent<Image>(), iconArrowRight.sprite);

        UIManager.Instance.DisableUI(switchDeskHerbButton.GetComponent<CanvasGroup>());
        UIManager.Instance.DisableUI(questLog.GetComponent<CanvasGroup>());
        UIManager.Instance.DisableUI(canvasGroupSubmitHerbCombinationButton);
    }
    
    // Hides all UI
    void HideAllCanvases()
    {
        foreach (CanvasGroup hide in allMainCanvasGroups)
        {
            UIManager.Instance.DisableUI(hide);
        }
    }

    public void SetupUI(List<CanvasGroup> canvasGroupList)
    {
        HideAllCanvases();

        foreach (CanvasGroup c in canvasGroupList)
        {
            UIManager.Instance.EnableUI(c);

            if (canvasGroupList == allCanvasesDesk)
            {
                if (!GameManager.Instance.ailmentSubmitted)
                {
                    UIManager.Instance.DisableUI(canvasGroupTreatmentPlan);
                }
                else
                {
                    UIManager.Instance.EnableUI(canvasGroupTreatmentPlan);
                }
            }
        }
        // for enabling/disabling interaction when entering a menu
        currentCanvasGroup = canvasGroupList == allCanvasesDesk ? mainCanvasGroupDesk : canvasGroupList == allCanvasesClientWindow ? mainCanvasGroupPatientWindow : mainCanvasGroupHerbWall;
        MenuManager.Instance.HideAllMenuCanvases();
    }

    public void UpdateAilment(string ailment)
    {
        selectedAilment = ailment;
    }

    public void ShowPatient(Image imageToUpdate)
    {
        if (DayManager.Instance.currentDayNumber == 0)
        {
            patientWindowPatientObject.transform.position = new Vector2(defaultPatientImagePosition.x, (defaultPatientImagePosition.y - 200f));
        }
        
        Sprite tempSprite = imageToUpdate.sprite;
        UIManager.Instance.SpriteShift(clientImage, tempSprite);

        patientWindowPatientObject.SetActive(true);
        // clientWindowClientIcon.alpha = 1f;
        // Enables navigation, but only to the desk
        //UIManager.Instance.EnableUI(canvasGroupSceneNavigationArrows);
        //UIManager.Instance.DisableUI(switchDeskHerbButton.GetComponent<CanvasGroup>());




        // once randomised, mimic movement onto the screen? or just fade in?
        // Spawn Client once randomised (invoke 2.0f) 
        // Client/SinglePatientData::
        // SpawnPatient
    }

    public void EnableGameplay()
    {
        UIManager.Instance.EnableUI(canvasGroupSceneNavigationArrows);
        UIManager.Instance.EnableUI(switchDeskClientButton.GetComponent<CanvasGroup>());
        UIManager.Instance.EnableUI(questLog.GetComponent<CanvasGroup>());
    }

    public void UnlockHerbWall()
    {
        Debug.Log("Unlocking Herb Wall.");
        
        UIManager.Instance.EnableUI(switchDeskHerbButton.GetComponent<CanvasGroup>());

        if (GameManager.Instance.runningTutorial)
        {
            MenuManager.Instance.OpenTutorialPopup("toHerbWall");
        }
        
        herbWallActive = true;
        
    }

    public void OnReturnToGame()
    {
        if ((onDesk) && (GameManager.Instance.ailmentSubmitted))
        {
            UIManager.Instance.EnableInteraction(canvasGroupTreatmentPlan);
        }
    }

    public void OnGameplaySuspended()
    {
        if ((onDesk) && (GameManager.Instance.ailmentSubmitted))
        {
            UIManager.Instance.DisableInteraction(canvasGroupTreatmentPlan);
        }
    }


    // gets spawnpoints for UI (mainly useful later)
    /*void GenerateSpawnpoints()
    {
        letterSpawnPoint = canvasGroupPatientLetter.GetComponent<RectTransform>().anchoredPosition;
        grimoireSpawnPoint = canvasGroupGrimoire.GetComponent<RectTransform>().anchoredPosition;
        diagnosisSheetSpawnPoint = canvasGroupTreatmentPlan.GetComponent<RectTransform>().anchoredPosition;
        //herbalistGuideSpawnPoint = herbGuide.GetComponent<RectTransform>().anchoredPosition;  
    }*/

    public void ReturnToClient()
    {
        SetupUI(allCanvasesClientWindow);
		// RESET THE NAVIGATION ARROWS!!!
        CleanupScene();
        
        // DialogueRunner.Instance.GetDialogue("submit herbs to client");
        
        
    }
}
