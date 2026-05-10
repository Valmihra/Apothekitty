using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilmentData : MonoBehaviour
{
    public class Ailment
    {
        public string affectedClientName;

        public int primaryEffectDropdownNumber;
        public int primaryTargetDropdownNumber;
        public int secondaryEffectDropdownNumber;
        public int secondaryTargetDropdownNumber;

            //public string modifierType;
        public int modifierType;
        public List<string> acceptableHerbs;


                // MIGHT LATER ADD SPECIFIC HERB NAMES TO REFINE RESULTS PROCESS?
                    // eg.
                // public string primaryEffectHerb;
                // public string primaryTargetHerb;
                // public string secondaryEffectHerb;
                // public string secondaryTargetHerb;
                

        // sets the name of the client with the associated ailment for the results screen to check
        public void AttachToClient(string targetClient)
        {
            affectedClientName = targetClient;
        }

        // the ailment's dropdown numbers are set according to the arguments (see GenerateAilments for rules)
        // the results screen can then check the dropdown numbers on the diagnosis sheet for matches 
        public void SetAilmentInformation(string primEffect, string primTarget, string secEffect, string secTarget, int mod)//(int primEffect, int primTarget, int secEffect, int secTarget, int mod)
        {
            primaryEffectDropdownNumber = primEffect == "fortify" ? 1 : primEffect == "heal" ? 2 : primEffect == "ease" ? 3 : 0;
            primaryTargetDropdownNumber = primTarget == "mind" ? 1 : primTarget == "body" ? 2 : primTarget == "spirit" ? 3 : 0;
            secondaryEffectDropdownNumber = secEffect == "fortify" ? 1 : secEffect == "heal" ? 2 : secEffect == "ease" ? 3 : 0;
            secondaryTargetDropdownNumber = secTarget == "mind" ? 1 : secTarget == "body" ? 2 : secTarget == "spirit" ? 3 : 0;
            
                //SetModifier(mod);
            modifierType = mod;
        }

        public void SpecifyAcceptableHerbs(List<string> herbs/*string*/ )
        {
            acceptableHerbs = new List<string>();
            foreach (string s in herbs)
            {
                acceptableHerbs.Add(s);
            }
        }
        // 
        /*private void SetModifier(int number)
        {
            int modifier = number == 0 ? 0 : number == 1 ? 1 : number == 2 ? 2 : 3;

            if (modifier != 3)
            {
                UpdateModifier(modifier);
            }
            else
            {
                Debug.Log("Issue when trying to assign the correct modifier number.");
            }
            
        }

        private void UpdateModifier(int number)
        {
            string modName = number == 0 ? "none" : number == 1 ? "enhanced" : "inverted";
        }*/
    }

    public List<Ailment> allAilments;
    public Ailment currentAilment;

    private static AilmentData _instance;
    public static AilmentData Instance
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

    void Start()
    {
        GenerateAilments();
    }

    
        /* Follow these conventions to generate the ailment correctly:
                SetAilmentInformation:
            accepted strings for the effects are:           accepted strings for the targets are:
            - "fortify"                                     - "mind"
            - "heal"                                        - "body"
            - "ease"                                        - "spirit"
            anything else will not be registered correctly.

            The modifier numbers are:
            0   No modifier required
            1   Enhancer required
            2   Invertor required
            any other number will not be registered correctly.

                AttachToClient:
            To attach the ailment to the correct client, refer to the client's place in ClientLetter.Instance.clientsList
            If the ailment has no set client, write:
            - "none"
            This is easier to read when checking results later.
            */
    void GenerateAilments()
    {
        allAilments = new List<Ailment>();

        Ailment chronicInsomnia = new Ailment();
            chronicInsomnia.SetAilmentInformation("ease", "mind", "heal", "body", 0);
            chronicInsomnia.AttachToClient("none");
            allAilments.Add(chronicInsomnia);
            List<string> acceptableHerbs = new List<string>{"spiceLeaf", "bumbleBlooms", "warmWhisper", "meltingRoot"};
            chronicInsomnia.SpecifyAcceptableHerbs(acceptableHerbs);

        Ailment contaminationOCD = new Ailment();
            contaminationOCD.SetAilmentInformation("ease", "mind", "x", "x", 1);
            contaminationOCD.AttachToClient("none");
            allAilments.Add(contaminationOCD);
            acceptableHerbs = new List<string>{"pupilPetal", "spiceLeaf", "watchersWeed"};
            contaminationOCD.SpecifyAcceptableHerbs(acceptableHerbs);

        Ailment dietDrift = new Ailment();
            dietDrift.SetAilmentInformation("ease", "mind", "heal", "body", 1);
            dietDrift.AttachToClient(ClientLetter.Instance.clientsList[1]._clientName);
            allAilments.Add(dietDrift);
            acceptableHerbs = new List<string>{"pupilPetal", "spiceLeaf", "warmWhisper", "meltingRoot", "crystalMoss"};
            dietDrift.SpecifyAcceptableHerbs(acceptableHerbs);

        Ailment honEye = new Ailment();
            honEye.SetAilmentInformation("heal", "body", "x", "x", 1);
            honEye.AttachToClient(ClientLetter.Instance.clientsList[0]._clientName);
            allAilments.Add(honEye);
            acceptableHerbs = new List<string>{"sweetRotCap", "crystalVine", "queensReed"};
            honEye.SpecifyAcceptableHerbs(acceptableHerbs);

        Ailment illnessAnxiety = new Ailment();
            illnessAnxiety.SetAilmentInformation("ease", "mind", "x", "x", 0);
            illnessAnxiety.AttachToClient("none");
            allAilments.Add(illnessAnxiety);
            acceptableHerbs = new List<string>{"crystalVine", "heavensHollyhock"};
            illnessAnxiety.SpecifyAcceptableHerbs(acceptableHerbs);

        Ailment orthorexia = new Ailment();
            orthorexia.SetAilmentInformation("ease", "mind", "heal", "body", 0);
            orthorexia.AttachToClient("none");
            allAilments.Add(orthorexia);
            acceptableHerbs = new List<string>{"spiceLeaf", "bumbleBlooms", "warmWhisper", "meltingRoot"};
            orthorexia.SpecifyAcceptableHerbs(acceptableHerbs);

        Ailment sapEye = new Ailment();
            sapEye.SetAilmentInformation("heal", "body", "x", "x", 0);
            sapEye.AttachToClient("none");
            allAilments.Add(sapEye);
            acceptableHerbs = new List<string>{"sweetRotCap", "crystalVine"};
            sapEye.SpecifyAcceptableHerbs(acceptableHerbs);

        Ailment theBlues = new Ailment();
            theBlues.SetAilmentInformation("ease", "mind", "heal", "body", 0);
            theBlues.AttachToClient("none");
            allAilments.Add(theBlues);
            acceptableHerbs = new List<string>{"spiceLeaf", "bumbleBlooms", "warmWhisper", "meltingRoot"};
            theBlues.SpecifyAcceptableHerbs(acceptableHerbs);

        Ailment theFanging = new Ailment();
            theFanging.SetAilmentInformation("heal", "body", "fortify", "mind", 0);
            theFanging.AttachToClient(ClientLetter.Instance.clientsList[2]._clientName);
            allAilments.Add(theFanging);
            acceptableHerbs = new List<string>{"warmWhisper", "meltingRoot", "hexacore", "spiceLeaf"};
            theFanging.SpecifyAcceptableHerbs(acceptableHerbs);

        Ailment theFawning = new Ailment();
            theFawning.SetAilmentInformation("heal", "body", "ease", "mind", 0);
            theFawning.AttachToClient("none");
            allAilments.Add(theFawning);
            acceptableHerbs = new List<string>{"warmWhisper", "meltingRoot", "crystalMoss", "bumbleBlooms"};
            theFawning.SpecifyAcceptableHerbs(acceptableHerbs);
    }

    // Called from ClientLetter once the current client has been set. 
    // This searches through the current ailments in the list for a name that matches the
    // current client. If found, it links the client and ailment for the results screen.
    public void LinkAilmentInformation(string name)      //(Ailment ailment)
    {
        Debug.Log("String sent is: " + name);
        foreach (Ailment a in allAilments)
        {
            //Debug.Log(a.affectedClientName);
            if (name == a.affectedClientName)
            {
                currentAilment = a;
                //Debug.Log("The client's ailment is " + currentAilment.);
            }
            else
            {
                continue;
                //Debug.Log("Error when attempting to link the client and the ailment. Check both scripts for naming inconsistencies.");
            }
        }
    }

    public string ConvertClientAilment(string name)
    {
        string ailmentName;
        ailmentName = name;

        for (int i = 0; i < allAilments.Count; i++)
        {
            if (allAilments[i].affectedClientName == name)
            {
                ailmentName = GrimoirePagesData.Instance.pagesArray[i]._ailmentName;
                //return ailmentName;
            }
            else
            {
                continue;
            }
        }

        return ailmentName;
    }
}
