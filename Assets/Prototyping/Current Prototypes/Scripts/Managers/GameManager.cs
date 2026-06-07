using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool isPaused;
    private bool quitting;

    public bool beginningDay;
    public bool canStartDay;
    public bool reloaded;

    public bool ailmentChosen;
    public bool diagnosisSubmitted;
    public bool herbsSubmitted;

    //private bool debugging;
    
    public Button resetSceneButton;
    public DayTrigger curtainAccess;
    private DiagnosisSheetInteractables diagnosisSheetInteractables;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        quitting = false;


        diagnosisSheetInteractables = FindObjectOfType<DiagnosisSheetInteractables>();
        // maybe search for all components in scene instead and delete any not on Constant UI?
            // singleton trauma is REAL, people!!

        // Assigns function to the associated button
        resetSceneButton.onClick.AddListener(delegate { ResetScene(); });
        isPaused = false;
        reloaded = false;
    }

    void Start()
    {
        //BeginDay();
        ResetScene();
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pausing");
            if (isPaused)
            {
                MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenu);
            }
            else
            {
                MenuManager.Instance.OpenMenu(MenuManager.Instance.pauseMenu);
            }
            isPaused = !isPaused;
        }*/
        /*if (quitting)
        {
            Application.Quit();
        }*/
    }


    // Resets the scene
    public void ResetScene()        // NextDayResetScene
    {
        Debug.Log("Resetting Scene.");

            //if (reloaded)
            //{
                SceneManager.Instance.ResetScene();
            //}
        // read data from GameData (when I've written that,,,) and assign the day as required

        /* resets any elements in the scene that might have changed over the course of gameplay
            SCRIPTS THAT NEED RESET FUNCTIONS:
            - Herb wall
            - Inventory
            - Desk? idk, could be reset already when randomising the client,,
                way i have that set up rn, i think you can only really have
                one active patient at a time though,,,,
            - ((to be continued,,,,))
            */

            // Desk UI
            // Herb Wall UI
            // Client Window UI
        
            // Client Window UI
        curtainAccess.ResetCurtain();
        ResetClientProgress();
        ResultsScreen.Instance.ResetResultsScreen();
        beginningDay = true;
        canStartDay = false;

        DialogueRunner.Instance.ResetDialogueRunner();

        // RESETTING INTERACTABLES
        GrimoirePagesData.Instance.ResetGrimoire();
        //AilmentIconColourController.Instance.ResetAilmentIconBackground();
        diagnosisSheetInteractables.ResetDiagnosisSheet();

        // Resetting the herb wall and guide pages
        HerbalistGuidePages.Instance.ResetHerbalistGuide();
        Inventory.Instance.ResetInventory();
        HerbDrawersController.Instance.ResetHerbDrawerIcons();
        // CLOSE ANY OPEN DRAWERS

        // BEGINS TUTORIAL DIALOGUE
        BeginTutorial();
        //SceneManager.Instance.SetupInitialScene();
    }

    void BeginTutorial()
    {
        DialogueRunner.Instance.GetDialogue("tutorial");
    }

    public void NextClient()
    {
        ResetClientProgress();
        //UIManager / ScreenNav - .ResetCanvasLocations         ? maybe ?
    }
    
    void ResetClientProgress()
    {
        ailmentChosen = false;
        diagnosisSubmitted = false;
        Debug.Log("Client Progress reset.");
    }

    // Triggered by the curtain interaction.
    public void BeginDay()
    {
        ClientLetter.Instance.RandomiseIncomingClientLetter();
        //DialogueRunner.Instance.GetDialogue("patientArrive");
        // randomises the client and reads DayData to set the relevant information?
            // once randomised, mimic movement onto the screen? or just fade in?
        // Spawn Client once randomised (invoke 2.0f) 
            // Client/PatientData::
            // SpawnClient
    }


    // STILL NEED SOMETHING TO SUBMIT THE FULL AILMENT WITH!!

    // maybe trigger on submission to patient?
    /*public void SetClientAilment()
    {
        string client;
        foreach (Ailment a in AilmentData.Global.allAilments)
        {
            if (a.affectedClientName == ClientData.Instance.clientLetter.name)
            {
                client = a.affectedClientName;
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

    //
}
