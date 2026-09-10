using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimatedTextEffectNewDay : MonoBehaviour
{
    [SerializeField] private TMP_Text displayedTextComponentNewDay;
    private float slowTimeBetweenCharacters = 1.0f;
    private float fastTimeBetweenCharacters = 0.05f;
    private int allCharactersInStringNewDay;
    public bool currentlyAnimatingNewDay = false;

    private string textToDisplay;
    /*
    public void JumpEndLine()
    {
        CancelInvoke();
        currentlyAnimating = false;
        displayedTextComponentNewDay.maxVisibleCharacters = allCharactersInString;
    }*/

    // called in dialogueRunner to animate the dialogue
    public void BeginAnimatingNewDayText(string textToAnimate)
    {
        displayedTextComponentNewDay.text = textToAnimate;

        currentlyAnimatingNewDay = true;
        displayedTextComponentNewDay.ForceMeshUpdate();
        allCharactersInStringNewDay = displayedTextComponentNewDay.textInfo.characterCount;

        displayedTextComponentNewDay.maxVisibleCharacters = 0;
        AnimateNewDayText();
    }

    void AnimateNewDayText()
    {
        // Debug.Log("gets to here");
        if (displayedTextComponentNewDay.maxVisibleCharacters > 4)
        {
            Invoke(nameof(ShowNextCharacterInNewDayString), slowTimeBetweenCharacters);
        }
        else
        {
            Invoke(nameof(ShowNextCharacterInNewDayString), fastTimeBetweenCharacters);
        }

    }

    void ShowNextCharacterInNewDayString()
    {
        //  Debug.Log("Begins the invoke");
        displayedTextComponentNewDay.ForceMeshUpdate();
        if (displayedTextComponentNewDay.maxVisibleCharacters != allCharactersInStringNewDay)
        {
            // Debug.Log("reads numbers correctly");
            displayedTextComponentNewDay.maxVisibleCharacters++;
            AnimateNewDayText();
        }
        else
        {
            currentlyAnimatingNewDay = false;
            Invoke(nameof(CanMoveScreens), 0.1f);
            return;
        }
    }

    void CanMoveScreens()
    {
        MenuManager.Instance.NewDayMenu("close");
        GameManager.Instance.StartDay();
    }

    public void SetTextSpeed(float speed)
    {
        //
    }

    public void NewDayAnimation()
    {
        Debug.Log("Working to here");
        textToDisplay = ("Day: " + DayManager.Instance.currentDayNumber.ToString());

        BeginAnimatingNewDayText(textToDisplay);
        // if number > 5 , then change speed
    }
}
