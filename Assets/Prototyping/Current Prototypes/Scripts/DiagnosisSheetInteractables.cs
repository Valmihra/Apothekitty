using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiagnosisSheetInteractables : MonoBehaviour
{
    public TMP_Dropdown primaryEffect;
    public TMP_Dropdown primaryTarget;
    public TMP_Dropdown secondaryEffect;
    public TMP_Dropdown secondaryTarget;

    public Toggle enhancer;
    public Toggle invertor;

    public Button submitDiagnosisButton;

    private List<TMP_Dropdown> dropdownsList;
    private List<Toggle> togglesList;

    public TMP_Text proposedRecipe;
        string heldName01;// = " ";
        string heldName02;// = " ";
        string heldName03;// = " ";
        string heldName04;// = " ";

        string slot01 = "x";
        string slot02 = "x";
        string slot03 = "x";
        string slot04 = "x";
    
    //private GrimoireNavigation grimoireNavigation;
    
    public TMP_Text clientName;
    public TMP_Text clientSpecies;
    public TMP_Text clientExtras;
    public TMP_Text clientAilment;

    // Start is called before the first frame update
    void Start()
    {
        //grimoireNavigation = UIManager.Instance.grimoireNavScript;
            //FindObjectOfType<GrimoireNavigation>();

        GenerateLists();

            primaryEffect.onValueChanged.AddListener(delegate {DropdownValueUpdate(primaryEffect); });
            primaryTarget.onValueChanged.AddListener(delegate {DropdownValueUpdate(primaryTarget); });
            secondaryEffect.onValueChanged.AddListener(delegate {DropdownValueUpdate(secondaryEffect); });
            secondaryTarget.onValueChanged.AddListener(delegate {DropdownValueUpdate(secondaryTarget); });

            enhancer.onValueChanged.AddListener(delegate {ChangeToggleActivity(enhancer); });
            invertor.onValueChanged.AddListener(delegate {ChangeToggleActivity(invertor); });

            submitDiagnosisButton.onClick.AddListener(delegate {SubmissionButtonPressed(); });

        //FillSheet();
    }

    public void FillSheet()
    {
        clientName.text = ClientLetter.ClientsGlobal.clientName.text;
        clientSpecies.text = ClientLetter.ClientsGlobal.clientSpecies.text;
        clientExtras.text = ClientLetter.ClientsGlobal.clientExtras.text;
        //clientAilment.text = g.selectedAilment;
        clientAilment.text = UIManager.Instance.selectedAilment;
        
    }

    void DropdownValueUpdate(TMP_Dropdown selected)
    {
        int dropdownValue = selected.value;
        string dropdownName = selected.options[dropdownValue].text;

        int listNumber = 0;
        int companionNumber = 0;

        //Debug.Log("dropdownName is currently " + dropdownName + ".");
        //Debug.Log("recipe is currently " + proposedRecipe.text + ".");

        if (selected == dropdownsList[0])
        {
            listNumber = 0;
            companionNumber = 2; 
        }
        else if (selected == dropdownsList[1])
        {
            listNumber = 1;
            companionNumber = 3;
        }
        else if (selected == dropdownsList[2])
        {
            listNumber = 2;
            companionNumber = 0;
        }
        else if (selected == dropdownsList[3])
        {
            listNumber = 3;
            companionNumber = 1;
        }
        else
        {
            Debug.Log("Error when trying to read dropdown values.");
        }
        
        if (dropdownValue > 0)
        {
            // "Disables" duplicate targets and effects
            if (dropdownsList[companionNumber].value == dropdownValue)
            {
                selected.value = 0;
                return;
            }
        }
        else
        {
            // If going back to primary option, recipe reflects the change.
            dropdownName = "x";
        }

        UpdateRecipeDisplay(dropdownsList[listNumber], dropdownName);
    }


    void ToggleValueUpdate(Toggle selected)
    {
        string invertedEffectName = "x";

        if (selected == invertor)
        {
            int dropdownValue = primaryEffect.value;

            if (invertor.isOn)
            {
                if (dropdownValue == 1)
                {
                    invertedEffectName = "Weaken";
                    UpdateRecipeDisplay(dropdownsList[0], invertedEffectName);
                }
                else if (dropdownValue == 2)
                {
                    invertedEffectName = "Damage";
                    UpdateRecipeDisplay(dropdownsList[0], invertedEffectName);
                }
                else if (dropdownValue == 3)
                {
                    invertedEffectName = "Frenzy";
                    UpdateRecipeDisplay(dropdownsList[0], invertedEffectName);
                }
                else
                {
                    Debug.Log("Ran into an issue when trying to invert effect names.");
                }
            }
            else
            {
                DropdownValueUpdate(primaryEffect);
            }
        }
        else if (selected == enhancer)
        {
            DropdownValueUpdate(primaryEffect);
        }
    }

    void ChangeToggleActivity(Toggle toggle)
    {
        if (toggle.isOn)
        {
            for (int i = 0; i < togglesList.Count; i++)
            {
                if (togglesList[i] != toggle)
                {
                    togglesList[i].enabled = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < togglesList.Count; i++)
            {
                if (togglesList[i] != toggle)
                {
                    togglesList[i].enabled = true;
                }
            }
        }
        ToggleValueUpdate(toggle);
    }

    void UpdateRecipeDisplay(TMP_Dropdown dropdown, string name)
    {
        if (dropdown == primaryEffect)
        {
            heldName01 = name;
            UpdateSlotOne(name);
        }
        else if (dropdown == primaryTarget)
        {
            heldName02 = name;
            UpdateSlotTwo(name);
        }
        else if (dropdown == secondaryEffect)
        {
            heldName03 = name;
            UpdateSlotThree(name);
        }
        else if (dropdown == secondaryTarget)
        {
            heldName04 = name;
            UpdateSlotFour(name);
        }
        else
        {
            Debug.Log("Issue while attempting to update recipe display.");
        }
        
        //var name = (dropdown == primaryEffect) ? UpdateSlotOne(name) : (dropdown == primaryTarget) ? UpdateSlotTwo(name) : (dropdown == secondaryEffect) ? UpdateSlotThree(name) : UpdateSlotFour (name);
    }

    void UpdateSlotOne(string name)
    {
        slot01 = name;
        //Debug.Log("call to slot one.");
        GenerateRecipe();
    }

    void UpdateSlotTwo(string name)
    {
        slot02 = name;
        //Debug.Log("call to slot two.");
        GenerateRecipe();
    }

    void UpdateSlotThree(string name)
    {
        slot03 = name;
        //Debug.Log("call to slot three.");
        GenerateRecipe();
    }

    void UpdateSlotFour(string name)
    {
        slot04 = name;
        //Debug.Log("call to slot four.");
        GenerateRecipe();
    }

    void GenerateRecipe()
    {
        if (enhancer.isOn)
        {
            proposedRecipe.text = (("Enhanced ") + (slot01) + (" ") + (slot02) + (" ") + (slot03) + (" ") + (slot04)).ToString();
        }
        else
        {
            proposedRecipe.text = ((slot01) + (" ") + (slot02) + (" ") + (slot03) + (" ") + (slot04)).ToString();
        }
        Debug.Log("Recipe updated.");
    }

    void GenerateLists()
    {
        dropdownsList = new List<TMP_Dropdown>();
        dropdownsList.Add(primaryEffect);
        dropdownsList.Add(primaryTarget);
        dropdownsList.Add(secondaryEffect);
        dropdownsList.Add(secondaryTarget);

        togglesList = new List<Toggle>();
        togglesList.Add(enhancer);
        togglesList.Add(invertor);
    }

    void SubmissionButtonPressed()
    {
        Debug.Log("Button Pressed!");
        
        bool validPrimaryRecipeCombination = (primaryEffect.value <= 0) && (primaryTarget.value <= 0) ? false : true;
        bool validSecondaryRecipeCombination = (secondaryEffect.value <= 0) && (secondaryTarget.value <= 0) ? true : (secondaryEffect.value > 0) && (secondaryTarget.value > 0) ? true : false;
        
        if(validPrimaryRecipeCombination)
        {
            if (validSecondaryRecipeCombination)
            {
                UIManager.Instance.SubmitDiagnosis();

                // Prevents interaction with the canvas elements
                primaryEffect.interactable = false;
                primaryTarget.interactable = false;
                secondaryEffect.interactable = false;
                secondaryTarget.interactable = false;

                enhancer.interactable = false;
                invertor.interactable = false;
            }
            else
            {
                Debug.Log("Would bring up invalid recipe text, try again popup.");
            }
        }
        else
        {
            Debug.Log("Would bring up invalid recipe text, try again popup.");
        }

        //        
        
    }
}
