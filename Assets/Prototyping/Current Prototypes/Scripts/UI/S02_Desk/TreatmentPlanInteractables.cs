using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreatmentPlanInteractables : MonoBehaviour
{
    [Header("Property Dropdowns")]
    public TMP_Dropdown primaryEffectDropdown;
    public TMP_Dropdown primaryTargetDropdown;
    public TMP_Dropdown secondaryEffectDropdown;
    public TMP_Dropdown secondaryTargetDropdown;
    private List<TMP_Dropdown> diagnosisSheetPropertyDropdownsList;
        
    [Header("Modifier Toggles")]
    public Toggle enhancerToggle;
    public Toggle inverterToggle;
    public CanvasGroup diagnosisSheetToggleBoxes;
    private List<Toggle> diagnosisSheetTogglesList;
        
    [Header("Displayed Text")]  // Text displayed at the top of the diagnosis sheet
    public TMP_Text displayedPatientName;
    public TMP_Text displayedPatientSpecies;
    public TMP_Text displayedPatientExtras;
    public TMP_Text displayedClientAilment;
    public TMP_Text proposedRecipeDisplayText;
        private string slot01;
        private string slot02;
        private string slot03;
        private string slot04;
        
        private string emptySlotText = "-";
        private string defaultTargetDropdownText = "Please select a target";
        private string defaultEffectDropdownText = "Please select an effect";

        private bool updatedForMVP;
        private bool usingSimpleConfiguration;
        // private bool usingIntermediateConfiguration;
        // private bool usingComplicatedConfiguration;
            
    [Header("Treatment Submission Button")]
    public Button submitDiagnosisButton;
    
    // Vector used to reset draggable objects
    private Vector2 diagnosisSheetStartingPosition;


    public void InitialiseDiagnosisSheet()
    {
        diagnosisSheetStartingPosition = transform.position;
        GenerateDiagnosisSheetInteractablesLists();

            primaryEffectDropdown.onValueChanged.AddListener(delegate {DiagnosisSheetPropertyValueUpdate(primaryEffectDropdown); });
            primaryTargetDropdown.onValueChanged.AddListener(delegate {DiagnosisSheetPropertyValueUpdate(primaryTargetDropdown); });
            secondaryEffectDropdown.onValueChanged.AddListener(delegate {DiagnosisSheetPropertyValueUpdate(secondaryEffectDropdown); });
            secondaryTargetDropdown.onValueChanged.AddListener(delegate {DiagnosisSheetPropertyValueUpdate(secondaryTargetDropdown); });

            enhancerToggle.onValueChanged.AddListener(delegate {OnModifierTogglePressed(enhancerToggle); });
            inverterToggle.onValueChanged.AddListener(delegate {OnModifierTogglePressed(inverterToggle); });

            submitDiagnosisButton.onClick.AddListener(delegate {OnSubmissionButtonPressed(); });
            
        slot01 = emptySlotText;
        slot02 = emptySlotText;
        slot03 = emptySlotText;
        slot04 = emptySlotText;

        updatedForMVP = false;
        
        usingSimpleConfiguration = false;
        // usingIntermediateConfiguration = false;
        // usingComplicatedConfiguration = false;
    }
    
    // Resets the position of the UI in the scene, and then checks which elements to display and resets all values.
    public void ResetDiagnosisSheet()
    {
        transform.position = diagnosisSheetStartingPosition;
        
        // Removes spirit from the target dropdowns if running current MVP version
        if (GameManager.Instance.isMVP)
        {
            if (!updatedForMVP)
            {
                primaryTargetDropdown.options.RemoveAt(3);
                primaryTargetDropdown.RefreshShownValue();
                secondaryTargetDropdown.options.RemoveAt(3);
                secondaryTargetDropdown.RefreshShownValue();
                
                updatedForMVP = true;
            }
        }

        // checks to see which configuration to display
        if (usingSimpleConfiguration)
        {
            // Debug.Log("Setting up the simplified diagnosis sheet.");
            secondaryEffectDropdown.gameObject.SetActive(false);
            secondaryTargetDropdown.gameObject.SetActive(false);
            diagnosisSheetToggleBoxes.gameObject.SetActive(false);
        }
        else
        {
            // Debug.Log("Setting up the full diagnosis sheet.");
            // would/could also include check for intermediateConfig too
            secondaryEffectDropdown.gameObject.SetActive(true);
            secondaryTargetDropdown.gameObject.SetActive(true);
            diagnosisSheetToggleBoxes.gameObject.SetActive(true);
        }

        primaryEffectDropdown.options[0].text = defaultEffectDropdownText;
        secondaryEffectDropdown.options[0].text = defaultEffectDropdownText;

        primaryTargetDropdown.options[0].text = defaultTargetDropdownText;
        secondaryTargetDropdown.options[0].text = defaultTargetDropdownText;

        // allows interaction with the canvas elements          -- check to see if safe to use this here or if need to disable interaction for hidden objs separately!
        foreach (TMP_Dropdown dropdown in diagnosisSheetPropertyDropdownsList)
        {
            dropdown.value = 0;
            dropdown.interactable = true;
            dropdown.RefreshShownValue();
        }
        foreach (Toggle toggle in diagnosisSheetTogglesList)
        {
            toggle.isOn = false;
            toggle.interactable = true;
        }
        
        // allows the player to try to submit their combination
        submitDiagnosisButton.interactable = true;
        UpdateProposedRecipeDisplay();
    }

    public void SetDiagnosisSheetConfiguration()
    {
        if ((DayManager.Instance.currentDayNumber == 0) || (DayManager.Instance.currentDayNumber == 1))
        {
            usingSimpleConfiguration = true;
        }
        else if (DayManager.Instance.currentDayNumber >= 2)
        {
            usingSimpleConfiguration = false;
        }
    }

    // Uses data from player's previous interactions to fill the diagnosis sheet accurately
    public void FillDiagnosisSheet()
    {
        /*displayedPatientName.text = PatientData.Instance.displayedPatientName.text;
        displayedPatientSpecies.text = PatientData.Instance.displayedPatientSpecies.text;
        displayedPatientExtras.text = PatientData.Instance.displayedPatientExtras.text;
        displayedClientAilment.text = SceneManager.Instance.selectedAilment;*/
        
        displayedPatientName.text = PatientData.Instance.activePatientData._patientName;
        displayedPatientSpecies.text = PatientData.Instance.activePatientData._patientSpecies;
        displayedPatientExtras.text = PatientData.Instance.activePatientData._patientExtras;
        displayedClientAilment.text = SceneManager.Instance.selectedAilment;
    }

    void DiagnosisSheetPropertyValueUpdate(TMP_Dropdown chosenDropdown)
    {
        int propertyDropdownValue = chosenDropdown.value;
        string propertyDropdownName = chosenDropdown.options[propertyDropdownValue].text;

        // Debug.Log("dropdown value stored as: " + propertyDropdownValue);
        // Debug.Log("dropdown name stored as: " + propertyDropdownName);

        int numberInDropdownsList = 0;
        int companionValue = 0;

        if (chosenDropdown == diagnosisSheetPropertyDropdownsList[0])
        {
            numberInDropdownsList = 0;
            companionValue = 2; 
        }
        else if (chosenDropdown == diagnosisSheetPropertyDropdownsList[1])
        {
            numberInDropdownsList = 1;
            companionValue = 3;
        }
        else if (chosenDropdown == diagnosisSheetPropertyDropdownsList[2])
        {
            numberInDropdownsList = 2;
            companionValue = 0;
        }
        else if (chosenDropdown == diagnosisSheetPropertyDropdownsList[3])
        {
            numberInDropdownsList = 3;
            companionValue = 1;
        }
        else
        {
            Debug.Log("Error when trying to read dropdown values.");
        }
        
        if (propertyDropdownValue > 0)  
        {
            if (diagnosisSheetPropertyDropdownsList[companionValue].value == propertyDropdownValue)
            {
                // *TAG* - maybe flash this and the companion dropdown to indicate that it's a double up?   (FOR SAUCE)
                
                // (for now)
                chosenDropdown.value = 0;
                MenuManager.Instance.DiagnosisSheetCategoryDoubleUpPopup();
                
                return;
            }
        }
        else
        {
            propertyDropdownName = emptySlotText;
        }

        UpdateRecipeDisplay(diagnosisSheetPropertyDropdownsList[numberInDropdownsList], propertyDropdownName);
    }


    void DiagnosisSheetToggleValueUpdate(Toggle chosenToggle)     // Can remove inverter elements for now
    {
        Debug.Log("Adjusting recipe display for toggles...");
        if (chosenToggle == inverterToggle)
        {
            if (inverterToggle.isOn)
            {
                string invertedEffectName = emptySlotText;  // int propertyDropdownValue = primaryEffectDropdown.value;
                
                if (primaryEffectDropdown.value == 1) // (propertyDropdownValue == 1)
                {
                    invertedEffectName = "Weaken";
                    UpdateRecipeDisplay(diagnosisSheetPropertyDropdownsList[0], invertedEffectName);
                }
                else if (primaryEffectDropdown.value == 2) // (propertyDropdownValue == 2)
                {
                    invertedEffectName = "Damage";
                    UpdateRecipeDisplay(diagnosisSheetPropertyDropdownsList[0], invertedEffectName);
                }
                else if (primaryEffectDropdown.value == 3) // (propertyDropdownValue == 3)
                {
                    invertedEffectName = "Frenzy";
                    UpdateRecipeDisplay(diagnosisSheetPropertyDropdownsList[0], invertedEffectName);
                }
                else
                {
                    Debug.Log("Ran into an issue when trying to invert effect names.");
                }
            }
            else
            {
                DiagnosisSheetPropertyValueUpdate(primaryEffectDropdown);
            }
        }
        else if (chosenToggle == enhancerToggle)
        {
            DiagnosisSheetPropertyValueUpdate(primaryEffectDropdown);
        }
    }

    void OnModifierTogglePressed(Toggle chosenToggle)
    {
        if (chosenToggle.isOn)
        {
            for (int i = 0; i < diagnosisSheetTogglesList.Count; i++)
            {
                if (diagnosisSheetTogglesList[i] != chosenToggle)
                {
                    diagnosisSheetTogglesList[i].enabled = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < diagnosisSheetTogglesList.Count; i++)
            {
                if (diagnosisSheetTogglesList[i] != chosenToggle)
                {
                    diagnosisSheetTogglesList[i].enabled = true;
                }
            }
        }
        DiagnosisSheetToggleValueUpdate(chosenToggle);
    }

    void UpdateRecipeDisplay(TMP_Dropdown dropdown, string name)
    {
        // Debug.Log("Updating recipe display...");
        if (dropdown == primaryEffectDropdown)
        {
            slot01 = name;
            UpdateProposedRecipeDisplay();
        }
        else if (dropdown == primaryTargetDropdown)
        {
            slot02 = name;
            UpdateProposedRecipeDisplay();
        }
        else if (dropdown == secondaryEffectDropdown)
        {
            slot03 = name;
            UpdateProposedRecipeDisplay();
        }
        else if (dropdown == secondaryTargetDropdown)
        {
            slot04 = name;
            UpdateProposedRecipeDisplay();
        }
        else
        {
            Debug.Log("Issue while attempting to update recipe display.");
        }
        
        //var name = (dropdown == primaryEffectDropdown) ? UpdateSlotOne(name) : (dropdown == primaryTargetDropdown) ? UpdateSlotTwo(name) : (dropdown == secondaryEffectDropdown) ? UpdateSlotThree(name) : UpdateSlotFour (name);
    }
    
    void UpdateProposedRecipeDisplay()
    {
        // Debug.Log("Updating proposed recipe display...");
        if (enhancerToggle.isOn)
        {
            if (GameManager.Instance.isMVP)
            {
                proposedRecipeDisplayText.text = (("Enhanced ") + (slot01) + (" ") + (slot02)).ToString();
            }
            else
            {
                proposedRecipeDisplayText.text = (("Enhanced ") + (slot01) + (" ") + (slot02) + (" ") + (slot03) + (" ") + (slot04)).ToString();
            }
        }
        else
        {
            if (GameManager.Instance.isMVP)
            {
                proposedRecipeDisplayText.text = ((slot01) + (" ") + (slot02)).ToString();
            }
            else
            {
                proposedRecipeDisplayText.text = ((slot01) + (" ") + (slot02) + (" ") + (slot03) + (" ") + (slot04)).ToString();
            }
            
        }
        // Debug.Log(proposedRecipeDisplayText.text);
        // Debug.Log("Recipe updated.");
    }

    void GenerateDiagnosisSheetInteractablesLists()
    {
        diagnosisSheetPropertyDropdownsList = new List<TMP_Dropdown>();
        diagnosisSheetPropertyDropdownsList.Add(primaryEffectDropdown);
        diagnosisSheetPropertyDropdownsList.Add(primaryTargetDropdown);
        diagnosisSheetPropertyDropdownsList.Add(secondaryEffectDropdown);
        diagnosisSheetPropertyDropdownsList.Add(secondaryTargetDropdown);

        diagnosisSheetTogglesList = new List<Toggle>();
        diagnosisSheetTogglesList.Add(enhancerToggle);
        diagnosisSheetTogglesList.Add(inverterToggle);
    }

    void OnSubmissionButtonPressed()
    {
        // Debug.Log("Button Pressed!");
        // shouldn't have to update these bools, since hidden dropdown values should always be 0.
        bool validPrimaryRecipeCombination = (primaryEffectDropdown.value <= 0) || (primaryTargetDropdown.value <= 0) ? false : true;
        bool validSecondaryRecipeCombination = (secondaryEffectDropdown.value <= 0) && (secondaryTargetDropdown.value <= 0) ? true : (secondaryEffectDropdown.value > 0) && (secondaryTargetDropdown.value > 0) ? true : false;
        
        if(validPrimaryRecipeCombination)
        {
            if (validSecondaryRecipeCombination)
            {
                submitDiagnosisButton.interactable = false;
                SceneManager.Instance.SubmitDiagnosis();

                // Prevents interaction with the canvas elements
                foreach (TMP_Dropdown dropdown in diagnosisSheetPropertyDropdownsList)
                {
                    dropdown.interactable = false;
                }
                foreach (Toggle toggle in diagnosisSheetTogglesList)
                {
                    toggle.interactable = false;
                }
            }
            else
            {
                MenuManager.Instance.DiagnosisSheetInvalidCombinationPopup();
            }
        }
        else
        {
            MenuManager.Instance.DiagnosisSheetInvalidCombinationPopup();
        }
    }
}