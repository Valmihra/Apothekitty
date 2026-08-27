using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimatedTextEffect : MonoBehaviour
{
    // This is just sauce, but I thought it would drastically improve
    // dialogue feeling and separate it from the standard popups some more..
    [SerializeField]
    private TMP_Text displayedTextComponent;
    private float timeBetweenCharacters = 0.05f;
    private int allCharactersInString;
    public bool currentlyAnimating = false;
    // private bool animatingText = false;
    
    
    // private int numberVisibleCharacters;
    // private Coroutine animationCoroutine;
    
    
    void Start()
    {
        
        // Begin
        // displayedTextComponent = GetComponent<TMP_Text>();
        // BeginAnimatingText("This is a test!");
    }
    
    // called in dialogueRunner to animate the dialogue
    /*public void UpdateTextComponent()
    {
        //
    }*/
    
    // called by dialogueSensor to skip the animation
    public void JumpEndLine()
    {
        CancelInvoke();
        currentlyAnimating = false;
        displayedTextComponent.maxVisibleCharacters = allCharactersInString;
    }
    
    // called in dialogueRunner to animate the dialogue
    public void BeginAnimatingText(string textToAnimate)
    {
        
        // makes sure that there are no double ups
        /*if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }*/

        displayedTextComponent.text = textToAnimate;
        
        currentlyAnimating = true;
        displayedTextComponent.ForceMeshUpdate();
        allCharactersInString = displayedTextComponent.textInfo.characterCount;
        // Debug.Log(allCharactersInString);
        displayedTextComponent.maxVisibleCharacters = 0;
        // numberVisibleCharacters = 0;
        //animationCoroutine = StartCoroutine(AnimateText());
        
        AnimateText();
        
        /*
        int allCharactersInString = displayedTextComponent.textInfo.characterCount;
        int numberVisibleCharacters = 0;

        displayedTextComponent.maxVisibleCharacters = 0;*/

        //AnimateText();
        
        /*for (int i = 0; i < allCharactersInString; i++)
        {
            // ShowNextCharacter
        }*/

    }

    void AnimateText()
    {
        // Debug.Log("gets to here");
        Invoke(nameof(ShowNextCharacterInString), timeBetweenCharacters);
        
        
        
        
        /*int allCharactersInString = displayedTextComponent.textInfo.characterCount;
        int numberVisibleCharacters = 0;

        displayedTextComponent.maxVisibleCharacters = 0;*/

        /*for (int i = 0; i < allCharactersInString; i++)
        {
            //
        }*/
    }

    void ShowNextCharacterInString()
    {
        //  Debug.Log("Begins the invoke");
        displayedTextComponent.ForceMeshUpdate();
        if (displayedTextComponent.maxVisibleCharacters != allCharactersInString) 
        {
            // Debug.Log("reads numbers correctly");
            displayedTextComponent.maxVisibleCharacters++;
            AnimateText();
        }
        else
        {
            currentlyAnimating = false;
            return;
        }
    }

    /*private IEnumerator AnimateText()
    {
        // displayedTextComponent.ForceMeshUpdate();
        int allCharactersInString = displayedTextComponent.textInfo.characterCount;
        
        // int numberVisibleCharacters = 0;
        displayedTextComponent.maxVisibleCharacters = 0;

        for (int i = 0; i < allCharactersInString; i++)
        {
            Debug.Log("Test");
            displayedTextComponent.maxVisibleCharacters = i;
            
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
        
    }*/
}
