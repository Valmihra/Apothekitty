using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueSensor : MonoBehaviour, IPointerClickHandler
{
    private AnimatedTextEffect textEffectDialogueBox;

    void Start()
    {
        textEffectDialogueBox = GetComponent<AnimatedTextEffect>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxNextDialogue);
        
        // *TAG* - PROTOTYPE CURSOR EVENT
        // _PrototypeCursor.Instance.QuickClick();
        
        if (!textEffectDialogueBox.currentlyAnimating)
        {
            DialogueRunner.Instance.RunDialogue();
        }
        else
        {
            textEffectDialogueBox.JumpEndLine();
        }
        
    }
}
