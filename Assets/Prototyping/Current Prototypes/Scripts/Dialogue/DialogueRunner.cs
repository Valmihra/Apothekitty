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
    public bool cleanDesk;
    private bool notSeenDiagnosisSheetHint;
    private bool notSeenFinalDiagnosisPopup;
    private bool justSubmittedDiagnosis;
    private bool justVisitedHerbWall;

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
    [SerializeField]
	private AnimatedTextEffect dialogueAnimator;

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
        //nextDialogue.onClick.AddListener(delegate { RunDialogue(); });
    }

    /*void Start()
    {
        Reset();
    }*/

    public void ResetDialogueRunner()
    {
        Debug.Log("Resetting DialogueRunner...");
        FixDialogueBools();
        
        introductionComplete = false;
        firstDialogueComplete = false;

        notSeenDeskHint = true;
        firstVisitDesk = false;
        cleanDesk = false;
        notSeenDiagnosisSheetHint = true;
        notSeenFinalDiagnosisPopup = true;
        justSubmittedDiagnosis = false;
        justVisitedHerbWall = false;
    }

    public void FixDialogueBools()
    {
        UIManager.Instance.DisableUI(dialogueBox);
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
            // Debug.Log("Right now, currentDialogue is holding " + currentDialogue.Count + " strings of dialogue.");
            // Debug.Log("You are currently on line: " + currentLineNumber + ".");

            SetCorrectDialogueBoxPrompt();

            if (currentLineNumber >= currentDialogue.Count)
            {
                // at or over the limit of the lines of dialogue
                Debug.Log("currentLineNumber is larger than currentDialogue.Count. Checking whether this is intentional or not.");
                if (clientIsSpeaking)
                {
                    if (!introductionComplete)
                    {
                        GetDialogue("cat responds to client");
						clientIsSpeaking = false;
                    }
                    else
                    {
                        // CheckDialogueForActions();
						// clientIsSpeaking = false; // 
						Debug.Log("Bro how'd you fuck it up like this?");
                    }
                }
                else
                {
					if (clientIsAnswering)
					{
						GetDialogue("client responds to cat");
						clientIsSpeaking = false;
					}
					else
					{
						Debug.Log("You have reached the end of this DialogueSnippet.");
                        /*if (!hasActionOnFinishDialogue)
                        {
                            FinishDialogueSnippet();
                        }
                        else
                        {
                            GetNextAction();
                        }*/
                        FinishDialogueSnippet();
					}
                    
                }
            }
            else
            {
                // within the limit of the lines of dialogue
                if (currentLineNumber < currentDialogue.Count)
                {
                    // updates the text
                    //CheckForSpeaker();
                    currentString.text = currentDialogue[currentLineNumber];
					// BEGINS ANIMATING THAT STRING HERE!!
					dialogueAnimator.BeginAnimatingText(currentString.text);
                    CheckDialogueForActions();
                    currentLineNumber++;
                    //Debug.Log("Next line will be: " + currentLineNumber);
                }
            }
        }
        else
        {
            Debug.Log("No set dialogue. Closing the dialogue window.");
            FinishDialogueSnippet();
        }

        
    }

    // Determines what action to take after finishing reading through the dialogue
        // could probably put the client check here too instead of in RunDialogue if I wanted it cleaner!!
    void FinishDialogueSnippet()
    {
        if (canFinishDialogue)
        {
            CloseDialogueWindow();
            canFinishDialogue = false;
            GameManager.Instance.PrepareForNextClient();
        }
        else
        {
            if (GameManager.Instance.runningTutorial)
            {
                if (!firstDialogueComplete)
                {
                    CloseDialogueWindow();
                    firstDialogueComplete = true;
                    // ****         MIGHT BE BETTER TO HAVE A SEPARATE TUTORIAL SCRIPT INSTEAD
                    MenuManager.Instance.OpenTutorialPopup("initialTutorial");
                    GameManager.Instance.canStartDay = true;
                }
                else if (introductionComplete && notSeenDeskHint)
                {
                    CloseDialogueWindow();
                    MenuManager.Instance.OpenTutorialPopup("startPrompts");
                    // PUT NAVIGATION HERE INSTEAD
                    SceneManager.Instance.EnableGameplay();
                    notSeenDeskHint = false;
                }
                else if (firstVisitDesk && !cleanDesk)
                {
                    CloseDialogueWindow();
                    MenuManager.Instance.OpenTutorialPopup("initDeskPrompts");
                    firstVisitDesk = false;
                }
                else if ((!firstVisitDesk) && (cleanDesk))
                {
                    CloseDialogueWindow();
                    // MenuManager.Instance.OpenTutorialPopup("grimoire");
                    cleanDesk = false;  // just to avoid this in future checks
                }
                // if the player has submitted the ailment and hasn't seen the next set of hints
                else if (GameManager.Instance.ailmentChosen && notSeenDiagnosisSheetHint)
                {
                    SceneManager.Instance.GetDiagnosisSheet();
                    notSeenDiagnosisSheetHint = false;
                    JumpNextDialogue(DialogueHolder.Instance.td_DiagnosisSheetIntroduction._dialogue);//GetDialogue()
                    
                }
                else if (GameManager.Instance.ailmentChosen && !notSeenDiagnosisSheetHint && notSeenFinalDiagnosisPopup)
                {
                    CloseDialogueWindow();
                    
                    notSeenFinalDiagnosisPopup = false;
                    // MenuManager.Instance.OpenTutorialPopup("diagnosisSheet");
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

                    MenuManager.Instance.OpenTutorialPopup("finalPopup");
                }
            }
            else if (!GameManager.Instance.runningTutorial)
            {
                if (!firstDialogueComplete)
                {
                    CloseDialogueWindow();
                    firstDialogueComplete = true;
                    // ****         MIGHT BE BETTER TO HAVE A SEPARATE TUTORIAL SCRIPT INSTEAD
                    GameManager.Instance.canStartDay = true;
                    SceneManager.Instance.EnableGameplay();
                }
            }
        }
    }

    void CloseDialogueWindow()
    {
        Debug.Log("Closing dialogue window.");
        UIManager.Instance.DisableUI(dialogueBox);
        dialogueSet = false;
        currentLineNumber = 0;

        speakerName.text = defaultSpeaker;
        currentString.text = defaultString;
        //currentString.FontStyle.Normal;

        
    }

    void JumpNextDialogue(List<string> target)//, string speaker)
    {
        //if (target == )
        currentDialogue = target;
        SetupDialogueForCatSpeaker();
        RunDialogue();

        /*if (speaker == "NA")
        {
            speakerName.text = defaultSpeaker;
        }
        else
        {
            speakerName.text = speaker;
        }*/
    }

    public void GetDialogue(string target)
    {
        if (GameManager.Instance.runningTutorial)
        {
            Debug.Log("Tutorial toggled on. Running relevant dialogue.");
            if (target == "tutorial")
            {
                currentDialogue = DialogueHolder.Instance.td_Introduction._dialogue;
                SetupDialogueForCatSpeaker();
                RunDialogue();
            }
            else if (target == "desk")
            {
                // currentDialogue = DialogueHolder.Instance.deskIntroduction._dialogue;
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
            //Debug.Log("Patient time!");
            GetDialogueByClient("entry");
        }

        if (target == "cat responds to client")
        {
            introductionComplete = true;
            
            int randomisedNumber = Random.Range(0, 3);
            currentDialogue = DialogueHolder.Instance.pd_TheCatResponsesList[randomisedNumber]._dialogue;
            
            SetupDialogueForCatSpeaker();
            RunDialogue();
        }

        if (target == "client responds to cat")
        {
            GetDialogueByClient("finish");
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
            //CloseDialogueWindow();
            return;
        }
    }

    void SetupDialogueForCatSpeaker()
    {
        speakerName.text = catName;
        dialogueSet = true;
        currentLineNumber = 0;  // (?)?

        UIManager.Instance.EnableUI(dialogueBox);
    }

    void GetDialogueByClient(string dialogueType)
    {
        nameOfNPC = ClientLetter.Instance.displayedClientName.text;
        speakerName.text = nameOfNPC;
        currentLineNumber = 0;

		// clientIsSpeaking = true;
		

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
            else if (nameOfNPC == "cd_TEMP_NPC01Enter")
            {
                currentDialogue = DialogueHolder.Instance.cd_TEMP_NPC01Enter._dialogue;
            }
            else if (nameOfNPC == "cd_TEMP_NPC02Enter")
            {
                currentDialogue = DialogueHolder.Instance.cd_TEMP_NPC02Enter._dialogue;
            }
            else
            {
                Debug.Log("bruh you goofed it");
            }
        }

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
            else if (nameOfNPC == "cd_TEMP_NPC01Enter")
            {
                currentDialogue = DialogueHolder.Instance.cd_TEMP_NPC01Finish._dialogue;
            }
            else if (nameOfNPC == "cd_TEMP_NPC02Enter")
            {
                currentDialogue = DialogueHolder.Instance.cd_TEMP_NPC02Finish._dialogue;
            }
            else
            {
                Debug.Log("bruh you goofed it");
            }
        }
        
        
        // Debug.Log("SET DIALOGUE: " + nameOfNPC); //currentDialogue)
        dialogueSet = true;
        UIManager.Instance.EnableUI(dialogueBox);
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
            // might be -1... unsure rn,,,,
            /*if ((currentLineNumber == currentDialogue.Count) && (currentDialogue == DialogueHolder.Instance.cd_BarryEnter._dialogue) || (currentDialogue == DialogueHolder.Instance.cd_ArabellaEnter._dialogue) || (currentDialogue == DialogueHolder.Instance.cd_LawrenceEnter._dialogue))
            {
                promptString.text = continuePrompt;
            }
            else
            {*/
                promptString.text = closePrompt;
            //}
        }
    }

    // checks if there's highlighted text in the string and performs an action with the thing inside it
    void CheckDialogueForActions()
    {
        if (currentString.text.Contains("</color>"))
        {
            PerformAction(currentString.text);
        }
        else
        {
			if (clientIsAnswering)
			{
				// Might rely on current submission dialogue only having one line!!
				GetDialogueByClient("finish");
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
        /*else if (stringToRead.Contains("left some old notes out"))
        {

        }*/
        else
        {
            return;
        }
        /*else if (stringToRead.Contains("the characteristics of the herbs"))
        {
            Debug.Log("---.GetComponent<ThingThatDoesTheFlashy>().PulseColour();");
            UIManager.Instance.HighlightCanvasElement("herbGuide");
        }*/
        
    }

    /*void CheckForSpeaker()
    {
        if (currentDialogue == DialogueHolder.Instance.td_Introduction)
        {
            if (currentLineNumber == 2)
            {
                speakerName = null;
                currentString.FontStyle.Italic;
            }
        }
    }*/
}
