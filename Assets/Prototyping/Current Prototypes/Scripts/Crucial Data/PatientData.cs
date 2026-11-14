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

        /*public void ReadAllPatientInformation()
        {
            Debug.Log("Reading all patient information." + _patientName + " is a " + _patientSize + " " + _patientSpecies + ". Their diet is: " + _patientDiet);
        }*/

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
        
        // Debug.Log(mostPatientsInAnyGameDay);
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
        barry.SetPatientText(l, "These crystals formed after eating some homemade hot honey for dinner last night. My eyes are constantly pulsating, and I'm starting to lose my vision.\n\nPlease help me! I'm not sure what will happen if I leave it alone.");
        barry.SetPatientText(c, "The pulsating is gone and I can see again! I owe you my life! \n\nThank you, Apothekitty.");
        barry.SetPatientText(f, "<i>You recognise the writing as Barry's, though the penmanship is wobbly and lopsided. Letters fuse into each other as if he had no idea where the previous one began.\n\nYou don't need to read the first sentence to know that you have failed him.</i>");
        barry.AttachPatientToDay(1);
        barry.SetPatientIcon();
            allPatientsList.Add(barry);
            // barry.ReadAllPatientInformation();

        SinglePatientData arabella = new SinglePatientData();
        arabella.SetPatientInformation("Arabella Bunny", "Rabbit");
        arabella.SetPatientExtras(s, sizeS);
        arabella.SetPatientExtras(d, dietH);
        arabella.SetPatientText(l, "My family have been starving recently... One of my sons passed from this mysterious illness... I had no choice but to cook him up for supper as we had nothing to eat... I'm starting to have an urge for flesh, and I'm afraid of what I might do to my other children. Please help me, Apothekitty!");
        arabella.SetPatientText(c, "The cravings have passed thanks to you. I can watch my children grow up now without those unpleasant thoughts and feelings. \n\nYou saved my children's lives and my own, Thank you, Apothekitty.");
        arabella.SetPatientText(f, "Do not blame yourself. This is my own doing. I just couldn't help it, dear. They all were so sweet. I am a terrible mother, and no medicine can help that.");
        arabella.AttachPatientToDay(2); 
        arabella.SetPatientIcon();
            allPatientsList.Add(arabella);

        SinglePatientData lawrence = new SinglePatientData();
        lawrence.SetPatientInformation("Lawrence Lark", "Bird");
        lawrence.SetPatientExtras(s, sizeS);
        lawrence.SetPatientExtras(d, dietO);    // *TAG* - CURRENTLY SET AS OMNIVORE
        lawrence.SetPatientText(l, "I love going for nightly glides amongst the treetops! However, a week ago, I noticed I developed this weird bite after one of my adventures... And now I've started growing teeth and bat wings! I don't know what's going on, but I don't like it! Please fix me, Apothekitty!");
        lawrence.SetPatientText(c, "Hello Kitty!! I am back to peak flying condition thanks to you! The sky and sunlight have never felt so good. Besides, feathers suit a fellow like me more than fur.");
        lawrence.SetPatientText(f, "I don't understand. \n\nMy feathers are all falling out, and I can't meet with my friends in the sunshine anymore. You said you could help me, but I'm still hurting. Will I ever feel normal again?");
        lawrence.AttachPatientToDay(2);
        lawrence.SetPatientIcon();
            allPatientsList.Add(lawrence);
            

        SinglePatientData jimothy = new SinglePatientData();
        jimothy.SetPatientInformation("Jimothy", "Jerboa");
        jimothy.SetPatientExtras(s, sizeS);
        jimothy.SetPatientExtras(d, dietO);
        jimothy.SetPatientText(l, "When I was appointed to be the king's private jester, I was thrilled beyond belief. Every morning I would jump with joy as I got dressed for my shift. \n\nNow, however, it's so hard to even get out of bed. The thought of the king's eyes watching my every move as I juggle and dance for him. \n\nIt's even worse when I am to perform in front of his guests from neighbouring kingdoms. My legs tremble and my mouth quivers at the mere thought of it.");
        jimothy.SetPatientText(c, "Gramercy, cat doctor! Thanks to you, my job is safe, and I'm back to doing what I love! The king and I are eternally indebted to you.");
        jimothy.SetPatientText(f, "I'm sorry, cat doctor... The cure didn't work... After losing my job, I cannot bare to put on my hood or ribbons...");
        jimothy.AttachPatientToDay(0);
        jimothy.SetPatientIcon();
            allPatientsList.Add(jimothy);
            
            // Debug.Log(barry._patientSize + " " + barry._patientDiet);

            
        SinglePatientData ferguson = new SinglePatientData();
        ferguson.SetPatientInformation("Ferguson", "Fox");//, "Medium, Omnivore");
        ferguson.SetPatientExtras(s, sizeM);
        ferguson.SetPatientExtras(d, dietO);
        ferguson.SetPatientText(l, "Ya know I do love my family, but I think maybe their comments are getting to me. I don't want to be a failure. \n\nIt's been hard to eat anything lately, everything's too tough and chewy. I'm starting to not even like the taste of the dinners ma prepares. I should be grateful, I'm not a good enough son.");
        ferguson.SetPatientText(c, "Thank ya, Apothekitty! My claws and teeth are back, the antlers are gone, and ma and pa want me in the family again! It's been a long time since I felt this confident.");
        ferguson.SetPatientText(f, "What's happenin' to me... I've totally lost mahself and turned into a monster... Ma and pa won't even look me in the eyes anymore. Hell, I can't even look at mahself...");
        ferguson.AttachPatientToDay(2);
        ferguson.SetPatientIcon();
            allPatientsList.Add(ferguson);

        // letter text not updating. check reset?
        SinglePatientData peridot = new SinglePatientData();
        peridot.SetPatientInformation("Peridot", "Pangolin");//, "Medium, Omnivore");
        peridot.SetPatientExtras(s, sizeM);
        peridot.SetPatientExtras(d, dietO);
        peridot.SetPatientText(l, "It's been a few days since I've properly eaten. I don't trust what's in my pantry, I have no way to measure the grams of fat that I may ingest. And if I were to eat something unhealthy, what if I got sick? My body is too precious for me to consume anything unhealthy or clean enough.");
        peridot.SetPatientText(c, "My love! Thanks to your cure, I'm back to baking the best sweets in Faunbridge. Oh, you really are an angel! I'm forever grateful.");
        peridot.SetPatientText(f, "I can't eat or touch anything anymore... Everything dirty... I can't move from this corner, too impure... I'm sorry, my love...");
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

        Debug.Log("Active patient = " + activePatientData._patientName + ". " + activePatientData._patientName + " is a: " + activePatientData._patientSize + " " + activePatientData._patientDiet);
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
        
        UIManager.Instance.SpriteShift(displayedPatientIcon, data._patientIcon.sprite);
        
        
        // *TAG* - Get audio for footsteps here!
        string audioToPlay = data._patientSize == sizeS ? "Fast steps" : data._patientSize == sizeM ? "Medium steps" : data._patientSize == sizeL ? "Slow steps" : "Error";
        Debug.Log("Would play sound effect for: " + audioToPlay);
        SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxFootsteps);

        
        GameManager.Instance.SetClientData();
        
        Invoke(nameof(SpawnPatient), 1.0f);
    }
    
    public void ChangePatientSprite()
    {
        // Changes the patient's displayed sprite when they receive their treatment.
        Image imageToUpdate = displayedPatientIcon;
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
    }

    public void SetActivePatient(SinglePatientData patient)
    {
        activePatientData = patient;
    }

    public void UpdateCurrentDayPatientsList()
    {
        int failsafe = 10;
        int index = allPatientsList.Count + failsafe;

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
        
        currentDayPatientsList.RemoveAt(index);
        Debug.Log("Patients remaining today: " + currentDayPatientsList.Count);
    }

    public void ResetPatientLetter()
    {
        patientLetter.ResetPatientLetterPosition();
    }

    public string ReturnPatientCheckupLetter(string patientName, bool curedText)
    {
        string checkupLetterText = "default text. Issue with update on ReturnPatientCheckupLetter";
        
        foreach (SinglePatientData c in currentDayPatientsList)
        {
            if (c._patientName == patientName)
            {
                checkupLetterText = curedText ? c._patientCuredText : c._patientFailedText;
                break;
            }
            else
            {
                continue;
            }
        }

        Debug.Log("Patient checkup letter text saved.");
        return checkupLetterText;
    }
}
