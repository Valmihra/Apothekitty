using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*
    [Header("All Interactable UI Types in Scene")]
    [Header("All Canvas Groups - Desk")]
    public CanvasGroup clientLetter;
    public CanvasGroup grimoire;
    public CanvasGroup diagnosisSheet;
    
    public CanvasGroup grimoireNavigation;
    [Header("All Canvas Groups - Herb Wall")]
    public CanvasGroup draggableHerbs;
    public CanvasGroup inventory;
    [Header("All Canvas Groups - Client Window")]
    public CanvasGroup dialogueWindow;
    public CanvasGroup characterIcon;
    */
    [Header("Main Canvas Groups")]
    public CanvasGroup deskGroup;
    public CanvasGroup herbWallGroup;
    public CanvasGroup clientWindowGroup;

    [Header("Determinant Canvas Groups")]
    public CanvasGroup diagnosisSheet;
    public CanvasGroup clientLetter;
    public CanvasGroup grimoire;
    public CanvasGroup grimoireNavigation;

    private Vector2 randomisedOrigin;
    private Vector2 position;
            /// if child canvasgroup tagged DETERMINANT, run check to see what should be displayed!!

    // Locators
    private Vector2 letterSpawnPoint;
    private Vector2 grimoireSpawnPoint;
    private Vector2 diagnosisSheetSpawnPoint;
    //private Vector2 herbalistGuideSpawnPoint;     if hanging, no need for this!

        ////
    // SHOULD CONTAIN EVERY SWITCHABLE CANVASGROUP IN THE GAME
    private List<CanvasGroup> allMainCanvases;
        ////

    // Lists for canvasGroups when more detailed scene switches.
    private List<CanvasGroup> allCanvasesClientWindow; //clientWindow;
    private List<CanvasGroup> allCanvasesHerbWall; //herbWall;
    private List<CanvasGroup> allCanvasesDesk; //allCanvasesDesk;

    //private List<CanvasGroup> canvasListTwo;
    private List<Vector2> canvasSpawnPoints;

    private bool onDesk;

    public string selectedAilment;

    [Header("Button References")]
    public Button switchDeskHerb;
    public Button switchDeskClient;
    public Image arrowLeft;
    public Image arrowRight;
    public Image arrowUp;
    public Image arrowDown;

    public GrimoireNavigation grimoireNavScript;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        onDesk = true;
        
        grimoireNavScript = FindObjectOfType<GrimoireNavigation>();

        switchDeskHerb.onClick.AddListener(delegate {SwitchSceneDeskHerb(); });
        switchDeskClient.onClick.AddListener(delegate {SwitchSceneDeskClient(); });

        InitialiseLists();
        //SetupUI(allCanvasesDesk);
        SetupInitialScene();
    }


    //  ---
    // Scripts responsible for toggling between canvas groups
    public void EnableUI(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisableUI(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    // ---

    // Hides the Diagnosis sheet and enables all other canvas groups in the list
    /*void InitialiseSceneUI()
    {
        Debug.Log("Initialising UI in scene");
        //DisableUI()
        foreach (CanvasGroup c in allMainCanvases)
        {
            PlaceUI(c);

            if (c == diagnosisSheet)
            {
                DisableUI(c);
            }
            else
            {
                EnableUI(c);
            }
        }

        DisableUI(draggableHerbs);
        DisableUI(inventory);
    }*/
    


    // Determines the correct position for each canvas group to be enabled at during the setup phase
        // Useful later on, maybe letters arrive on the desk in a certain area. Maybe the patients pass
        // them over and the location is randomised slightly within an area radius? Could mimic sliding
        // the sheets over a desk? 
    void PlaceUI(CanvasGroup canvasGroup)
    {
        int placementNumber = allMainCanvases.IndexOf(canvasGroup);

            
            canvasGroup.GetComponent<RectTransform>().anchoredPosition = canvasSpawnPoints[placementNumber];
            return;
       
    }

    // Hides the navigation buttons on the grimoire and enables the diagnosis sheet.
        // Also currently removes the client letter, but am looking to flesh this out better (see PlaceUI)
    public void SubmitAilment()
    {
        /*float radius = 1f;
        
        randomisedOrigin = Random.insideUnitCircle * radius;
        randomisedOrigin += diagnosisSheet.GetComponent<RectTransform>().anchoredPosition;
        diagnosisSheet.GetComponent<RectTransform>().anchoredPosition = randomisedOrigin;
        */
        
        DisableUI(grimoireNavigation);
        EnableUI(diagnosisSheet);

        /*Invoke(nameof(MovePosition), 0.2f);
        randomisedOrigin = position;
        diagnosisSheet.GetComponent<RectTransform>().anchoredPosition = position;
        Invoke(nameof(MovePosition), 0.2f);
        randomisedOrigin = position;
        diagnosisSheet.GetComponent<RectTransform>().anchoredPosition = position;
        Invoke(nameof(SetPosition), 0.2f);
        */
        diagnosisSheet.GetComponent<DiagnosisSheetInteractables>().FillSheet();

        diagnosisSheet.GetComponent<RectTransform>().SetAsLastSibling();
    }

    void MovePosition()
    {
        position = Vector2.Lerp(randomisedOrigin, diagnosisSheetSpawnPoint, Random.value);
        //MimicAnimation();
    }

    void SetPosition()
    {
        diagnosisSheet.GetComponent<RectTransform>().anchoredPosition = diagnosisSheetSpawnPoint;
    }

    public void SubmitDiagnosis()
    {
        Debug.Log("Submission registered.");
        Debug.Log("This command would bring up UI to indicate the submission went through, and also prompt the player to look at the next area (herb wall)");
        
        // PlaceUI(herbalistGuide)

        // OOUHHH if i set it up in world space, can move camera 
        // to set points and mimic movement? but then have to 
        // address UI again,, smth to bring up with the g ang
    }


    /*void InitialiseLists()
    {
        allMainCanvases = new List<CanvasGroup>();
        allMainCanvases.Add(clientLetter);   // 0
        allMainCanvases.Add(grimoire);       // 1
        allMainCanvases.Add(diagnosisSheet); // 2

        allMainCanvases.Add(draggableHerbs);
        allMainCanvases.Add(inventory);
        allMainCanvases.Add(dialogueWindow);
        allMainCanvases.Add(characterIcon);

        //canvasListTwo = new List<CanvasGroup>();
        //canvasListTwo.Add()

        letterSpawnPoint = clientLetter.GetComponent<RectTransform>().anchoredPosition;
        grimoireSpawnPoint = grimoire.GetComponent<RectTransform>().anchoredPosition;
        diagnosisSheetSpawnPoint = diagnosisSheet.GetComponent<RectTransform>().anchoredPosition;

        canvasSpawnPoints = new List<Vector2>();
        canvasSpawnPoints.Add(letterSpawnPoint);            // 0
        canvasSpawnPoints.Add(grimoireSpawnPoint);          // 1
        canvasSpawnPoints.Add(diagnosisSheetSpawnPoint);    // 2



            // LISTS OF UI FOR EACH SEPARATE SCENE

        allCanvasesDesk = new List<CanvasGroup>();
            allCanvasesDesk.Add(clientLetter);
            allCanvasesDesk.Add(grimoire);
            allCanvasesDesk.Add(diagnosisSheet);
        herbWall = new List<CanvasGroup>();
            herbWall.Add(draggableHerbs);
        clientWindow = new List<CanvasGroup>();
            clientWindow.Add(dialogueWindow);

    }*/

    void InitialiseLists()
    {
        allMainCanvases = new List<CanvasGroup>();
        allMainCanvases.Add(deskGroup);             // 0
        allMainCanvases.Add(herbWallGroup);         // 1
        allMainCanvases.Add(clientWindowGroup);     // 2

        letterSpawnPoint = clientLetter.GetComponent<RectTransform>().anchoredPosition;
        grimoireSpawnPoint = grimoire.GetComponent<RectTransform>().anchoredPosition;
        diagnosisSheetSpawnPoint = diagnosisSheet.GetComponent<RectTransform>().anchoredPosition;

        //canvasSpawnPoints = new List<Vector2>();
        //canvasSpawnPoints.Add(letterSpawnPoint);            // 0
        //canvasSpawnPoints.Add(grimoireSpawnPoint);          // 1
        //canvasSpawnPoints.Add(diagnosisSheetSpawnPoint);    // 2



            // LISTS OF UI FOR MORE CONTROL OVER WHAT TO DISPLAY ON SCENE SWITCHES
        CanvasGroup[] tempCanvasGroups = deskGroup.GetComponentsInChildren<CanvasGroup>();
            allCanvasesDesk = new List<CanvasGroup>(tempCanvasGroups);
        //tempCanvasGroups = herbWallGroup.GetComponentsInChildren<CanvasGroup>();
            //allCanvasesHerbWall = new List<CanvasGroup>(tempCanvasGroups);
            allCanvasesHerbWall = new List<CanvasGroup>();
            allCanvasesHerbWall.Add(herbWallGroup);
        tempCanvasGroups = clientWindowGroup.GetComponentsInChildren<CanvasGroup>();
            allCanvasesClientWindow = new List<CanvasGroup>(tempCanvasGroups);
            
            tempCanvasGroups = null;

    }

    // Shows/hides secondary navigation arrow and changes the button's sprite to reflect the correct direction
    // Enables the UI associated with the correct scene
    public void SwitchSceneDeskHerb()
    {
        if (onDesk)
        {
            DisableUI(switchDeskClient.GetComponent<CanvasGroup>());
            switchDeskHerb.GetComponent<Image>().sprite = arrowLeft.sprite;

            SetupUI(allCanvasesHerbWall);
        }
        else
        {
            EnableUI(switchDeskClient.GetComponent<CanvasGroup>());
            switchDeskHerb.GetComponent<Image>().sprite = arrowRight.sprite;

            SetupUI(allCanvasesDesk);
        }
        onDesk = !onDesk;
    }

    public void SwitchSceneDeskClient()
    {
        if (onDesk)
        {
            DisableUI(switchDeskHerb.GetComponent<CanvasGroup>());
            switchDeskClient.GetComponent<Image>().sprite = arrowDown.sprite;

            SetupUI(allCanvasesClientWindow);
        }
        else
        {
            EnableUI(switchDeskHerb.GetComponent<CanvasGroup>());
            switchDeskClient.GetComponent<Image>().sprite = arrowUp.sprite;

            SetupUI(allCanvasesDesk);
        }
        onDesk = !onDesk;
    }

    void SetupInitialScene()
    {
        // gets spawnpoints for UI 
        letterSpawnPoint = clientLetter.GetComponent<RectTransform>().anchoredPosition;
        grimoireSpawnPoint = grimoire.GetComponent<RectTransform>().anchoredPosition;
        diagnosisSheetSpawnPoint = diagnosisSheet.GetComponent<RectTransform>().anchoredPosition;
        /*// foreach (CanvasGroup hide in allMainCanvases)
        {
            DisableUI(hide);
        }
        
        EnableUI(deskGroup);
        DisableUI(diagnosisSheet);*/
        SetupUI(allCanvasesDesk);
    }


    public void SetupUI(List<CanvasGroup> canvasGroupList)
    {
        // Hides all UI
        foreach (CanvasGroup hide in allMainCanvases)
        {
            DisableUI(hide);
        }

        foreach (CanvasGroup c in canvasGroupList)
        {
            EnableUI(c);

            if (canvasGroupList == allCanvasesDesk)
            {
                if (!grimoireNavScript.ailmentChosen)
                {
                    DisableUI(diagnosisSheet);
                }
                else
                {
                    EnableUI(diagnosisSheet);
                }
            }
        }
    }

    public void UpdateAilment(string ailment)
    {
        selectedAilment = ailment;
    }
}
