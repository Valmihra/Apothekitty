using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public class DialogueSnippet
    {
        //public string speakerName_;
        //public string currentLine_;

        public List<string> dialogue_;


        //public void 
    }

    // Separate Dialogue Snippets
    public DialogueSnippet introduction;
    
    public DialogueSnippet barry;
    public DialogueSnippet barryFinish;
    //public DialogueSnippet 
    public DialogueSnippet arabella;
    public DialogueSnippet arabellaFinish;

    public DialogueSnippet lawrence;
    public DialogueSnippet lawrenceFinish;


    private static DialogueHolder _instance;
    public static DialogueHolder Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        GenerateDialogue();
    }

    void GenerateDialogue()
    {
        introduction = new DialogueSnippet();
            introduction.dialogue_ = new List<string>();
            introduction.dialogue_.Add("Back to work...");
            introduction.dialogue_.Add("Sounds busy out there. I'd better open up the shop.");

        barry = new DialogueSnippet();
            barry.dialogue_ = new List<string>();
            barry.dialogue_.Add("Good day, little kitten healer. I'm in need of some help.");
            barry.dialogue_.Add("Well, my wife thinks I need help. I'm just here to humour her.");
            barry.dialogue_.Add("No, really! Takes a lot to get me down.");
            barry.dialogue_.Add("Hahaha...");

        barryFinish = new DialogueSnippet();
            barryFinish.dialogue_ = new List<string>();
            barryFinish.dialogue_.Add("Thank you little kitten - I- I mean Apothekitty. Keep up the good work.");

        arabella = new DialogueSnippet();
            arabella.dialogue_ = new List<string>();
            arabella.dialogue_.Add("Hello...");
            arabella.dialogue_.Add("Um... I heard you were the one to go to for... discrete treatments?");
            arabella.dialogue_.Add("O-Oh! My name is Arabella.");
        
        arabellaFinish = new DialogueSnippet();
            arabellaFinish.dialogue_ = new List<string>();
            arabellaFinish.dialogue_.Add("Helloooo. I-is my order ready, Apothekitty?");
            arabellaFinish.dialogue_.Add("Oh! Thank you.");
            arabellaFinish.dialogue_.Add("I appreciate the help, and your non-judgemental nature. I will write back soon.");
            
            

        lawrence = new DialogueSnippet();
            lawrence.dialogue_ = new List<string>();
            lawrence.dialogue_.Add("Hi friend... I think I might need some help.");
            lawrence.dialogue_.Add("I don't mean to alarm you, but I was bitten by something, and I don't know what to do about it!");

        lawrenceFinish = new DialogueSnippet();
            lawrenceFinish.dialogue_ = new List<string>();
            lawrenceFinish.dialogue_.Add("Thank you, thank you, thank you Apothekitty!!");
    }


    //
}
