using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int savedDayNumber;
    public List<ResultsCalculator.FinishedPatient> seenPatientsInCurrentDay;
    public PatientData.SinglePatientData savedActivePatient;
    // public string currentPatientName;
    
    public int numberPatientsSeen;
    public int numberPatientsCured;
    
    
    }
