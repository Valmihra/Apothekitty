using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    // SHOULD CONTAIN EVERY SWITCHABLE CANVASGROUP IN THE GAME
    [Header("Main Canvas Groups")]
    public CanvasGroup deskGroup;
    public CanvasGroup herbWallGroup;
    public CanvasGroup clientWindowGroup;
        private List<CanvasGroup> allMainCanvases;

    [Header("Determinant Canvas Groups")]   /// if child canvasgroup tagged DETERMINANT, maybe check what should be displayed(??)
    public CanvasGroup clientLetter;
    public CanvasGroup grimoire;
    public CanvasGroup diagnosisSheet;
    public CanvasGroup grimoireNavigation;
    public CanvasGroup clientWindowClientIcon;
    public CanvasGroup herbDrawers;
    public CanvasGroup submissionButton;
        // Lists for canvasGroups when more detailed scene switches.
        private List<CanvasGroup> allCanvasesClientWindow;      //clientWindow;
        private List<CanvasGroup> allCanvasesHerbWall;          //herbWall;
        private List<CanvasGroup> allCanvasesDesk;              //allCanvasesDesk;

    [Header("Scene Navigation Reference")]
    public CanvasGroup sceneNavigation;

    [Header("Navigation Button References")]
    public Button switchDeskHerb;
    public Button switchDeskClient;
    public Image arrowLeft;
    public Image arrowRight;
    public Image arrowUp;
    public Image arrowDown;
    
    // Movement vectors
    private Vector2 randomisedOrigin;
    private Vector2 position;    

    // Locators
    private Vector2 letterSpawnPoint;
    private Vector2 grimoireSpawnPoint;
    private Vector2 diagnosisSheetSpawnPoint;
    private Vector2 herbalistGuideSpawnPoint;     //if hanging, no need for this!
        private List<Vector2> canvasSpawnPoints;   
    
    
    
    // Information for the current display
    public CanvasGroup currentCanvasGroup;
    public Image clientImage;

        //private bool diagnosisSubmitted;
    //private bool spawnSet;
    public bool onDesk;
    private bool herbWallActive;
    private bool firstVisitHerbWall;
    public string selectedAilment;

    bool firstVisitDesk;

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
        //if (_instance = null)
        //{
            _instance = this;
            firstVisitDesk = true;
        //}

        //spawnSet = false;
    }

    
    void Start()
    {
        SetStartingScreen();

        switchDeskHerb.onClick.AddListener(delegate {SwitchSceneDeskHerb(); });
        switchDeskClient.onClick.AddListener(delegate {SwitchSceneDeskClient(); });

        
            //if (!spawnSet)
            //{
                //GenerateSpawnpoints();
                InitialiseLists();
            //}
        //SetupUI(allCanvasesDesk);
        SetupInitialScene();
    }

    void SetStartingScreen()
    {
        // DESIRED SCREEN TO SETUP FIRST
        onDesk = false;
        herbWallActive = false;
        firstVisitHerbWall = true;
    }

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
        // looking to flesh this out better (see PlaceUI)
    public void SubmitAilment()
    {
        GameManager.Instance.ailmentChosen = true;

        // prevents further navigation in grimoire and brings out diagnosis sheet
        UIManager.Instance.DisableUI(grimoireNavigation);
        UIManager.Instance.EnableUI(diagnosisSheet);

        diagnosisSheet.GetComponent<DiagnosisSheetInteractables>().FillSheet();
        diagnosisSheet.GetComponent<RectTransform>().SetAsLastSibling();

        MenuManager.Instance.TutorialPopup("diagnosisSheet");
    }

    void MovePosition()
    {
        position = Vector2.Lerp(randomisedOrigin, diagnosisSheetSpawnPoint, Random.value);
    }

    void SetPosition()
    {
        diagnosisSheet.GetComponent<RectTransform>().anchoredPosition = diagnosisSheetSpawnPoint;
    }

    public void SubmitDiagnosis()
    {
        Debug.Log("Submission registered.");
        Debug.Log("Unlocking Herb Wall.");
        UnlockHerbWall();

        MenuManager.Instance.TutorialPopup("herbalistGuide");
        //diagnosisSubmitted = true;
        // WHEN SET UP, PLACE UI WHERE RELEVANT!
        // PlaceUI(herbalistGuide)

        // OOUHHH if i set it up in world space, can move camera 
        // to set points and mimic movement? but then have to 
        // address UI again,, smth to bring up with the g ang
    }

    // Sets up basic lists to use when resetting scenes
    void InitialiseLists()
    {
        allMainCanvases = new List<CanvasGroup>();
        allMainCanvases.Add(deskGroup);             // 0
        allMainCanvases.Add(herbWallGroup);         // 1
        allMainCanvases.Add(clientWindowGroup);     // 2

        letterSpawnPoint = clientLetter.GetComponent<RectTransform>().anchoredPosition;
        grimoireSpawnPoint = grimoire.GetComponent<RectTransform>().anchoredPosition;
        diagnosisSheetSpawnPoint = diagnosisSheet.GetComponent<RectTransform>().anchoredPosition;

        canvasSpawnPoints = new List<Vector2>();
        canvasSpawnPoints.Add(letterSpawnPoint);            // 0
        canvasSpawnPoints.Add(grimoireSpawnPoint);          // 1
        canvasSpawnPoints.Add(diagnosisSheetSpawnPoint);    // 2
        //canvasSpawnPoints.Add(herbalistGuideSpawnPoint);  // 3


        SetupDetailedLists();
    }
        /*    // LISTS OF UI FOR MORE CONTROL OVER WHAT TO DISPLAY ON SCENE SWITCHES
        CanvasGroup[] tempCanvasGroups = deskGroup.GetComponentsInChildren<CanvasGroup>();
            allCanvasesDesk = new List<CanvasGroup>(tempCanvasGroups);
            //tempCanvasGroups = herbWallGroup.GetComponentsInChildren<CanvasGroup>();
                //allCanvasesHerbWall = new List<CanvasGroup>(tempCanvasGroups);
            allCanvasesHerbWall = new List<CanvasGroup>();
            allCanvasesHerbWall.Add(herbWallGroup);
        tempCanvasGroups = clientWindowGroup.GetComponentsInChildren<CanvasGroup>();
            allCanvasesClientWindow = new List<CanvasGroup>(tempCanvasGroups);
            
            tempCanvasGroups = null;*/

    

    // Sets up detailed lists that can be used to control what is displayed on scene switches
    void SetupDetailedLists()
    {
        CanvasGroup[] tempCanvasGroups = deskGroup.GetComponentsInChildren<CanvasGroup>();
        allCanvasesDesk = new List<CanvasGroup>(tempCanvasGroups);

        allCanvasesHerbWall = new List<CanvasGroup>();
        allCanvasesHerbWall.Add(herbWallGroup);

        tempCanvasGroups = clientWindowGroup.GetComponentsInChildren<CanvasGroup>();
        allCanvasesClientWindow = new List<CanvasGroup>(tempCanvasGroups);
            
            tempCanvasGroups = null;
                //tempCanvasGroups = herbWallGroup.GetComponentsInChildren<CanvasGroup>();
                //allCanvasesHerbWall = new List<CanvasGroup>(tempCanvasGroups);
    }



    // Shows/hides secondary navigation arrow and changes the button's sprite to reflect the correct direction
    // Enables the UI associated with the correct scene
    public void SwitchSceneDeskHerb()
    {
        if (onDesk)
        {
            if (firstVisitHerbWall)
            {
                // could put dialogue tut popup here inst... (?)
                UIManager.Instance.EnableUI(submissionButton);
                firstVisitHerbWall = false;
            }
            UIManager.Instance.DisableUI(switchDeskClient.GetComponent<CanvasGroup>());
            switchDeskHerb.GetComponent<Image>().sprite = arrowLeft.sprite;

            SetupUI(allCanvasesHerbWall);
        }
        else
        {
            UIManager.Instance.EnableUI(switchDeskClient.GetComponent<CanvasGroup>());
            switchDeskHerb.GetComponent<Image>().sprite = arrowRight.sprite;

            SetupUI(allCanvasesDesk);
        }
        onDesk = !onDesk;
    }

    public void SwitchSceneDeskClient()
    {
        if (onDesk)
        {
            UIManager.Instance.DisableUI(switchDeskHerb.GetComponent<CanvasGroup>());
            switchDeskClient.GetComponent<Image>().sprite = arrowDown.sprite;

            SetupUI(allCanvasesClientWindow);
        }
        else
        {
            if (herbWallActive)
            {
                UIManager.Instance.EnableUI(switchDeskHerb.GetComponent<CanvasGroup>());
            }
            else
            {
                UIManager.Instance.DisableUI(switchDeskHerb.GetComponent<CanvasGroup>());
            }
            
            switchDeskClient.GetComponent<Image>().sprite = arrowUp.sprite;
            SetupUI(allCanvasesDesk);

                if (firstVisitDesk)
                {
                    MenuManager.Instance.TutorialPopup("desk");
                    firstVisitDesk = false;
                }
        }
        onDesk = !onDesk;
    }



    void SetupInitialScene()
    {
        // gets spawnpoints for UI 
        letterSpawnPoint = clientLetter.GetComponent<RectTransform>().anchoredPosition;
        grimoireSpawnPoint = grimoire.GetComponent<RectTransform>().anchoredPosition;
        diagnosisSheetSpawnPoint = diagnosisSheet.GetComponent<RectTransform>().anchoredPosition;
        
        //SetupUI(allCanvasesDesk);
        UIManager.Instance.DisableUI(sceneNavigation);
        SetupUI(allCanvasesClientWindow);
            //GameManager.Instance.ResetScene();

        clientWindowClientIcon.alpha = 0;
        switchDeskClient.GetComponent<Image>().sprite = arrowDown.sprite;
        UIManager.Instance.DisableUI(switchDeskHerb.GetComponent<CanvasGroup>());


        UIManager.Instance.DisableUI(submissionButton);

        //diagnosisSubmitted = false;
    }


    public void SetupUI(List<CanvasGroup> canvasGroupList)
    {
        Debug.Log("Setting up UI in scene.");
        // Hides all UI
        foreach (CanvasGroup hide in allMainCanvases)
        {
            UIManager.Instance.DisableUI(hide);
        }

        foreach (CanvasGroup c in canvasGroupList)
        {
            UIManager.Instance.EnableUI(c);

            if (canvasGroupList == allCanvasesDesk)
            {
                if (!GameManager.Instance.ailmentChosen)
                {
                    UIManager.Instance.DisableUI(diagnosisSheet);
                }
                else
                {
                    UIManager.Instance.EnableUI(diagnosisSheet);
                }
            }
        }
        // for enabling/disabling interaction when entering a menu
        currentCanvasGroup = canvasGroupList == allCanvasesDesk ? deskGroup : canvasGroupList == allCanvasesClientWindow ? clientWindowGroup : herbWallGroup;
        MenuManager.Instance.SetupInitialScene();
    }

    public void UpdateAilment(string ailment)
    {
        selectedAilment = ailment;
    }


    public void ShowClient(Image imageToUpdate)//(Image iconToUpdate)
    {
        Debug.Log("Received");
        Sprite tempSprite = imageToUpdate.sprite;
        UIManager.Instance.SpriteShift(clientImage, tempSprite);

        clientWindowClientIcon.alpha = 1f;
        //clientImage.sprite = iconToUpdate.sprite;
        // Enables navigation, but only to the desk
        UIManager.Instance.EnableUI(sceneNavigation);
        //UIManager.Instance.DisableUI(switchDeskHerb.GetComponent<CanvasGroup>());
    }

    public void UnlockHerbWall()
    {
        UIManager.Instance.EnableUI(switchDeskHerb.GetComponent<CanvasGroup>());
        //UIManager.Instance.EnableUI
        herbWallActive = true;
    }

    /*public void SpriteShift(Image image, Sprite sprite)
    {
        image.sprite = sprite;
    }*/




    /*void GenerateSpawnpoints()
    {
        letterSpawnPoint = clientLetter.GetComponent<RectTransform>().anchoredPosition;
        grimoireSpawnPoint = grimoire.GetComponent<RectTransform>().anchoredPosition;
        diagnosisSheetSpawnPoint = diagnosisSheet.GetComponent<RectTransform>().anchoredPosition;
        //herbalistGuideSpawnPoint = clientLetter.GetComponent<RectTransform>().anchoredPosition;  
    }*/
}
