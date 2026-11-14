using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.EventSystems;

public class QuestLog : MonoBehaviour
{
    public CanvasGroup questLog;

    // public Button toggleQuestLog;
    
    public TMP_Text diagnoseAilment;
    public TMP_Text createRecipe;
    public TMP_Text submitHerbs;
    
    private TMP_Text currentObjective;

    public Color finishedQuestItem;
    public Color currentQuestItem;
    public Color futureQuestItem;
    
    // public GameObject questLogExpandedDisplay;
    
    
    /*void Start()
    {
        questLogExpandedDisplay.SetActive(false);
    }*/

    // Called once on desk?
    public void ResetQuestLog()
    {
        currentObjective = diagnoseAilment;
        SetColour(currentQuestItem, diagnoseAilment);
        SetColour(futureQuestItem, createRecipe);
        SetColour(futureQuestItem, submitHerbs);

        diagnoseAilment.fontStyle = FontStyles.Normal;
        createRecipe.fontStyle = FontStyles.Normal;
        submitHerbs.fontStyle = FontStyles.Normal;
        
        // StartLevelQuestLog();
    }


    void SetColour(Color colour, TMP_Text text)
    {
        text.color = colour;
    }

    public void UpdateQuestLog()
    {
        if (GameManager.Instance.ailmentSubmitted)
        {
            if (GameManager.Instance.diagnosisSubmitted)
            {
                MakeCurrentObjective(submitHerbs);
            }
            else
            {
                MakeCurrentObjective(createRecipe);
            }
        }
        else
        {
            ResetQuestLog();
        }
    }

    public void FinishQuestLog()
    {
        currentObjective.fontStyle = FontStyles.Strikethrough;
        SetColour(finishedQuestItem, currentObjective);
    }

    void MakeCurrentObjective(TMP_Text nextObjective)
    {
        currentObjective.fontStyle = FontStyles.Strikethrough;
        SetColour(finishedQuestItem, currentObjective);

        SetColour(currentQuestItem, nextObjective);
        currentObjective = nextObjective;
    }
    
}
