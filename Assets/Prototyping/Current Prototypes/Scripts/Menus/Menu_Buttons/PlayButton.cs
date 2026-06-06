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
        MenuManager.Instance.ExitMenu(MenuManager.Instance.pauseMenu);
    }
}
