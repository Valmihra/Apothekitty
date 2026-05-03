using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool isPaused;

    public bool beginningDay;
    public bool canStartDay;
    //public bool canInteractWithCurtain


    public bool ailmentChosen;
    public bool diagnosisSubmitted;
    public bool herbsSubmitted;
    
    public Button resetSceneButton;
    public DayTrigger curtainAccess;

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
        // maybe search for all components in scene instead and delete any not on Constant UI?
            // singleton trauma is REAL, people!!

        // Assigns function to the associated button
        resetSceneButton.onClick.AddListener(delegate { ResetScene(); });
        isPaused = false;
    }

    void Start()
    {
        //BeginDay();
        ResetScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        }
    }


    // Resets the scene
    public void ResetScene()        // NextDayResetScene
    {
        Debug.Log("Resetting Scene.");
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
        beginningDay = true;
        canStartDay = false;
        //canInteractWithCurtain = false;

        DialogueRunner.Instance.GetDialogue("tutorial");
        //SceneManager.Instance.SetupInitialScene();
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
        Debug.Log("Ailment chosen? " + ailmentChosen + ". Diagnosis submitted? " + diagnosisSubmitted);
    }

    // Triggered by the curtain interaction.
    public void BeginDay()
    {
        ClientLetter.ClientsGlobal.RandomiseIncomingClientLetter();
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
            if (a.affectedClientName == ClientData.ClientsGlobal.clientLetter.name)
            {
                client = a.affectedClientName;
                GameData.CalculateResultFor(client);
            }
            else
            continue;
        }
    }*/


    //
}
