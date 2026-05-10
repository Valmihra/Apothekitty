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
    
    //[Header("Pause Menu Buttons")]
    //public Button resumeGameButton;
    //public Button returnToMainMenuButton;
    //public Button resetSceneButton;
    
    public TMP_Text popupTextBox;
    string popupText;
    bool diagnosisSheetPopup;
    bool deskPopup;

    public TMP_Text popupPrompt;
    string defaultpopupPromptText = "Click to close.";

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

        InitialiseList();
        diagnosisSheetPopup = false;
        popupPrompt.text = defaultpopupPromptText;
        //SetupInitialScene();
    }

    public void Reset()
    {
        //GameManager.Instance.ResetScene();
    }

    void InitialiseList()
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

    public void SetupInitialScene()
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
            popupText = "Welcome to Apothekitty!\n\nAs the town healer, it's your job to carefully diagnose and treat your patients. \n\nClick on the curtain to receive your first client!";
        }
        else if (popupType == "startPrompts")
        {
            popupText = "You'll find the patient form on your desk. Click the arrows in the bottom right corner to navigate between screens as needed.\n\nIf you feel lost at any point, click the arrow by the quest log to see what you still need to do.";
        }
        else if (popupType == "desk")
        {
            popupPrompt.text = "Next...";
            deskPopup = true;
            popupText = "Welcome to your work station! Your client has already filled out their patient form, so it's up to you to figure out what's wrong with them.\n\nYou can do this with your grimoire.";
        }
        else if (popupType == "grimoire")
        {
            popupPrompt.text = defaultpopupPromptText;
            deskPopup = false;
            popupText = "Your grimoire acts as a compendium of ailments. Pay close attention to the descriptions of each ailment and compare them to the symptoms your client is describing.\n\nWhen you think you've found the correct diagnosis, click on the icon of the ailment to submit it.";
        }
        else if (popupType == "diagnosisSheet")
        {
            popupPrompt.text = "Next...";
            diagnosisSheetPopup = true;
            popupText = "Now that you've selected an ailment, you need to come up with a recipe to treat it.\n\nYou need to figure out what type of EFFECT makes the most sense, and what that effect should TARGET.";
        }
        else if (popupType == "diagnosisSheetTwo")
        {
            popupPrompt.text = defaultpopupPromptText;
            diagnosisSheetPopup = false;
            popupText = "You can heal something that's been harmed, ease something that needs soothing, or fortify something that needs resistance.\n\n You can target the mind, the body, or you can try to release a spirit -- but just between you and me, you won't find any spirit possessions in this stage, because it's a tutorial and I'm nice.";
        }
        else if (popupType == "herbalistGuide")
        {
            popupText = "Wahoo!! You've come up with a treatment, so now you have access to your herb stores.\n\nUsing the recipe you came up with and the guide on the wall, follow the riddles to find the correct herbs to prescribe.\nDrag and drop them into your inventory, and when you're done, click the submission button to give the herbs to your patient.";
        }
        else
        {
            popupText = "You've done it! ,,,";// how to submit and view results
        }
        popupTextBox.text = popupText;
    }

    public void ClosePopup()
    {
        if (diagnosisSheetPopup)
        {
            TutorialPopup("diagnosisSheetTwo");
        }
        else if (deskPopup)
        {
            TutorialPopup("grimoire");
        }
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
