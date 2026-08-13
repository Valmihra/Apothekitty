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

        public void UpdateBasicInfo(string newName, string newSpecies, string newExtras)
        {
            _clientName = newName;
            _clientSpecies = newSpecies;
            _clientExtras = newExtras;

            // could also include the ailment & possibly key/mandatory ingredients/targets
            // for treatment as information to be compared against later on in the game.
        }

        public void UpdateClientLetter(string letterText)
        {
            _clientLetterText = letterText;
        }

        public void AssignDay(int dayNum)
        {
            _clientDayNumber = dayNum;
        }

        public void SetIcon(Image icon)
        {
            _clientIcon = icon;
        }
    }

    [HideInInspector]
    public List<ClientData> clientsList;
    public List<ClientData> treatedClientsList;
    private ClientData[] clientsArray;
    private List<Image> clientIconList;
    private Image[] clientIconArray;
    public ClientData clientLetter;

    public TMP_Text clientName;
    public TMP_Text clientSpecies;
    public TMP_Text clientExtras;
    public TMP_Text clientLetterText;
    public Image clientIcon;

    // all client images here
    public Image clientIcon01;
    public Image clientIcon02;
    public Image clientIcon03;

    public Image clientIcon04;
    public Image clientIcon05;
    public Image clientIcon06;


    public List<ClientData> currentDayClientsList;

    // public ClientData jimothy;


    private ResultsCalculator resultsCalculator;

    private static ClientLetter _instance;
    public static ClientLetter Instance
    {
        get
        {
            return _instance;
        }
    }

    // Vector used to reset draggable objects
    private Vector2 startingPosition;

    void Awake()
    {
        // not full singleton because i'm still scared from last semester's Horrors lmao
        _instance = this;

        resultsCalculator = FindObjectOfType<ResultsCalculator>();
        startingPosition = transform.position;
    }

    public void InitialiseClientLetter()
    {
        Debug.Log("Creating all clients...");
        //InitialiseLists();
        CreateClientInformation();

        return;
    }

    public void ResetClientLetterPosition()
    {
        transform.position = startingPosition;
    }

    public void UpdateCurrentDayClientsList()
    {
        // THIS WILL NEED TO BE FIXED. HONESTLY SHOULD PROBABLY MOVE ASSIGN DAILY TO HERE INSTEAD, BUT,,, IDK


        if (DayManager.Instance.currentDayNumber == 1)
        {
            currentDayClientsList = new List<ClientData>(DayManager.Instance.day01ClientsList);
        }
        else
        {
            currentDayClientsList = new List<ClientData>(DayManager.Instance.day02ClientsList);
        }




        foreach (ClientData c in currentDayClientsList)
        {
            Debug.Log(c._clientName);
        }
    }

    // Creates the lists used to access the clients and their associated images
        // Later versions would also account for the days and their set clients eg. clientIconsDayOne
    void InitialiseLists()
    {
        clientsList = new List<ClientData>();
        //clientIconList = new List<Image>();

        // Add all client icons here
        /*
        clientIconList.Add(clientIcon01);   // barry
        clientIconList.Add(clientIcon02);   // arabella
        clientIconList.Add(clientIcon03);   // lawrence

        clientIconList.Add(clientIcon04);   // jimothy
        clientIconList.Add(clientIcon05);   // TEMP_NPC01
        clientIconList.Add(clientIcon06);   // TEMP_NPC02*/

        //treatedClientsList = new List<ClientData>(clientsList.Count);
        //Debug.Log("clientsList is currently " + clientsList.Count + " entries long!.");
    }

    // Creates the individual ClientData objects and fills them out with information related to each specific client.
    void CreateClientInformation()
    {
        clientsList = new List<ClientData>();



        ClientData barry = new ClientData();
        barry.UpdateBasicInfo("Barry Buff", "Bear", "Large, Omnivore");
        barry.UpdateClientLetter("These crystals formed after eating some homemade hot honey for dinner last night. My eyes are constantly pulsating, and I'm starting to lose my vision.\n\nPlease help me! I'm not sure what will happen if I leave it alone.");
        barry.AssignDay(2);
        barry.SetIcon(clientIcon01);
            clientsList.Add(barry);

        ClientData arabella = new ClientData();
        arabella.UpdateBasicInfo("Arabella Bunny", "Rabbit", "Small, Herbivore");
        arabella.UpdateClientLetter("My family have been starving recently... One of my sons passed from this mysterious illness... I had no choice but to cook him up for supper as we had nothing to eat... I'm starting to have an urge for flesh, and I'm afraid of what I might do to my other children. Please help me, Apothekitty!");
        arabella.AssignDay(2);
        arabella.SetIcon(clientIcon02);
            clientsList.Add(arabella);

        ClientData lawrence = new ClientData();
        lawrence.UpdateBasicInfo("Lawrence Lark", "Bird", "Small, Herbivore");
        lawrence.UpdateClientLetter("I love going for nightly glides amongst the treetops! However, a week ago, I noticed I developed this weird bite after one of my adventures... And now I've started growing teeth and bat wings! I don't know what's going on, but I don't like it! Please fix me, Apothekitty!");
        lawrence.AssignDay(2);
        lawrence.SetIcon(clientIcon03);
            clientsList.Add(lawrence);




        ClientData jimothy = new ClientData();
        jimothy.UpdateBasicInfo("Jimothy", "Jerboa", "Small, Omnivore");
        jimothy.UpdateClientLetter("PLACEHOLDER LETTER TEXT - JIMOTHY");
        jimothy.AssignDay(1);
        jimothy.SetIcon(clientIcon04);
            clientsList.Add(jimothy);

        ClientData TEMP_NPC01 = new ClientData();
        TEMP_NPC01.UpdateBasicInfo("TEMP_NPC01", "PLACEHOLDER", "PLACEHOLDER, PLACEHOLDER");
        TEMP_NPC01.UpdateClientLetter("PLACEHOLDER LETTER TEXT - TEMP_NPC01");
        TEMP_NPC01.AssignDay(1);
        TEMP_NPC01.SetIcon(clientIcon05);
            clientsList.Add(TEMP_NPC01);

        ClientData TEMP_NPC02 = new ClientData();
        TEMP_NPC02.UpdateBasicInfo("TEMP_NPC02", "PLACEHOLDER", "PLACEHOLDER, PLACEHOLDER");
        TEMP_NPC02.UpdateClientLetter("PLACEHOLDER LETTER TEXT - TEMP_NPC02");
        TEMP_NPC02.AssignDay(1);
        TEMP_NPC02.SetIcon(clientIcon06);
            clientsList.Add(TEMP_NPC02);
    }

    /*public void ()
    {

    }*/
 
    // Randomises the client that visits the player
    public void RandomiseIncomingClientLetter()
    {
        if (GameManager.Instance.runningTutorial)
        {
            Debug.Log("    ----    FINDING TUTORIAL CHARACTER IN THE CLIENTS LIST    ----    ");
            clientLetter = clientsList[3];  // jimothy;
            Debug.Log("    ----    CLIENT'S NAME IS: " + clientLetter._clientName + "     ----    ");
            Debug.Log("    ----    ATTEMPTING TO LOCATE THE CLIENT ICON TO DISPLAY    ----    ");
            Debug.Log(clientLetter._clientIcon);
            Debug.Log("    ----    ASSIGNING THE CLIENT ICON    ----    ");
            clientIcon.sprite = clientLetter._clientIcon.sprite;
            Debug.Log("    ----    SPAWNING CLIENT    ----    ");

            Invoke(nameof(SpawnClient), 1.0f);
            // should add function into client data that assigns sprite from set image to the one onscreen
        }
        /*int randomisedNumber = Random.Range(0, clientsList.Count);
        clientLetter = clientsList[randomisedNumber];
        clientIcon.sprite = clientIconList[randomisedNumber].sprite;

        Invoke(nameof(SpawnClient), 1.0f);*/
        else
        {
            if (GameManager.Instance.seenFirstClient);
            {
                Debug.Log("Seen first client");
                Debug.Log(DayManager.Instance.currentDayNumber);
                Debug.Log(currentDayClientsList.Count);
                // Removes specific tutorial character from list to avoid pulling again
                if ((DayManager.Instance.currentDayNumber == 1) && (currentDayClientsList.Count == 3))
                {
                    
                    currentDayClientsList.RemoveAt(0);
                    //int indexNumber = 
                }
            }
            

            int randomisedNumber = Random.Range(0, currentDayClientsList.Count);
            clientLetter = currentDayClientsList[randomisedNumber];
            clientIcon.sprite = clientLetter._clientIcon.sprite;
            //clientIcon.sprite = clientIconList[randomisedNumber].sprite;

            /*if (!GameManager.Instance.seenFirstClient)
            {
                GameManager.Instance.seenFirstClient = true;
            }*/

            Invoke(nameof(SpawnClient), 1.0f);

        }








        //Debug.Log("Client icon is of " + clientLetter._clientName);
        //Debug.Log(clientIcon.sprite.name);
        //int safeSlots = 0;
        /*int numChecked = 0;

        for (int i = 0; i < clientsList.Count; i++)
        {
            numChecked++;
            
            if (clientLetter == treatedClientsList[i])
            {
                Debug.Log("Doubleup client. Rerolling.");
                RandomiseIncomingClientLetter();
            }
            else
            {
                //safeSlots++;
                
                continue;
            }
        }

        if (numChecked == clientsList.Count)
        {
            Invoke(nameof(SpawnClient), 1.0f);
        }*/
    }

    // "spawns" the randomised client, and updates the relevant scripts with their information.
    void SpawnClient()
    {
        SceneManager.Instance.ShowClient(clientIcon);
        InitialiseLetterDisplay(clientLetter);
        SetClientAilment(clientLetter);
        
        DialogueRunner.Instance.GetDialogue("patientArrive");
    }

    // Updates the text on the letter UI with the information from the specified ClientData
    void InitialiseLetterDisplay(ClientData data)
    {
        clientName.text = data._clientName;
        clientSpecies.text = data._clientSpecies;
        clientExtras.text = data._clientExtras;
        clientLetterText.text = data._clientLetterText;
    }

    // Sends the current client's name to AilmentData to link it with the correct ailment
    void SetClientAilment(ClientData client)
    {
        string currentData = client._clientName;
        AilmentData.Instance.LinkAilmentInformation(currentData);
        currentData = AilmentData.Instance.ConvertClientAilment(currentData);
        resultsCalculator.SetClientData(currentData);
        Debug.Log("Linking ailment with randomised client.");
    }


    public void UpdateLists()
    {
        int index = clientsList.Count + 10;     // failsafe to ensure the index is correct

        /*foreach (ClientData c in clientsList)
        {
            if (c == clientLetter)
            {
                index = clientsList.IndexOf(c);
                Debug.Log("Found client");
            }
            else
            {
                continue;
            }
        }
        
        if (index !> clientsList.Count)
        {
            clientsList.RemoveAt(index);
            clientIconList.RemoveAt(index);
        }*/

        foreach (ClientData c in currentDayClientsList)
        {
            Debug.Log(c._clientName);
            if (c == clientLetter)
            {
                index = currentDayClientsList.IndexOf(c);
                Debug.Log("Found client.");
                Debug.Log(index);
                

            }
            else
            {
                continue;
            }
        }

        currentDayClientsList.RemoveAt(index);
        //Debug.Log(currentDayClientsList.Count + "NUMBERRR");
        /*if (index !> currentDayClientsList.Count)
        {
            Debug.Log("Removing client.");
            currentDayClientsList.RemoveAt(index);

            List<ClientData> tempList = new List<ClientData>(currentDayClientsList);
            Debug.Log(tempList.Count + "NUMBERRR");
            // clientIconList.RemoveAt(index);
        }*/
    }
}
