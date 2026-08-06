using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // game states
    private bool isPaused;
    private bool quitting;
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

        // Assigns function to the associated button
        resetSceneButton.onClick.AddListener(delegate { ResetScene(); });

        // sets bools
        quitting = false;
        isPaused = false;
        reloaded = false;

        // for now set to run automatically, but will only go on tutorial lv when Jimmy is implemented
        runningTutorial = true;
    }

    void Start()
    {
        

        // opens the main menu canvas
        GoMainMenu();

        // ----------


        //BeginDay();
        //ResetScene();
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
    public void ResetScene()
    {
        Debug.Log("Resetting Scene.");

        // Resets all basic information in the scene
        SceneManager.Instance.ResetScene();             // Quest Log also resets in this SceneManager function
        ResetClientProgress();

        // Sets bools for the beginning of the day
        beginningDay = true;
        canStartDay = false;

        // Resets Client Window UI
        curtainAccess.ResetCurtain();

        // Resets Desk UI
        ClientLetter.Instance.ResetCLientLetter();
        GrimoirePagesData.Instance.ResetGrimoire();
        diagnosisSheetInteractables.ResetDiagnosisSheet();
            //AilmentIconColourController.Instance.ResetAilmentIconBackground();

        // Resets Herb Wall UI
        HerbalistGuidePages.Instance.ResetHerbalistGuide();
        HerbDrawersController.Instance.ResetHerbDrawerIcons();
        Inventory.Instance.ResetInventory();

        // Resets Extras
        ResultsScreen.Instance.ResetResultsScreen();
        DialogueRunner.Instance.ResetDialogueRunner();
        
        if (runningTutorial)
        {
            BeginTutorial();
        }
        else
        {
            Debug.Log("Skipping tutorial.");
        }
        // BEGINS TUTORIAL DIALOGUE
        //BeginTutorial();
        //SceneManager.Instance.SetupInitialScene();


                // for positions, it might be easier to add a reset function to the 
                // draggable components? then search for all of them and reset? idk.
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

    public void GoNextClient()
    {
        ClientLetter.Instance.UpdateLists();
        Debug.Log("Clients List is now " + ClientLetter.Instance.clientsList.Count + " entries long.");

        
        /*int index = ClientLetter.Instance.clientsList.FindIndex(ClientLetter.Instance.clientLetter);

        if (ClientLetter.Instance.clientsList[index] == ClientLetter.Instance.clientLetter)
        {
            ClientLetter.Instance.clientsList.Remove(ClientLetter.Instance.clientLetter);
        }
        if (ClientLetter.Instance.clientIconList[index] == ClientLetter.Instance.clientIcon)
        {
            ClientLetter.Instance.clientIconList.Remove(ClientLetter.Instance.clientIcon);
        }
        */

        // ClientLetter.Instance.clientsList.Remove(ClientLetter.Instance.clientLetter);
        // ClientLetter.Instance.treatedClientsList.Add(clientLetter);


        


        SceneManager.Instance.ResetScene();             // Quest Log also resets in this SceneManager function
        ResetClientProgress();

        // Sets bools for the beginning of the day
        //beginningDay = true;
        //canStartDay = false;


        // Resets Desk UI
        ClientLetter.Instance.ResetCLientLetter();
        GrimoirePagesData.Instance.ResetGrimoire();
        diagnosisSheetInteractables.ResetDiagnosisSheet();
            //AilmentIconColourController.Instance.ResetAilmentIconBackground();

        // Resets Herb Wall UI
        HerbalistGuidePages.Instance.ResetHerbalistGuide();
        HerbDrawersController.Instance.ResetHerbDrawerIcons();
        Inventory.Instance.ResetInventory();

        // Resets Extras
        ResultsScreen.Instance.ResetResultsScreen();
        DialogueRunner.Instance.ResetDialogueRunner();

        ClientLetter.Instance.RandomiseIncomingClientLetter();
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
    public void GoMainMenu()
    {
        onMainMenu = true;
        SceneManager.Instance.SetupMainMenu();
    }
}
