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

    int currentLine;
    bool dialogueSet;
    public bool firstDialogueComplete;
    public bool introductionComplete;
    private bool notSeenDeskHint;

    string defaultSpeaker = null;
    string defaultString = "You have encountered this due to an error with the DialogueRunner or DialogueHolder scripts";

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
        //UIManager.Instance.DisableUI(dialogueBox);
        Debug.Log("Resetting DialogueRunner...");

        currentLine = 0;
        dialogueSet = false;
        introductionComplete = false;
        firstDialogueComplete = false;
        notSeenDeskHint = true;
    }

    public void RunDialogue()
    {
        //Debug.Log("Dialogue set? " + dialogueSet);
        

        if (dialogueSet)
        {
            Debug.Log("Right now, currentDialogue is holding " + currentDialogue.Count + " strings of dialogue.");
            Debug.Log("You are currently on line: " + currentLine + ".");

            if (currentLine >= currentDialogue.Count)
            {
                Debug.Log("currentLine is larger than currentDialogue.Count. Checking whether this is intentional or not.");
                if ((currentDialogue == DialogueHolder.Instance.barry.dialogue_) || (currentDialogue == DialogueHolder.Instance.arabella.dialogue_) || (currentDialogue == DialogueHolder.Instance.lawrence.dialogue_))
                {
                    if (!introductionComplete)
                    {
                        currentDialogue = null;
                        currentLine = 0;
                        speakerName.text = "The Cat";
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
                    CloseDialogueWindow();
                }
            }
            else
            {
                if (currentLine < currentDialogue.Count)
                {
                    // updates the text
                    //CheckForSpeaker();
                    currentString.text = currentDialogue[currentLine];
                    currentLine++;
                    //Debug.Log("Next line will be: " + currentLine);
                }
            }
        }
        else
        {
            Debug.Log("No set dialogue. Closing the dialogue window.");
            CloseDialogueWindow();
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

        if (!firstDialogueComplete)
        {
            firstDialogueComplete = true;
            // ****         MIGHT BE BETTER TO HAVE A SEPARATE TUTORIAL SCRIPT INSTEAD
            MenuManager.Instance.TutorialPopup("initialTutorial");
            GameManager.Instance.canStartDay = true;
        }

        if (introductionComplete && notSeenDeskHint)
        {
            MenuManager.Instance.TutorialPopup("startPrompts");
            // PUT NAVIGATION HERE INSTEAD
            SceneManager.Instance.EnableGameplay();
            notSeenDeskHint = false;
        }
    }

    public void GetDialogue(string target)
    {
        if (target == "tutorial")
        {
            currentDialogue = DialogueHolder.Instance.introduction.dialogue_;
            speakerName.text = "The Cat";
            dialogueSet = true;

            UIManager.Instance.EnableUI(dialogueBox);
            RunDialogue();
        }

        else if (target == "patientArrive")
        {
            //Debug.Log("Patient time!");
            GetDialogueByClient();

            /*if (ClientLetter.Instance.clientLetter.clientName_ == "Barry")
            {
                currentDialogue = DialogueHolder.Instance.barry.dialogue_;
                //speakerName.text = "The Cat";
                //dialogueSet = true;
            }
            if (ClientLetter.Instance.clientLetter.clientName_ == "Arabella")
            {
                currentDialogue = DialogueHolder.Instance.arabella.dialogue_;
            }
            if (ClientLetter.Instance.clientLetter.clientName_ == "Lawrence")
            {
                currentDialogue = DialogueHolder.Instance.lawrence.dialogue_;
            }
            dialogueSet = true;
            speakerName.text = ClientLetter.Instance.clientLetter.clientName_;
            UIManager.Instance.EnableUI(dialogueBox);
            RunDialogue();*/
        }
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
