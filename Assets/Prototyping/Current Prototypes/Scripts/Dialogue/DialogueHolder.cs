using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public class DialogueSnippet
    {
        public List<string> _dialogue;
    }

    /*public class CharacterDialogue
    {
        public List<string> _shopEntry;
        public List<string> _patientResponse;
        public List<string> _catResponse;
        public List<string> _catSubmit;
        public List<string> _shopExit;

        public bool _hasLinkedDialogue = false;
    }*/
    
    // public class 

    
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
    // public DialogueSnippet td_HerbWall02;

    
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
    
    //public CharacterDialogue activeCharacterDialogue;


    public List<string> patientEntryDialogue;
    public List<string> patientExitDialogue;
    
    //public CharacterDialogue cd_Jimothy;



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
            td_Introduction._dialogue.Add("Oh, is it open time already?!");
            td_Introduction._dialogue.Add("Sounds busy out there. I'd better open up the shop!");
            
        td_Desk01 = new DialogueSnippet();
        //td_Desk01.SetSpeaker(theCat);
            td_Desk01._dialogue = new List<string>();
            td_Desk01._dialogue.Add("I've been so preoccupied with my studies, it's been a while since my last shift.");
            td_Desk01._dialogue.Add("Oh, silly me! I left my workspace a mess. I should get rid of all this <#8A1E1E>paper</color> and <#8A1E1E>clean up</color> before I get started.");

        td_Desk02 = new DialogueSnippet();
        //td_Desk02.SetSpeaker(theCat);
            td_Desk02._dialogue = new List<string>();
            td_Desk02._dialogue.Add("Phew, much better!");
            td_Desk02._dialogue.Add("Ah, here's Jimothy's patient form. Now let's check my <#8A1E1E>Grimoire</color>... I can't diagnose him without it!");     // should be "New Patient Form" ,,,!
                    
        td_GrimoireAilmentSubmitted = new DialogueSnippet();
        //td_GrimoireAilmentSubmitted.SetSpeaker(theCat);
            td_GrimoireAilmentSubmitted._dialogue = new List<string>();
            td_GrimoireAilmentSubmitted._dialogue.Add("Now that's complete, I can create a treatment plan for him!");
        
        td_DiagnosisSheetIntroduction = new DialogueSnippet();
        //td_DiagnosisSheetIntroduction.SetSpeaker(theCat);
            td_DiagnosisSheetIntroduction._dialogue = new List<string>();
            /*td_DiagnosisSheetIntroduction._dialogue.Add("Let's see... Each recipe has an <#8A1E1E>effect</color>. I can choose to <b>heal</b>, <b>ease</b>, or <b>fortify</b>.");
            td_DiagnosisSheetIntroduction._dialogue.Add("<b>Heal</b> restores damage, <b>ease</b> provides a soothing effect, and <b>fortify</b> builds resistance and resilience.");
            td_DiagnosisSheetIntroduction._dialogue.Add("Then, I just need to choose a <#8A1E1E>target</color> area for that effect. I can treat the client's <b>mind</b> or <b>body</b>.");
            */
            td_DiagnosisSheetIntroduction._dialogue.Add("Hmm... okay, when treating my clients, I can choose what <#8A1E1E>effect</color> the treatment can have.");
            td_DiagnosisSheetIntroduction._dialogue.Add("These seem to be <b>heal</b>, <b>ease</b>, or <b>fortify</b>.");
            td_DiagnosisSheetIntroduction._dialogue.Add("... heal restores damage,");
            td_DiagnosisSheetIntroduction._dialogue.Add("... ease provides a soothing result,");
            td_DiagnosisSheetIntroduction._dialogue.Add("... and fortify builds resistance and strength.");
            td_DiagnosisSheetIntroduction._dialogue.Add("Then, I need to choose the <#8A1E1E>target</color> area to treat - either the mind or body.");
            
        td_DiagnosisSheetSubmitted = new DialogueSnippet();
        //td_DiagnosisSheetSubmitted.SetSpeaker(theCat);
            td_DiagnosisSheetSubmitted._dialogue = new List<string>();
            td_DiagnosisSheetSubmitted._dialogue.Add("Huzzah! Now that I have all the paperwork complete, I can collect the necessary herbs for treatment!");

        td_HerbWall = new DialogueSnippet();
        //td_HerbWall.SetSpeaker(theCat);
            td_HerbWall._dialogue = new List<string>();
            /*td_HerbWall._dialogue.Add("Hello, my precious herb collection! Let's see... I can click each drawer to see what's inside... And if I hover over the contents, it'll give me a description.");
            td_HerbWall._dialogue.Add("The <#8A1E1E>herb guide</color> on the left of the screen will tell me the characteristics of the herbs I need to treat the client. I need to find one herb for each effect and each target I have chosen.");
            td_HerbWall._dialogue.Add("Once I think I found the right herb, I can click and drag them across into my inventory.");
            td_HerbWall._dialogue.Add("And if I think I've made the wrong choice, I can click the <#8A1E1E>remove herb</color> button to get rid of it!");
            */
            td_HerbWall._dialogue.Add("Let's see... I haven't visited the herb wall in a while, I wonder what's in here.");
            td_HerbWall._dialogue.Add("Ah, this herb is [insert chosen herb]! Each herb has a corresponding page in my botanical codex, which I can look at by hovering over the plant.");
            td_HerbWall._dialogue.Add("Hmm. I can't remember how this herb may affect my patient though. Let me just drag this down and check the herb compendium to be sure.");
            td_HerbWall._dialogue.Add("The compendium gives me clues to figure out if a herb can heal, ease, or fortify something, as well as if it targets the body or mind.");
            td_HerbWall._dialogue.Add("But some clients have specific needs of their own which may conflict with some herbs, such as their <b>size</b> or <b>diet</b>.");
            td_HerbWall._dialogue.Add("I can cross-reference information on the treatment plan, botanical codex, and herb compendium to ensure I provide my client the correct herbs!");
            
        /*td_HerbWall02 = new DialogueSnippet();
            td_HerbWall02._dialogue = new List<string>();
            td_HerbWall02._dialogue.Add("")*/
            
            
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
            cd_ArabellaEnter._dialogue.Add("Hello.");
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
            cd_JimothyEnter._dialogue.Add("I'm so w-worried... that I'll lose my job as the king's j-jester… please, dear cat doctor...");
            cd_JimothyEnter._dialogue.Add("Could you help me? I would be ever so g-greatful…!");
            // cd_JimothyEnter._dialogue.Add("AH! Oh, my sincerest a-apologies cat d-doctor… I'm quite jumpy...");
        // UPDATED
        cd_JimothyFinish = new DialogueSnippet();
            cd_JimothyFinish._dialogue = new List<string>();
            cd_JimothyFinish._dialogue.Add("And you’re s-sure this will help me p-perform better...?");
            cd_JimothyFinish._dialogue.Add("Well, alright... you are the p-professional. Thank you for t-treating me, cat doctor.");
            
            
            
        /*cd_Jimothy = new CharacterDialogue();
            cd_Jimothy._shopEntry = new List<string>();
            cd_Jimothy._shopEntry.Add("AAAAAAAAAAAAAA E-Excuse me… could I take a moment of your time?");
            cd_Jimothy._shopEntry.Add("I'm so w- so worried... that I'll lose my job as the king's j-jester. Please help me, dear cat doctor...");
            
            cd_Jimothy._hasLinkedDialogue = true;
            // cd_Jimothy._catResponse = new List<string>();
            // cd_Jimothy._catResponse.Add("")
            
            cd_Jimothy._patientResponse = new List<string>();
            cd_Jimothy._patientResponse.Add("Y-You can help me!? Oh, I would be ever so g-greatful!");
            // cd_Jimothy._patientResponse.Add("AH! Oh, my sincerest a-apologies cat d-doctor... I'm quite jumpy...");
            
            cd_Jimothy._catSubmit = new List<string>();
            cd_Jimothy._catSubmit.Add("Here's your treatment Jimothy, I'm sure you'll be amazing! The king would want no one else but you to perform for him.");
            
            cd_Jimothy._shopExit = new List<string>();
            cd_Jimothy._shopExit.Add("Y-you're sure this will help me perform better?");
            cd_Jimothy._shopExit.Add("Well, alright... you are the p-professional. Thank you for t-treating me, cat doctor.");*/
            
            

        // UPDATED
        cd_FergusonEnter = new DialogueSnippet();
            cd_FergusonEnter._dialogue = new List<string>();
            cd_FergusonEnter._dialogue.Add("Hey there! Heard from yonder that this was the place for questionable ailments?");
            cd_FergusonEnter._dialogue.Add("Actually, I ain't sure if there's anythin' wrong with me, but my ma and pa are really upset, think I'm a failure and advised I get checked up...");
            cd_FergusonEnter._dialogue.Add("I'll do anythin' to help reassure 'em and make 'em proud of me!");
        // UPDATED
        cd_FergusonFinish = new DialogueSnippet();
            cd_FergusonFinish._dialogue = new List<string>();
            cd_FergusonFinish._dialogue.Add("'Preciate ya. I'm more confident approachin' ma and pa now.");
            cd_FergusonFinish._dialogue.Add("I really wanna make 'em proud, ya know?");
            
            
        cd_PeridotEnter = new DialogueSnippet();
            cd_PeridotEnter._dialogue = new List<string>();
            cd_PeridotEnter._dialogue.Add("Well hello there, love! Nice to see your face in the shop once again.");
            cd_PeridotEnter._dialogue.Add(/*"You see... i*/ "It's a bit embarrassing, but I've been struggling to attend my shifts at the bakery lately.");
            cd_PeridotEnter._dialogue.Add("Thinking about all the fat and sugar in every brioche bun and muffin makes my stomach go all topsy-turvy.");
            cd_PeridotEnter._dialogue.Add("Oh, please help me. I can't even dare to touch anything the slightest bit unhealthy for me.");

        cd_PeridotFinish = new DialogueSnippet();
            cd_PeridotFinish._dialogue = new List<string>();
            cd_PeridotFinish._dialogue.Add("Thank you, love! You're an absolute angel.");
            
            
            
            
            
        pd_TheCatResponse01 = new DialogueSnippet();
            pd_TheCatResponse01._dialogue = new List<string>();
            pd_TheCatResponse01._dialogue.Add("You've come to the right place! Just pass me your patient form and take a seat. I'll take care of everything.");
            
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
            pd_TheCatSubmitHerbs._dialogue.Add("Thank you kindly for waiting, here's your treatment!");
            //
    }

    /*public void UpdatePatientDialogue(string nameOfNPC)
    {
        switch (nameOfNPC)
        {
            case "Barry Buff":
                patientEntryDialogue = cd_BarryEnter._dialogue;
                patientExitDialogue = cd_BarryFinish._dialogue;
                break;
            case "Arabella Bunny":
                patientEntryDialogue = cd_ArabellaEnter._dialogue;
                patientExitDialogue = cd_ArabellaFinish._dialogue;
                break;
            case "Lawrence Lark":
                patientEntryDialogue = cd_LawrenceEnter._dialogue;
                patientExitDialogue = cd_LawrenceFinish._dialogue;
                break;
            case "Jimothy":
                // patientEntryDialogue = cd_Jimothy._shopEntry;// cd_JimothyEnter._dialogue;
                // patientExitDialogue = cd_JimothyFinish._dialogue;
                activeCharacterDialogue = cd_Jimothy;
                break;
            case "Ferguson":
                patientEntryDialogue = cd_FergusonEnter._dialogue;
                patientExitDialogue = cd_FergusonFinish._dialogue;
                break;
            case "Peridot":
                patientEntryDialogue = cd_PeridotEnter._dialogue;
                patientExitDialogue = cd_PeridotFinish._dialogue;
                break;
                
            default:
                patientEntryDialogue = cd_FergusonEnter._dialogue;
                patientExitDialogue = cd_FergusonFinish._dialogue;
                break;
        }
    }*/

}