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

        public string _patientDiet;// {get; private set;}
        public string _patientSize;// {get; private set;}
        
        public Image _patientIcon;
        public Image _patientAltIcon;
        
        public int _patientDayNumber;
        
        public string _patientLetterText;
        public string _patientCuredText;
        public string _patientFailedText;
        
        
        public string _patientExtras = "x"; // *TAG* - (FOR NOW)

        public void ReadAllPatientInformation()
        {
            Debug.Log("Reading all patient information." + _patientName + " is a " + _patientSize + " " + _patientSpecies + ". Their diet is: " + _patientDiet);
        }

        public void SetPatientInformation(string newName, string newSpecies)//, string newExtras)
        {
            _patientName = newName;
            _patientSpecies = newSpecies;
            // _patientExtras = newExtras;
        }

        public void SetPatientExtras(string extraToUpdate, string newExtras)
        {
            /*extraToUpdate = newExtras;

            string size = "Size";
            string diet = "Diet";
            string debugExtra = newExtras == PatientData.Instance.sizeS ? size : newExtras ==  PatientData.Instance.sizeM ? size : newExtras == PatientData.Instance.sizeL ? size : diet;
            
            Debug.Log("Updated " + _patientName + "'s " + debugExtra + " with: " + extraToUpdate);*/
            // Debug.Log(extraToUpdate);
            
            if (extraToUpdate == "size")
            {
                _patientSize = newExtras;
            }
            else
            {
                _patientDiet = newExtras;
            }
        }
        
        /*public void SetPatientDiet(string newExtras)
        {
            _patientDiet = newExtras;
            // Debug.Log(extraToUpdate);
        }*/

        public void SetPatientText(string textToUpdate, string newText)
        {
            if (textToUpdate == "letter")
            {
                _patientLetterText = newText;
            }

            if (textToUpdate == "cured")
            {
                _patientCuredText = newText;
            }

            if (textToUpdate == "failed")
            {
                _patientFailedText = newText;
            }
        }
        
        public void SetPatientIcon()
        {
            // Sets default patient icon
            string nameToSearch = (_patientName + " Icon").ToString();
            GameObject iconObject = GameObject.Find(nameToSearch);
            _patientIcon = iconObject.GetComponent<Image>();
            
            // Sets alt icon for when receiving treatment from the player
            Transform childTransform = iconObject.transform.Find(_patientName + " Alt");
            
            if (childTransform.TryGetComponent<Image>(out Image foundImage))
            {
                _patientAltIcon = foundImage;
            }
            else
            {
                Debug.Log("Could not find an alt image, does the patient name match the icon name?");
            }
        }
        
        public void AttachPatientToDay(int dayNum)
        {
            _patientDayNumber = dayNum;
        }
    }

    [HideInInspector] public List<SinglePatientData> allPatientsList {get; private set;}
    [HideInInspector] public SinglePatientData activePatientData {get; private set;}

    private List<SinglePatientData> day00PatientsList;
    private List<SinglePatientData> day01PatientsList;
    private List<SinglePatientData> day02PatientsList;
    private Dictionary<int, List<SinglePatientData>> dailyPatientsDictionary;
    public List<SinglePatientData> currentDayPatientsList;
    
    
    public Image displayedPatientIcon;
    
    [HideInInspector] public string sizeS = "Small";
    [HideInInspector] public string sizeM = "Medium";
    [HideInInspector] public string sizeL = "Large";
    
    [HideInInspector] public string dietC = "Carnivore";
    [HideInInspector] public string dietO = "Omnivore";
    [HideInInspector] public string dietH = "Herbivore";
    
    
    // private ResultsCalculator resultsCalculatorReference;
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
        // resultsCalculatorReference = FindObjectOfType<ResultsCalculator>();
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
        day00PatientsList = new List<SinglePatientData>();
        day01PatientsList = new List<SinglePatientData>();
        day02PatientsList = new List<SinglePatientData>();
        
        dailyPatientsDictionary = new Dictionary<int, List<SinglePatientData>>();

        dailyPatientsDictionary[0] = day00PatientsList;
        dailyPatientsDictionary[1] = day01PatientsList;
        dailyPatientsDictionary[2] = day02PatientsList;

        return;
    }

    public void SetCurrentDayPatientsList()
    {
        // Uses the current day number to access the correct entry in dailyPatientsDictionary
        currentDayPatientsList = dailyPatientsDictionary[DayManager.Instance.currentDayNumber];
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
                    // If the patient's day number matches i (the day to check for)
                    dailyPatientsDictionary[i].Add(c);
                }
            }
        }
        
        // determines the day with the most clients and feeds that number into the results calculator
        // results calculator uses this to set up its dailyTreatedClientsList!!
        GetmostPatientsInAnyGameDay();
    }

    void GetmostPatientsInAnyGameDay()
    {
        int mostPatientsInAnyGameDay = 0;
        
        for (int i = 0; i < dailyPatientsDictionary.Count; i++)
        {
            if (dailyPatientsDictionary[i].Count > mostPatientsInAnyGameDay)
            {
                mostPatientsInAnyGameDay = dailyPatientsDictionary[i].Count;
            }
            else
            {
                continue;
            }
        }
        
        Debug.Log(mostPatientsInAnyGameDay);
        ResultsCalculator.Instance.SetMaxPatientsNumber(mostPatientsInAnyGameDay);
    }
    
    void CreatePatientInformation()              // *TAG* - Would be good to reorder, but then I'll have to fix a bunch,,, HHHHHHHHbruhhhhhh
    {
        // Creates the individual SinglePatientData objects and fills them out with information related to each specific patient.
        allPatientsList = new List<SinglePatientData>();

        string s = "size";
        string d = "diet";

        string l = "letter";
        string c = "cured";
        string f = "failed";

        SinglePatientData barry = new SinglePatientData();
        barry.SetPatientInformation("Barry Buff", "Bear");
        barry.SetPatientExtras(s, sizeL);
        barry.SetPatientExtras(d, dietO);
        //barry._patientSize = sizeL;
        //barry._patientDiet = dietO;
        barry.SetPatientText(l, "These crystals formed after eating some homemade hot honey for dinner last night. My eyes are constantly pulsating, and I'm starting to lose my vision.\n\nPlease help me! I'm not sure what will happen if I leave it alone.");
        barry.SetPatientText(c, "UPDATE ME!");
        barry.SetPatientText(f, "<i>You recognise the writing as Barry's, though the penmanship is wobbly and lopsided. Letters fuse into each other as if he had no idea where the previous one began./n/nYou don't need to read the first sentence to know that you have failed him.</i>");
        barry.AttachPatientToDay(1);
        barry.SetPatientIcon();
            allPatientsList.Add(barry);
            barry.ReadAllPatientInformation();

        SinglePatientData arabella = new SinglePatientData();
        arabella.SetPatientInformation("Arabella Bunny", "Rabbit");
        arabella.SetPatientExtras(s, sizeS);
        arabella.SetPatientExtras(d, dietH);
        arabella.SetPatientText(l, "My family have been starving recently... One of my sons passed from this mysterious illness... I had no choice but to cook him up for supper as we had nothing to eat... I'm starting to have an urge for flesh, and I'm afraid of what I might do to my other children. Please help me, Apothekitty!");
        arabella.SetPatientText(c, "UPDATE ME!");
        arabella.SetPatientText(f, "UPDATE ME! What have I done? UPDATE ME!");
        arabella.AttachPatientToDay(2); 
        arabella.SetPatientIcon();
            allPatientsList.Add(arabella);

        SinglePatientData lawrence = new SinglePatientData();
        lawrence.SetPatientInformation("Lawrence Lark", "Bird");
        lawrence.SetPatientExtras(s, sizeS);
        lawrence.SetPatientExtras(d, dietO);    // *TAG* - CURRENTLY SET AS OMNIVORE
        lawrence.SetPatientText(l, "I love going for nightly glides amongst the treetops! However, a week ago, I noticed I developed this weird bite after one of my adventures... And now I've started growing teeth and bat wings! I don't know what's going on, but I don't like it! Please fix me, Apothekitty!");
        lawrence.SetPatientText(c, "UPDATE ME!");
        lawrence.SetPatientText(f, "UPDATE ME!");
        lawrence.AttachPatientToDay(2);
        lawrence.SetPatientIcon();
            allPatientsList.Add(lawrence);
            

        SinglePatientData jimothy = new SinglePatientData();
        jimothy.SetPatientInformation("Jimothy", "Jerboa");
        jimothy.SetPatientExtras(s, sizeS);
        jimothy.SetPatientExtras(d, dietO);
        jimothy.SetPatientText(l, "UPDATE ME!");
        jimothy.SetPatientText(c, "UPDATE ME!");
        jimothy.SetPatientText(f, "UPDATE ME!");
        jimothy.AttachPatientToDay(0);
        jimothy.SetPatientIcon();
            allPatientsList.Add(jimothy);
            
            // Debug.Log(barry._patientSize + " " + barry._patientDiet);

            
        SinglePatientData ferguson = new SinglePatientData();
        ferguson.SetPatientInformation("Ferguson", "Fox");//, "Medium, Omnivore");
        ferguson.SetPatientExtras(s, sizeM);
        ferguson.SetPatientExtras(d, dietO);
        ferguson.SetPatientText(ferguson._patientLetterText, "PLACEHOLDER LETTER TEXT - Ferguson");
        ferguson.SetPatientText(ferguson._patientCuredText, "UPDATE ME!");
        ferguson.SetPatientText(ferguson._patientFailedText, "UPDATE ME!");
        ferguson.AttachPatientToDay(2);
        ferguson.SetPatientIcon();
            allPatientsList.Add(ferguson);

        SinglePatientData peridot = new SinglePatientData();
        peridot.SetPatientInformation("Peridot", "Pangolin");//, "Medium, Omnivore");
        peridot.SetPatientExtras(s, sizeM);
        peridot.SetPatientExtras(d, dietO);
        peridot.SetPatientText(peridot._patientLetterText, "PLACEHOLDER LETTER TEXT - Peridot");
        peridot.SetPatientText(peridot._patientCuredText, "UPDATE ME!");
        peridot.SetPatientText(peridot._patientFailedText, "UPDATE ME!");
        peridot.AttachPatientToDay(1);
        peridot.SetPatientIcon();
            allPatientsList.Add(peridot);
            
        // Debug.Log(jimothy._patientName + " is a: " + jimothy._patientSize + " " + jimothy._patientDiet);
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

        Debug.Log("Active patient = " + activePatientData._patientName);
        Debug.Log(activePatientData._patientName + " is a: " + activePatientData._patientSize + " " + activePatientData._patientDiet);
        SendPatientData(activePatientData);
    }

    public void SetSpecificPatientData(SinglePatientData data)
    {
        activePatientData = data;
        SendPatientData(activePatientData);
    }

    void SendPatientData(SinglePatientData data)
    {
        SetPatientAilment(data);
        patientLetter.UpdateLetterDisplay(data);
        displayedPatientIcon = data._patientIcon;

        SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxFootsteps);

        GameManager.Instance.SetClientData();
        
        Invoke(nameof(SpawnPatient), 1.0f);
    }
    
    public void ChangePatientSprite()
    {
        Debug.Log("Should be changing patient sprite now...");
        UIManager.Instance.SpriteShift(displayedPatientIcon, activePatientData._patientAltIcon.sprite);
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
        ResultsCalculator.Instance.SetPatientData(currentData);

        Debug.Log("Current ailment is affecting: " + AilmentData.Instance.currentAilment._affectedClientName);
        // Debug.Log("Linking ailment with randomised patient.");
    }

    public void SetActivePatient(SinglePatientData patient)
    {
        activePatientData = patient;
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
