using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClientLetter : MonoBehaviour
{
    public class ClientData
    {
        public string clientName_;
        public string clientSpecies_;
        public string clientExtras_;
        public string clientLetterText_;

        public void UpdateBasicInfo(string newName, string newSpecies, string newExtras)
        {
            clientName_ = newName;
            clientSpecies_ = newSpecies;
            clientExtras_ = newExtras;

            // could also include the ailment & possibly key/mandatory ingredients/targets
            // for treatment as information to be compared against later on in the game.
        }

        public void UpdateClientLetter(string letterText)
        {
            clientLetterText_ = letterText;
        }
    }

    private List<ClientData> clientsList;
    private ClientData[] clientsArray;
    private List<Image> clientIconList;
    private Image[] clientIconArray;
    private ClientData clientLetter;

    public TMP_Text clientName;
    public TMP_Text clientSpecies;
    public TMP_Text clientExtras;
    public TMP_Text clientLetterText;
    public Image clientIcon;

    // all client images here
    public Image clientIcon01;

    public static ClientLetter ClientsGlobal
    {
        get
        {
            return clientsGlobal;
        }
    }
    private static ClientLetter clientsGlobal;


    void Start()
    {
        // not full singleton because i'm still scared from last semester's Horrors lmao
        clientsGlobal = this;

        InitialiseLists();
        SetLetterData();
        SetArrays();
        //Debug.Log("clientsList is currently " + clientsList.Count + " entries long!.");

        RandomiseInitialClientLetter();
    }

    void InitialiseLists()
    {
        clientsList = new List<ClientData>();
        clientIconList = new List<Image>();

        // Add all icons here
        clientIconList.Add(clientIcon01);
    }

    void SetArrays()
    {
        clientsArray = clientsList.ToArray();
        clientIconArray = clientIconList.ToArray();
        //Debug.Log("clientsArray is " + clientsArray.Length + " units long!");
    }

    void SetLetterData()
    {
        ClientData Barry = new ClientData();
        Barry.UpdateBasicInfo("Barry Buff", "Bear", "Large, Omnivore");
        Barry.UpdateClientLetter("These crystals formed after eating some homemade hot honey for dinner last night. My eyes are constantly pulsating, and I'm starting to lose my vision. Please help me! I'm not sure what will happen if I leave it alone.");
            clientsList.Add(Barry);

        // add other clients here.
    }

    void RandomiseInitialClientLetter()
    {
        //clientLetter = new ClientData();
        int randomisedNumber = Random.Range(0, clientsArray.Length);
        clientLetter = clientsArray[randomisedNumber];
        clientIcon.sprite = clientIconArray[randomisedNumber].sprite;

        InitialiseLetterDisplay(clientLetter);
    }

    void InitialiseLetterDisplay(ClientData data)
    {
        clientName.text = data.clientName_;
        clientSpecies.text = data.clientSpecies_;
        clientExtras.text = data.clientExtras_;
        clientLetterText.text = data.clientLetterText_;
    }

}
