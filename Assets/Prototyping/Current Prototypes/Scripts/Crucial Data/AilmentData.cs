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

        // public int _modifierTypeByNumber;
        public List<string> _acceptableHerbsForTreatment;

        // Sets the affected client name                for the results screen to check
        public void AttachAilmentToSpecificClient(string targetClient)
        {
            _affectedClientName = targetClient;
        }

        // the ailment's dropdown numbers are set according to the arguments (see InitialiseAilmentData for rules)
        // the results screen can then check the dropdown numbers on the diagnosis sheet for matches 
        public void SetAilmentInformation(string primEffect, string primTarget, string secEffect, string secTarget)// , int mod)//(int primEffect, int primTarget, int secEffect, int secTarget, int mod)
        {
            _primaryEffectDropdownNumber = primEffect == "fortify" ? 1 : primEffect == "heal" ? 2 : primEffect == "ease" ? 3 : 0;
            _primaryTargetDropdownNumber = primTarget == "mind" ? 1 : primTarget == "body" ? 2 : primTarget == "spirit" ? 3 : 0;
            _secondaryEffectDropdownNumber = secEffect == "fortify" ? 1 : secEffect == "heal" ? 2 : secEffect == "ease" ? 3 : 0;
            _secondaryTargetDropdownNumber = secTarget == "mind" ? 1 : secTarget == "body" ? 2 : secTarget == "spirit" ? 3 : 0;
            
            // _modifierTypeByNumber = mod;
        }

        public void SpecifyAcceptableHerbsForTreatment(List<string> herbs)
        {
            _acceptableHerbsForTreatment = new List<string>();
            foreach (string s in herbs)
            {
                _acceptableHerbsForTreatment.Add(s);
            }
        }
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
            To attach the ailment to the correct client, refer to the client's place in PatientData.Instance.allPatientsList
            If the ailment has no set client, write:
            - "none"
            This is easier to read when checking results later.
            */
            
    public void InitialiseAilmentData()
    {
        allAilmentsList = new List<Ailment>();

        Ailment chronicInsomnia = new Ailment();
        chronicInsomnia.SetAilmentInformation("ease", "mind", "heal", "body");//, 0);
            chronicInsomnia.AttachAilmentToSpecificClient("none");
            allAilmentsList.Add(chronicInsomnia);
                // unedited
                List<string> _acceptableHerbsForTreatment = new List<string>{"spiceLeaf", "bumbleBlooms", "warmWhisper", "meltingRoot"};
                chronicInsomnia.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment contaminationOCD = new Ailment();
        contaminationOCD.SetAilmentInformation("ease", "mind", "x", "x");//, 1);
            contaminationOCD.AttachAilmentToSpecificClient("none");
            allAilmentsList.Add(contaminationOCD);
                // unedited
                _acceptableHerbsForTreatment = new List<string>{"pupilPetal", "spiceLeaf", "watchersWeed"};
                contaminationOCD.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        // ARABELLA'S 
        Ailment dietDrift = new Ailment();
        dietDrift.SetAilmentInformation("ease", "mind", "heal", "body");//, 0); //, 1);
            dietDrift.AttachAilmentToSpecificClient(PatientData.Instance.allPatientsList[1]._patientName);
            allAilmentsList.Add(dietDrift);
                _acceptableHerbsForTreatment = new List<string>{"featherFern", "heavensHollyhock", "bumbleBlooms", "bellBloom"}; //, "crystalMoss"};
                dietDrift.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        // BARRY'S
        Ailment honEye = new Ailment();
        honEye.SetAilmentInformation("heal", "body", "x", "x");//, 0); //1);
            honEye.AttachAilmentToSpecificClient(PatientData.Instance.allPatientsList[0]._patientName);
            allAilmentsList.Add(honEye);
                _acceptableHerbsForTreatment = new List<string>{"morningMint", "lavendear"};
                honEye.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment illnessAnxiety = new Ailment();
        illnessAnxiety.SetAilmentInformation("ease", "mind", "x", "x");//, 0);
            illnessAnxiety.AttachAilmentToSpecificClient("none");
            allAilmentsList.Add(illnessAnxiety);
                // unedited
                _acceptableHerbsForTreatment = new List<string>{"crystalVine", "heavensHollyhock"};
                illnessAnxiety.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        // PERIDOT'S
        Ailment orthorexia = new Ailment();
        orthorexia.SetAilmentInformation("ease", "mind", "heal", "body");//, 0);
            orthorexia.AttachAilmentToSpecificClient(PatientData.Instance.allPatientsList[5]._patientName);     // pangolin
            allAilmentsList.Add(orthorexia);
                _acceptableHerbsForTreatment = new List<string>{"pupilPetal", "wingroot", "crystalVine", "lavendear"};
                orthorexia.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment sapEye = new Ailment();
        sapEye.SetAilmentInformation("heal", "body", "x", "x");//, 0);
            sapEye.AttachAilmentToSpecificClient("none");
            allAilmentsList.Add(sapEye);
                // unedited
                _acceptableHerbsForTreatment = new List<string>{"sweetRotCap", "crystalVine"};
                sapEye.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        // JIMOTHY'S
        Ailment stageFright = new Ailment();
        stageFright.SetAilmentInformation("ease", "mind", "x", "x");//, 0);
            stageFright.AttachAilmentToSpecificClient(PatientData.Instance.allPatientsList[3]._patientName); // Jimothy
            allAilmentsList.Add(stageFright);
                _acceptableHerbsForTreatment = new List<string>{"lavendear", "heavensHollyhock"};
                stageFright.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        Ailment theBlues = new Ailment();
        theBlues.SetAilmentInformation("ease", "mind", "heal", "body");//, 0);
            theBlues.AttachAilmentToSpecificClient("none");
            allAilmentsList.Add(theBlues);
                // unedited
                _acceptableHerbsForTreatment = new List<string>{"spiceLeaf", "bumbleBlooms", "warmWhisper", "meltingRoot"};
                theBlues.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        // LAWRENCE'S
        Ailment theFanging = new Ailment();
        theFanging.SetAilmentInformation("heal", "body", "fortify", "mind");//, 0);
            theFanging.AttachAilmentToSpecificClient(PatientData.Instance.allPatientsList[2]._patientName);
            allAilmentsList.Add(theFanging);
                _acceptableHerbsForTreatment = new List<string>{"spiceLeaf", "heavensHollyhock", "bumbleBlooms", "lavendear"};
                theFanging.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);

        // FERGUSON'S
        Ailment theFawning = new Ailment();
        theFawning.SetAilmentInformation("heal", "body", "ease", "mind");//, 0);
            theFawning.AttachAilmentToSpecificClient(PatientData.Instance.allPatientsList[4]._patientName);   // Ferguson
            allAilmentsList.Add(theFawning);
                _acceptableHerbsForTreatment = new List<string>{"sweetRotCap", "wingroot", "crystalVine", "lavendear"};
                theFawning.SpecifyAcceptableHerbsForTreatment(_acceptableHerbsForTreatment);
    }

    // Called from PatientData once the current client has been set. 
    // This searches through the current ailments in the list for a name that matches the
    // current client. If found, it links the client and ailment for the results screen.
    public void SetCurrentAilmentByPatient(string name)      //(Ailment ailment)
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

    
    public string ConvertPatientNameToAilmentName(string name)
    {
        // Takes a client name string and turns it into an ailment name string.
        string ailmentName = name;

        for (int i = 0; i < allAilmentsList.Count; i++)
        {
            if (allAilmentsList[i]._affectedClientName == name)
            {
				// +1 to account for the empty page at the beginning of the pages array
                ailmentName = GrimoirePagesData.Instance.grimoirePagesArray[i+1]._ailmentName;
            }
            else
            {
                continue;
            }
        }

        return ailmentName;
    }
}
