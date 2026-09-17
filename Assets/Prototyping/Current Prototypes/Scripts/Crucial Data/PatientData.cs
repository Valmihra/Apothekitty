using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PatientData : MonoBehaviour
{
    public class SinglePatientData
    {
        public string _patientName;
        public string _patientSpecies;
        public string _patientExtras;
        public string _patientLetterText;

        public int _patientDayNumber;

        public Image _patientIcon;
                // private Image _patientIconStandard
                // private Image _patientIconHappy / sad / cured ??

            //public AilmentData patientAilment_;

        public void SetPatientInformation(string newName, string newSpecies, string newExtras)
        {
            _patientName = newName;
            _patientSpecies = newSpecies;
            _patientExtras = newExtras;
        }

        public void SetPatientLetterText(string letterText)
        {
            _patientLetterText = letterText;
        }
        
        public void SetPatientIcon()//(Image icon)
        {
            // _patientIcon = icon;
            string nameToSearch = (_patientName + " Icon").ToString();
            GameObject iconObject = GameObject.Find(nameToSearch);
            _patientIcon = iconObject.GetComponent<Image>();
        }
        
        public void AttachPatientToDay(int dayNum)
        {
            _patientDayNumber = dayNum;
        }
    }

    [HideInInspector] public List<SinglePatientData> allPatientsList {get; private set;}
    [HideInInspector] public SinglePatientData activePatientData {get; private set;}
    
    

    // all patient images here
		// *TAG* - Might be able to instead work out another nameToSearch function and update it like that? not a priority rn,,
    /*public Image patientIcon01;
    public Image patientIcon02;
    public Image patientIcon03;

    public Image patientIcon04;
    public Image patientIcon05;
    public Image patientIcon06;*/
    public Image displayedPatientIcon;
    
    public List<SinglePatientData> day00PatientsList;
    public List<SinglePatientData> day01PatientsList;
    public List<SinglePatientData> day02PatientsList;

	public List<SinglePatientData> currentDayPatientsList;
    private Dictionary<int, List<SinglePatientData>> dailyPatientsDictionary;

    // private Vector2 patientLetterStartingPosition;
    private ResultsCalculator resultsCalculatorReference;
    private PatientLetter patientLetter;

    private static PatientData _instance;
    public static PatientData Instance
    {
        get
        {
            return _instance;
        }
    }

    

    void Awake()
    {
        // not full singleton because i'm still scared from last semester's Horrors lmao
        _instance = this;
        resultsCalculatorReference = FindObjectOfType<ResultsCalculator>();
        patientLetter = FindObjectOfType<PatientLetter>(); //GetComponent<PatientLetter>();
    }

    public void InitialisePatientLetter()
    {
        CreatePatientInformation();
        patientLetter.GetPatientLetterPosition();
        return;
    }

    void SetupPatientDictionary()
    {
        //
        day00PatientsList = new List<SinglePatientData>();
        day01PatientsList = new List<SinglePatientData>();
        day02PatientsList = new List<SinglePatientData>();
        
        dailyPatientsDictionary = new Dictionary<int, List<SinglePatientData>>();

        dailyPatientsDictionary[0] = day00PatientsList;
        dailyPatientsDictionary[1] = day01PatientsList;
        dailyPatientsDictionary[2] = day02PatientsList;

        return;
        //if (!dailyPatientsDictionary.TryGetValue(key, out))
    }

    public void SetCurrentDayPatientsList()
    {
        currentDayPatientsList = dailyPatientsDictionary[DayManager.Instance.currentDayNumber];
        // Debug.Log("Current day is " + DayManager.Instance.currentDayNumber + " and the current day patients are...");
    }

    public void AssignPatientsToGameDays()
    {
        SetupPatientDictionary();
        
        for (int i = 0; i < DayManager.Instance.allGameDays.Count; i++)
        {
            foreach (SinglePatientData c in allPatientsList)
            {
                if (c._patientDayNumber == i)
                {
                    dailyPatientsDictionary[i].Add(c);
                }
            }
        }
        // Debug.Log("Number of patients present for day 1 is: " + dailyPatientsDictionary[1].Count);
    }

    
    void CreatePatientInformation()              // *TAG* - Would be good to reorder, but then I'll have to fix a bunch,,, HHHHHHHHbruhhhhhh
    {
        // Creates the individual SinglePatientData objects and fills them out with information related to each specific patient.
        allPatientsList = new List<SinglePatientData>();

        SinglePatientData barry = new SinglePatientData();
        barry.SetPatientInformation("Barry Buff", "Bear", "Large, Omnivore");
        barry.SetPatientLetterText("These crystals formed after eating some homemade hot honey for dinner last night. My eyes are constantly pulsating, and I'm starting to lose my vision.\n\nPlease help me! I'm not sure what will happen if I leave it alone.");
        barry.AttachPatientToDay(2);
        barry.SetPatientIcon();//(patientIcon01);
            allPatientsList.Add(barry);

        SinglePatientData arabella = new SinglePatientData();
        arabella.SetPatientInformation("Arabella Bunny", "Rabbit", "Small, Herbivore");
        arabella.SetPatientLetterText("My family have been starving recently... One of my sons passed from this mysterious illness... I had no choice but to cook him up for supper as we had nothing to eat... I'm starting to have an urge for flesh, and I'm afraid of what I might do to my other children. Please help me, Apothekitty!");
        arabella.AttachPatientToDay(2); 
        arabella.SetPatientIcon();//(patientIcon02);
            allPatientsList.Add(arabella);

        SinglePatientData lawrence = new SinglePatientData();
        lawrence.SetPatientInformation("Lawrence Lark", "Bird", "Small, Herbivore");
        lawrence.SetPatientLetterText("I love going for nightly glides amongst the treetops! However, a week ago, I noticed I developed this weird bite after one of my adventures... And now I've started growing teeth and bat wings! I don't know what's going on, but I don't like it! Please fix me, Apothekitty!");
        lawrence.AttachPatientToDay(2);
        lawrence.SetPatientIcon();//(patientIcon03);
            allPatientsList.Add(lawrence);




        SinglePatientData jimothy = new SinglePatientData();
        jimothy.SetPatientInformation("Jimothy", "Jerboa", "Small, Omnivore");
        jimothy.SetPatientLetterText("PLACEHOLDER LETTER TEXT - JIMOTHY");
        jimothy.AttachPatientToDay(0);
        jimothy.SetPatientIcon();//(patientIcon04);
            allPatientsList.Add(jimothy);

        SinglePatientData TEMP_PATIENT01 = new SinglePatientData();
        TEMP_PATIENT01.SetPatientInformation("TEMP_PATIENT01", "PLACEHOLDER", "PLACEHOLDER, PLACEHOLDER");
        TEMP_PATIENT01.SetPatientLetterText("PLACEHOLDER LETTER TEXT - TEMP_PATIENT01");
        TEMP_PATIENT01.AttachPatientToDay(1);
        TEMP_PATIENT01.SetPatientIcon();//(patientIcon05);
            allPatientsList.Add(TEMP_PATIENT01);

        SinglePatientData TEMP_PATIENT02 = new SinglePatientData();
        TEMP_PATIENT02.SetPatientInformation("TEMP_PATIENT02", "PLACEHOLDER", "PLACEHOLDER, PLACEHOLDER");
        TEMP_PATIENT02.SetPatientLetterText("PLACEHOLDER LETTER TEXT - TEMP_PATIENT02");
        TEMP_PATIENT02.AttachPatientToDay(1);
        TEMP_PATIENT02.SetPatientIcon();//(patientIcon06);
            allPatientsList.Add(TEMP_PATIENT02);
    }
    
    public void RandomiseIncomingPatientData()
    {
        // Randomises the patient that visits the player
        if (GameManager.Instance.runningTutorial)
        {
            // Sets to Jimothy specifically. If changing his position in list, update from 3!!
			activePatientData = allPatientsList[3];
        }
        else
        {
			int randomisedNumber = Random.Range(0, currentDayPatientsList.Count);
            activePatientData = currentDayPatientsList[randomisedNumber];
        }
        
        SendPatientData(activePatientData);
    }

    void SendPatientData(SinglePatientData data)
    {
        SetPatientAilment(data);
        patientLetter.UpdateLetterDisplay(data);
        displayedPatientIcon = data._patientIcon;
        
        Invoke(nameof(SpawnPatient), 1.0f);
    }
    
    void SpawnPatient()
    {
        // "spawns" the randomised patient, and updates the relevant scripts with their information.
        SceneManager.Instance.ShowPatient(displayedPatientIcon);
        DialogueRunner.Instance.GetDialogue("patientArrive");
    }
    
    void SetPatientAilment(SinglePatientData patient)
    {
        // Uses patient name to find and set their ailment in AilmentData
        string currentData = patient._patientName;
        AilmentData.Instance.SetCurrentAilmentByPatient(currentData);
        currentData = AilmentData.Instance.ConvertPatientNameToAilmentName(currentData);
        resultsCalculatorReference.SetPatientData(currentData);
        // Debug.Log("Linking ailment with randomised patient.");
    }


    public void UpdateCurrentDayPatientsList()
    {
        int index = allPatientsList.Count + 10;     // failsafe to ensure the index is correct

        foreach (SinglePatientData c in currentDayPatientsList)
        {
            if (c == activePatientData)
            {
                index = currentDayPatientsList.IndexOf(c);
            }
            else
            {
                continue;
            }
        }
        
        // if (index < allPatientsList.Count + 10)
        currentDayPatientsList.RemoveAt(index);
        Debug.Log("Patients remaining today: " + currentDayPatientsList.Count);
    }

    public void ResetPatientLetter()
    {
        patientLetter.ResetPatientLetterPosition();
    }
}
