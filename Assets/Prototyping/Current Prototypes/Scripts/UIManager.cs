using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

    // Locators
    private Vector2 letterSpawnPoint;
    private Vector2 grimoireSpawnPoint;
    private Vector2 diagnosisSheetSpawnPoint;
    //private Vector2 herbalistGuideSpawnPoint;     if hanging, no need for this!

        ////
    // SHOULD CONTAIN EVERY SWITCHABLE CANVASGROUP IN THE GAME
    private List<CanvasGroup> canvasListOne;
        ////

    // Lists for canvasGroups when more detailed scene switches.
    private List<CanvasGroup> clientWindow;
    private List<CanvasGroup> herbWall;
    private List<CanvasGroup> deskUI;

    //private List<CanvasGroup> canvasListTwo;
    private List<Vector2> canvasSpawnPoints;

    private bool sceneswitch;
    private bool firstSetup;
    private bool onDesk;

    [Header("Button References")]
    public Button switchDeskHerb;
    public Button switchDeskClient;
    public Image arrowLeft;
    public Image arrowRight;
    public Image arrowUp;
    public Image arrowDown;

    private GrimoireNavigation grimoireNavScript;

    // Start is called before the first frame update
    void Start()
    {
            firstSetup = true;
            sceneswitch = false;
            onDesk = true;
         grimoireNavScript = FindObjectOfType<GrimoireNavigation>();


         switchDeskHerb.onClick.AddListener(delegate {SwitchSceneDeskHerb(); });
         switchDeskClient.onClick.AddListener(delegate {SwitchSceneDeskClient(); });

         InitialiseLists();
         //InitialiseSceneUI();
         SetupUI(deskUI);
    }


    //  ---
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
    void InitialiseSceneUI()
    {
        Debug.Log("Initialising UI in scene");
        //DisableUI()
        foreach (CanvasGroup c in canvasListOne)
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
    }

    // Determines the correct position for each canvas group to be enabled at during the setup phase
        // Useful later on, maybe letters arrive on the desk in a certain area. Maybe the patients pass
        // them over and the location is randomised slightly within an area radius? Could mimic sliding
        // the sheets over a desk? 
    void PlaceUI(CanvasGroup canvasGroup)
    {
        int placementNumber = canvasListOne.IndexOf(canvasGroup);

            canvasGroup.GetComponent<RectTransform>().anchoredPosition = canvasSpawnPoints[placementNumber];
            return;
       
    }

    // Hides the navigation buttons on the grimoire and enables the diagnosis sheet.
        // Also currently removes the client letter, but am looking to flesh this out better (see PlaceUI)
    public void SubmitAilment()
    {
        //

        // currently hides the client letter/patient sheet to preserve space
        // while I test. Might have it so that it stays on the desk later on?

        DisableUI(clientLetter);
        DisableUI(grimoireNavigation);
        EnableUI(diagnosisSheet);
    }

    public void SubmitDiagnosis()
    {
        Debug.Log("Submission registered.");
        Debug.Log("This command would bring up UI to indicate the submission went through, and also prompt the player to look at the next area (herb wall)");
        // PlaceUI(herbalistGuide)

        // OOUHHH if i set it up in world space, can move camera 
        // to set points and mimic movement? but then have to 
        // address UI again,, smth to bring up with the gang
    }


    void InitialiseLists()
    {
        canvasListOne = new List<CanvasGroup>();
        canvasListOne.Add(clientLetter);   // 0
        canvasListOne.Add(grimoire);       // 1
        canvasListOne.Add(diagnosisSheet); // 2

        canvasListOne.Add(draggableHerbs);
        canvasListOne.Add(inventory);
        canvasListOne.Add(dialogueWindow);
        canvasListOne.Add(characterIcon);

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

        deskUI = new List<CanvasGroup>();
            deskUI.Add(clientLetter);
            deskUI.Add(grimoire);
            deskUI.Add(diagnosisSheet);
        herbWall = new List<CanvasGroup>();
            herbWall.Add(draggableHerbs);
        clientWindow = new List<CanvasGroup>();
            clientWindow.Add(dialogueWindow);

    }

    /*void HideButton(Button button)
    {
        CanvasGroup change = button.GetComponent<CanvasGroup>();
        DisableUI()
    }*/


    /*public void SwitchScenes()
    {
        
        // Hides the initial scene and shows Inventory prototype if scene has not been switched yet
        if (!sceneswitch)
        {
            foreach (CanvasGroup c in canvasListOne)
            {
                DisableUI(c);
            }
            EnableUI(draggableHerbs);
            EnableUI(inventory);
        }
        else
        {
            DisableUI(draggableHerbs);
            DisableUI(inventory);

            EnableUI(grimoire);

            if (!grimoireNavScript.ailmentChosen)
            {
                EnableUI(clientLetter);
            }
            else
            {
                EnableUI(diagnosisSheet);
            }
            
        }
        
        sceneswitch = !sceneswitch;
    }*/

    public void SwitchSceneDeskHerb()
    {
        if (onDesk)
        {
            // Hides Client Wall arrow
            DisableUI(switchDeskClient.GetComponent<CanvasGroup>());
            //flips arrow image direction and sets up the Herb Wall
                //Debug.Log("Switching icons");
            switchDeskHerb.GetComponent<Image>().sprite = arrowLeft.sprite;
            SetupUI(herbWall);
        }
        else
        {
            // Shows Client Wall arrow
            EnableUI(switchDeskClient.GetComponent<CanvasGroup>());
            //flips arrow image direction and sets up the desk UI
            switchDeskHerb.GetComponent<Image>().sprite = arrowRight.sprite;
            SetupUI(deskUI);
        }
        onDesk = !onDesk;
    }

    public void SwitchSceneDeskClient()
    {
        if (onDesk)
        {
            DisableUI(switchDeskHerb.GetComponent<CanvasGroup>());
            //flip arrow image direction
            switchDeskClient.GetComponent<Image>().sprite = arrowDown.sprite;
            SetupUI(clientWindow);
        }
        else
        {
            EnableUI(switchDeskHerb.GetComponent<CanvasGroup>());
            //flip arrow image direction
            switchDeskClient.GetComponent<Image>().sprite = arrowUp.sprite;
            SetupUI(deskUI);
        }
        onDesk = !onDesk;
    }


    public void SetupUI(List<CanvasGroup> canvasGroupList)
    {
        // Hides all UI
        foreach (CanvasGroup hide in canvasListOne)
        {
            DisableUI(hide);
        }

        // Checks if initial setup or not
        if (firstSetup)
        {
            foreach (CanvasGroup c in canvasGroupList)
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
                firstSetup = false;
            }
        }
        else
        {
            // Enables all UI in the canvasGroup. Checks to see which UI to use if switching to desk.
            foreach (CanvasGroup c in canvasGroupList)
            {
                EnableUI(c);

                if (canvasGroupList == deskUI)
                {
                    if (!grimoireNavScript.ailmentChosen)
                    {
                        DisableUI(diagnosisSheet);
                        EnableUI(clientLetter);
                    }
                    else
                    {
                        DisableUI(clientLetter);
                        EnableUI(diagnosisSheet);
                    }
                }
            }
                    //foreach (CanvasGroup c in canvasGroupList)
            //{
                /*if (canvasGroupList == clientWindow)
                {
                    foreach (canvasGroup c in canvasGroupList)
                    {
                        EnableUI(c);
                    }
                }
                else if (canvasGroupList == herbWall)
                {

                }
                else
                {
                    // DESK
                }
                if (c == diagnosisSheet)
                {
                    DisableUI(c);
                }
                else
                {
                    EnableUI(c);
                }*/
                //if (canvasGroupList != deskUI)
                //{
                    
                //}
                //else
                //{
                    //foreach (CanvasGroup c in )
                    //if (!grimoireNavScript.ailmentChosen)
                    //{
                    //    EnableUI(clientLetter);
                    //}
                    //else
                    //{
                    //    EnableUI(diagnosisSheet);
                    //}
        }

        // LISTS FOR EACH UI SETUP 
    }
}
