using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int savedDayNumber;
    public List<ResultsCalculator.FinishedPatient> seenPatientsInCurrentDay;
    public PatientData.SinglePatientData savedActivePatient;
    
    public int numberPatientsSeen;
    public int numberPatientsCured;
    
    
    /*public void ResetGameData()
    {
        numberPatientsSeen = 0;
        numberPatientsCured = 0;
        
        public int savedDayNumber = 0;
        seenPatientsInCurrentDay = new List<ResultsCalculator.FinishedPatient>();
    }*/
    
}
