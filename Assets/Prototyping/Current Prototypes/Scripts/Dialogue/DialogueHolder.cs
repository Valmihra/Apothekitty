using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public class DialogueSnippet
    {
        public List<string> _dialogue;
        
        /*public bool _isAResponse { get; }
        public string _characterToRespondTo { get; }
        
        public string _speakerName {get; private set;}
        
        
        
        public DialogueSnippet(bool isAResponse, string characterToRespondTo)
        {
            _isAResponse = isAResponse;
            _characterToRespondTo = characterToRespondTo;
        }*/

        /*public void SetSpeaker(string speakerName)
        {
            _speakerName = speakerName;
        }*/
    }

    
    //private const bool isResp = true;
    //private const bool isNotResp = false;
    //private string notResponding = "N/A";
    
    //private string theCat = "The Cat";
    
    
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

    public DialogueSnippet cd_FergusonEnter;
    public DialogueSnippet cd_FergusonFinish;

    public DialogueSnippet cd_PeridotEnter;
    public DialogueSnippet cd_PeridotFinish;
    
    // Initial responses to client before treating
    public  DialogueSnippet pd_TheCatResponse01;
    public  DialogueSnippet pd_TheCatResponse02;
    public  DialogueSnippet pd_TheCatResponse03;

    // Responses to client after submitting?
    // public DialogueSnippet pd_TheCatResponseBarry;
    // public DialogueSnippet pd_TheCatResponseArabella;
    // public DialogueSnippet pd_TheCatResponseLawrence;
    // public DialogueSnippet pd_TheCatResponseJimothy;
    // public DialogueSnippet pd_TheCatResponseFerguson;
    // public DialogueSnippet pd_TheCatResponsePeridot;
    
    public List<DialogueSnippet> pd_TheCatResponsesList;
    
    public  DialogueSnippet pd_TheCatSubmitHerbs;



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
        //
        //td_Introduction = new DialogueSnippet(isNotResp, notResponding);
        //td_Introduction.SetSpeaker(theCat);
        td_Introduction = new DialogueSnippet();
            td_Introduction._dialogue = new List<string>();
            td_Introduction._dialogue.Add("Back to work...");
            td_Introduction._dialogue.Add("Sounds busy out there. I'd better open up the shop!");
            
        td_Desk01 = new DialogueSnippet();
        //td_Desk01.SetSpeaker(theCat);
            td_Desk01._dialogue = new List<string>();
            td_Desk01._dialogue.Add("I haven't done this in a while... I'd better refamiliarise myself with the process.");
            td_Desk01._dialogue.Add("Oh, silly me! I've left some old notes out. I'd better <#8A1E1E>clean up the workspace</color> a bit before I get started.");

        td_Desk02 = new DialogueSnippet();
        //td_Desk02.SetSpeaker(theCat);
            td_Desk02._dialogue = new List<string>();
            td_Desk02._dialogue.Add("Much better!");
            td_Desk02._dialogue.Add("Okay, all the information I'll need is on the New Client Form. I just have to match it up to the <#8A1E1E>Grimoire</color>!");     // should be "New Patient Form" ,,,!
                    
        td_GrimoireAilmentSubmitted = new DialogueSnippet();
        //td_GrimoireAilmentSubmitted.SetSpeaker(theCat);
            td_GrimoireAilmentSubmitted._dialogue = new List<string>();
            td_GrimoireAilmentSubmitted._dialogue.Add("Yay! I've got a good feeling about this... Now I need to come up with the treatment plan!");
        
        td_DiagnosisSheetIntroduction = new DialogueSnippet();
        //td_DiagnosisSheetIntroduction.SetSpeaker(theCat);
            td_DiagnosisSheetIntroduction._dialogue = new List<string>();
            td_DiagnosisSheetIntroduction._dialogue.Add("Let's see... Each recipe has an <#8A1E1E>effect</color>. I can choose to <b>heal</b>, <b>ease</b>, or <b>fortify</b>.");
            td_DiagnosisSheetIntroduction._dialogue.Add("<b>Heal</b> restores damage, <b>ease</b> provides a soothing effect, and <b>fortify</b> builds resistance and resilience.");
            td_DiagnosisSheetIntroduction._dialogue.Add("Then, I just need to choose a <#8A1E1E>target</color> area for that effect. I can treat the client's <b>mind</b> or <b>body</b>.");

        td_DiagnosisSheetSubmitted = new DialogueSnippet();
        //td_DiagnosisSheetSubmitted.SetSpeaker(theCat);
            td_DiagnosisSheetSubmitted._dialogue = new List<string>();
            td_DiagnosisSheetSubmitted._dialogue.Add("Wahoo! Now it's time for the fun part... Creating the recipe!");

        td_HerbWall = new DialogueSnippet();
        //td_HerbWall.SetSpeaker(theCat);
            td_HerbWall._dialogue = new List<string>();
            td_HerbWall._dialogue.Add("Hello, my precious herb collection! Let's see... I can click each drawer to see what's inside... And if I hover over the contents, it'll give me a description.");
            td_HerbWall._dialogue.Add("The <#8A1E1E>herb guide</color> on the left of the screen will tell me the characteristics of the herbs I need to treat the client. I need to find one herb for each effect and each target I have chosen.");
            td_HerbWall._dialogue.Add("Once I think I found the right herb, I can click and drag them across into my inventory.");
            td_HerbWall._dialogue.Add("And if I think I've made the wrong choice, I can click the <#8A1E1E>remove herb</color> button to get rid of it!");

            
            
            
            
        // Character dialogue snippets
        
        // UPDATED
        cd_BarryEnter = new DialogueSnippet();
            cd_BarryEnter._dialogue = new List<string>();
            cd_BarryEnter._dialogue.Add("Good morrow, little kitten healer. I'm in need of some help.");
            cd_BarryEnter._dialogue.Add("Well, the missus thinks I need help. I'm just here to humour her.");
            cd_BarryEnter._dialogue.Add("No, really! Takes a lot to get me down.");
        // UPDATED
        cd_BarryFinish = new DialogueSnippet();
            cd_BarryFinish._dialogue = new List<string>();
            cd_BarryFinish._dialogue.Add("Thank you, little kitten. Keep up the good work.");
            cd_BarryFinish._dialogue.Add("Both myself and my wife are very grateful for your service.");

        // UPDATED
        cd_ArabellaEnter = new DialogueSnippet();
            cd_ArabellaEnter._dialogue = new List<string>();
            cd_ArabellaEnter._dialogue.Add("Hello...");
            cd_ArabellaEnter._dialogue.Add("Um... I heard you were the one to go to for... discrete treatments?");
            cd_ArabellaEnter._dialogue.Add("O-Oh! My name is Arabella.");
        // UPDATED
        cd_ArabellaFinish = new DialogueSnippet();
            cd_ArabellaFinish._dialogue = new List<string>();
            cd_ArabellaFinish._dialogue.Add("Sorry to be a bother, but is  my order ready, Apothekitty?");
            cd_ArabellaFinish._dialogue.Add("Oh! Thank you.");
            cd_ArabellaFinish._dialogue.Add("I appreciate the help, and your non-judgemental nature. I will be sure to write a letter to update you of my p-progress soon.");
        
        // UPDATED
        cd_LawrenceEnter = new DialogueSnippet();
            cd_LawrenceEnter._dialogue = new List<string>();
            cd_LawrenceEnter._dialogue.Add("Well hello there, pal ... I think I might be in need of some help.");
            cd_LawrenceEnter._dialogue.Add("I don't wish to alarm you, but I fear I was bitten by something, and have noticed some abnormalities since!");
        // UPDATED
        cd_LawrenceFinish = new DialogueSnippet();
            cd_LawrenceFinish._dialogue = new List<string>();
            cd_LawrenceFinish._dialogue.Add("Thank you, thank you, thank you Apothekitty!!");
            cd_LawrenceFinish._dialogue.Add("Yes yes, I can see the care you put into your work! You shall forever be a close pal of Lawrence! Take care of yourself. I bid you adieu.");




        // UPDATED
        cd_JimothyEnter = new DialogueSnippet();
            cd_JimothyEnter._dialogue = new List<string>();
            cd_JimothyEnter._dialogue.Add("E-Excuse me… could I take a moment of your time?");
            cd_JimothyEnter._dialogue.Add("I'm so w-worried… that I'll lose my job as the king's j-jester… please, dear cat doctor...");
            cd_JimothyEnter._dialogue.Add("...Y-You can help me!? Oh, I would be ever so g-greatful…!");
            cd_JimothyEnter._dialogue.Add("AH! Oh, my sincerest a-apologies cat d-doctor… I'm quite jumpy...");
        // UPDATED
        cd_JimothyFinish = new DialogueSnippet();
        //cd_JimothyFinish.SetSpeaker(theCat);();
            cd_JimothyFinish._dialogue = new List<string>();
            cd_JimothyFinish._dialogue.Add("And you’re s-sure this will help me p-perform better...?");
            cd_JimothyFinish._dialogue.Add("Well, alright... you are the p-professional. Thank you for t-treating me, cat doctor.");

        // UPDATED
        cd_FergusonEnter = new DialogueSnippet();
            cd_FergusonEnter._dialogue = new List<string>();
            cd_FergusonEnter._dialogue.Add("Hey there! Heard from yonder that this was the place for questionable ailments?");
            cd_FergusonEnter._dialogue.Add("All good. Actually, I ain't sure if there's anythin' wrong with me, but my ma and pa are really upset, think I'm a failure and advised I get checked up...");
            cd_FergusonEnter._dialogue.Add("I'll do anythin' to help reassure 'em and make 'em proud of me!");
        // UPDATED
        cd_FergusonFinish = new DialogueSnippet();
            cd_FergusonFinish._dialogue = new List<string>();
            cd_FergusonFinish._dialogue.Add("'Preciate ya. I'm more confident approachin' ma and pa now.");
            cd_FergusonFinish._dialogue.Add("I really wan' make 'em proud, ya know?");
            
            
        cd_PeridotEnter = new DialogueSnippet();
            cd_PeridotEnter._dialogue = new List<string>();
            cd_PeridotEnter._dialogue.Add("PLACEHOLDER TEXT: why do they call it oven when you of in the cold food of out hot eat the food?");

        cd_PeridotFinish = new DialogueSnippet();
            cd_PeridotFinish._dialogue = new List<string>();
            cd_PeridotFinish._dialogue.Add("PLACEHOLDER TEXT: nvm apothekitty, i think i had a stroke actually");
            
            
            
            
            
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