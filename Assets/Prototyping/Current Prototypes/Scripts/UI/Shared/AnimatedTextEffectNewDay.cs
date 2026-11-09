using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimatedTextEffectNewDay : MonoBehaviour
{
    [SerializeField] private TMP_Text displayedTextComponentNewDay;
    private float slowTimeBetweenCharacters = 1.0f;
    private float fastTimeBetweenCharacters = 0.18f;
    private int allCharactersInStringNewDay;
    private int finalCharacterNumber;
    public bool currentlyAnimatingNewDay = false;
    private bool onFinalCharacter;

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
        finalCharacterNumber = allCharactersInStringNewDay - 1;

        displayedTextComponentNewDay.maxVisibleCharacters = 0;
        AnimateNewDayText();
    }

    void AnimateNewDayText()
    {
        if (displayedTextComponentNewDay.maxVisibleCharacters == finalCharacterNumber)
        {
            onFinalCharacter = true;
        }
        
        float timeToInvoke = onFinalCharacter ? slowTimeBetweenCharacters : fastTimeBetweenCharacters;
        Invoke(nameof(ShowNextCharacterInNewDayString), timeToInvoke);

        //displayedTextComponentNewDay.maxVisibleCharacters++;
        /*// Debug.Log("gets to here");
        if (displayedTextComponentNewDay.maxVisibleCharacters < finalCharacterNumber)// > 5)
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxTypewriterSingle);
            displayedTextComponentNewDay.maxVisibleCharacters++;
            Invoke(nameof(ShowNextCharacterInNewDayString), fastTimeBetweenCharacters);

        }
        else
        {
            // SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxTypewriterEnd);
            Invoke(nameof(ShowNextCharacterInNewDayString), slowTimeBetweenCharacters);
        }
        */
    }

    void ShowNextCharacterInNewDayString()
    {
        //  Debug.Log("Begins the invoke");
        displayedTextComponentNewDay.maxVisibleCharacters++;
        displayedTextComponentNewDay.ForceMeshUpdate();
        if (onFinalCharacter)
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxTypewriterEnd);
            currentlyAnimatingNewDay = false;
            onFinalCharacter = false;
            Invoke(nameof(CanMoveScreens), slowTimeBetweenCharacters);
            return;
            //
        }
        else
        {
            if (displayedTextComponentNewDay.maxVisibleCharacters < finalCharacterNumber)
            {
                SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxTypewriterSingle);
            }
            AnimateNewDayText();
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
        // Debug.Log("Working to here");
        textToDisplay = ("Day: " + DayManager.Instance.currentDayNumber.ToString());
        onFinalCharacter = false;

        BeginAnimatingNewDayText(textToDisplay);
        // if number > 5 , then change speed
    }
}
