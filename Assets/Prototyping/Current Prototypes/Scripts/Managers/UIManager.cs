using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*// SHOULD CONTAIN EVERY SWITCHABLE CANVASGROUP IN THE GAME
    [Header("Main Canvas Groups")]
    public CanvasGroup deskGroup;
    public CanvasGroup herbWallGroup;
    public CanvasGroup clientWindowGroup;
        private List<CanvasGroup> allMainCanvases;

    [Header("Menu Canvas Groups")]
    public CanvasGroup pauseMenu;
        private List<CanvasGroup> allCanvasesMenus;

    [Header("Determinant Canvas Groups")]   /// if child canvasgroup tagged DETERMINANT, maybe check what should be displayed(??)
    public CanvasGroup clientLetter;
    public CanvasGroup grimoire;
    public CanvasGroup diagnosisSheet;
    public CanvasGroup grimoireNavigation;
    public CanvasGroup clientWindowClientIcon;
    public CanvasGroup herbDrawers;
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
    private CanvasGroup currentCanvasGroup;
    private Image clientImage;

    
    private bool spawnSet;
    private bool onDesk;
    private bool herbWallActive;
    public string selectedAilment;

    public GrimoireNavigation grimoireNavScript;*/

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

        //spawnSet = false;
    }

    
    void Start()
    {
        //onDesk = false;
        //herbWallActive = false;
        
        //grimoireNavScript = FindObjectOfType<GrimoireNavigation>();

        //switchDeskHerb.onClick.AddListener(delegate {SwitchSceneDeskHerb(); });
        //switchDeskClient.onClick.AddListener(delegate {SwitchSceneDeskClient(); });

        
            //if (!spawnSet)
            //{
            //    GenerateSpawnpoints();
            //    InitialiseLists();
            //}
        //SetupUI(allCanvasesDesk);
        //SetupInitialScene();
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

    // Determines the correct position for each canvas group to be enabled at during the setup phase
        // Useful later on, maybe letters arrive on the desk in a certain area. Maybe the patients pass
        // them over and the location is randomised slightly within an area radius? Could mimic sliding
        // the sheets over a desk? 
    //void PlaceUI(CanvasGroup canvasGroup)
    //{
    //    int placementNumber = allMainCanvases.IndexOf(canvasGroup);
    //        canvasGroup.GetComponent<RectTransform>().anchoredPosition = canvasSpawnPoints[placementNumber];
    //        return;
    //   
    //}

    

    void MovePosition()
    {
        //position = Vector2.Lerp(randomisedOrigin, diagnosisSheetSpawnPoint, Random.value);
            //MimicAnimation();
    }

    void SetPosition()
    {
        //diagnosisSheet.GetComponent<RectTransform>().anchoredPosition = diagnosisSheetSpawnPoint;
    }

    /*public void SubmitDiagnosis()
    {
        Debug.Log("Submission registered.");
        Debug.Log("Unlocking Herb Wall.");
        UnlockHerbWall();
        // PlaceUI(herbalistGuide)

        // OOUHHH if i set it up in world space, can move camera 
        // to set points and mimic movement? but then have to 
        // address UI again,, smth to bring up with the g ang
    }


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

            allCanvasesMenus = new List<CanvasGroup>();
                allCanvasesMenus.Add(pauseMenu);

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
            if (herbWallActive)
            {
                EnableUI(switchDeskHerb.GetComponent<CanvasGroup>());
            }
            else
            {
                DisableUI(switchDeskHerb.GetComponent<CanvasGroup>());
            }
            
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
        
        //SetupUI(allCanvasesDesk);
        DisableUI(sceneNavigation);
        SetupUI(allCanvasesClientWindow);
            GameManager.Instance.ResetScene();

        foreach (CanvasGroup c in allCanvasesMenus)
        {
            DisableUI(c);
        }

        clientWindowClientIcon.alpha = 0;
        switchDeskClient.GetComponent<Image>().sprite = arrowDown.sprite;
        DisableUI(switchDeskHerb.GetComponent<CanvasGroup>());
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
        // for enabling/disabling interaction when entering a menu
        currentCanvasGroup = canvasGroupList == allCanvasesDesk ? deskGroup : canvasGroupList == allCanvasesClientWindow ? clientWindowGroup : herbWallGroup;
    }

    public void UpdateAilment(string ailment)
    {
        selectedAilment = ailment;
    }*/


    public void SpriteShift(Image image, Sprite sprite)
    {
        image.sprite = sprite;
    }

    // toggles interaction with the canvasgroup while keeping it visible
    public void EnableInteraction(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisableInteraction(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }


            /*public void OpenMenu(CanvasGroup menuCanvasGroup)
            {
                DisableInteraction(currentCanvasGroup);
                if ((onDesk) && (grimoireNavScript.ailmentChosen))
                {
                    DisableInteraction(diagnosisSheet);
                }

                EnableUI(menuCanvasGroup);
                menuCanvasGroup.GetComponent<RectTransform>().SetAsLastSibling();
            }

            public void ExitMenu(CanvasGroup menuCanvasGroup)
            {
                DisableUI(menuCanvasGroup);
                EnableInteraction(currentCanvasGroup);
                if ((onDesk) && (grimoireNavScript.ailmentChosen))
                {
                    EnableInteraction(diagnosisSheet);
                }
            }*/


    /*public void ShowClient(Sprite spriteToUpdate)//(Image iconToUpdate)
    {
        Debug.Log("Received");
        SpriteShift(clientImage, spriteToUpdate);

        clientWindowClientIcon.alpha = 1f;
        //clientImage.sprite = iconToUpdate.sprite;
        // Enables navigation, but only to the desk
        EnableUI(sceneNavigation);
        //DisableUI(switchDeskHerb.GetComponent<CanvasGroup>());
    }

    public void UnlockHerbWall()
    {
        EnableUI(switchDeskHerb.GetComponent<CanvasGroup>());
        herbWallActive = true;
    }





    void GenerateSpawnpoints()
    {
        letterSpawnPoint = clientLetter.GetComponent<RectTransform>().anchoredPosition;
        grimoireSpawnPoint = grimoire.GetComponent<RectTransform>().anchoredPosition;
        diagnosisSheetSpawnPoint = diagnosisSheet.GetComponent<RectTransform>().anchoredPosition;
        //herbalistGuideSpawnPoint = clientLetter.GetComponent<RectTransform>().anchoredPosition;  
    }*/
}