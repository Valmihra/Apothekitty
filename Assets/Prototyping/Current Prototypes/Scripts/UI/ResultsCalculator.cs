using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsCalculator : MonoBehaviour
{
    private Button submissionButton;

    private Inventory inventory;

    private string currentClient;
    private string currentAilment;
    private List<string> inventoryContentsOnSubmission;

    public bool correctAilment;
    public bool correctRecipe;
    public bool correctHerbs;
    
    private DiagnosisSheetInteractables diagnosisSheetInteractables;

    void Awake()
    {
        submissionButton = GetComponent<Button>();
        diagnosisSheetInteractables = FindObjectOfType<DiagnosisSheetInteractables>();
    }
    
    void Start()
    {
        // add warning popup first? (!!!!)
        submissionButton.onClick.AddListener(delegate { CalculateResults(); });
        
        inventory = FindObjectOfType<Inventory>();
        ResetBools();
    }

    void CheckInventory()
    {
        inventoryContentsOnSubmission = new List<string>();

        foreach (InventorySlot i in inventory.inventorySlots)
        {
            if (!i.isEmpty)
            {
                Debug.Log(i.slotContents);
                inventoryContentsOnSubmission.Add(i.slotContents);
                // would be read by the results checker
            }
        }
    }

    void ResetBools()
    {
        correctAilment = false;
        correctRecipe = false;
        correctHerbs = false;
    }
    //void GetRequiredProperties()
    //{
        //currentClient = ClientLetter.Instance.clientName.text;
        //Debug.Log("Client is " + currentClient);
        //Debug.Log("Ailment is set as " + currentAilment);
        //currentClient = 
        //ClientLetter.Instance.clientLetter
    //}

    // Checks the inventory contents and compares it with the requirements for the client.
    void CalculateResults()
    {
        CheckInventory();

        CheckCorrectAilment();
        CheckCorrectRecipe();
        CheckCorrectHerbs();

        // function to send to end screen w/results
    }

    void CheckCorrectAilment()
    {
        if (SceneManager.Instance.selectedAilment == currentAilment)
        {
            correctAilment = true;
        }
    }

    void CheckCorrectRecipe()
    {
        bool correct = false;
        if ((diagnosisSheetInteractables.primaryEffect.value == AilmentData.Instance.currentAilment.primaryEffectDropdownNumber) && (diagnosisSheetInteractables.primaryTarget.value == AilmentData.Instance.currentAilment.primaryTargetDropdownNumber))
        {
            if ((diagnosisSheetInteractables.secondaryEffect.value == AilmentData.Instance.currentAilment.secondaryEffectDropdownNumber) && (diagnosisSheetInteractables.secondaryTarget.value == AilmentData.Instance.currentAilment.secondaryTargetDropdownNumber))
            {
                correct = true;
            }
        }

        if (correct)
        {
            if (AilmentData.Instance.currentAilment.modifierType == 0)
            {
                if (!diagnosisSheetInteractables.enhancer.isOn && !diagnosisSheetInteractables.invertor.isOn)
                {
                    correctRecipe = true;
                }
            }
            else if (AilmentData.Instance.currentAilment.modifierType == 1)
            {
                if (diagnosisSheetInteractables.enhancer.isOn && !diagnosisSheetInteractables.invertor.isOn)
                {
                    correctRecipe = true;
                }
            }
            else if (AilmentData.Instance.currentAilment.modifierType == 2)
            {
                if (!diagnosisSheetInteractables.enhancer.isOn && diagnosisSheetInteractables.invertor.isOn)
                {
                    correctRecipe = true;
                }
            }
            //mods
        }
        else
        {
            Debug.Log("Incorrect diagnosis sheet choices for ailment");
        }
    }

    void CheckCorrectHerbs()
    {
        bool match = false;
        foreach (string s in inventoryContentsOnSubmission)
        {
            match = false;
            foreach (string h in AilmentData.Instance.currentAilment.acceptableHerbs)
            {
                if (s == h)
                {
                    match = true;
                }
                else
                {
                    continue;
                }
            }

            if (!match)
            {
                Debug.Log("Incorrect herb chosen.");
            }
        }

        if (match)
        {
            correctHerbs = true;
        }
            /*foreach (SingleHerb h in AllHerbsData.Instance.herbDrawerContents)
            {
                if (s == h.herbName)
                {
                    if (AilmentData.Instance.currentAilment)
                    {

                    }
                    
                    h 
                }
                else
                {
                    continue;
                }
            }*/
            //if (s == )
            GoResultsScreen();
        
    }

    void GoResultsScreen()
    {
        //ResultsScreen.Instance.Send
        ResultsScreen.Instance.GenerateResultsScreen(correctAilment, correctRecipe, correctHerbs);
    }


    public void SetClientData(string ailmentName)
    {
        //foreach (SinglePage s in GrimoirePagesData.SinglePage)
    
        currentAilment = ailmentName;
        currentClient = ClientLetter.Instance.clientName.text;
        Debug.Log("Current client in ResultsCalculator is " + currentClient + " and the current ailment is " + currentAilment);
    }
}
