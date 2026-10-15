using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton = GetComponent<Button>();
        playButton.onClick.AddListener(delegate { PlayGame(); });
    }

    
    void PlayGame()
    {
        // SoundManager.Instance.PlayAudio(SoundManager.Instance.menuReturn);
        
        SoundManager.Instance.PlayAudio(SoundManager.Instance.menuSelect);

        SoundManager.Instance.GetAudioToReturnTo();
        // SwitchInstrumental(SoundManager.Instance.musicInstrumentalMoonGuitar);
        // if (SceneManager.)
        
        MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenuCanvasGroup);
    }
}
