using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public class DialogueSnippet
    {
        //public string speakerName_;
        //public string currentLine_;

        public List<string> _dialogue;


        //public void 
    }

    // Separate Dialogue Snippets
    public DialogueSnippet introduction;
    public DialogueSnippet deskIntroduction;
    public DialogueSnippet ailmentSubmittedIntroduction;
    public DialogueSnippet diagnosisSheetIntroduction;
    public DialogueSnippet treatmentPlanSubmittedIntroduction;
    public DialogueSnippet herbWallIntroduction;
    
    public DialogueSnippet barry;
    public DialogueSnippet barryFinish;
    //public DialogueSnippet 
    public DialogueSnippet arabella;
    public DialogueSnippet arabellaFinish;

    public DialogueSnippet lawrence;
    public DialogueSnippet lawrenceFinish;

    public DialogueSnippet jimothy;
    public DialogueSnippet jimothyFinish;

    public DialogueSnippet TEMP_NPC01;
    public DialogueSnippet TEMP_NPC01Finish;

    public DialogueSnippet TEMP_NPC02;
    public DialogueSnippet TEMP_NPC02Finish;

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
        introduction = new DialogueSnippet();
            introduction._dialogue = new List<string>();
            introduction._dialogue.Add("Back to work...");
            introduction._dialogue.Add("Sounds busy out there. I'd better open up the shop!");
            //introduction._dialogue.Add("DEBUGGING TEST");

        deskIntroduction = new DialogueSnippet();
            deskIntroduction._dialogue = new List<string>();
            deskIntroduction._dialogue.Add("I haven't done this in a while... I'd better refamiliarise myself with the process.");
            deskIntroduction._dialogue.Add("Okay, all the information I'll need is on the New Patient Form. I just have to match it up to the <#8A1E1E>Grimoire</color>!");

        ailmentSubmittedIntroduction = new DialogueSnippet();
            ailmentSubmittedIntroduction._dialogue = new List<string>();
            ailmentSubmittedIntroduction._dialogue.Add("Yay! I've got a good feeling about this... Now I need to come up with the treatment plan!");
            //IF ISSUE WITH JUST ONE ITEM IN LIST: ailmentSubmittedIntroduction._dialogue.Add("Let's see...");
        
        diagnosisSheetIntroduction = new DialogueSnippet();
            diagnosisSheetIntroduction._dialogue = new List<string>();
            //diagnosisSheetIntroduction._dialogue.Add("Let's see... Each recipe has an <b>effect</b>. I can choose to <#8A1E1E>heal</color>, <#8A1E1E>ease</color>, or <#8A1E1E>fortify</color>.");
            diagnosisSheetIntroduction._dialogue.Add("Let's see... Each recipe has an <#8A1E1E>effect</color>. I can choose to <b>heal</b>, <b>ease</b>, or <b>fortify</b>.");
            /*NEEDS ATTENTION!*/    //diagnosisSheetIntroduction._dialogue.Add("<#8A1E1E>Heal</color> restores damage, <#8A1E1E>ease</color> provides a soothing effect, and <#8A1E1E>fortify</color> builds resistance and resilience.");
                                    diagnosisSheetIntroduction._dialogue.Add("<b>Heal</b> restores damage, <b>ease</b> provides a soothing effect, and <b>fortify</b> builds resistance and resilience.");
            //diagnosisSheetIntroduction._dialogue.Add("Then, I just need to choose a target area for that effect. I can treat the client's <#8A1E1E>mind</color>, <#8A1E1E>body</color>, or <#8A1E1E>spirit</color>.");
            diagnosisSheetIntroduction._dialogue.Add("Then, I just need to choose a <#8A1E1E>target</color> area for that effect. I can treat the client's <b>mind</b>, <b>body</b>, or <b>spirit</b>.");

        treatmentPlanSubmittedIntroduction = new DialogueSnippet();
            treatmentPlanSubmittedIntroduction._dialogue = new List<string>();
            treatmentPlanSubmittedIntroduction._dialogue.Add("Wahoo! Now it's time for the fun part... Creating the recipe!");

        herbWallIntroduction = new DialogueSnippet();
            herbWallIntroduction._dialogue = new List<string>();
            herbWallIntroduction._dialogue.Add("Hello, my precious herb collection! Let's see... I can click each drawer to see what's inside... And if I hover over the contents, it'll give me a description.");
            herbWallIntroduction._dialogue.Add("The <#8A1E1E>herb guide</color> on the left of the screen will tell me the characteristics of the herbs I need to treat the client. I need to find one herb for each effect, target, and enhancer or inverter I have chosen.");
            herbWallIntroduction._dialogue.Add("Once I think I found the right herb, I can click and drag them across into my inventory.");
            herbWallIntroduction._dialogue.Add("And if I think I've made the wrong choice, I can click the <#8A1E1E>remove herb</color> button to get rid of it!");

            //
        








        // Character dialogue snippets
        barry = new DialogueSnippet();
            barry._dialogue = new List<string>();
            barry._dialogue.Add("Good day, little kitten healer. I'm in need of some help.");
            barry._dialogue.Add("Well, my wife thinks I need help. I'm just here to humour her.");
            barry._dialogue.Add("No, really! Takes a lot to get me down.");
            barry._dialogue.Add("Hahaha...");

        barryFinish = new DialogueSnippet();
            barryFinish._dialogue = new List<string>();
            barryFinish._dialogue.Add("Thank you little kitten - I- I mean Apothekitty. Keep up the good work.");

        arabella = new DialogueSnippet();
            arabella._dialogue = new List<string>();
            arabella._dialogue.Add("Hello...");
            arabella._dialogue.Add("Um... I heard you were the one to go to for... discrete treatments?");
            arabella._dialogue.Add("O-Oh! My name is Arabella.");
        
        arabellaFinish = new DialogueSnippet();
            arabellaFinish._dialogue = new List<string>();
            arabellaFinish._dialogue.Add("Helloooo. I-is my order ready, Apothekitty?");
            arabellaFinish._dialogue.Add("Oh! Thank you.");
            arabellaFinish._dialogue.Add("I appreciate the help, and your non-judgemental nature. I will write back soon.");
            
        lawrence = new DialogueSnippet();
            lawrence._dialogue = new List<string>();
            lawrence._dialogue.Add("Hi friend... I think I might need some help.");
            lawrence._dialogue.Add("I don't mean to alarm you, but I was bitten by something, and I don't know what to do about it!");

        lawrenceFinish = new DialogueSnippet();
            lawrenceFinish._dialogue = new List<string>();
            lawrenceFinish._dialogue.Add("Thank you, thank you, thank you Apothekitty!!");





        jimothy = new DialogueSnippet();
            jimothy._dialogue = new List<string>();
            jimothy._dialogue.Add("PLACEHOLDER TEXT: bruh i got stagefright and bells that jingle jangle jingle (jingle jangle) as i go riding merrily along");

        jimothyFinish = new DialogueSnippet();
            jimothyFinish._dialogue = new List<string>();
            jimothyFinish._dialogue.Add("PLACEHOLDER TEXT: sayounara you later");


        TEMP_NPC01 = new DialogueSnippet();
            TEMP_NPC01._dialogue = new List<string>();
            TEMP_NPC01._dialogue.Add("PLACEHOLDER TEXT: i don't exist yet!");

        TEMP_NPC01Finish = new DialogueSnippet();
            TEMP_NPC01Finish._dialogue = new List<string>();
            TEMP_NPC01Finish._dialogue.Add("PLACEHOLDER TEXT: ariga-thank you for your treatment");


        TEMP_NPC02 = new DialogueSnippet();
            TEMP_NPC02._dialogue = new List<string>();
            TEMP_NPC02._dialogue.Add("PLACEHOLDER TEXT: why do they call it oven when you of in the cold food of out hot eat the food?");

        TEMP_NPC02Finish = new DialogueSnippet();
            TEMP_NPC02Finish._dialogue = new List<string>();
            TEMP_NPC02Finish._dialogue.Add("PLACEHOLDER TEXT: nvm apothekitty, i think i had a stroke actually");
    }


    //
}


