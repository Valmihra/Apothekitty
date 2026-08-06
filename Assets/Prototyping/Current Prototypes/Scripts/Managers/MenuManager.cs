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
    //public Button resumeGameButton;
    //public Button returnToMainMenuButton;
    //public Button resetSceneButton;
    private Vector2 spawnPosition;
    private Vector2 movedDownPosition;
    private Vector2 movedUpPosition;

    public TMP_Text popupTextBox;
    string popupText;
    bool diagnosisSheetPopup;
    bool deskPopup;

    public TMP_Text popupPrompt;
    string defaultPopupPromptText = "Click box to close.";
    string continuePopupPromptText = "Click box to continue.";

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

        spawnPosition = popupMenu.transform.position;
        movedDownPosition = new Vector2(spawnPosition.x, spawnPosition.y-100f);
        movedUpPosition = new Vector2(spawnPosition.x, spawnPosition.y+100f);
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
            allCanvasesMenus.Add(mainMenu);
        //Debug.Log(allCanvasesMenus.Count + pauseMenu.name);
    }

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
            diagnosisSheetPopup = true;
            popupText = "Scan the ailment's description for clues to make a suitable treatment plan.\n\nYou can click and drag papers on your desk if you need to see something that's being obscured.\n\nNot all ailments require two targets, but some may require a <#8A1E1E>modifier</color>.";
            
        }
        else if (popupType == "diagnosisSheetTwo")
        {
            popupPrompt.text = defaultPopupPromptText;
            diagnosisSheetPopup = false;
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
        if (diagnosisSheetPopup)
        {
            popupMenu.transform.position = spawnPosition;
            TutorialPopup("diagnosisSheetTwo");
        }

        
        /*else if (deskPopup)
        {
            TutorialPopup("grimoire");
        }*/
        else
        {
            if (popupTextBox.text.Contains("Pay close attention to each ailment's description"))
            {
                //AilmentIconColourController.Instance.ResetAilmentIconBackground();
                popupMenu.transform.position = spawnPosition;
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
        }
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
