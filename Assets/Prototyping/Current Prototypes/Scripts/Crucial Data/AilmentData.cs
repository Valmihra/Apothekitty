using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilmentData : MonoBehaviour
{
    public class Ailment
    {
        public string _affectedClientName;

        public int _primaryEffectDropdownNumber;
        public int _primaryTargetDropdownNumber;
        public int _secondaryEffectDropdownNumber;
        public int _secondaryTargetDropdownNumber;

        public int _modifierTypeByNumber;
        public List<string> _acceptableHerbsForTreatment;

        // Sets the affected client name                for the results screen to check
        public void AttachAilmentToSpecificClient(string targetClient)
        {
            _affectedClientName = targetClient;
        }

        // the ailment's dropdown numbers are set according to the arguments (see InitialiseAilmentData for rules)
        // the results screen can then check the dropdown numbers on the diagnosis sheet for matches 
        public void SetAilmentInformation(string primEffect, string primTarget, string secEffect, string secTarget, int mod)//(int primEffect, int primTarget, int secEffect, int secTarget, int mod)
        {
            _primaryEffectDropdownNumber = primEffect == "fortify" ? 1 : primEffect == "heal" ? 2 : primEffect == "ease" ? 3 : 0;
            _primaryTargetDropdownNumber = primTarget == "mind" ? 1 : primTarget == "body" ? 2 : primTarget == "spirit" ? 3 : 0;
            _secondaryEffectDropdownNumber = secEffect == "fortify" ? 1 : secEffect == "heal" ? 2 : secEffect == "ease" ? 3 : 0;
            _secondaryTargetDropdownNumber = secTarget == "mind" ? 1 : secTarget == "body" ? 2 : secTarget == "spirit" ? 3 : 0;
            
            _modifierTypeByNumber = mod;
        }

        public void SpecifyAcceptableHerbsForTreatment(List<string> herbs)
        {
            _acceptableHerbsForTreatment = new List<string>();
            foreach (string s in herbs)
            {
                _acceptableHerbsForTreatment.Add(s);
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

    public List<Ailment> allAilmentsList;
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

                AttachAilmentToSpecificClient:
            To attach the ailment to the correct client, refer to the client's place in ClientLetter.Instance.allClientsList
            If the ailment has no set client, write:
            - "none"
            This is easier to read when checking results later.
            */
            
    public void InitialiseAilmentData()
    {
        allAilmentsList = new List<Ailment>();

        Ailment chronicInsomnia = new Ailment();
            chronicInsomnia.SetAilmentInformation("ease", "mind", "heal", "body", 0);
            //chronicInsomnia.AttachAilmentToSpecificClient("none");
                            chronicInsomnia.AttachAilmentToSpecificClient(ClientLetter.Instance.allClientsList[3]._clientName); // Jimothy
            allAilmentsList.Add(chronicInsomnia);
            List<string> _acceptableHerbsForTreatment = new List<string>{"spiceLeaf", "bumbleBlooms", "warmWhisper", "meltingRoot"};
            chronicInsomnia.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment contaminationOCD = new Ailment();
            contaminationOCD.SetAilmentInformation("ease", "mind", "x", "x", 1);
            //contaminationOCD.AttachAilmentToSpecificClient("none");
                            contaminationOCD.AttachAilmentToSpecificClient(ClientLetter.Instance.allClientsList[4]._clientName);   //temp1
            allAilmentsList.Add(contaminationOCD);
            _acceptableHerbsForTreatment = new List<string>{"pupilPetal", "spiceLeaf", "watchersWeed"};
            contaminationOCD.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment dietDrift = new Ailment();
            dietDrift.SetAilmentInformation("ease", "mind", "heal", "body", 1);
            dietDrift.AttachAilmentToSpecificClient(ClientLetter.Instance.allClientsList[1]._clientName);
            allAilmentsList.Add(dietDrift);
            _acceptableHerbsForTreatment = new List<string>{"pupilPetal", "spiceLeaf", "warmWhisper", "meltingRoot", "crystalMoss"};
            dietDrift.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment honEye = new Ailment();
            honEye.SetAilmentInformation("heal", "body", "x", "x", 1);
            honEye.AttachAilmentToSpecificClient(ClientLetter.Instance.allClientsList[0]._clientName);
            allAilmentsList.Add(honEye);
            _acceptableHerbsForTreatment = new List<string>{"sweetRotCap", "crystalVine", "queensReed"};
            honEye.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment illnessAnxiety = new Ailment();
            illnessAnxiety.SetAilmentInformation("ease", "mind", "x", "x", 0);
            //illnessAnxiety.AttachAilmentToSpecificClient("none");
                            illnessAnxiety.AttachAilmentToSpecificClient(ClientLetter.Instance.allClientsList[5]._clientName);
            allAilmentsList.Add(illnessAnxiety);
            _acceptableHerbsForTreatment = new List<string>{"crystalVine", "heavensHollyhock"};
            illnessAnxiety.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment orthorexia = new Ailment();
            orthorexia.SetAilmentInformation("ease", "mind", "heal", "body", 0);
            orthorexia.AttachAilmentToSpecificClient("none");
            allAilmentsList.Add(orthorexia);
            _acceptableHerbsForTreatment = new List<string>{"spiceLeaf", "bumbleBlooms", "warmWhisper", "meltingRoot"};
            orthorexia.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment sapEye = new Ailment();
            sapEye.SetAilmentInformation("heal", "body", "x", "x", 0);
            sapEye.AttachAilmentToSpecificClient("none");
            allAilmentsList.Add(sapEye);
            _acceptableHerbsForTreatment = new List<string>{"sweetRotCap", "crystalVine"};
            sapEye.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment theBlues = new Ailment();
            theBlues.SetAilmentInformation("ease", "mind", "heal", "body", 0);
            theBlues.AttachAilmentToSpecificClient("none");
            allAilmentsList.Add(theBlues);
            _acceptableHerbsForTreatment = new List<string>{"spiceLeaf", "bumbleBlooms", "warmWhisper", "meltingRoot"};
            theBlues.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment theFanging = new Ailment();
            theFanging.SetAilmentInformation("heal", "body", "fortify", "mind", 0);
            theFanging.AttachAilmentToSpecificClient(ClientLetter.Instance.allClientsList[2]._clientName);
            allAilmentsList.Add(theFanging);
            _acceptableHerbsForTreatment = new List<string>{"warmWhisper", "meltingRoot", "hexacore", "spiceLeaf"};
            theFanging.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment theFawning = new Ailment();
            theFawning.SetAilmentInformation("heal", "body", "ease", "mind", 0);
            theFawning.AttachAilmentToSpecificClient("none");
            allAilmentsList.Add(theFawning);
            _acceptableHerbsForTreatment = new List<string>{"warmWhisper", "meltingRoot", "crystalMoss", "bumbleBlooms"};
            theFawning.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);
    }

    // Called from ClientLetter once the current client has been set. 
    // This searches through the current ailments in the list for a name that matches the
    // current client. If found, it links the client and ailment for the results screen.
    public void SetCurrentAilmentByClient(string name)      //(Ailment ailment)
    {
        // Debug.Log("String sent is: " + name);
        foreach (Ailment a in allAilmentsList)
        {
            if (name == a._affectedClientName)
            {
                currentAilment = a;
            }
            else
            {
                continue;
            }
        }
    }

    // Takes a client name string and turns it into an ailment name string.
    public string ConvertClientNameToAilmentName(string name)
    {
        string ailmentName = name;
        // ailmentName = name;

        for (int i = 0; i < allAilmentsList.Count; i++)
        {
            if (allAilmentsList[i]._affectedClientName == name)
            {
				// +1 to account for the empty page at the beginning of the pages array
                ailmentName = GrimoirePagesData.Instance.grimoirePagesArray[i+1]._ailmentName;
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
