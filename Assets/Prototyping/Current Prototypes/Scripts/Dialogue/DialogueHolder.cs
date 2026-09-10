using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public class DialogueSnippet
    {
        //public string speakerName_;
        //public string currentLineNumber_;

        public List<string> _dialogue;


        //public void 
    }

    // Separate Dialogue Snippets
    // td = tutorial dialogue
    // cd = client dialogue
    // pd = player dialogue
    public DialogueSnippet td_Introduction;
    public DialogueSnippet td_Desk01;
    public DialogueSnippet td_Desk02;
    public DialogueSnippet td_GrimoireAilmentSubmitted;
    public DialogueSnippet td_DiagnosisSheetIntroduction;
    public DialogueSnippet td_DiagnosisSheetSubmitted;
    public DialogueSnippet td_HerbWall;
    
    public DialogueSnippet cd_BarryEnter;
    public DialogueSnippet cd_BarryFinish;
    
    public DialogueSnippet cd_ArabellaEnter;
    public DialogueSnippet cd_ArabellaFinish;

    public DialogueSnippet cd_LawrenceEnter;
    public DialogueSnippet cd_LawrenceFinish;

    public DialogueSnippet cd_JimothyEnter;
    public DialogueSnippet cd_JimothyFinish;

    public DialogueSnippet cd_TEMP_PATIENT01Enter;
    public DialogueSnippet cd_TEMP_PATIENT01Finish;

    public DialogueSnippet cd_TEMP_PATIENT02Enter;
    public DialogueSnippet cd_TEMP_PATIENT02Finish;
    
    public  DialogueSnippet pd_TheCatResponse01;
    public  DialogueSnippet pd_TheCatResponse02;
    public  DialogueSnippet pd_TheCatResponse03;
    public List<DialogueSnippet> pd_TheCatResponsesList;
    
    public  DialogueSnippet pd_TheCatSubmitHerbs;

    // COLOUR CHANGES
    // <color=red>  <#8A1E1E>
    // private string customColourHexadecimal = "#8A1E1E";  DIDN'T WORK?? UHHH AM I ESTUPIDO

    //public CanvasGroup


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
        // Tutorial dialogue snippets
        td_Introduction = new DialogueSnippet();
            td_Introduction._dialogue = new List<string>();
            td_Introduction._dialogue.Add("Back to work...");
            td_Introduction._dialogue.Add("Sounds busy out there. I'd better open up the shop!");
            
        td_Desk01 = new DialogueSnippet();
            td_Desk01._dialogue = new List<string>();
            td_Desk01._dialogue.Add("I haven't done this in a while... I'd better refamiliarise myself with the process.");
            td_Desk01._dialogue.Add("Oh, silly me! I've left some old notes out. I'd better <#8A1E1E>clean up the workspace</color> a bit before I get started.");

        td_Desk02 = new DialogueSnippet();
            td_Desk02._dialogue = new List<string>();
            td_Desk02._dialogue.Add("Much better!");
            td_Desk02._dialogue.Add("Okay, all the information I'll need is on the New Client Form. I just have to match it up to the <#8A1E1E>Grimoire</color>!");     // should be "New Patient Form" ,,,!
                    
        td_GrimoireAilmentSubmitted = new DialogueSnippet();
            td_GrimoireAilmentSubmitted._dialogue = new List<string>();
            td_GrimoireAilmentSubmitted._dialogue.Add("Yay! I've got a good feeling about this... Now I need to come up with the treatment plan!");
        
        td_DiagnosisSheetIntroduction = new DialogueSnippet();
            td_DiagnosisSheetIntroduction._dialogue = new List<string>();
            td_DiagnosisSheetIntroduction._dialogue.Add("Let's see... Each recipe has an <#8A1E1E>effect</color>. I can choose to <b>heal</b>, <b>ease</b>, or <b>fortify</b>.");
            td_DiagnosisSheetIntroduction._dialogue.Add("<b>Heal</b> restores damage, <b>ease</b> provides a soothing effect, and <b>fortify</b> builds resistance and resilience.");
            td_DiagnosisSheetIntroduction._dialogue.Add("Then, I just need to choose a <#8A1E1E>target</color> area for that effect. I can treat the client's <b>mind</b> or <b>body</b>.");

        td_DiagnosisSheetSubmitted = new DialogueSnippet();
            td_DiagnosisSheetSubmitted._dialogue = new List<string>();
            td_DiagnosisSheetSubmitted._dialogue.Add("Wahoo! Now it's time for the fun part... Creating the recipe!");

        td_HerbWall = new DialogueSnippet();
            td_HerbWall._dialogue = new List<string>();
            td_HerbWall._dialogue.Add("Hello, my precious herb collection! Let's see... I can click each drawer to see what's inside... And if I hover over the contents, it'll give me a description.");
            td_HerbWall._dialogue.Add("The <#8A1E1E>herb guide</color> on the left of the screen will tell me the characteristics of the herbs I need to treat the client. I need to find one herb for each effect and each target I have chosen.");
            td_HerbWall._dialogue.Add("Once I think I found the right herb, I can click and drag them across into my inventory.");
            td_HerbWall._dialogue.Add("And if I think I've made the wrong choice, I can click the <#8A1E1E>remove herb</color> button to get rid of it!");

            
            
            
            
        // Character dialogue snippets
        cd_BarryEnter = new DialogueSnippet();
            cd_BarryEnter._dialogue = new List<string>();
            cd_BarryEnter._dialogue.Add("Good day, little kitten healer. I'm in need of some help.");
            cd_BarryEnter._dialogue.Add("Well, my wife thinks I need help. I'm just here to humour her.");
            cd_BarryEnter._dialogue.Add("No, really! Takes a lot to get me down.");
            cd_BarryEnter._dialogue.Add("Hahaha...");

        cd_BarryFinish = new DialogueSnippet();
            cd_BarryFinish._dialogue = new List<string>();
            cd_BarryFinish._dialogue.Add("Thank you little kitten - I- I mean Apothekitty. Keep up the good work.");

        cd_ArabellaEnter = new DialogueSnippet();
            cd_ArabellaEnter._dialogue = new List<string>();
            cd_ArabellaEnter._dialogue.Add("Hello...");
            cd_ArabellaEnter._dialogue.Add("Um... I heard you were the one to go to for... discrete treatments?");
            cd_ArabellaEnter._dialogue.Add("O-Oh! My name is Arabella.");
        
        cd_ArabellaFinish = new DialogueSnippet();
            cd_ArabellaFinish._dialogue = new List<string>();
            cd_ArabellaFinish._dialogue.Add("Helloooo. I-is my order ready, Apothekitty?");
            cd_ArabellaFinish._dialogue.Add("Oh! Thank you.");
            cd_ArabellaFinish._dialogue.Add("I appreciate the help, and your non-judgemental nature. I will write back soon.");
            
        cd_LawrenceEnter = new DialogueSnippet();
            cd_LawrenceEnter._dialogue = new List<string>();
            cd_LawrenceEnter._dialogue.Add("Hi friend... I think I might need some help.");
            cd_LawrenceEnter._dialogue.Add("I don't mean to alarm you, but I was bitten by something, and I don't know what to do about it!");

        cd_LawrenceFinish = new DialogueSnippet();
            cd_LawrenceFinish._dialogue = new List<string>();
            cd_LawrenceFinish._dialogue.Add("Thank you, thank you, thank you Apothekitty!!");





        cd_JimothyEnter = new DialogueSnippet();
            cd_JimothyEnter._dialogue = new List<string>();
            cd_JimothyEnter._dialogue.Add("PLACEHOLDER TEXT: bruh i got stagefright and bells that jingle jangle jingle (jingle jangle) as i go riding merrily along");

        cd_JimothyFinish = new DialogueSnippet();
            cd_JimothyFinish._dialogue = new List<string>();
            cd_JimothyFinish._dialogue.Add("PLACEHOLDER TEXT: sayounara you later");
            

        cd_TEMP_PATIENT01Enter = new DialogueSnippet();
            cd_TEMP_PATIENT01Enter._dialogue = new List<string>();
            cd_TEMP_PATIENT01Enter._dialogue.Add("PLACEHOLDER TEXT: i don't exist yet!");

        cd_TEMP_PATIENT01Finish = new DialogueSnippet();
            cd_TEMP_PATIENT01Finish._dialogue = new List<string>();
            cd_TEMP_PATIENT01Finish._dialogue.Add("PLACEHOLDER TEXT: ariga-thank you for your treatment");


        cd_TEMP_PATIENT02Enter = new DialogueSnippet();
            cd_TEMP_PATIENT02Enter._dialogue = new List<string>();
            cd_TEMP_PATIENT02Enter._dialogue.Add("PLACEHOLDER TEXT: why do they call it oven when you of in the cold food of out hot eat the food?");

        cd_TEMP_PATIENT02Finish = new DialogueSnippet();
            cd_TEMP_PATIENT02Finish._dialogue = new List<string>();
            cd_TEMP_PATIENT02Finish._dialogue.Add("PLACEHOLDER TEXT: nvm apothekitty, i think i had a stroke actually");
            
            
            
            
            
        pd_TheCatResponse01 = new DialogueSnippet();
            pd_TheCatResponse01._dialogue = new List<string>();
            pd_TheCatResponse01._dialogue.Add("You've come to the right place! Just pass me your client form and take a seat. I'll take care of everything.");
            
        pd_TheCatResponse02 = new DialogueSnippet();
            pd_TheCatResponse02._dialogue = new List<string>();
            pd_TheCatResponse02._dialogue.Add("Not a problem. If you've got your paperwork prepared, I'm happy to look at it right away.");
        
        pd_TheCatResponse03 = new DialogueSnippet();
            pd_TheCatResponse03._dialogue = new List<string>();
            pd_TheCatResponse03._dialogue.Add("You're in safe hands, my friend. Thank you for completing the paperwork, I'll get you feeling better in no time.");
            
        pd_TheCatResponsesList = new List<DialogueSnippet>();
            pd_TheCatResponsesList.Add(pd_TheCatResponse01);
            pd_TheCatResponsesList.Add(pd_TheCatResponse02);
            pd_TheCatResponsesList.Add(pd_TheCatResponse03);
            
            
            
            
            
        pd_TheCatSubmitHerbs = new DialogueSnippet();
            pd_TheCatSubmitHerbs._dialogue = new List<string>();
            pd_TheCatSubmitHerbs._dialogue.Add("Here's your treatment, thank you for waiting!");
            //
    }
}