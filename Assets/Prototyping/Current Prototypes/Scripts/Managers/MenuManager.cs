using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Canvas Groups")]
    public CanvasGroup pauseMenu;
    public CanvasGroup popupMenu;
    public List<CanvasGroup> allCanvasesMenus;
    
    // COLOUR CHANGES
    // <color=red>  <#8A1E1E>

    //[Header("Pause Menu Buttons")]
    //public Button resumeGameButton;
    //public Button returnToMainMenuButton;
    //public Button resetSceneButton;
    
    public TMP_Text popupTextBox;
    string popupText;
    bool diagnosisSheetPopup;
    bool deskPopup;

    public TMP_Text popupPrompt;
    string defaultPopupPromptText = "Click to close.";
    string continuePopupPromptText = "Click to continue.";

    private static MenuManager _instance;
    public static MenuManager Instance
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
        //}

        //resumeGameButton.onClick.AddListener(delegate { Resume(); });
        //returnToMainMenuButton.onClick.AddListener(delegate { Return(); });
        //resetSceneButton.onClick.AddListener(delegate { Reset(); });

        InitialiseMenuCanvasGroupList();
        diagnosisSheetPopup = false;
        popupPrompt.text = defaultPopupPromptText;
        //HideMenuCanvases();
    }

    public void Reset()
    {
        //GameManager.Instance.ResetScene();
    }

    void InitialiseMenuCanvasGroupList()
    {
        allCanvasesMenus = new List<CanvasGroup>();
        allCanvasesMenus.Add(pauseMenu);
        allCanvasesMenus.Add(popupMenu);
        //Debug.Log(allCanvasesMenus.Count + pauseMenu.name);
    }

    public void OpenMenu(CanvasGroup menuCanvasGroup)
    {
        UIManager.Instance.DisableInteraction(SceneManager.Instance.currentCanvasGroup);
        if ((SceneManager.Instance.onDesk) && (GameManager.Instance.ailmentChosen))
        {
            UIManager.Instance.DisableInteraction(SceneManager.Instance.diagnosisSheet);
        }

        UIManager.Instance.EnableUI(menuCanvasGroup);
        menuCanvasGroup.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void ExitMenu(CanvasGroup menuCanvasGroup)
    {
        UIManager.Instance.DisableUI(menuCanvasGroup);
        UIManager.Instance.EnableInteraction(SceneManager.Instance.currentCanvasGroup);
        if ((SceneManager.Instance.onDesk) && (GameManager.Instance.ailmentChosen))
        {
            UIManager.Instance.EnableInteraction(SceneManager.Instance.diagnosisSheet);
        }
    }

    public void HideMenuCanvases()
    {
        foreach (CanvasGroup c in allCanvasesMenus)
        {
            UIManager.Instance.DisableUI(c);
        }
    }

    public void TutorialPopup(string popupType)
    {
        OpenMenu(popupMenu);
        
        if (popupType == "initialTutorial")
        {
            // HighlightCanvasElement(curtain)
            popupText = "Welcome to Apothekitty!\n\nAs the town healer, it's your job to carefully diagnose and treat your patients. \n\nClick on the <#8A1E1E>curtain</color> to receive your first client!";
        }
        else if (popupType == "startPrompts")
        {
            popupText = "You'll find the patient form on your <#8A1E1E>desk</color>. Click the arrows in the bottom right to navigate between screens.\n\nIf you feel lost at any point, click the arrow by the quest log to see what you still need to do.";
        }
        else if (popupType == "grimoire")
        {
            //popupPrompt.text = defaultPopupPromptText;
            popupText = "Your Grimoire acts as your reference point for ailments. Pay close attention to each ailment's description and compare it to your client's symptoms.\n\nOnce you think you've found the correct diagnosis, click on the <#8A1E1E>ailment's picture</color> to select it!";
        }
        else if (popupType == "diagnosisSheet")
        {
            popupPrompt.text = continuePopupPromptText;
            diagnosisSheetPopup = true;
            popupText = "Scan the ailment's description for clues to make a suitable treatment plan.\n\nYou can click and drag papers on your desk if you need to see something that's being obscured.\n\nNot all ailments require two targets, but some may require a <#8A1E1E>modifier</color>.";
            
        }
        else if (popupType == "diagnosisSheetTwo")
        {
            popupPrompt.text = defaultPopupPromptText;
            diagnosisSheetPopup = false;
            popupText = "MIGHT NEED TWEAKING::\n\nFor later-stage ailments or large clients, you can strengthen the treatment with the <b>enhancer</b>. You can also choose the <b>inverter</b> to achieve the opposite effect, if the description calls for it.\n\nClick <b>submit treatment plan</b> when you're ready.";
        }
        else if (popupType == "toHerbWall")
        {
            popupText = "Now that you've chosen a treatment plan, you can access your herb stores to create your treatment.\n\nClick the new arrow in the bottom right to navigate to the <#8A1E1E>herb wall</color>.";
        }
        else
        {
            popupText = "If you forget your chosen treatment plan, you can click the arrow in the bottom right to navigate to the desk.\n\nOnce you're done, click 'submit treatment' to hand your recipe to the client.\n\nMake sure you're 100% certain before submitting, as there's no going back!";// how to submit and view results
        }
        popupTextBox.text = popupText;

        /*if (popupTextBox.text.Contains("</color>"))
        {
            popupTextBox.text = "works";
        }*/



        // IF RED IS PRESENT, WAIT .5F AND THEN INVOKE CHANGE COLOUR ON THE OBJECT
        // CHANGE COLOUR (WAITS .5F AND THEN SETS BACK TO DEFAULT COLOUR?)
        // INVOKE IT 3X? EACH TIME PERFORMING TIMESCALLED++ UNTIL 3X
        // THEN RESET TO 0 AND STOP RUNNING CHANGE COLOUR.
    }

    public void InvalidPopup()
    {
        popupTextBox.text = "This combination is invalid.\n\nPlease ensure you are choosing <b> at least </b> a single effect and a target to pair it with.";
        OpenMenu(popupMenu);
    }

    public void ClosePopup()
    {
        if (diagnosisSheetPopup)
        {
            TutorialPopup("diagnosisSheetTwo");
        }
        /*else if (deskPopup)
        {
            TutorialPopup("grimoire");
        }*/
        else
        {
            ExitMenu(popupMenu);
        }
    }


    /*public void MiniPopup()
    {
        miniText.text = "Patient form is on your desk.";

        miniText.text = "Access to herb wall granted.";

        miniText.text = "Submitting.";
    }*/


    // QUEST LOG
        // TREAT CLIENT
        // DIAGNOSE AILMENT
        // MAKE A RECIPE
        // SELECT THE HERBS
        // SUBMIT TO CLIENT
    //public void Po
}
