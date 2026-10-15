using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    private Button newGameButton;

    void Start()
    {
        newGameButton = GetComponent<Button>();
        newGameButton.onClick.AddListener(delegate { StartNewGame(); });
    }

    // Exits the menu and starts a new game
    void StartNewGame()
    {
        SoundManager.Instance.PlayAudio(SoundManager.Instance.menuSelect);
        
        SoundManager.Instance.SwitchInstrumental(SoundManager.Instance.musicPatientWindow);
        
        MenuManager.Instance.ExitMenu(MenuManager.Instance.mainMenuCanvasGroup);
        GameManager.Instance.NewGame();
    }
}
