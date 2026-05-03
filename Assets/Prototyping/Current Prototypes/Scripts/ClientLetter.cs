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

            //public AilmentData clientAilment_;

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

    public static ClientLetter ClientsGlobal
    {
        get
        {
            return clientsGlobal;
        }
    }
    private static ClientLetter clientsGlobal;


    void Awake()
    {
        // not full singleton because i'm still scared from last semester's Horrors lmao
        clientsGlobal = this;

        InitialiseLists();
        SetLetterData();
        SetArrays();
        //Debug.Log("clientsList is currently " + clientsList.Count + " entries long!.");

        //RandomiseIncomingClientLetter();
    }

    void InitialiseLists()
    {
        clientsList = new List<ClientData>();
        clientIconList = new List<Image>();

        // Add all icons here
        clientIconList.Add(clientIcon01);
        clientIconList.Add(clientIcon02);
        clientIconList.Add(clientIcon03);
    }

    void SetArrays()
    {
        clientsArray = clientsList.ToArray();
        clientIconArray = clientIconList.ToArray();
        //Debug.Log("clientsArray is " + clientsArray.Length + " units long!");
    }

    void SetLetterData()
    {
        ClientData barry = new ClientData();
        barry.UpdateBasicInfo("Barry Buff", "Bear", "Large, Omnivore");
        barry.UpdateClientLetter("These crystals formed after eating some homemade hot honey for dinner last night. My eyes are constantly pulsating, and I'm starting to lose my vision. Please help me! I'm not sure what will happen if I leave it alone.");
            clientsList.Add(barry);

        // add other clients here.
        ClientData arabella = new ClientData();
        arabella.UpdateBasicInfo("Arabella Bunny", "Rabbit", "Small, Herbivore");
        arabella.UpdateClientLetter("I don't have any specific dialogue yet, but I so hungy for flesh, help-a-me!");
            clientsList.Add(arabella);

        ClientData lawrence = new ClientData();
        lawrence.UpdateBasicInfo("Lawrence Lark", "Bird", "Small, Herbivore");
        lawrence.UpdateClientLetter("I don't have any specific dialogue yet, but bro I'm turning into a bat!");
            clientsList.Add(lawrence);
    }

    public void RandomiseIncomingClientLetter()
    {
        //clientLetter = new ClientData();
        int randomisedNumber = Random.Range(0, clientsArray.Length);
        clientLetter = clientsArray[randomisedNumber];
        clientIcon.sprite = clientIconArray[randomisedNumber].sprite;

        //Debug.Log("Client icon is of " + clientLetter.clientName_);
        //Debug.Log(clientIcon.sprite.name);

        Invoke(nameof(SpawnClient), 1.0f);
        //InitialiseLetterDisplay(clientLetter);
    }

    void SpawnClient()
    {
        SceneManager.Instance.ShowClient(clientIcon);
        Debug.Log("The letter has been sent.");
        InitialiseLetterDisplay(clientLetter);

        DialogueRunner.Instance.GetDialogue("patientArrive");
    }

    void InitialiseLetterDisplay(ClientData data)
    {
        clientName.text = data.clientName_;
        clientSpecies.text = data.clientSpecies_;
        clientExtras.text = data.clientExtras_;
        clientLetterText.text = data.clientLetterText_;
    }



    /*void SetClientAilment()
    {
        foreach (Ailment a in AilmentData.Global.allAilments)
        {
            if (a.affectedClientName == clientLetter.name)
            {
                //GameData
            }
            else
            continue;
        }
    }*/

}
