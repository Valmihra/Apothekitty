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

    /*
    [Header("Slider Values")]
    public float defaultSliderValue;
    public float openedSliderValue;
    // public float sliderMoveSpeed;
    private float previousSliderValue;
    */
    
    //private bool active;

    //[SerializeField] 
    public GameObject questLogExpandedDisplay;
    private QuestLogSlider questLogSliderReference;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // toggleQuestLog.onClick.AddListener(delegate { ToggleQuestLog(); });
        questLogSliderReference = GetComponent<QuestLogSlider>();
        questLogSliderReference.InitialiseSlider();
        questLogSliderReference.SetLog(this);
        
        questLogExpandedDisplay.SetActive(false);
        //UIManager.
        //ResetQuestLog
        //ResetQuestLog();

    }

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
        
        StartLevelQuestLog();
    }

    void StartLevelQuestLog()
    {
        //active = true;
        questLogSliderReference.ResetSliderValue();
        // ToggleQuestLog();
    }

    void SetColour(Color colour, TMP_Text text)
    {
        text.color = colour;
    }

    /*void ToggleQuestLog()
    {
        if (active)
        {
            //UIManager.Instance.SpriteShift(displayArrow, arrowDown);
            UIManager.Instance.DisableUI(questLog);
        }
        else
        {
            //UIManager.Instance.SpriteShift(displayArrow, arrowUp);
            //UpdateQuestLog();
            UIManager.Instance.EnableUI(questLog);
        }
        active = !active;
        
    }*/

    //public override void OnDrag(PointerEventData eventData)
    //{
        // questLogSlider.value 
    //}

    

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
