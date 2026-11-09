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

        defaultColour = new Color (.8f, .8f, .8f);              // image.color;
        hoverColour = image.color;                              //new Color (defaultColour.x, defaultColour.y, defaultColour.z, 0.5f);
        image.color = defaultColour;                            //defaultColour = new Color (1f,1f,1f,0f);
        
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
    }
    
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (menuObject)
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.menuNavigation);
        }

        if (debugObject)
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.sfxTypewriterSingle);
        }
        image.color = hoverColour;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        image.color = defaultColour;
    }
}
