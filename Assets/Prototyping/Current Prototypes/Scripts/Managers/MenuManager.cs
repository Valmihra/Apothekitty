using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Canvas Groups")]
    public CanvasGroup mainMenu;
    public CanvasGroup pauseMenu;
    public CanvasGroup popupMenu;
    
        public List<CanvasGroup> allCanvasesMenus;
    
    // COLOUR CHANGES
    // <color=red>  <#8A1E1E>

    //[Header("Pause Menu Buttons")]
    //public Button resumeGameButton;               //resumeGameButton.onClick.AddListener(delegate { Resume(); });
    //public Button returnToMainMenuButton;         //returnToMainMenuButton.onClick.AddListener(delegate { Return(); });
    //public Button resetSceneButton;               //resetSceneButton.onClick.AddListener(delegate { Reset(); });

    // PLACEHOLDER VECTORS FOR TUTORIAL POPUP LOCATIONS
    private Vector2 initialPopupMenuPosition;
    private Vector2 movedDownPosition;
    private Vector2 movedUpPosition;

    [Header("Popup Text References")]
    public TMP_Text popupTextBox;
    public TMP_Text popupPrompt;

    private string popupText;
    private string defaultPopupPromptText = "Click box to close.";
    private string continuePopupPromptText = "Click box to continue.";

    private bool diagnosisSheetPopupActive;
    private bool setPositions;
    //private bool deskPopupActive;

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
        //if (_instance = null)         {
            _instance = this;

        InitialiseMenuManager();
        
        
        // diagnosisSheetPopupActive = false;
        // popupPrompt.text = defaultPopupPromptText;

        // initialPopupMenuPosition = popupMenu.transform.position;
        // movedDownPosition = new Vector2(initialPopupMenuPosition.x, initialPopupMenuPosition.y-100f);
        // movedUpPosition = new Vector2(initialPopupMenuPosition.x, initialPopupMenuPosition.y+100f);
        //HideMenuCanvases();
    }

    // ---------------------------------
    //      BASIC SETUP
    // ---------------------------------

    // Performs all basic functions required to run MenuManager 
    void InitialiseMenuManager()
    {
        // Sets bool and default prompt text for popup
        popupPrompt.text = defaultPopupPromptText;
        diagnosisSheetPopupActive = false;
        setPositions = false;
        

        InitialiseMenuCanvasGroupList();
        GetPositions();
    }
    
    // Creates a list of all menu canvases
    void InitialiseMenuCanvasGroupList()
    {
        allCanvasesMenus = new List<CanvasGroup>();
        allCanvasesMenus.Add(pauseMenu);
        allCanvasesMenus.Add(popupMenu);
            allCanvasesMenus.Add(mainMenu);

        //Debug.Log(allCanvasesMenus.Count + pauseMenu.name);
    }

    // Plots the relevant locations for the tutorial popup
    void GetPositions()
    {
        if (!setPositions)
        {
            initialPopupMenuPosition = popupMenu.transform.position;
            movedDownPosition = new Vector2(initialPopupMenuPosition.x, initialPopupMenuPosition.y - 100f);
            movedUpPosition = new Vector2(initialPopupMenuPosition.x, initialPopupMenuPosition.y + 100f);
            setPositions = true;
        }
        else
        {
            Debug.Log("Set positions already exist.");
            return;
        }
    }

    // public void Reset()
    // {
        // GameManager.Instance.ResetScene();
    // }

    // ---------------------------------
    //      MENU DISPLAY FUNCTIONS
    // ---------------------------------

    public void OpenMenu(CanvasGroup menuCanvasGroup)
    {
        // game is paused?
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

        if (menuCanvasGroup == mainMenu)
        {
            GameManager.Instance.onMainMenu = false;
        }
        // game is no longer paused?
    }

    public void HideMenuCanvases()
    {
        foreach (CanvasGroup c in allCanvasesMenus)
        {
            UIManager.Instance.DisableUI(c);
        }
    }

    /// ---------------------------------
    //      TUTORIAL POPUP FUNCTIONS
    // ---------------------------------

    public void TutorialPopup(string popupType)
    {
        /*if (GameManager.Instance.runningTutorial)
        {
            ,,
        }*/
        
        OpenMenu(popupMenu);
        
        if (popupType == "initialTutorial")
        {
            //UIManager.Instance.HighlightCanvasElement("curtain");
            popupText = "Welcome to Apothekitty!\n\nAs the town healer, it's your job to carefully diagnose and treat your patients. \n\nClick on the <#8A1E1E>curtain</color> to receive your first client!";
        }
        else if (popupType == "startPrompts")
        {
            UIManager.Instance.HighlightCanvasElement("arrows");
            popupText = "You'll find the patient form on your desk. Click the <#8A1E1E>arrows</color> in the bottom right to navigate between screens.\n\nIf you feel lost at any point, click the arrow by the quest log to see what you still need to do.";
        }
        else if (popupType == "initDeskPrompts")
        {
            // UIManager.Instance.
            popupText = "Papers pile up quickly, but you can always click and drag things on your desk to keep it organised. \n\nTry moving the old note to the bin.";
        }
        else if (popupType == "grimoire")
        {
            popupMenu.transform.position = movedDownPosition;   //AilmentIconColourController.Instance.ShowAilmentIconBackground();   //UIManager.Instance.HighlightCanvasElement("ailmentIcon");
            UIManager.Instance.HighlightCanvasElement("ailmentIcon");
            popupText = "Your Grimoire acts as your reference point for ailments. Pay close attention to each ailment's description and compare it to your client's symptoms.\n\nOnce you think you've found the correct diagnosis, click on the <#8A1E1E>ailment's picture</color> to select it!";
        }
        else if (popupType == "diagnosisSheet")
        {
            popupMenu.transform.position = movedUpPosition;
            UIManager.Instance.HighlightCanvasElement("modifiers");
            popupPrompt.text = continuePopupPromptText;
            diagnosisSheetPopupActive = true;
            popupText = "Scan the ailment's description for clues to make a suitable treatment plan.\n\nYou can click and drag papers on your desk if you need to see something that's being obscured.\n\nNot all ailments require two targets, but some may require a <#8A1E1E>modifier</color>.";
            
        }
        else if (popupType == "diagnosisSheetTwo")
        {
            popupPrompt.text = defaultPopupPromptText;
            diagnosisSheetPopupActive = false;
            popupText = "For extreme cases or large clients, you can strengthen the treatment with the <b>enhancer</b>. You can also choose the <b>inverter</b> to achieve the opposite effect, if the description calls for it.\n\nClick <b>submit treatment plan</b> when you're ready.";
        }
        else if (popupType == "toHerbWall")
        {
            //UIManager.Instance.HighlightCanvasElement("arrows");
            popupText = "Now that you've chosen a treatment plan, you can access your herb stores to create your treatment.\n\nClick the new arrow in the bottom right to navigate to the <#8A1E1E>herb wall</color>.";
        }
        else
        {
            popupText = "If you forget your chosen treatment plan, you can click the arrow in the bottom right to navigate to the desk.\n\nOnce you're done, click <b>'submit treatment'</b> to hand your recipe to the client.\n\nMake sure you're 100% certain before submitting, as there's no going back!";// how to submit and view results
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

    // ---------------------------------
    //      ALERT MESSAGE FUNCTIONS
    // ---------------------------------

    public void InvalidPopup()
    {
        popupTextBox.text = "This combination is invalid.\n\nPlease ensure you are choosing <b> at least </b> a single effect and a target to pair it with.";
        OpenMenu(popupMenu);
    }

    public void NothingChosenPopup()
    {
        popupTextBox.text = "Bro, you can't give the client <i>nothing</i>. They're a paying customer!!";
        OpenMenu(popupMenu);
    }

    public void OneHerbChosenPopup()
    {
        popupTextBox.text = "Don't get stingy, you need to pick at least two herbs for a recipe to do something!!";
        OpenMenu(popupMenu);
    }

    public void ExitGamePopup()
    {
        popupTextBox.text = "The game should close after you click this box.\n\nThank-a-you so much for to playing our game!";
        OpenMenu(popupMenu);
    }

    public void ClosePopup()
    {
        // bool tutorialCheckComplete = false;
        // Tutorial-specific conditions to check for
        if (GameManager.Instance.runningTutorial)
        {
            if (diagnosisSheetPopupActive)
            {
                popupMenu.transform.position = initialPopupMenuPosition;
                TutorialPopup("diagnosisSheetTwo");
                return;
            }
            else if (popupTextBox.text.Contains("Pay close attention to each ailment's description"))
            {
                //AilmentIconColourController.Instance.ResetAilmentIconBackground();
                popupMenu.transform.position = initialPopupMenuPosition;
                ExitMenu(popupMenu);
                return;
            }
        }

        // Constant conditions to check for
        //else
        //{
            // Checks if trying to exit the game
            if (popupTextBox.text.Contains("Thank-a-you so much for to playing our game!"))
            {
                ExitMenu(popupMenu);
                Invoke(nameof(Quit), 1f);
            }
            else
            {
                ExitMenu(popupMenu);
            }
        // }


        //if (diagnosisSheetPopupActive)
        //{
        //    popupMenu.transform.position = initialPopupMenuPosition;
        //    TutorialPopup("diagnosisSheetTwo");
        //}
        /*else if (deskPopupActive)
        {
            TutorialPopup("grimoire");
        }
        else
        {
            if (popupTextBox.text.Contains("Pay close attention to each ailment's description"))
            {
                //AilmentIconColourController.Instance.ResetAilmentIconBackground();
                popupMenu.transform.position = initialPopupMenuPosition;
                ExitMenu(popupMenu);
            }
            else if (popupTextBox.text.Contains("Thank-a-you so much for to playing our game!"))
            {
                ExitMenu(popupMenu);
                Invoke(nameof(Quit), 1f);
            }
            else
            {
                ExitMenu(popupMenu);
            }
        }*/
    }

    void Quit()
    {
        Debug.Log("got to the quit command");
        GameManager.Instance.ExitGame();
        //Debug.Log("got past instance.exit");
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
