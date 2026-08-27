using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClientLetter : MonoBehaviour
{
    // name should really be changed to PatientForm or Client / PatientInfo
    // functions could probably be split into two scripts (?)

    public class ClientData
    {
        public string _clientName;
        public string _clientSpecies;
        public string _clientExtras;
        public string _clientLetterText;

        public int _clientDayNumber;

        public Image _clientIcon;
                // private Image _clientIconStandard
                // private Image _clientIconHappy / sad / cured ??

            //public AilmentData clientAilment_;

        public void SetClientInformation(string newName, string newSpecies, string newExtras)
        {
            _clientName = newName;
            _clientSpecies = newSpecies;
            _clientExtras = newExtras;

            // could also include the ailment & possibly key/mandatory ingredients/targets
            // for treatment as information to be compared against later on in the game.
        }

        public void SetClientLetterText(string letterText)
        {
            _clientLetterText = letterText;
        }
        
        public void SetClientIcon(Image icon)
        {
            _clientIcon = icon;
        }
        
        public void AttachClientToDay(int dayNum)
        {
            _clientDayNumber = dayNum;
        }
    }

    [HideInInspector]
    public List<ClientData> allClientsList;
    public ClientData activeClientData;
    // public List<ClientData> treatedClientsList;
    
    
    // private ClientData[] clientsArray;
    // private List<Image> clientIconList;
    // private Image[] clientIconArray;
    
    // Variables used to display the active client's data on the screen
    public TMP_Text displayedClientName;
    public TMP_Text displayedClientSpecies;
    public TMP_Text displayedClientExtras;
    public TMP_Text displayedClientLetterText;
    public Image displayedClientIcon;

    // all client images here
    public Image clientIcon01;
    public Image clientIcon02;
    public Image clientIcon03;

    public Image clientIcon04;
    public Image clientIcon05;
    public Image clientIcon06;


    public List<ClientData> currentDayClientsList;
    
    public List<ClientData> day00ClientsList;
    public List<ClientData> day01ClientsList;
    public List<ClientData> day02ClientsList;
    
     private Dictionary<int, List<ClientData>> dailyClientsDictionary;
    //private List<List<ClientData>> allClientListsForEachGameDay;
    
    // public ClientData Jimothy;

    // Vector used to reset draggable objects
    private Vector2 clientLetterStartingPosition;
    private ResultsCalculator resultsCalculator;

    private static ClientLetter _instance;
    public static ClientLetter Instance
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
        resultsCalculator = FindObjectOfType<ResultsCalculator>();
    }

    public void InitialiseClientLetter()
    {
        Debug.Log("Creating all clients...");
        CreateClientInformation();
        clientLetterStartingPosition = transform.position;
        
        return;
    }

    public void ResetClientLetterPosition()
    {
        transform.position = clientLetterStartingPosition;
    }

    void SetupClientDictionary()
    {
        //
        day00ClientsList = new List<ClientData>();
        day01ClientsList = new List<ClientData>();
        day02ClientsList = new List<ClientData>();
        
        dailyClientsDictionary = new Dictionary<int, List<ClientData>>();

        dailyClientsDictionary[0] = day00ClientsList;
        dailyClientsDictionary[1] = day01ClientsList;
        dailyClientsDictionary[2] = day02ClientsList;
        //int key = 0;

        return;
        //if (!dailyClientsDictionary.TryGetValue(key, out))
    }
    
    // void 

    public void SetCurrentDayClientsList()
    {
        currentDayClientsList = dailyClientsDictionary[DayManager.Instance.currentDayNumber];
        
        /*Debug.Log("Current day is " + DayManager.Instance.currentDayNumber + " and the current day clients are...");
        
        foreach (ClientData c in currentDayClientsList)
        {
            Debug.Log(c._clientName);
        }*/
    }

    public void AssignClientsToGameDays()
    {
        SetupClientDictionary();
        
        for (int i = 0; i < DayManager.Instance.allGameDays.Count; i++)
        {
            foreach (ClientData c in allClientsList)
            {
                if (c._clientDayNumber == i)
                {
                    dailyClientsDictionary[i].Add(c);
                }
            }
        }

        Debug.Log("Number of clients present for day 1 is: " + dailyClientsDictionary[1].Count);
    }

    // Creates the individual ClientData objects and fills them out with information related to each specific client.
    void CreateClientInformation()
    {
        allClientsList = new List<ClientData>();



        ClientData barry = new ClientData();
        barry.SetClientInformation("Barry Buff", "Bear", "Large, Omnivore");
        barry.SetClientLetterText("These crystals formed after eating some homemade hot honey for dinner last night. My eyes are constantly pulsating, and I'm starting to lose my vision.\n\nPlease help me! I'm not sure what will happen if I leave it alone.");
        barry.AttachClientToDay(2);
        barry.SetClientIcon(clientIcon01);
            allClientsList.Add(barry);

        ClientData arabella = new ClientData();
        arabella.SetClientInformation("Arabella Bunny", "Rabbit", "Small, Herbivore");
        arabella.SetClientLetterText("My family have been starving recently... One of my sons passed from this mysterious illness... I had no choice but to cook him up for supper as we had nothing to eat... I'm starting to have an urge for flesh, and I'm afraid of what I might do to my other children. Please help me, Apothekitty!");
        arabella.AttachClientToDay(2); 
        arabella.SetClientIcon(clientIcon02);
            allClientsList.Add(arabella);

        ClientData lawrence = new ClientData();
        lawrence.SetClientInformation("Lawrence Lark", "Bird", "Small, Herbivore");
        lawrence.SetClientLetterText("I love going for nightly glides amongst the treetops! However, a week ago, I noticed I developed this weird bite after one of my adventures... And now I've started growing teeth and bat wings! I don't know what's going on, but I don't like it! Please fix me, Apothekitty!");
        lawrence.AttachClientToDay(2);
        lawrence.SetClientIcon(clientIcon03);
            allClientsList.Add(lawrence);




        ClientData jimothy = new ClientData();
        jimothy.SetClientInformation("Jimothy", "Jerboa", "Small, Omnivore");
        jimothy.SetClientLetterText("PLACEHOLDER LETTER TEXT - JIMOTHY");
        jimothy.AttachClientToDay(0);
        jimothy.SetClientIcon(clientIcon04);
            allClientsList.Add(jimothy);

        ClientData TEMP_NPC01 = new ClientData();
        TEMP_NPC01.SetClientInformation("TEMP_NPC01", "PLACEHOLDER", "PLACEHOLDER, PLACEHOLDER");
        TEMP_NPC01.SetClientLetterText("PLACEHOLDER LETTER TEXT - TEMP_NPC01");
        TEMP_NPC01.AttachClientToDay(1);
        TEMP_NPC01.SetClientIcon(clientIcon05);
            allClientsList.Add(TEMP_NPC01);

        ClientData TEMP_NPC02 = new ClientData();
        TEMP_NPC02.SetClientInformation("TEMP_NPC02", "PLACEHOLDER", "PLACEHOLDER, PLACEHOLDER");
        TEMP_NPC02.SetClientLetterText("PLACEHOLDER LETTER TEXT - TEMP_NPC02");
        TEMP_NPC02.AttachClientToDay(1);
        TEMP_NPC02.SetClientIcon(clientIcon06);
            allClientsList.Add(TEMP_NPC02);
    }
 
    // Randomises the client that visits the player
    public void RandomiseIncomingClientLetter()
    {
        if (GameManager.Instance.runningTutorial)
        {
            activeClientData = allClientsList[3];  // Jimothy;
            displayedClientIcon.sprite = activeClientData._clientIcon.sprite;
                // Maybe activeClientData = tutorialClient (as a set private ClientData object?)
            Invoke(nameof(SpawnClient), 1.0f);
            // should add function into client data that assigns sprite from set image to the one onscreen
        }
        else
        {
            /*if (GameManager.Instance.seenFirstClient)//;
            {
                Debug.Log("First client has already been treated. It is currently day " + DayManager.Instance.currentDayNumber + ", and there are " + currentDayClientsList.Count + " clients that still require a treatment submission.");
                // Removes specific tutorial character from list to avoid pulling again
                if ((DayManager.Instance.currentDayNumber == 1) && (currentDayClientsList.Count == 3))
                {
                    // WILL NEED TO REWORK AFTER SEPARATING JIMOTHY TO A SINGLE CLIENT DAY!!
                    currentDayClientsList.RemoveAt(0);
                    //int indexNumber = 
                }
            }
            */

            int randomisedNumber = Random.Range(0, currentDayClientsList.Count);
            activeClientData = currentDayClientsList[randomisedNumber];
            displayedClientIcon.sprite = activeClientData._clientIcon.sprite;
            //displayedClientIcon.sprite = clientIconList[randomisedNumber].sprite;

            /*if (!GameManager.Instance.seenFirstClient)
            {
                GameManager.Instance.seenFirstClient = true;
            }*/

            Invoke(nameof(SpawnClient), 1.0f);

        }
    }

    // "spawns" the randomised client, and updates the relevant scripts with their information.
    void SpawnClient()
    {
        SceneManager.Instance.ShowClient(displayedClientIcon);
        InitialiseLetterDisplay(activeClientData);
        SetClientAilment(activeClientData);
        
        DialogueRunner.Instance.GetDialogue("patientArrive");
    }

    // Updates the text on the letter UI with the information from the specified ClientData
    void InitialiseLetterDisplay(ClientData data)
    {
        displayedClientName.text = data._clientName;
        displayedClientSpecies.text = data._clientSpecies;
        displayedClientExtras.text = data._clientExtras;
        displayedClientLetterText.text = data._clientLetterText;
    }

    // Sends the current client's name to AilmentData to link it with the correct ailment
    void SetClientAilment(ClientData client)
    {
        string currentData = client._clientName;
        // uses client name to find their ailment and set it as currentAilment in AilmentData
        AilmentData.Instance.SetCurrentAilmentByClient(currentData);
        
        currentData = AilmentData.Instance.ConvertClientNameToAilmentName(currentData);
        resultsCalculator.SetClientData(currentData);
        Debug.Log("Linking ailment with randomised client.");
    }


    public void UpdateCurrentDayClientsList()
    {
        int index = allClientsList.Count + 10;     // failsafe to ensure the index is correct

        foreach (ClientData c in currentDayClientsList)
        {
            if (c == activeClientData)
            {
                index = currentDayClientsList.IndexOf(c);
            }
            else
            {
                continue;
            }
        }

        currentDayClientsList.RemoveAt(index);
        // Debug.Log("There are currently " + currentDayClientsList.Count + " clients left today.");
    }
}
