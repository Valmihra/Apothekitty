using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsCalculator : MonoBehaviour
{
    public class FinishedClient
    {
        public string _treatedClientName; // public string _treatedClientAilment;
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
            
            // do smth else here? or no?
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

	// private int timesCalculated;
	int totalClientResults;
	private int currentClientResultNumber;
    
    [SerializeField]
	private Button submissionButton;

    private Inventory inventory;
    // private FinishedClientsData finishedClientsData;
    private DiagnosisSheetInteractables diagnosisSheetInteractables;

	[SerializeField]
	private Button goNextButton;
	// [SerializeField]
	// private Button goBackButton;

    /*void Awake()
    {
        submissionButton = GetComponent<Button>();
        submissionButton.onClick.AddListener(delegate { CalculateResults(); });
        
        inventory = FindObjectOfType<Inventory>();
        finishedClientsData = FindObjectOfType<FinishedClientsData>();
        diagnosisSheetInteractables = FindObjectOfType<DiagnosisSheetInteractables>();
    }*/
    
    void Start()
    {
        // add warning popup first? (!!!!)
        // ResetBools();
    }

    public void InitialiseResultsCalculator()
    {
        // submissionButton = GetComponent<Button>();
        submissionButton.onClick.AddListener(delegate { CalculateResults(); });

		goNextButton.onClick.AddListener(delegate { OnProgressButtonPushed(); });
		// goBackButton.onClick.AddListener(delegate { GoBackScreen(); });
        
        inventory = FindObjectOfType<Inventory>();
        // finishedClientsData = FindObjectOfType<FinishedClientsData>();
        diagnosisSheetInteractables = FindObjectOfType<DiagnosisSheetInteractables>();
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
                // would be read by the results checker
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

    public void ResetTreatedClientData()
    {
        // called at the beginning of each day to ensure the clients are up to date
		Debug.Log("Resetting treated client data for a new day...");
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
            
            // GOES BACK TO CLIENT WINDOW HERE INSTEAD?
            // SceneManager.Instance.ResultsScreenPrep();
            CheckCorrectAilment();
            CheckCorrectRecipe();
            CheckCorrectHerbs();
            
            // Stores the data in a container
            StoreClientTreatmentData();


            GameManager.Instance.SubmitTreatmentToClient();
        }

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
        if ((diagnosisSheetInteractables.primaryEffect.value == AilmentData.Instance.currentAilment._primaryEffectDropdownNumber) && (diagnosisSheetInteractables.primaryTarget.value == AilmentData.Instance.currentAilment._primaryTargetDropdownNumber))
        {
            if ((diagnosisSheetInteractables.secondaryEffect.value == AilmentData.Instance.currentAilment._secondaryEffectDropdownNumber) && (diagnosisSheetInteractables.secondaryTarget.value == AilmentData.Instance.currentAilment._secondaryTargetDropdownNumber))
            {
                correct = true;
            }
        }

        if (correct)
        {
            if (AilmentData.Instance.currentAilment._modifierTypeByNumber == 0)
            {
                if (!diagnosisSheetInteractables.enhancerToggle.isOn && !diagnosisSheetInteractables.inverterToggle.isOn)
                {
                    correctRecipe = true;
                }
            }
            else if (AilmentData.Instance.currentAilment._modifierTypeByNumber == 1)
            {
                if (diagnosisSheetInteractables.enhancerToggle.isOn && !diagnosisSheetInteractables.inverterToggle.isOn)
                {
                    correctRecipe = true;
                }
            }
            else if (AilmentData.Instance.currentAilment._modifierTypeByNumber == 2)
            {
                if (!diagnosisSheetInteractables.enhancerToggle.isOn && diagnosisSheetInteractables.inverterToggle.isOn)
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
        // bool match = false;
        int numMatches = 0;
        int targetIngredientCount = AilmentData.Instance.currentAilment._acceptableHerbsForTreatment.Count;
        //bool final

        int ingredientsChosen = 0;
        foreach (string ingredient in inventoryContentsOnSubmission)
        {
            ingredientsChosen++;
        }

        //if (ingredientsChosen == AilmentData.Instance.currentAilment._acceptableHerbsForTreatment.Count)
        if (ingredientsChosen == targetIngredientCount)
        {
            
            //foreach (string h in AilmentData.Instance.currentAilment._acceptableHerbsForTreatment)
            foreach (string ingredient in inventoryContentsOnSubmission)
            {
                Debug.Log("Checking for: " + ingredient);

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
        /*else
        {
            match = false;
        }*/
        
        if (numMatches == targetIngredientCount)
        {
            correctHerbs = true;
        }
        
        // GoResultsScreen();
        return;
        
    }

    public void GoResultsScreen()
    {
        // first call will always be at 0, for the first client in the list
		currentClientToDisplay = dailyTreatedClientsList[currentClientResultNumber];
		// increments number
		currentClientResultNumber++;
		
		// checks if last client of day and updates the text on the button accordingly
		ResultsScreen.Instance.UpdateResultsScreenButtonText(currentClientResultNumber == dailyTreatedClientsList.Count);
		ResultsScreen.Instance.GenerateResultsScreen(currentClientToDisplay._treatedClientName, currentClientToDisplay._correctAilment, currentClientToDisplay._correctRecipe, currentClientToDisplay._correctHerbs);
    }


    public void SetClientData(string ailmentName)
    {
        //foreach (SinglePage s in GrimoirePagesData.SinglePage)
    
        currentAilment = ailmentName;
        currentClient = ClientLetter.Instance.displayedClientName.text;
        Debug.Log("Current client in ResultsCalculator is " + currentClient + " and the current ailment is " + currentAilment);
    }
    
    
    void InitialiseFinishedClientsData()
    {
        // resultsCalculator = FindObjectOfType<ResultsCalculator>();
    }

    void CreateDailyTreatedClientsList()
    {
        dailyTreatedClientsList = new List<FinishedClient>();
        
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
        // AdjustListForNumberDailyClients();
    }
    
    void AdjustListForNumberDailyClients()
    {
        // assuming 5 is the maximum number of clients per day
        
            //dailyTreatedClientsList.Add
        

        for (int i = dailyTreatedClientsList.Count; i > ClientLetter.Instance.currentDayClientsList.Count; i--)
        {
            dailyTreatedClientsList.RemoveAt(i - 1);
        }

        Debug.Log("There are " + dailyTreatedClientsList.Count + " clients to be treated today.");
    }
    

    void StoreClientTreatmentData()
    {
        /*if (dailyTreatedClientsList == null || dailyTreatedClientsList.Count == 0)
        {
            dailyTreatedClientsList = new List<FinishedClient>();
            // (ClientLetter.Instance.currentDayClientsList.Count);
        }*/

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
        
        
        //
    }

	void OnProgressButtonPushed()
	{
		if (currentClientResultNumber == dailyTreatedClientsList.Count)
		{
			// Go Next Day
            // Sets the day in DayManager
            //
            // Goes to the next day in GameManager
			GameManager.Instance.GoNextDay();
		}
		else
		{
			GoResultsScreen();
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
    
}
