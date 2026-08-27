using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedClientsData : MonoBehaviour
{
    /*public class FinishedClient
    {
        public string _treatedClientName;
        // public string _treatedClientAilment;
        public bool _correctAilment;
        public bool _correctRecipe;
        public bool _correctHerbs;
        public bool _clientDataStored; // = false;

        public void SetupEmptyClientSlot()
        {
            _clientDataStored = false;
        }
        public void FinishTreatingClient()
        {
            _clientDataStored = true;
            Debug.Log("Client treatment data stored.");
            
            // resultsCalculator.
        }
        
        public void StoreCurrentProgressForThisClient()
        {
            _treatedClientName = ClientLetter.Instance.activeClientData._clientName;
            // _treatedClientAilment = SceneManager.Instance.selectedAilment;
            
            _correctAilment = resultsCalculator.correctAilment;
            _correctRecipe = resultsCalculator.correctRecipe;
            _correctHerbs = resultsCalculator.correctHerbs;
        }
    }*/
    
    //private ResultsCalculator resultsCalculator;
    
    // for moving results to end of day instead of between clients::
    /*public class TreatedClientResults
    {
        
        public string _treatedClientRecipe;
        // public Image _treatedClientIcon;

        


        void CreateTreatedClientData(string treatedClientName)
        {
            _treatedClientName = treatedClientName;
            _treatedClientAilment = treatedClientName;


            if ()
        }
    }*/
    
    // public List<FinishedClient> dailyTreatedClientsList;
    
    /*void InitialiseFinishedClientsData()
    {
        resultsCalculator = FindObjectOfType<ResultsCalculator>();
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
            
            
        AdjustListForNumberDailyClients();
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
        /if (dailyTreatedClientsList == null || dailyTreatedClientsList.Count == 0)
        {
            dailyTreatedClientsList = new List<FinishedClient>();
            // (ClientLetter.Instance.currentDayClientsList.Count);
        }/

        foreach (FinishedClient f in dailyTreatedClientsList)
        {
            if (!f._clientDataStored)
            {
                f.StoreCurrentProgressForThisClient();

                Debug.Log("Client has been treated. Data stored is...");
                Debug.Log("Client name: " + f._treatedClientName);
                Debug.Log("Correct ailment: " + f._correctAilment + " correct recipe: " + f._correctRecipe + " correct treatment combination: " + f._correctHerbs);
                
                f.FinishTreatingClient();
                break;
            }
        }
        
        
        //
    }*/
}
