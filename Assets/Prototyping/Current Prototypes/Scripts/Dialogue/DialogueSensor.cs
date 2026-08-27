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
