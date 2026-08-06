using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueRunner : MonoBehaviour
{
    public CanvasGroup dialogueBox;
    //public Button nextDialogue;
    public TMP_Text speakerName;
    public TMP_Text currentString;
    public TMP_Text promptString;

    int currentLine;
    bool dialogueSet;
    public bool firstDialogueComplete;
    public bool introductionComplete;

    private bool notSeenDeskHint;
    private bool firstVisitDesk;
    private bool notSeenDiagnosisSheetHint;
    private bool notSeenFinalDiagnosisPopup;
    private bool justSubmittedDiagnosis;
    private bool justVisitedHerbWall;
    

    string defaultSpeaker = null;
    string defaultString = "You have encountered this due to an error with the DialogueRunner or DialogueHolder scripts";

    string continuePrompt = "Click box to continue";
    string closePrompt = "Click box to close";


    string catName = "The Cat";

    // oh i can't wait for the overarching "if (!tutorialCompleted) else that will RADICALLY influence how quickly I can navigate through all this,,
    

    List<string> currentDialogue;
    private string client;

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
        UIManager.Instance.DisableUI(dialogueBox);
        Debug.Log("Resetting DialogueRunner...");

        currentLine = 0;
        dialogueSet = false;
        introductionComplete = false;
        firstDialogueComplete = false;

        notSeenDeskHint = true;
        firstVisitDesk = false;
        notSeenDiagnosisSheetHint = true;
        notSeenFinalDiagnosisPopup = true;
        justSubmittedDiagnosis = false;
        justVisitedHerbWall = false;
    }

    public void RunDialogue()
    {
        //Debug.Log("Dialogue set? " + dialogueSet);
        

        if (dialogueSet)
        {
            Debug.Log("Right now, currentDialogue is holding " + currentDialogue.Count + " strings of dialogue.");
            Debug.Log("You are currently on line: " + currentLine + ".");

            GetPromptString();

            if (currentLine >= currentDialogue.Count)
            {
                // at or over the limit of the lines of dialogue
                Debug.Log("currentLine is larger than currentDialogue.Count. Checking whether this is intentional or not.");
                if ((currentDialogue == DialogueHolder.Instance.barry.dialogue_) || (currentDialogue == DialogueHolder.Instance.arabella.dialogue_) || (currentDialogue == DialogueHolder.Instance.lawrence.dialogue_))
                {
                    if (!introductionComplete)
                    {
                        currentDialogue = null;
                        currentLine = 0;
                        speakerName.text = catName;
                        currentString.text = "You've come to the right place! Just pass me your patient form and take a seat. I'll take care of everything.";
                        introductionComplete = true;
                        dialogueSet = false;
                    }
                    else
                    {
                        Debug.Log("Bro how'd you fuck it up like this?");
                    }
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
            else
            {
                // within the limit of the lines of dialogue
                if (currentLine < currentDialogue.Count)
                {
                    // updates the text
                    //CheckForSpeaker();
                    currentString.text = currentDialogue[currentLine];
                    CheckDialogueForActions();
                    currentLine++;
                    //Debug.Log("Next line will be: " + currentLine);
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
        if (GameManager.Instance.runningTutorial)
        {
            if (!firstDialogueComplete)
            {
                CloseDialogueWindow();
                firstDialogueComplete = true;
                // ****         MIGHT BE BETTER TO HAVE A SEPARATE TUTORIAL SCRIPT INSTEAD
                MenuManager.Instance.TutorialPopup("initialTutorial");
                GameManager.Instance.canStartDay = true;
            }
            else if (introductionComplete && notSeenDeskHint)
            {
                CloseDialogueWindow();
                MenuManager.Instance.TutorialPopup("startPrompts");
                // PUT NAVIGATION HERE INSTEAD
                SceneManager.Instance.EnableGameplay();
                notSeenDeskHint = false;
            }
            else if (firstVisitDesk)
            {
                CloseDialogueWindow();
                MenuManager.Instance.TutorialPopup("grimoire");
                firstVisitDesk = false;
            }
            // if the player has submitted the ailment and hasn't seen the next set of hints
            else if (GameManager.Instance.ailmentChosen && notSeenDiagnosisSheetHint)
            {
                SceneManager.Instance.GetDiagnosisSheet();
                notSeenDiagnosisSheetHint = false;
                JumpNextDialogue(DialogueHolder.Instance.diagnosisSheetIntroduction.dialogue_);//GetDialogue()
                
            }
            else if (GameManager.Instance.ailmentChosen && !notSeenDiagnosisSheetHint && notSeenFinalDiagnosisPopup)
            {
                CloseDialogueWindow();
                
                notSeenFinalDiagnosisPopup = false;
                MenuManager.Instance.TutorialPopup("diagnosisSheet");
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

                MenuManager.Instance.TutorialPopup("finalPopup");
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

    void CloseDialogueWindow()
    {
        Debug.Log("Closing dialogue window.");
        UIManager.Instance.DisableUI(dialogueBox);
        dialogueSet = false;
        currentLine = 0;

        speakerName.text = defaultSpeaker;
        currentString.text = defaultString;
        //currentString.FontStyle.Normal;

        
    }

    void JumpNextDialogue(List<string> target)//, string speaker)
    {
        //if (target == )
        currentDialogue = target;
        SetupDialogueForTutorial();
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
            Debug.Log("trueeee");
            if (target == "tutorial")
            {
                currentDialogue = DialogueHolder.Instance.introduction.dialogue_;
                SetupDialogueForTutorial();
                RunDialogue();
            }
            else if (target == "desk")
            {
                currentDialogue = DialogueHolder.Instance.deskIntroduction.dialogue_;
                SetupDialogueForTutorial();
                RunDialogue();
                firstVisitDesk = true;
            }
            else if (target == "ailmentSubmitted")
            {
                currentDialogue = DialogueHolder.Instance.ailmentSubmittedIntroduction.dialogue_;
                SetupDialogueForTutorial();
                RunDialogue();
            }
            else if (target == "treatmentPlanSubmitted")
            {
                currentDialogue = DialogueHolder.Instance.treatmentPlanSubmittedIntroduction.dialogue_;
                SetupDialogueForTutorial();
                RunDialogue();
                justSubmittedDiagnosis = true;
            }
            else if (target == "onHerbWall")
            {
                currentDialogue = DialogueHolder.Instance.herbWallIntroduction.dialogue_;
                SetupDialogueForTutorial();
                RunDialogue();
                justVisitedHerbWall = true;
            }
        }
        
        if (target == "patientArrive")
        {
            //Debug.Log("Patient time!");
            GetDialogueByClient();
        }

        else
        {
            //CloseDialogueWindow();
            return;
        }
    }

    void SetupDialogueForTutorial()
    {
        speakerName.text = catName;
        dialogueSet = true;
        currentLine = 0;// (?)?

        UIManager.Instance.EnableUI(dialogueBox);
    }

    void GetDialogueByClient()
    {
        client = ClientLetter.Instance.clientName.text;
        speakerName.text = client;
        
        if (client == "Barry Buff")
        {
            currentDialogue = DialogueHolder.Instance.barry.dialogue_;
        }
        else if (client == "Arabella Bunny")
        {
            currentDialogue = DialogueHolder.Instance.arabella.dialogue_;
        }
        else if (client == "Lawrence Lark")
        {
            currentDialogue = DialogueHolder.Instance.lawrence.dialogue_;
        }
        
        dialogueSet = true;
        UIManager.Instance.EnableUI(dialogueBox);
        RunDialogue();
    }

    void GetPromptString()
    {
        if (currentLine < currentDialogue.Count - 1)
        {
            promptString.text = continuePrompt;
        }
        else
        {
            // might be -1... unsure rn,,,,
            /*if ((currentLine == currentDialogue.Count) && (currentDialogue == DialogueHolder.Instance.barry.dialogue_) || (currentDialogue == DialogueHolder.Instance.arabella.dialogue_) || (currentDialogue == DialogueHolder.Instance.lawrence.dialogue_))
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
            return;
        }
    }

    void PerformAction(string stringToRead)
    {
        
        if (stringToRead.Contains("Grimoire"))
        {
            Debug.Log("---.GetComponent<ThingThatDoesTheFlashy>().PulseColour();");
            UIManager.Instance.HighlightCanvasElement("grimoire");
        }
        
        else if (stringToRead.Contains("Each recipe has an"))
        {
            Debug.Log("---.GetComponent<ThingThatDoesTheFlashy>().PulseColour();");
            UIManager.Instance.HighlightCanvasElement("effect");
        }
        else if (stringToRead.Contains("I just need to choose a"))
        {
            Debug.Log("---.GetComponent<ThingThatDoesTheFlashy>().PulseColour();");
            UIManager.Instance.HighlightCanvasElement("target");
        }
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
        if (currentDialogue == DialogueHolder.Instance.introduction)
        {
            if (currentLine == 2)
            {
                speakerName = null;
                currentString.FontStyle.Italic;
            }
        }
    }*/
}
