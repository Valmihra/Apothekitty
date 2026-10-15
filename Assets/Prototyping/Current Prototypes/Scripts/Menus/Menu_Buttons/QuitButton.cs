using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private Button quitButton;

    void Start()
    {
        quitButton = GetComponent<Button>();
        quitButton.onClick.AddListener(delegate { ExitGame(); });
    }

    
    void ExitGame()
    {
        if (GameManager.Instance.onMainMenu)
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.menuSelect);
            
            MenuManager.Instance.ExitGamePopup();
        }
        else
        {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.menuReturn);
            
            // Save progress here!!!
            
            MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenuCanvasGroup);
            GameManager.Instance.GoMainMenu();
        }
    }
}
