using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public class Day
    {
        public int _dayNumber;
        // public List<ClientLetter.ClientData> _dailyClientsList;

        public void AssignDayNumber(int dayNum)
        {
            _dayNumber = dayNum;
        }
        
        //public void GiveListContents()
        //{
        //    Debug.Log("Client list for day " + _dayNumber + " is " + _dailyClientsList.Count + " entries long.");
        //}
    }

    public Day currentDay;
    private List<Day> allGameDays;

    // private int totalGameDays = 14;
    private int totalGameDays = 2;
    public int currentDayNumber;
    
    public List<ClientLetter.ClientData> day01ClientsList;
    public List<ClientLetter.ClientData> day02ClientsList;

    private static DayManager _instance;
    public static DayManager Instance
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

    /*void Start()
    {
        InitialiseLists();
        AssignDailyClients();
    }*/


    public void InitialiseDayManager()
    {
        InitialiseLists();
        AssignDailyClients();
    }

    void AssignDailyClients()
    {
        List<ClientLetter.ClientData> temporaryClientList = new List<ClientLetter.ClientData>(ClientLetter.Instance.clientsList);
        // List<ClientLetter.ClientData> temporaryClientList = ClientLetter.Instance.clientsList.ToList();

        foreach (Day d in allGameDays)
        {
            foreach (ClientLetter.ClientData c in ClientLetter.Instance.clientsList)        //foreach (ClientLetter.ClientData c in temporaryClientList)
            {
                if (c._clientDayNumber == d._dayNumber)
                {
                    ClientLetter.ClientData tempClient = c;

                    // Debug.Log(tempClient._clientName);
                    //d._dailyClientsList.Add(tempClient);
                        // temporaryClientList.Remove(c);
                        //Debug.Log(c._clientName);

                        if (c._clientDayNumber == 1)
                        {
                            day01ClientsList.Add(tempClient);
                        }
                        else
                        {
                            day02ClientsList.Add(tempClient);
                        }
                }
                else
                {
                    continue;
                }
            }

            //d.GiveListContents();
        }

        
        /*Debug.Log("Clients found for day number 1 are...");         // + debugNumber + " are...");
        foreach (ClientLetter.ClientData c in day01ClientsList)// allGameDays[debugNumber]._dailyClientsList)
        {
            Debug.Log(c._clientName);
        }*/
        // ClientLetter.Instance.UpdateCurrentDayClientsList();
    }


    void InitialiseLists()
    {
        // allGameDays = new List<Day>(totalGameDays);
        allGameDays = new List<Day>(totalGameDays);

        // TUTORIAL DAY
        Day day01 = new Day();
        day01.AssignDayNumber(1);
            allGameDays.Add(day01);

        // BARRY, ARABELLA, LAWRENCE
        Day day02 = new Day();
        day02.AssignDayNumber(2);
            allGameDays.Add(day02);

        if (allGameDays.Count == totalGameDays)
        {
            Debug.Log("All game days set up and accounted for.");
        }

        day01ClientsList = new List<ClientLetter.ClientData>();
        day02ClientsList = new List<ClientLetter.ClientData>();
    }

    void SetDay()
    {
        currentDay = allGameDays[currentDayNumber - 1];
        Debug.Log("Current day is " + currentDay._dayNumber);
    }

    public void GoNextDay()
    {
        currentDayNumber++;
        SetDay();
    }

    public void ResetGameDays()
    {
        currentDayNumber = 1;
        SetDay();
    }

    public void DebugJumpDay(int dayNum)
    {
        currentDayNumber = dayNum;
    }
}
