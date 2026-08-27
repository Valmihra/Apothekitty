using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public class Day
    {
        public int _dayNumber;

        private int _numberOfClientsInDay;
        // public List<ClientLetter.ClientData> _dailyClientsList;

        public void AssignDayNumber(int dayNum)
        {
            _dayNumber = dayNum;
        }
    }
    
    // Single Day read by GameManager. Determines contents of the level.
    public Day currentDay;
    public int currentDayNumber;
    // List of all Day objects expected to run in the game.
    // private List<Day> allGameDays;
    public List<Day> allGameDays {get; private set;}
    private int totalGameDays = 3;      // private int totalGameDays = 14;
    
    
    //public List<ClientLetter.ClientData> day01ClientsList;
    //public List<ClientLetter.ClientData> day02ClientsList;

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
        // FillAllGameDays();
        AssignDailyClients();
    }*/


    public void InitialiseDayManager()
    {
        Debug.Log("Initialising day manager...");
        FillAllGameDays();
        // AssignDailyClients();
    }

    /*void AssignDailyClients()
    {
        Debug.Log("Days set up. Filling out daily client lists...");
        List<ClientLetter.ClientData> temporaryClientList = new List<ClientLetter.ClientData>(ClientLetter.Instance.allClientsList);
        // List<ClientLetter.ClientData> temporaryClientList = ClientLetter.Instance.allClientsList.ToList();

        foreach (Day d in allGameDays)
        {
            foreach (ClientLetter.ClientData c in ClientLetter.Instance.allClientsList)        //foreach (ClientLetter.ClientData c in temporaryClientList)
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

        
        --*Debug.Log("Clients found for day number 1 are...");         // + debugNumber + " are...");
        foreach (ClientLetter.ClientData c in day01ClientsList)// allGameDays[debugNumber]._dailyClientsList)
        {
            Debug.Log(c._clientName);
        }
        // ClientLetter.Instance.SetCurrentDayClientsList();
    }*/


    void FillAllGameDays()
    {
        allGameDays = new List<Day>(totalGameDays);

        // TUTORIAL DAY - JIMOTHY
        Day day00 = new Day();
        day00.AssignDayNumber(0);
            allGameDays.Add(day00);
        
        // TEMP_NPC01, TEMP_NPC02
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

        //day01ClientsList = new List<ClientLetter.ClientData>();
        //day02ClientsList = new List<ClientLetter.ClientData>();
    }

    void SetActiveGameDay()
    {
        currentDay = allGameDays[currentDayNumber];
        Debug.Log("Current day is " + currentDay._dayNumber);
    }

    public void GoNextGameDay()
    {
        currentDayNumber++;
        SetActiveGameDay();
    }

    public void ResetGameDays()
    {
        // currentDayNumber = 1;
        currentDayNumber = 0;
        SetActiveGameDay();
    }

    public void DebugJumpToDayNumber(int dayNum)
    {
        currentDayNumber = dayNum;
        // SetActiveGameDay();
    }
}
