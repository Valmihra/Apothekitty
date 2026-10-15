using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogSlider : MonoBehaviour
{
    [Header("Slider Values")]
    public float defaultSliderValue;
    public float openedSliderValue;
    private float previousSliderValue;
    // public float sliderMoveSpeed;
    
    [SerializeField] private Slider questLogSlider;
    private QuestLog questLogReference;
    //private bool closed;
    //private bool moving;
    
    
    // Start is called before the first frame update
    public void InitialiseSlider()
    {
        // questLogSlider.onValueChanged.AddListener(delegate { AdjustSlider(); });
        // StopAllCoroutines();
        defaultSliderValue = 0.12f;
        openedSliderValue = 0.986f;
        
        ResetSliderValue();
        // questLogReference.questLogExpandedDisplay.SetActive(false);
        
        questLogSlider.onValueChanged.AddListener(delegate { AdjustSlider(); });
    }
    
    public void SetLog(QuestLog questLog)
    {
        questLogReference = questLog;
    }

    public void ResetSliderValue()
    {
        questLogSlider.value = defaultSliderValue;
        previousSliderValue = defaultSliderValue;
        //closed = true;
        //moving = false;
    }
    
    void AdjustSlider()
    {
        /*if (!moving)
        {
            float halfwayValue = openedSliderValue / defaultSliderValue;
            
            if (closed)
            {
                if (questLogSlider.value < defaultSliderValue)
                {
                    questLogSlider.value = previousSliderValue;
                    return;
                }
                else
                {
                    closed = false;
                }
            }
            else
            {
                if (previousSliderValue > questLogSlider.value)
                {
                    questLogSlider.value = previousSliderValue;
                    return;
                }
                
                if (questLogSlider.value > openedSliderValue)
                {
                    LerpToValue(defaultSliderValue);
                    return;
                }

                if (questLogSlider.value > halfwayValue)
                {
                    LerpToValue(openedSliderValue);
                    return;
                }
                /*else
                {

                }/
            }
        
            previousSliderValue = questLogSlider.value;
        
            /*if (questLogSlider.value > openedSliderValue)
            {
                LerpToValue(defaultSliderValue);
            }*/
        /*if (!moving)
        {
            if (questLogSlider.value > openedSliderValue)
            {
                moving = true;
                LerpToValue(defaultSliderValue);
                previousSliderValue = openedSliderValue;
                return;
            }

            if (questLogSlider.value > defaultSliderValue) //previousSliderValue
            {
                moving = true;
                LerpToValue(openedSliderValue);
                previousSliderValue = defaultSliderValue;
                return;
            }
        }
        else
        {
            questLogSlider.value = defaultSliderValue;
            return;
        }*/

        if (questLogSlider.value >= openedSliderValue)
        {
            questLogReference.questLogExpandedDisplay.SetActive(true);
        }
        else
        {
            if (questLogReference.questLogExpandedDisplay.activeInHierarchy) //(true))
            {
                questLogReference.questLogExpandedDisplay.SetActive(false);
            }
        }

        /*
        if (previousSliderValue == openedSliderValue)
        {
            // return to original state
            LerpToValue(defaultSliderValue);
            previousSliderValue = defaultSliderValue;
        }
        else
        {
            // lerp to opened state
            LerpToValue(openedSliderValue);
            previousSliderValue = openedSliderValue;
        }*/
        //

        // if 
    }
    
    /*void LerpToValue(float value)
    {
        // Debug.Log("LerpToValue");
        questLogSlider.value = value;
        
        //moving = true;
        
        
        //StopAllCoroutines();
        //StartCoroutine(LerpTo(value));
        
    }*/
    
    /*IEnumerator LerpTo(float value)
    {
        Debug.Log("LerpTo: " + value);
        float elapsedTime = 0.0f;
        float timeToMove = value == 0.12f ? 0.5f : 0.1f;
        float targetValue = value == 0.12f ? defaultSliderValue : openedSliderValue;
        
        float currentValue = questLogSlider.value;
        
        while (elapsedTime < timeToMove)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/timeToMove);
            
            questLogSlider.value = Mathf.Lerp(currentValue, targetValue, t);
            
            yield return null;
        }

        questLogSlider.value = targetValue;
        if (targetValue == openedSliderValue)
        {
            questLogReference.questLogExpandedDisplay.SetActive(true);
        }
        
        moving = false;
    }*/
}
