using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Color defaultColour;
    public Color hoverColour;
    private bool menuObject;
    private bool debugObject;
    void Awake()
    {
        image = GetComponent<Image>();

        defaultColour = new Color (.8f, .8f, .8f);
        hoverColour = image.color;
        image.color = defaultColour;
        
        menuObject = false;
        CheckIfMenu();
    }

    private void CheckIfMenu()
    {
        if (this.gameObject.CompareTag("MenuButton"))
        {
            menuObject = true;
        }

        if (this.gameObject.CompareTag("DebugButton"))
        {
            debugObject = true;
        }

        if (debugObject || menuObject)
        {
            image.color = hoverColour;
        }
    }
    
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!menuObject && !debugObject)
        {
            image.color = hoverColour;
        }
        
        if (menuObject)
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.menuNavigation);
        }

        if (debugObject)
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxTypewriterSingle);
        }
        
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!menuObject && !debugObject)
        {
            image.color = defaultColour;
        }
    }
}
