using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsCalculator : MonoBehaviour
{
    public class FinishedPatient
    {
        public string _treatedPatientName;
        public bool _correctAilment;
        public bool _correctRecipe;
        public bool _correctHerbs;
        
        public bool _patientDataStored;

        // public string _patientCheckupLetter;

        public void SetupEmptyPatientSlot()
        {
            _patientDataStored = false;
        }

        public void FinishTreatingPatient()
        {
            // *TAG* - do smth else here? or no? // public string _treatedClientAilment;
            _patientDataStored = true;
            Debug.Log("Client treatment data stored.");
        }
        
        public void StoreCurrentProgressForThisPatient(bool ailment, bool recipe, bool herbs)
        {
            _treatedPatientName = PatientData.Instance.activePatientData._patientName;
            
            _correctAilment = ailment;
            _correctRecipe = recipe;
            _correctHerbs = herbs;
        }
    }

    public List<FinishedPatient> dailyTreatedPatientsList;
	private FinishedPatient currentPatientToDisplay;
    
    private string currentPatientAilmentFromClientData;
    private List<string> inventoryContentsOnSubmission;

    private bool correctAilment;
    private bool correctRecipe;
    private bool correctHerbs;

	private int totalClientResults;
	private int currentClientResultNumber;
    private int maxNumberOfDailyClientResults;

    [SerializeField] private Button submitHerbsButton;
	[SerializeField] private Button resultsScreenNavigationButton;
    private TMP_Text resultsScreenNavigationButtonText;

    private Inventory inventory;
    private TreatmentPlanInteractables treatmentPlanInteractables;
    
    private static ResultsCalculator _instance;
    public static ResultsCalculator Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }


    public void InitialiseResultsCalculator()
    {
        submitHerbsButton.onClick.AddListener(delegate { CalculateResults(); });
		resultsScreenNavigationButton.onClick.AddListener(delegate { OnNextResultButtonPushed(); });
        
        inventory = FindObjectOfType<Inventory>();
        treatmentPlanInteractables = FindObjectOfType<TreatmentPlanInteractables>();
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
        // called before every patient is fed into the finished patient list
        correctAilment = false;
        correctRecipe = false;
        correctHerbs = false;
    }

    public void ResetDailyTreatedClientData()
    {
        // called at the beginning of each day to ensure the patients are up to date
        CreateDailyTreatedPatientsList();
        AdjustListForNumberDailyClients();
		totalClientResults = 0;
		currentClientResultNumber = 0;
    }

    
    void CalculateResults()
    { 
        // Called when submit button is pressed
        CheckInventoryContents();
        
        if (inventoryContentsOnSubmission.Count == 0)
        {
            MenuManager.Instance.AlertMessage("herbWallNothingChosen");
        }
        
        else if(inventoryContentsOnSubmission.Count == 1)
        {
            MenuManager.Instance.AlertMessage("herbWallOneHerbChosen");
        }
        
        else
        {
            // Resets the calculator and performs necessary checks
            ResetResultsCalculatorValues();
            CheckCorrectAilment();
            CheckCorrectRecipe();
            CheckCorrectHerbs();
            
            // Stores the data in a container and continues the game
            StoreClientTreatmentData();
            GameManager.Instance.SubmitTreatmentToClient();
        }
    }

    void CheckCorrectAilment()
    {
        // *TAG* - should probably grab from gameData instead of scenemanager.
        // not a good place to store info like that,,
        if (SceneManager.Instance.selectedAilment == currentPatientAilmentFromClientData) 
        {
            correctAilment = true;
        }
    }

    void CheckCorrectRecipe()
    {
        bool correct = false;
        if ((treatmentPlanInteractables.primaryEffectDropdown.value == AilmentData.Instance.currentAilment._primaryEffectDropdownNumber) && (treatmentPlanInteractables.primaryTargetDropdown.value == AilmentData.Instance.currentAilment._primaryTargetDropdownNumber))
        {
            // Debug.Log("Primary dropdown values match");
            if ((treatmentPlanInteractables.secondaryEffectDropdown.value == AilmentData.Instance.currentAilment._secondaryEffectDropdownNumber) && (treatmentPlanInteractables.secondaryTargetDropdown.value == AilmentData.Instance.currentAilment._secondaryTargetDropdownNumber))
            {
                correct = true;
            }
        }

        if (correct)
        {
            // Debug.Log("Correct Recipe!!");
            correctRecipe = true;
        }
        
        else
        {
            Debug.Log("Incorrect treatment plan choices for ailment");
        }
    }

    void CheckCorrectHerbs()
    {
        int numMatches = 0;
		// figures out how many herbs should be used by checking the ailment's dropdown numbers.
		// If no secondary effect, needs 2 herbs. If secondary effect, needs 4 herbs.
		int targetIngredientCount = AilmentData.Instance.currentAilment._secondaryEffectDropdownNumber == 0 ? 2 : 4;
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
        // first call will always be at 0, for the first patient in the list. Increments AFTER this.
		currentPatientToDisplay = dailyTreatedPatientsList[currentClientResultNumber];
		currentClientResultNumber++;
		
		// checks if last patient of day and updates the text on the button accordingly
		UpdateResultsScreenButtonText(currentClientResultNumber == dailyTreatedPatientsList.Count);
		ResultsScreen.Instance.PrepNextClientResultScreen();
		ResultsScreen.Instance.GenerateResultsScreen(currentPatientToDisplay._treatedPatientName, currentPatientToDisplay._correctAilment, currentPatientToDisplay._correctRecipe, currentPatientToDisplay._correctHerbs);
    }

	
    void UpdateResultsScreenButtonText(bool isFinalClientToReview)
    {
        // Adjusts what is shown on the results screen button based on how many patient results are left to review
        if (isFinalClientToReview)
        {
            resultsScreenNavigationButtonText.text = ("Begin day " + (DayManager.Instance.currentDayNumber + 1)).ToString();
        }
        else
        {
            resultsScreenNavigationButtonText.text = "See results for next patient";
        }
    }


    public void SetPatientData(string ailmentName)
    {
        // Called from PatientData when receiving a new patient.
        // Updates the current ailment to treat for.
        currentPatientAilmentFromClientData = ailmentName;
    }

    public void SetMaxPatientsNumber(int maxPatientsNumber)
    {
        // Remove one from the number to adjust for the initial 0 value of a list
        maxNumberOfDailyClientResults = maxPatientsNumber;
    }

    void CreateDailyTreatedPatientsList()
    {
        dailyTreatedPatientsList = new List<FinishedPatient>();

        for (int i = 0; i < maxNumberOfDailyClientResults; i++)
        {
            FinishedPatient f = new FinishedPatient();
            
            f.SetupEmptyPatientSlot();
            dailyTreatedPatientsList.Add(f);
        }
        
        return;
    }
    
    void AdjustListForNumberDailyClients()
    {
        for (int i = dailyTreatedPatientsList.Count; i > PatientData.Instance.currentDayPatientsList.Count; i--)
        {
            dailyTreatedPatientsList.RemoveAt(i - 1);
        }
    }
    

    void StoreClientTreatmentData()
    {
        // *TAG* - better to do this in a for loop or okay like this?
        foreach (FinishedPatient f in dailyTreatedPatientsList)
        {
            if (!f._patientDataStored)
            {
                f.StoreCurrentProgressForThisPatient(correctAilment, correctRecipe, correctHerbs);

                Debug.Log("Client has been treated. Data stored is...");
                Debug.Log("Client name: " + f._treatedPatientName);
                Debug.Log("Correct ailment: " + f._correctAilment + " correct recipe: " + f._correctRecipe + " correct treatment combination: " + f._correctHerbs);
                
                //f._patientCheckupLetter = 
                
                
				totalClientResults++;
                f.FinishTreatingPatient();

                GameManager.Instance.AddFinishedPatientToData(f);
                break;
            }
        }
    }

	void OnNextResultButtonPushed()
	{
		if (currentClientResultNumber == dailyTreatedPatientsList.Count)
		{
			if (DayManager.Instance.currentDayNumber == DayManager.Instance.allGameDays.Count - 1)
			{
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
}
