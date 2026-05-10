using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClientLetter : MonoBehaviour
{
    // should really be changed to PatientForm or Client / PatientInfo


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


    void Awake()
    {
        // not full singleton because i'm still scared from last semester's Horrors lmao
        _instance = this;

        resultsCalculator = FindObjectOfType<ResultsCalculator>();

        InitialiseLists();
        UpdateClientInformation();
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
        arabella.UpdateClientLetter("I don't have any specific dialogue yet, but something happened to me and I... I can't even write it down, I'm so horrified with what I've done. But now I'm so hungry all the time. I need to eat, the craving won't stop.\n\nI have a family, I can't be near them like this!");
            clientsList.Add(arabella);

        ClientData lawrence = new ClientData();
        lawrence.UpdateBasicInfo("Lawrence Lark", "Bird", "Small, Herbivore");
        lawrence.UpdateClientLetter("I don't have any specific dialogue yet, but bro something bit me the other night, and since then things have been getting whacky! I think I'm growing teeth, and I'm tired all day, but I can't get to sleep at night.");
            clientsList.Add(lawrence);
    }
 
    // Randomises the client that visits the player
    public void RandomiseIncomingClientLetter()
    {
        int randomisedNumber = Random.Range(0, clientsList.Count);
        clientLetter = clientsList[randomisedNumber];
        clientIcon.sprite = clientIconList[randomisedNumber].sprite;

        //Debug.Log("Client icon is of " + clientLetter._clientName);
        //Debug.Log(clientIcon.sprite.name);

        Invoke(nameof(SpawnClient), 1.0f);
    }

    // "spawns" the randomised client, and updates the relevant scripts with their information.
    void SpawnClient()
    {
        SceneManager.Instance.ShowClient(clientIcon);
        InitialiseLetterDisplay(clientLetter);
        SetClientAilment(clientLetter);

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

}
