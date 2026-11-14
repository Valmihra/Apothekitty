using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _PrototypePopupHandler : MonoBehaviour
{
    [Header ("Parent GameObject")]
    public GameObject animationWindow;
    // public TMP_Text popupText;
    // private string defaultPopupText = "This is a popup.";
    
    [Header ("Animation Prefabs")]
    public GameObject curtainAnimation;
	[Space]
    public GameObject deskCleanupAnimation;
    public GameObject deskStampAnimation;
	[Space]
    public GameObject codexAnimation;
    public GameObject compendiumOneAnimation;
    public GameObject compendiumTwoAnimation;
    
    private GameObject activePopupAnimation;

    void InstantiatePopup(string popupToDisplay)
    {
        GameObject objectToInstantiate;
        
        switch (popupToDisplay)
        {
            case "curtain":
            {
                objectToInstantiate = curtainAnimation;
                break;
            }
			
			case "deskCleanup":
            {
                objectToInstantiate = deskCleanupAnimation;
                break;
            }

            case "deskStamp":
            {
                objectToInstantiate = deskStampAnimation;
                break;
            }

            case "codex":
            {
                objectToInstantiate = codexAnimation;
                break;
            }

            case "compendiumOne":
            {
                objectToInstantiate = compendiumOneAnimation;
                break;
            }

            case "compendiumTwo":
            {
                objectToInstantiate = compendiumTwoAnimation;
                break;
            }

            default:
            {
                objectToInstantiate = animationWindow;
                break;
            }
                
        }

        if (objectToInstantiate != animationWindow)
        {
            Instantiate(objectToInstantiate, animationWindow.transform);
        }
        
        string nameToSearch = objectToInstantiate.name + "(Clone)".ToString();
        activePopupAnimation = GameObject.Find(nameToSearch);
    }


    public void ResetImagePopup()
    {
		if (activePopupAnimation != null)
		{
			Destroy(activePopupAnimation);
		}
    }

	public void SearchForVideoPrefab(string popupToDisplay)
	{
		string prefabName = "Failed";
		// Called from MenuManager right now,, just faster temporarily :skull:
		
		if (popupToDisplay.Contains ("Click the <#8A1E1E>curtain</color> to receive"))
		{
			prefabName = "curtain";
		}
		
		if (popupToDisplay.Contains ("Try placing the paper into the bin."))
		{
			prefabName = "deskCleanup";
		}
		
		if (popupToDisplay.Contains ("Once you believe you've found the correct diagnosis, "))
		{
			prefabName = "deskStamp";
		}
		
		if (popupToDisplay.Contains ("The Compendium will help you to figure out if a herb can heal, ease, or "))
        {
        	prefabName = "compendiumOne";
        }

		if (popupToDisplay.Contains ("To check the specific information about a herb, "))
		{
			prefabName = "codex";
		}
		
		
		if (prefabName != "Failed")
		{
			InstantiatePopup(prefabName);
		}

	}

}
