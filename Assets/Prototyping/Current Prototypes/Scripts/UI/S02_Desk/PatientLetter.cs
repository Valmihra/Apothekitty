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
    public TMP_Text displayedPatientExtras;
    public TMP_Text displayedPatientLetterText;
    // public Image displayedPatientIcon;
    
    private Vector2 patientLetterStartingPosition;
    
    /*private static PatientLetter _instance;

    public static PatientLetter Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }*/
    
    public void GetPatientLetterPosition()
    {
        patientLetterStartingPosition = gameObject.transform.position;//new Vector2(transform.position.x, transform.position.y);
        //Debug.Log(patientLetterStartingPosition); //gameObject.transform.position;
    }
    
    public void ResetPatientLetterPosition()
    {
        transform.position = patientLetterStartingPosition;
    }
    
    public void UpdateLetterDisplay(PatientData.SinglePatientData data)
    {
        // Updates the text on the letter UI with the information from the specified SinglePatientData
        displayedPatientName.text = data._patientName;
        displayedPatientSpecies.text = data._patientSpecies;
        displayedPatientExtras.text = data._patientExtras;
        displayedPatientLetterText.text = data._patientLetterText;

        // displayedPatientIcon = data._patientIcon;
        // Invoke(nameof(SpawnPatient), 1.0f);
    }
    
    /*void SpawnPatient()
    {
        // "spawns" the randomised patient, and updates the relevant scripts with their information.
        
        SceneManager.Instance.ShowPatient(displayedPatientIcon);
        DialogueRunner.Instance.GetDialogue("patientArrive");
    }*/
}
