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
        
        InitialiseLists();
        UpdateClientInformation();
    }

    public void ResetCLientLetter()
    {
        transform.position = startingPosition;
    }

    // Creates the lists used to access the clients and their associated images
        // Later versions would also account for the days and their set clients eg. clientIconsDayOne
    void InitialiseLists()
    {
        clientsList = new List<ClientData>();
        clientIconList = new List<Image>();

        // Add all client icons here
        clientIconList.Add(clientIcon01);
        clientIconList.Add(clientIcon02);
        clientIconList.Add(clientIcon03);

        //treatedClientsList = new List<ClientData>(clientsList.Count);
        
        //Debug.Log("clientsList is currently " + clientsList.Count + " entries long!.");
    }

    // Creates the individual ClientData classes and fills them out with information related to each specific client.
    void UpdateClientInformation()
    {
        ClientData barry = new ClientData();
        barry.UpdateBasicInfo("Barry Buff", "Bear", "Large, Omnivore");
        barry.UpdateClientLetter("These crystals formed after eating some homemade hot honey for dinner last night. My eyes are constantly pulsating, and I'm starting to lose my vision.\n\nPlease help me! I'm not sure what will happen if I leave it alone.");
            clientsList.Add(barry);

        ClientData arabella = new ClientData();
        arabella.UpdateBasicInfo("Arabella Bunny", "Rabbit", "Small, Herbivore");
        arabella.UpdateClientLetter("My family have been starving recently... One of my sons passed from this mysterious illness... I had no choice but to cook him up for supper as we had nothing to eat... I'm starting to have an urge for flesh, and I'm afraid of what I might do to my other children. Please help me, Apothekitty!");
            clientsList.Add(arabella);

        ClientData lawrence = new ClientData();
        lawrence.UpdateBasicInfo("Lawrence Lark", "Bird", "Small, Herbivore");
        lawrence.UpdateClientLetter("I love going for nightly glides amongst the treetops! However, a week ago, I noticed I developed this weird bite after one of my adventures... And now I've started growing teeth and bat wings! I don't know what's going on, but I don't like it! Please fix me, Apothekitty!");
            clientsList.Add(lawrence);
    }
 
    // Randomises the client that visits the player
    public void RandomiseIncomingClientLetter()
    {
        int randomisedNumber = Random.Range(0, clientsList.Count);
        clientLetter = clientsList[randomisedNumber];
        clientIcon.sprite = clientIconList[randomisedNumber].sprite;

        Invoke(nameof(SpawnClient), 1.0f);
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
        //Debug.Log("Running correctly.");
        DialogueRunner.Instance.GetDialogue("patientArrive");
    }

    // Updates the TMP files on the letter UI with the information from the specified ClientData
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
        int index = clientsList.Count + 10;
        //int check = 0;

        foreach (ClientData c in clientsList)
        {
            if (c == clientLetter)
            {
                index = clientsList.IndexOf(c);
                //check++;
            }
            else
            {
                continue;
            }
        }

        //if (check > 0)
        if (index !> clientsList.Count)
        {
            clientsList.RemoveAt(index);
            clientIconList.RemoveAt(index);
        }
    }
}
