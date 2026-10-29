using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PatientLetter : MonoBehaviour
{
    // Variables used to display the active patient's data on the screen
    public TMP_Text displayedPatientName;
    public TMP_Text displayedPatientSpecies;
    public TMP_Text displayedPatientLetterText;

    /*private Image sizeSelectionSmall;
    private Image sizeSelectionMedium;
    private Image sizeSelectionLarge;
    
    private Image dietSelectionCarnivore;
    private Image dietSelectionOmnivore;
    private Image dietSelectionHerbivore;*/

    // Reference to the parent objects for the size and diet icons on the patient form
    [SerializeField] private GameObject sizeSelectionIcons;
    [SerializeField] private GameObject dietSelectionIcons;
    
    private Vector2 patientLetterStartingPosition;

    
    public void GetPatientLetterPosition()
    {
        patientLetterStartingPosition = gameObject.transform.position;
    }
    
    public void ResetPatientLetterPosition()
    {
		// *TAG* - Not resetting the whole letter here. Updated on Spawn patient?
        transform.position = patientLetterStartingPosition;
    }

    void HideSelectionIcons()
    {
        // Disables the selection icons on the patient form
        for (int i = 1; i < 3; i++)
        {
            string nameToSearch = i == 1 ? "Small" : i == 2 ? "Medium" : i == 3 ? "Large" : "Fail";

            if (nameToSearch != "Fail")
            {
                GameObject sizeIconToHide = GameObject.Find((nameToSearch + " - Selected").ToString());
                sizeIconToHide.GetComponent<Image>().enabled = false;
                
                nameToSearch = i == 1 ? "Carnivore" : i == 2 ? "Omnivore" : "Herbivore";
                GameObject dietIconToHide = GameObject.Find((nameToSearch + " - Selected").ToString());
                dietIconToHide.GetComponent<Image>().enabled = false;
            }
			else
			{
				Debug.Log("Error when trying to reset the selection icons on the Patient Form");
			}
        }
    }
    
    public void UpdateLetterDisplay(PatientData.SinglePatientData data)
    {
        // Updates the text on the letter UI with the information from the specified SinglePatientData
        
        // NEW VERSION
        HideSelectionIcons();
        
        displayedPatientName.text = data._patientName;
        // Debug.Log("Data in UpdateLetterDisplay received. Patient name is: " + displayedPatientName.text);
        
        displayedPatientSpecies.text = data._patientSpecies;
        UpdatePatientSizeAndDiet(data._patientSize, data._patientDiet);
        displayedPatientLetterText.text = data._patientLetterText;

    }

    void UpdatePatientSizeAndDiet(string size, string diet)
    {
        // Debug.Log(size + ", " + diet);
        if (size == PatientData.Instance.sizeS || size == PatientData.Instance.sizeM || size == PatientData.Instance.sizeL)
        {
            //string nameToSearch = ("STATIC TXT - Size Options - " + size).ToString();
            Image selectionImage = GameObject.Find(size + " - Selected").GetComponent<Image>(); //parentImage = sizeSelectionIcons.transform.Find(nameToSearch);
            selectionImage.GetComponent<Image>().enabled = true;

            if (diet == PatientData.Instance.dietC || diet == PatientData.Instance.dietO || diet == PatientData.Instance.dietH)
            {
                selectionImage = GameObject.Find(diet + " - Selected").GetComponent<Image>(); //parentImage = sizeSelectionIcons.transform.Find(nameToSearch);
                selectionImage.GetComponent<Image>().enabled = true;
            }
            else
            {
                Debug.Log("Issue with naming conventions when checking for sizes at PatientLetter -> UpdatePatientSizeAndDiet");
            }
        }
        else
        {
            Debug.Log("Issue with naming conventions when checking for sizes at PatientLetter -> UpdatePatientSizeAndDiet");
        }
        // string nameToSearch = size;
        
        // == PatientData.Instance.sizeS ? Small : PatientData.Instance.sizeM ? y : PatientData.Instance.sizeL ? y : "Failed";
        // if (size == PatientData.Instance.sizeS || )
    }
}