/*


Tutorial dialogue 
DONE    Back to work...
DONE    Sounds busy out there. I'd better open up the shop!

[popup]
Welcome to Apothekitty!
As the town healer, it's your job to carefully diagnose and treat your patients.
Click on the <#8A1E1E>curtain</color> to receive your first client!

[popup]
You'll find the patient form on your <#8A1E1E>desk</color>.
Click the arrows in the bottom right to navigate between screens.
If you feel lost, click the arrow by the quest log to see what you still need to do.

DONE    "I haven't done this in a while... I'd better familiarise myself with the process."
DONE    "Okay, all the information is on the form. I just have to match it up to the Grimoire!"

[popup]
Your Grimoire acts as your reference point for ailments.
Pay close attention to each ailment's description and compare it to your client's symptoms.
Once you think you've found the correct diagnosis, click on the <#8A1E1E>ailment's picture</color> to select it!

[once selected]
DONE    "Yay! I've got a good feeling about this... Now I need to come up with the treatment plan!"
DONE    "Let's see... Each recipe has an effect. I can choose to heal, ease, or fortify. Heal restores damage, ease provides a soothing, and fortify builds resistance and resilience."
DONE    "Then, I just need to choose a target area for that effect. I can treat the client's mind, body, or spirit."


[popup]
Scan the ailment's description for a clue to the suitable treatment plan.   // for clues to make a suitable treatment plan
Not all ailments will require two target areas.                             // Not all ailments require two targets, but some may require a modifier.
For later-stage ailments or large clients, you can strengthen the treatment with the <#8A1E1E>enhancer</color>.
You can also choose the <#8A1E1E>inverter</color> to achieve the opposite effect, if the description calls for it.
Click "submit treatment plan" when you're ready.

DONE    "Wahoo! Now it's time for the fun part... Creating the recipe!"

[popup]
Now that you've chosen a treatment plan, you can access your herb stores to create your recipe.
Click the new arrow in the bottom right to navigate to the <#8A1E1E>herb wall</color>.

DONE    "Hello, my precious herb collection! Let's see... I can click each drawer to see what's inside... And if I hover over the contents, it'll give me a description." 
DONE    "The herb guide on the left of the screen will tell me the characteristics of the herbs I need to treat the client. I need at least one herb for each effect and target I have chosen."
DONE    "Once I think I found the right herb, I can click and drag them across into my inventory."
DONE    "And if I think I've made the wrong choice, I can click the "remove" button to get rid of it!"

[popup]
If you forget your chosen treatment plan, you can click the arrow in the bottom right to navigate to the desk.  //return instead of navigate again? tiny edit,, idk if worth it,,
Once you're done, click "submit" (?) to hand your recipe to the client.
Make sure you're 100% certain before submitting, as there's no going back!
Note: I think text box and popups should say "click to continue..." in the bottom right corner to give players a sense of direction. 
*/