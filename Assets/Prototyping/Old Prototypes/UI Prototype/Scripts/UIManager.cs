using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("All Interactable UI Types in Scene")]
    public CanvasGroup clientLetter;
    public CanvasGroup grimoire;
    public CanvasGroup diagnosisSheet;
    
    public CanvasGroup grimoireNavigation;

    public CanvasGroup draggableHerbs;
    public CanvasGroup inventory;
    //public 

    private Vector2 letterSpawnPoint;
    private Vector2 grimoireSpawnPoint;
    private Vector2 diagnosisSheetSpawnPoint;
    //private Vector2 herbalistGuideSpawnPoint;     if hanging, no need for this!

    private List<CanvasGroup> canvasListOne;
    //private List<CanvasGroup> canvasListTwo;
    private List<Vector2> canvasSpawnPoints;

    private bool sceneswitch;
    public Button sceneswitchButton;

    // Start is called before the first frame update
    void Start()
    {
            sceneswitch = false;
         
         sceneswitchButton.onClick.AddListener(delegate {SwitchScenes(); });
         InitialiseLists();
         InitialiseSceneUI();
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

        //canvasListTwo = new List<CanvasGroup>();
        //canvasListTwo.Add()

        letterSpawnPoint = clientLetter.GetComponent<RectTransform>().anchoredPosition;
        grimoireSpawnPoint = grimoire.GetComponent<RectTransform>().anchoredPosition;
        diagnosisSheetSpawnPoint = diagnosisSheet.GetComponent<RectTransform>().anchoredPosition;

        canvasSpawnPoints = new List<Vector2>();
        canvasSpawnPoints.Add(letterSpawnPoint);            // 0
        canvasSpawnPoints.Add(grimoireSpawnPoint);          // 1
        canvasSpawnPoints.Add(diagnosisSheetSpawnPoint);    // 2

    }




    public void SwitchScenes()
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
            EnableUI(diagnosisSheet);
        }
        
        sceneswitch = !sceneswitch;
    }
}
