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
        public CanvasGroup mainCanvasGroupResultsScreen;
            private List<CanvasGroup> allMainCanvasGroups;

    [Header("Determinant Canvas Groups")]
        [SerializeField] private CanvasGroup canvasGroupPatientLetter;
        [SerializeField] private CanvasGroup canvasGroupTreatmentPlan;
        [SerializeField] private CanvasGroup canvasGroupGrimoire;
        [SerializeField] private CanvasGroup canvasGroupGrimoireNavigationArrows;
        [SerializeField] private CanvasGroup canvasGroupSubmitHerbCombinationButton;
        public CanvasGroup canvasGroupHerbDrawers;
    
    // Lists for canvasGroups when more detailed scene switches.
    private List<CanvasGroup> allCanvasesClientWindow;
    private List<CanvasGroup> allCanvasesHerbWall;
    private List<CanvasGroup> allCanvasesDesk;

    
    [Header("Scene Navigation References")]
        public Button switchDeskHerbButton;
        public Button switchDeskClientButton;
        [Space]
        public Image iconArrowLeft;
        public Image iconArrowRight;
        public Image iconArrowUp;
        public Image iconArrowDown;
        [Space]
        [SerializeField] private CanvasGroup canvasGroupSceneNavigationArrows;
    
        
    [Header("Current Display Information")]
    public Image patientImage;
    public CanvasGroup currentCanvasGroup;
    [SerializeField] private GameObject patientWindowPatientObject;
    private Vector2 defaultPatientImagePosition;
    
    // Tutorial and location booleans
    [HideInInspector] public bool onDesk;
    [HideInInspector] private bool herbWallActive;
    [HideInInspector] private bool firstVisitHerbWall;
    [HideInInspector] private bool firstVisitDesk;

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
            _instance = this;

        switchDeskHerbButton.onClick.AddListener(delegate {SwitchSceneDeskHerb(); });
        switchDeskClientButton.onClick.AddListener(delegate {SwitchSceneDeskClient(); });
        questLog = FindObjectOfType<QuestLog>();

        InitialiseSceneManager();
    }

    void InitialiseSceneManager()
    {
        InitialiseLists();
        defaultPatientImagePosition = patientWindowPatientObject.transform.position;
    }

    public void SetupMainMenu()
    {
        HideAllCanvases();                                  // may not even be necessary, but trying for now just in case,,
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
    
    void SetInitialBools()
    {
        // Sets the initial activation values for key elements in the game
        onDesk = false;
        herbWallActive = false;
        
        if (GameManager.Instance.runningTutorial)
        {
            firstVisitDesk = true;
            firstVisitHerbWall = true;
        }
    }
    
    public void SubmitAilment()
    {
        // Hides the navigation buttons on the grimoire and enables the diagnosis sheet.
        UIManager.Instance.DisableUI(canvasGroupGrimoireNavigationArrows);
        questLog.UpdateQuestLog();

        if (!GameManager.Instance.runningTutorial)
        {
            GetTreatmentPlan();
        }


    }
    
    public void GetTreatmentPlan()
    {
        // Enables the diagnosis sheet, fills it with the relevant information, and sends it to the front of the screen.
        UIManager.Instance.EnableUI(canvasGroupTreatmentPlan);

        canvasGroupTreatmentPlan.GetComponent<TreatmentPlanInteractables>().FillDiagnosisSheet();
        canvasGroupTreatmentPlan.GetComponent<RectTransform>().SetAsLastSibling();
		
		// Checks to see if needs to explain the more detailed treatment plan
		if (DayManager.Instance.currentDayNumber == 1)
		{
			bool firstClientDay1 = GameManager.Instance.GetCuredPatients() < 1;
            
			if (firstClientDay1)
			{
				MenuManager.Instance.OpenTutorialPopup("treatmentPlanExpanded");
			}
		}
        
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
    
    void InitialiseLists()
    {
        // Sets up basic lists to use when resetting scenes
        allMainCanvasGroups = new List<CanvasGroup>();
        allMainCanvasGroups.Add(mainCanvasGroupDesk);             // 0
        allMainCanvasGroups.Add(mainCanvasGroupHerbWall);         // 1
        allMainCanvasGroups.Add(mainCanvasGroupPatientWindow);     // 2
        allMainCanvasGroups.Add(mainCanvasGroupResultsScreen);

        SetupDetailedLists();
    }
    
    
    void SetupDetailedLists()
    {
        // LIST SETUP FOR MORE CONTROL OVER WHAT GETS DISPLAYED WHEN THE SCENE SWITCHES
        CanvasGroup[] tempCanvasGroups = mainCanvasGroupDesk.GetComponentsInChildren<CanvasGroup>();
        allCanvasesDesk = new List<CanvasGroup>(tempCanvasGroups);
        
        tempCanvasGroups = mainCanvasGroupPatientWindow.GetComponentsInChildren<CanvasGroup>();
        allCanvasesClientWindow = new List<CanvasGroup>(tempCanvasGroups);
            tempCanvasGroups = null;
            
        allCanvasesHerbWall = new List<CanvasGroup>();
        allCanvasesHerbWall.Add(mainCanvasGroupHerbWall);
        allCanvasesHerbWall.Add(canvasGroupSubmitHerbCombinationButton);
    }
    
    public void SwitchSceneDeskHerb()
    {
        // Enables the UI associated with the correct scene
        SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxSceneNavigation);
        
        if (onDesk)
        {
            if (GameManager.Instance.runningTutorial)
            {
                if (firstVisitHerbWall)
                {
                    DialogueRunner.Instance.GetDialogue("onHerbWall");
                    firstVisitHerbWall = false;
                }
            }
            
            UIManager.Instance.DisableUI(switchDeskClientButton.GetComponent<CanvasGroup>());
            UIManager.Instance.SpriteShift(switchDeskHerbButton.GetComponent<Image>(), iconArrowLeft.sprite);

            SetupUI(allCanvasesHerbWall);
        }
        else
        {
            UIManager.Instance.EnableUI(switchDeskClientButton.GetComponent<CanvasGroup>());
            UIManager.Instance.SpriteShift(switchDeskHerbButton.GetComponent<Image>(), iconArrowRight.sprite);

            SetupUI(allCanvasesDesk);
        }
        onDesk = !onDesk;
        
        SoundManager.Instance.GetAudioToReturnTo();
    }

    public void SwitchSceneDeskClient()
    {
        SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxSceneNavigation);
        
        if (onDesk)
        {
            UIManager.Instance.DisableUI(switchDeskHerbButton.GetComponent<CanvasGroup>());
            UIManager.Instance.SpriteShift(switchDeskClientButton.GetComponent<Image>(), iconArrowDown.sprite);

            SetupUI(allCanvasesClientWindow);
        }
        else
        {
            if (herbWallActive)
            {
                UIManager.Instance.EnableUI(switchDeskHerbButton.GetComponent<CanvasGroup>());
            }
            else
            {
                UIManager.Instance.DisableUI(switchDeskHerbButton.GetComponent<CanvasGroup>());
            }
            
            UIManager.Instance.SpriteShift(switchDeskClientButton.GetComponent<Image>(), iconArrowUp.sprite);
            SetupUI(allCanvasesDesk);

                if (GameManager.Instance.runningTutorial && firstVisitDesk)
                {
                    DialogueRunner.Instance.GetDialogue("desk");
                    firstVisitDesk = false;
                }

        }
        onDesk = !onDesk;
        
        SoundManager.Instance.GetAudioToReturnTo();
    }
    
    public void SetupInitialGameScene()
    {
        SetupUI(allCanvasesClientWindow);

        // Prevents navigating to other screens and hides irrelevant UI
        UIManager.Instance.DisableUI(canvasGroupSceneNavigationArrows);
        patientWindowPatientObject.SetActive(false);
        CleanupScene();
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
        
        // Sets the current canvas group
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
            patientWindowPatientObject.transform.position = new Vector2((defaultPatientImagePosition.x + 150f), defaultPatientImagePosition.y);
        }
        
        Sprite tempSprite = imageToUpdate.sprite;
        UIManager.Instance.SpriteShift(patientImage, tempSprite);

        patientWindowPatientObject.SetActive(true);
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

    public void ReturnToClient()
    {
        SetupUI(allCanvasesClientWindow);
        CleanupScene();
    }
}
