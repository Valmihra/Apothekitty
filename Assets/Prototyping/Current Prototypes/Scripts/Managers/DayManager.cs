using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public class Day
    {
        public int _dayNumber;
        private int _numberOfClientsInDay;

        public void AssignDayNumber(int dayNum)
        {
            _dayNumber = dayNum;
        }
    }
    
    public Day currentDay;
    public int currentDayNumber;

    public List<Day> allGameDays {get; private set;}
    private int totalGameDays = 3;      // 0, 1, 2      (= 3)

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

    public void InitialiseDayManager()
    {
        FillAllGameDays();
		currentDayNumber = 0;
		SetActiveGameDay();
    }

    void FillAllGameDays()
    {
        allGameDays = new List<Day>(totalGameDays);

        // DAY 00 - 			JIMOTHY		(TUTORIAL)
        Day day00 = new Day();
        day00.AssignDayNumber(0);
            allGameDays.Add(day00);
        
        // DAY 01 - 			TEMP_PATIENT01, TEMP_PATIENT02
        Day day01 = new Day();
        day01.AssignDayNumber(1);
            allGameDays.Add(day01);

        // DAY 02 - 			BARRY, ARABELLA, LAWRENCE
        Day day02 = new Day();
        day02.AssignDayNumber(2);
            allGameDays.Add(day02);

        /*if (allGameDays.Count == totalGameDays)
        {
            Debug.Log("All game days set up and accounted for.");
        }*/
    }

    public void JumpToDayNumber(int dayNumSpecified)
    {
        currentDayNumber = dayNumSpecified;
        SetActiveGameDay();
    }

    void SetActiveGameDay()
    {
        currentDay = allGameDays[currentDayNumber];
    }

    public void GoNextGameDay()
    {
        currentDayNumber++;
        SetActiveGameDay();
    }
}
