using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private Button pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        pauseButton = GetComponent<Button>();
        pauseButton.onClick.AddListener(delegate { PauseGame(); });
    }

    
    void PauseGame()
    {
        SoundManager.Instance.PlayAudio(SoundManager.Instance.menuReturn);

        SoundManager.Instance.SwitchInstrumental(SoundManager.Instance.musicPauseMenu);
        
        MenuManager.Instance.OpenMenu(MenuManager.Instance.pauseMenuCanvasGroup);
    }
}
