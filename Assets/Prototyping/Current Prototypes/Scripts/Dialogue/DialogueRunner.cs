using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueRunner : MonoBehaviour
{
    [Header("Dialogue Box Objects")]
        public CanvasGroup dialogueBox;
        public TMP_Text speakerName;
        public TMP_Text currentString;
        public TMP_Text promptString;
    
    // Values checked to run any dialogue
    private int currentLineNumber;
    private bool dialogueSet;
    
    // Tutorial-specific values to check
    private bool firstDialogueComplete;
    private bool introductionComplete;
    private bool notSeenDeskHint;
    private bool firstVisitDesk;
    public bool deskIsClean;
    private bool notSeenDiagnosisSheetHint;
    private bool notSeenFinalDiagnosisPopup;
    private bool justSubmittedDiagnosis;
    private bool justVisitedHerbWall;
	
	// Values to check when mimicking a conversation
	private bool clientIsSpeaking;
    private bool clientIsAnswering;
	private bool canFinishDialogue;
    
    // Internal values to keep dialogue running safely
    private string defaultSpeaker = null;
    private string defaultString = "You have encountered this due to an error with the DialogueRunner or DialogueHolder scripts";
    
    private string continuePrompt = "Click box to continue";
    private string closePrompt = "Click box to close";
    
    private string catName = "The Cat";

    // oh i can't wait for the overarching "if (!tutorialCompleted) else that will
    // RADICALLY influence how quickly I can navigate through all this,,
    [SerializeField] private AnimatedTextEffect dialogueAnimator;

    private List<string> currentDialogue;
    private string nameOfNPC;

    private static DialogueRunner _instance;
    public static DialogueRunner Instance
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

    public void InitialiseDialogueRunner()
    {
        notSeenDeskHint = true;
        firstVisitDesk = false;
        deskIsClean = false;
        notSeenDiagnosisSheetHint = true;
        notSeenFinalDiagnosisPopup = true;
        justSubmittedDiagnosis = false;
        justVisitedHerbWall = false;
    }

    public void ResetDialogueRunner()
    {
        FixDialogueBools();
        introductionComplete = false;
        firstDialogueComplete = false;
    }

    public void FixDialogueBools()
    {
        // Debug.Log("Should be hiding now...");
        // UIManager.Instance.DisableUI(dialogueBox);
        dialogueBox.gameObject.SetActive(false);
        currentLineNumber = 0;
        
		clientIsSpeaking = false;
        clientIsAnswering = false;
		canFinishDialogue = false;
        dialogueSet = false;
    }

    public void RunDialogue()
    {
        if (dialogueSet)
        {
            // If the dialogue is set, it sets the correct prompt and then continues...
            SetCorrectDialogueBoxPrompt();

            if (currentLineNumber >= currentDialogue.Count)
            {
                if (clientIsSpeaking)
                {
                    // If you're at or over the limit of the lines of dialogue, but the client is speaking
                    if (!introductionComplete)
                    {
                        GetDialogue("cat responds to client");
						clientIsSpeaking = false;
                    }
                    else
                    {
                        // Should never get to here.
						Debug.Log("Bro how'd you fuck it up like this?");
                    }
                }
                else
                {
					if (clientIsAnswering)
                    {
                        // If you're at or over the limit of the lines of dialogue, but the client is answering
                        PatientData.Instance.ChangePatientSprite();
						GetDialogue("client responds to cat");
						clientIsSpeaking = false;
					}
					else
					{
                        // If you're at or over the limit of the lines of dialogue, with nothing else to do
                        FinishDialogueSnippet();
					}
                    
                }
            }
            
            else
            {
                if (currentLineNumber < currentDialogue.Count)
                {
                    // Updates and animates the text if it is within the range of the current dialogue.
                    currentString.text = currentDialogue[currentLineNumber];
                    
                    dialogueAnimator.BeginAnimatingText(currentString.text);
                    
                    CheckDialogueForActions();
                    currentLineNumber++;
                }
            }
        }
        else
        {
            // If no dialogue is set, it closes the dialogue box.
            Debug.Log("Trying to access dialogue without it being set first. Check how you're calling it, dweeb.");
            FinishDialogueSnippet();
        }

        
    }
    
    void FinishDialogueSnippet()
    {
        // Determines what action to take after finishing reading through the dialogue
        // could probably put the client check here too instead of in RunDialogue if I wanted it cleaner!!
        if (canFinishDialogue)
        {
            CloseDialogueWindow();
            canFinishDialogue = false;
            MenuManager.Instance.ProgressDayPopup();
        }
        else
        {
            if (GameManager.Instance.runningTutorial)
            {
                if (!firstDialogueComplete)
                {
                    CloseDialogueWindow();
                    firstDialogueComplete = true;
                    
                    // Opens initial tutorial popup
                    MenuManager.Instance.OpenTutorialPopup("initialTutorial");
                    GameManager.Instance.canOpenShop = true;
                }
                else if (introductionComplete && notSeenDeskHint)
                {
                    CloseDialogueWindow();
                    MenuManager.Instance.OpenTutorialPopup("startPrompts");
                    
                    // Enables navigation to desk
                    SceneManager.Instance.EnableGameplay();
                    notSeenDeskHint = false;
                }
                else if (firstVisitDesk && !deskIsClean)
                {
                    CloseDialogueWindow();
                    MenuManager.Instance.OpenTutorialPopup("initDeskPrompts");
                    firstVisitDesk = false;
                }
                else if ((!firstVisitDesk) && (deskIsClean))
                {
                    CloseDialogueWindow();
                    deskIsClean = false;  // just to avoid this in future checks
                }
                // if the player has submitted the ailment and hasn't seen the next set of hints
                else if (GameManager.Instance.ailmentSubmitted && notSeenDiagnosisSheetHint)
                {
                    SceneManager.Instance.GetTreatmentPlan();
                    notSeenDiagnosisSheetHint = false;
                    JumpNextDialogue(DialogueHolder.Instance.td_DiagnosisSheetIntroduction._dialogue);//GetDialogue()
                    
                }
                else if (GameManager.Instance.ailmentSubmitted && !notSeenDiagnosisSheetHint && notSeenFinalDiagnosisPopup)
                {
                    CloseDialogueWindow();
                    notSeenFinalDiagnosisPopup = false;
                }
                else if (justSubmittedDiagnosis)
                {
                    CloseDialogueWindow();
                    justSubmittedDiagnosis = false;

                    SceneManager.Instance.UnlockHerbWall();
                }
                else if (justVisitedHerbWall)
                {
                    CloseDialogueWindow();
                    justVisitedHerbWall = false;
					
					// *TAG* - SEE IF WORKS HERE??
                    MenuManager.Instance.OpenTutorialPopup("finalHerbWall");
                }
            }
            else if (!GameManager.Instance.runningTutorial)
            {
                if (!firstDialogueComplete)
                {
                    CloseDialogueWindow();
                    firstDialogueComplete = true;
					
					// *TAG* - Should probably move these elsewhere,, gumming up the dialogue runner,,, is ok for now,,,,
                    GameManager.Instance.canOpenShop = true;
                    SceneManager.Instance.EnableGameplay();
                }
            }
        }
    }

    void CloseDialogueWindow()
    {
        dialogueBox.gameObject.SetActive(false);
        dialogueSet = false;
        currentLineNumber = 0;

        speakerName.text = defaultSpeaker;
        currentString.text = defaultString;
    }

    void JumpNextDialogue(List<string> target)//, string speaker)
    {
        currentDialogue = target;
        SetupDialogueForCatSpeaker();
        RunDialogue();
    }

    public void GetDialogue(string target)
    {
        if (GameManager.Instance.runningTutorial)
        {
            if (target == "tutorial")
            {
                currentDialogue = DialogueHolder.Instance.td_Introduction._dialogue;
                SetupDialogueForCatSpeaker();
                RunDialogue();
            }
            else if (target == "desk")
            {
                currentDialogue = DialogueHolder.Instance.td_Desk01._dialogue;
                SetupDialogueForCatSpeaker();
                RunDialogue();
                firstVisitDesk = true;
            }
            else if (target == "desk two")
            {
                currentDialogue = DialogueHolder.Instance.td_Desk02._dialogue;
                SetupDialogueForCatSpeaker();
                RunDialogue();
            }
            else if (target == "ailmentSubmitted")
            {
                currentDialogue = DialogueHolder.Instance.td_GrimoireAilmentSubmitted._dialogue;
                SetupDialogueForCatSpeaker();
                RunDialogue();
            }
            else if (target == "treatmentPlanSubmitted")
            {
                currentDialogue = DialogueHolder.Instance.td_DiagnosisSheetSubmitted._dialogue;
                SetupDialogueForCatSpeaker();
                RunDialogue();
                justSubmittedDiagnosis = true;
            }
            else if (target == "onHerbWall")
            {
                currentDialogue = DialogueHolder.Instance.td_HerbWall._dialogue;
                SetupDialogueForCatSpeaker();
                RunDialogue();
                justVisitedHerbWall = true;
            }
        }
        
        if (target == "patientArrive")
        {
            GetDialogueByClient("entry");
        }

		if (target == "client responds to cat")
        {
            GetDialogueByClient("finish");
        }

        if (target == "cat responds to client")
        {
            introductionComplete = true;
            
            int randomisedNumber = Random.Range(0, 3);
            currentDialogue = DialogueHolder.Instance.pd_TheCatResponsesList[randomisedNumber]._dialogue;
            
            SetupDialogueForCatSpeaker();
            RunDialogue();
        }

        if (target == "submit herbs to client")
        {
            currentDialogue = DialogueHolder.Instance.pd_TheCatSubmitHerbs._dialogue;
            SetupDialogueForCatSpeaker();
            RunDialogue();
			clientIsAnswering = true;
        }

        else
        {
            return;
        }
    }

    void SetupDialogueForCatSpeaker()
    {
        speakerName.text = catName;
        dialogueSet = true;
        currentLineNumber = 0;

        // UIManager.Instance.EnableUI(dialogueBox);
        dialogueBox.gameObject.SetActive(true);
    }

    void GetDialogueByClient(string dialogueType)
    {
        nameOfNPC = PatientData.Instance.activePatientData._patientName;
        speakerName.text = nameOfNPC;
        currentLineNumber = 0;

        if (dialogueType == "entry")
        {
			clientIsSpeaking = true;
            if (nameOfNPC == "Barry Buff")
            {
                currentDialogue = DialogueHolder.Instance.cd_BarryEnter._dialogue;
            }
            else if (nameOfNPC == "Arabella Bunny")
            {
                currentDialogue = DialogueHolder.Instance.cd_ArabellaEnter._dialogue;
            }
            else if (nameOfNPC == "Lawrence Lark")
            {
                currentDialogue = DialogueHolder.Instance.cd_LawrenceEnter._dialogue;
            }
            else if (nameOfNPC == "Jimothy")
            {
                currentDialogue = DialogueHolder.Instance.cd_JimothyEnter._dialogue;
            }
            else if (nameOfNPC == "Ferguson")
            {
                currentDialogue = DialogueHolder.Instance.cd_FergusonEnter._dialogue;
            }
            else if (nameOfNPC == "Peridot")
            {
                currentDialogue = DialogueHolder.Instance.cd_PeridotEnter._dialogue;
            }
            else
            {
                Debug.Log("bruh you goofed it");
            }
        }
        else
        {
            if (dialogueType == "finish")
            {
                clientIsAnswering = false;
                canFinishDialogue = true;
                if (nameOfNPC == "Barry Buff")
                {
                    currentDialogue = DialogueHolder.Instance.cd_BarryFinish._dialogue;
                }
                else if (nameOfNPC == "Arabella Bunny")
                {
                    currentDialogue = DialogueHolder.Instance.cd_ArabellaFinish._dialogue;
                }
                else if (nameOfNPC == "Lawrence Lark")
                {
                    currentDialogue = DialogueHolder.Instance.cd_LawrenceFinish._dialogue;
                }
                else if (nameOfNPC == "Jimothy")
                {
                    currentDialogue = DialogueHolder.Instance.cd_JimothyFinish._dialogue;
                }
                else if (nameOfNPC == "Ferguson")
                {
                    currentDialogue = DialogueHolder.Instance.cd_FergusonFinish._dialogue;
                }
                else if (nameOfNPC == "Peridot")
                {
                    currentDialogue = DialogueHolder.Instance.cd_PeridotFinish._dialogue;
                }
                else
                {
                    Debug.Log("bruh you goofed it");
                }
            }
        }
        
        dialogueSet = true;
        // UIManager.Instance.EnableUI(dialogueBox);
        dialogueBox.gameObject.SetActive(true);
        RunDialogue();
    }

    void SetCorrectDialogueBoxPrompt()
    {
        if (currentLineNumber < currentDialogue.Count - 1)
        {
            promptString.text = continuePrompt;
        }
        else
        {
			promptString.text = closePrompt;
        }
    }

    // checks if there's highlighted text in the string and performs an action with the thing inside it
    void CheckDialogueForActions()
    {
        if (currentString.text.Contains("</color>"))
        {
            Debug.Log("Would perform highlight action here. Is this still necessary?");
            PerformAction(currentString.text);
        }
        else
        {
			if (clientIsAnswering)
			{
				// Relies on submission dialogue only having one line!!
				GetDialogueByClient("finish");
                
                // *TAG* - could instead directly reset the dialogue list in this
                // script, but,,, idk.. messy. yuck.
			}
			else
			{
				return;
			}
        }
    }

    void PerformAction(string stringToRead)
    {
		// Highlights the corresponding canvas element through UIManager
        if (stringToRead.Contains("Each recipe has an"))
        {
            UIManager.Instance.HighlightCanvasElement("effect");
        }
        else if (stringToRead.Contains("I just need to choose a"))
        {
            UIManager.Instance.HighlightCanvasElement("target");
        }
        else
        {
            return;
        }
    }
}
