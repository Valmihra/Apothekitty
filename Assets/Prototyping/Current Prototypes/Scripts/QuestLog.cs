using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.EventSystems;

public class QuestLog : MonoBehaviour
{
    public CanvasGroup questLog;

    public Button toggleQuestLog;

    //public TMP_Text currentObjective;
    public TMP_Text diagnoseAilment;
    public TMP_Text createRecipe;
    public TMP_Text submitHerbs;
    
    private TMP_Text currentObjective;

    public Color finishedQuestItem;
    public Color currentQuestItem;
    public Color futureQuestItem;

    public Image displayArrow;
    public Sprite arrowUp;
    public Sprite arrowDown;

    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        toggleQuestLog.onClick.AddListener(delegate { ToggleQuestLog(); });
        //UIManager.
        //ResetQuestLog
        InitialiseQuestLog();
    }

    // Called once on desk?
    public void InitialiseQuestLog()
    {
        currentObjective = diagnoseAilment;
        SetColour(currentQuestItem, diagnoseAilment);
        SetColour(futureQuestItem, createRecipe);
        SetColour(futureQuestItem, submitHerbs);

        active = true;
        ToggleQuestLog();
    }

    void SetColour(Color colour, TMP_Text text)
    {
        text.color = colour;
    }

    void ToggleQuestLog()
    {
        if (active)
        {
            UIManager.Instance.SpriteShift(displayArrow, arrowDown);
            UIManager.Instance.DisableUI(questLog);
        }
        else
        {
            UIManager.Instance.SpriteShift(displayArrow, arrowUp);
            UpdateQuestLog();
            UIManager.Instance.EnableUI(questLog);
        }
        active = !active;
        
    }

    void UpdateQuestLog()
    {
        if (GameManager.Instance.ailmentChosen)
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
            InitialiseQuestLog();
        }
    }

    void MakeCurrentObjective(TMP_Text nextObjective)
    {
        currentObjective.fontStyle = FontStyles.Strikethrough;
        SetColour(finishedQuestItem, currentObjective);

        SetColour(currentQuestItem, nextObjective);
        currentObjective = nextObjective;
    }
}
