using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private Button quitButton;
    private bool buttonSavesProgress;

    void Start()
    {
        quitButton = GetComponent<Button>();
        quitButton.onClick.AddListener(delegate { ExitGame(); });
            
        GameObject saveButton = GameObject.Find("Pause Menu Button - Save and Quit");
        if (this.gameObject == saveButton)
        {
            buttonSavesProgress = true;
        }
        else
        {
            buttonSavesProgress = false;
        }
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
            if (buttonSavesProgress)
            {
                GameManager.Instance.SaveGame();
            }
            
            MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenuCanvasGroup);
            GameManager.Instance.GoMainMenu();
        }
    }
}
