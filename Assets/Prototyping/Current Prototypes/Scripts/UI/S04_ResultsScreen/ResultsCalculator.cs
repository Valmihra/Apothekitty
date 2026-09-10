using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsCalculator : MonoBehaviour
{
    public class FinishedClient
    {
        public string _treatedClientName;
        public bool _correctAilment;
        public bool _correctRecipe;
        public bool _correctHerbs;
        
        public bool _clientDataStored;

        public void SetupEmptyClientSlot()
        {
            _clientDataStored = false;
        }

        public void FinishTreatingClient()
        {
            _clientDataStored = true;
            Debug.Log("Client treatment data stored.");
            
            // *TAG* - do smth else here? or no? // public string _treatedClientAilment;
        }
        
        public void StoreCurrentProgressForThisClient(bool ailment, bool recipe, bool herbs)
        {
            _treatedClientName = ClientLetter.Instance.activeClientData._clientName;
            
            _correctAilment = ailment;
            _correctRecipe = recipe;
            _correctHerbs = herbs;
        }
    }

    public List<FinishedClient> dailyTreatedClientsList;
	private FinishedClient currentClientToDisplay;
    
    
    private string currentClient;
    private string currentAilment;
    private List<string> inventoryContentsOnSubmission;

    public bool correctAilment;
    public bool correctRecipe;
    public bool correctHerbs;

	private int totalClientResults;
	private int currentClientResultNumber;

    [SerializeField] private Button submitHerbsButton;
	[SerializeField] private Button resultsScreenNavigationButton;
    private TMP_Text resultsScreenNavigationButtonText;

    private Inventory inventory;
    private DiagnosisSheetInteractables treatmentPlanInteractables;


    public void InitialiseResultsCalculator()
    {
        submitHerbsButton.onClick.AddListener(delegate { CalculateResults(); });
		resultsScreenNavigationButton.onClick.AddListener(delegate { OnNextResultButtonPushed(); });
        
        inventory = FindObjectOfType<Inventory>();
        treatmentPlanInteractables = FindObjectOfType<DiagnosisSheetInteractables>();
		resultsScreenNavigationButtonText = resultsScreenNavigationButton.GetComponentInChildren<TMP_Text>();
    }

    void CheckInventoryContents()
    {
        inventoryContentsOnSubmission = new List<string>();

        foreach (InventorySlot i in inventory.inventorySlots)
        {
            if (!i.isEmpty)
            {
                Debug.Log(i.slotContents);
                inventoryContentsOnSubmission.Add(i.slotContents);
            }
        }
        return;
    }

    void ResetResultsCalculatorValues()
    {
        // called before every client is fed into the finished client list
        correctAilment = false;
        correctRecipe = false;
        correctHerbs = false;
    }

    public void ResetDailyTreatedClientData()
    {
        // called at the beginning of each day to ensure the clients are up to date
		// Debug.Log("Resetting treated client data for a new day...");
        CreateDailyTreatedClientsList();
        AdjustListForNumberDailyClients();
		totalClientResults = 0;
		currentClientResultNumber = 0;

    }

    // Called when submit button is pressed
    void CalculateResults()
    { 
        // Checks the inventory contents and compares with the requirements for the client.
        CheckInventoryContents();

        if (inventoryContentsOnSubmission.Count == 0)
        {
            MenuManager.Instance.HerbWallNothingChosenToSubmitPopup();
        }
        else if(inventoryContentsOnSubmission.Count == 1)
        {
            MenuManager.Instance.HerbWallOneHerbChosenToSubmitPopup();
        }
        else
        {
            // Resets for calculation
            ResetResultsCalculatorValues();

            CheckCorrectAilment();
            CheckCorrectRecipe();
            CheckCorrectHerbs();
            
            // Stores the data in a container
            StoreClientTreatmentData();
			// Continues the game
            GameManager.Instance.SubmitTreatmentToClient();
        }
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
        if ((treatmentPlanInteractables.primaryEffect.value == AilmentData.Instance.currentAilment._primaryEffectDropdownNumber) && (treatmentPlanInteractables.primaryTarget.value == AilmentData.Instance.currentAilment._primaryTargetDropdownNumber))
        {
            if ((treatmentPlanInteractables.secondaryEffect.value == AilmentData.Instance.currentAilment._secondaryEffectDropdownNumber) && (treatmentPlanInteractables.secondaryTarget.value == AilmentData.Instance.currentAilment._secondaryTargetDropdownNumber))
            {
                correct = true;
            }
        }

        if (correct)
        {
            if (AilmentData.Instance.currentAilment._modifierTypeByNumber == 0)
            {
                if (!treatmentPlanInteractables.enhancerToggle.isOn && !treatmentPlanInteractables.inverterToggle.isOn)
                {
                    correctRecipe = true;
                }
            }
            else if (AilmentData.Instance.currentAilment._modifierTypeByNumber == 1)
            {
                if (treatmentPlanInteractables.enhancerToggle.isOn && !treatmentPlanInteractables.inverterToggle.isOn)
                {
                    correctRecipe = true;
                }
            }
            else if (AilmentData.Instance.currentAilment._modifierTypeByNumber == 2)
            {
                if (!treatmentPlanInteractables.enhancerToggle.isOn && treatmentPlanInteractables.inverterToggle.isOn)
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
        int numMatches = 0;
        int targetIngredientCount = AilmentData.Instance.currentAilment._acceptableHerbsForTreatment.Count;
        int ingredientsChosen = 0;

        foreach (string ingredient in inventoryContentsOnSubmission)
        {
            ingredientsChosen++;
        }

        if (ingredientsChosen == targetIngredientCount)
        {
            foreach (string ingredient in inventoryContentsOnSubmission)
            {
                Debug.Log("Checking to see whether " + ingredient + " is a valid ingredient to treat this ailment...");

                for (int i = 0; i < ingredientsChosen; i++)
                {
                    if (AilmentData.Instance.currentAilment._acceptableHerbsForTreatment[i] == ingredient)
                    {
                        // Debug.Log("Match found: " + AilmentData.Instance.currentAilment._acceptableHerbsForTreatment[i] + " and " + ingredient);
                        numMatches++;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        
        if (numMatches == targetIngredientCount)
        {
            correctHerbs = true;
			GameManager.Instance.RecordCuredPatient();
        }
        
        return;
        
    }

    public void UpdateAndShowResultsScreen()
    {
        // first call will always be at 0, for the first client in the list
		currentClientToDisplay = dailyTreatedClientsList[currentClientResultNumber];
		// increments number
		currentClientResultNumber++;
		
		// checks if last client of day and updates the text on the button accordingly
		UpdateResultsScreenButtonText(currentClientResultNumber == dailyTreatedClientsList.Count);
		ResultsScreen.Instance.PrepNextClientResultScreen();
		ResultsScreen.Instance.GenerateResultsScreen(currentClientToDisplay._treatedClientName, currentClientToDisplay._correctAilment, currentClientToDisplay._correctRecipe, currentClientToDisplay._correctHerbs);
    }

	// This should be the button seen on the results screen itself. Adjusts based on how many clients left
    void UpdateResultsScreenButtonText(bool isFinalClientToReview)
    {
        if (isFinalClientToReview)
        {
            resultsScreenNavigationButtonText.text = ("Begin day " + (DayManager.Instance.currentDayNumber + 1)).ToString();
        }
        else
        {
            resultsScreenNavigationButtonText.text = "See results for next client";
        }
    }


    public void SetClientData(string ailmentName)
    {
        // uses the correct information from ClientLetter to set the ailment.
        currentAilment = ailmentName;
        currentClient = ClientLetter.Instance.displayedClientName.text;
        // Debug.Log("Current client in ResultsCalculator is " + currentClient + " and the current ailment is " + currentAilment);
    }

    void CreateDailyTreatedClientsList()
    {
        dailyTreatedClientsList = new List<FinishedClient>();
        // assuming 5 is the maximum number of clients per day
        FinishedClient treatedClient01 = new FinishedClient();
            treatedClient01.SetupEmptyClientSlot();
            dailyTreatedClientsList.Add(treatedClient01);
            
        FinishedClient treatedClient02 = new FinishedClient();
            treatedClient02.SetupEmptyClientSlot();
            dailyTreatedClientsList.Add(treatedClient02);
            
        FinishedClient treatedClient03 = new FinishedClient();
            treatedClient03.SetupEmptyClientSlot();
            dailyTreatedClientsList.Add(treatedClient03);
            
        FinishedClient treatedClient04 = new FinishedClient();
            treatedClient04.SetupEmptyClientSlot();
            dailyTreatedClientsList.Add(treatedClient04);
            
        FinishedClient treatedClient05 = new FinishedClient();
            treatedClient05.SetupEmptyClientSlot();
            dailyTreatedClientsList.Add(treatedClient05);
            
            return;
    }
    
    void AdjustListForNumberDailyClients()
    {
        for (int i = dailyTreatedClientsList.Count; i > ClientLetter.Instance.currentDayClientsList.Count; i--)
        {
            dailyTreatedClientsList.RemoveAt(i - 1);
        }

        // Debug.Log("Clients to be treated today: " + dailyTreatedClientsList.Count);
    }
    

    void StoreClientTreatmentData()
    {
        // *TAG* - better to do this in a for loop or okay like this?
        foreach (FinishedClient f in dailyTreatedClientsList)
        {
            if (!f._clientDataStored)
            {
                f.StoreCurrentProgressForThisClient(correctAilment, correctRecipe, correctHerbs);

                Debug.Log("Client has been treated. Data stored is...");
                Debug.Log("Client name: " + f._treatedClientName);
                Debug.Log("Correct ailment: " + f._correctAilment + " correct recipe: " + f._correctRecipe + " correct treatment combination: " + f._correctHerbs);
                
				totalClientResults++;
                f.FinishTreatingClient();
                break;
            }
        }
    }

	void OnNextResultButtonPushed()
	{
		if (currentClientResultNumber == dailyTreatedClientsList.Count)
		{
			if (DayManager.Instance.currentDayNumber == DayManager.Instance.allGameDays.Count - 1)
			{
				// GameManager.Instance.GoEndMVP();
				// MENUMANAGER INST.
				MenuManager.Instance.OpenTutorialPopup("endOfMVP");
			}
			else
			{
				GameManager.Instance.GoNextDay();
			}
		}
		else
		{
			UpdateAndShowResultsScreen();
		}
	}
    
	/*void GoNextScreen()
	{
		
		currentClientToDisplay = dailyTreatedClientsList[currentClientResultNumber];
		
		// currentClientResultNumber++;
		ResultsScreen.Instance.GenerateResultsScreen(currentClientToDisplay._treatedClientName, currentClientToDisplay._correctAilment, currentClientToDisplay._correctRecipe, currentClientToDisplay._correctHerbs);
		// 
		//
	}*/






    // STILL NEED SOMETHING TO SUBMIT THE FULL AILMENT WITH!!

    // maybe trigger on submission to client?
    /*public void SetClientAilment()
    {
        string client;
        foreach (Ailment a in AilmentData.Global.allAilmentsList)
        {
            if (a._affectedClientName == ClientData.Instance.activeClientData.name)
            {
                client = a._affectedClientName;
                GameData.CalculateResultFor(client);
            }
            else
            continue;
        }
    }*/
    
}
