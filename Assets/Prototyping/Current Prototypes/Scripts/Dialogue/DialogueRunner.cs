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

    void Start()
    {
        Reset();
    }

    void Reset()
    {
        //UIManager.Instance.DisableUI(dialogueBox);

        currentLine = 0;
        dialogueSet = false;
        introductionComplete = false;
        firstDialogueComplete = false;
        notSeenDeskHint = true;
    }
    public void RunDialogue()
    {
        //Debug.Log("Running");
        if (dialogueSet)
        {
            if (currentLine < currentDialogue.Count)
            {
                // updates the text
                //CheckForSpeaker();
                currentString.text = currentDialogue[currentLine];
                currentLine++;
            }
            else
            {
                //if (!introductionComplete)
                //{
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
                    }
                //}   //else if !submittingFinal
                else
                {
                    CloseDialogueWindow();
                }
                //CloseDialogueWindow();
            }
        }
        else
        {
            CloseDialogueWindow();
        }

        
    }

    void CloseDialogueWindow()
    {
        UIManager.Instance.DisableUI(dialogueBox);
        dialogueSet = false;
        currentLine = 0;

        speakerName.text = defaultSpeaker;
        currentString.text = defaultString;
        //currentString.FontStyle.Normal;

        if (!firstDialogueComplete)
        {
            firstDialogueComplete = true;
            MenuManager.Instance.TutorialPopup("initialTutorial");
            GameManager.Instance.canStartDay = true;
            //GameManager.Instance.canInteractWithCurtain = true;
        }
        if (introductionComplete && notSeenDeskHint)
        {
            MenuManager.Instance.TutorialPopup("startPrompts");
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
            Debug.Log("Patient time!");
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
            //dialogueSet = true;
            //UIManager.Instance.EnableUI(dialogueBox);
            //RunDialogue();
        }
        else if (client == "Lawrence Lark")
        {
            currentDialogue = DialogueHolder.Instance.lawrence.dialogue_;
            //dialogueSet = true;
            //UIManager.Instance.EnableUI(dialogueBox);
            //RunDialogue();
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
