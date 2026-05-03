using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Canvas Groups")]
    public CanvasGroup pauseMenu;
    public List<CanvasGroup> allCanvasesMenus;
    
    //[Header("Pause Menu Buttons")]
    //public Button resumeGameButton;
    //public Button returnToMainMenuButton;
    //public Button resetSceneButton;

    private static MenuManager _instance;
    public static MenuManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        //if (_instance = null)
        //{
            _instance = this;
        //}
        //resumeGameButton.onClick.AddListener(delegate { Resume(); });
        //returnToMainMenuButton.onClick.AddListener(delegate { Return(); });
        //resetSceneButton.onClick.AddListener(delegate { Reset(); });

        InitialiseList();
        //SetupInitialScene();
    }

    public void Reset()
    {
        //GameManager.Instance.ResetScene();
    }

    void InitialiseList()
    {
        allCanvasesMenus = new List<CanvasGroup>();
        allCanvasesMenus.Add(pauseMenu);
        Debug.Log(allCanvasesMenus.Count + pauseMenu.name);
    }

    public void OpenMenu(CanvasGroup menuCanvasGroup)
    {
        UIManager.Instance.DisableInteraction(SceneManager.Instance.currentCanvasGroup);
        if ((SceneManager.Instance.onDesk) && (GameManager.Instance.ailmentChosen))
        {
            UIManager.Instance.DisableInteraction(SceneManager.Instance.diagnosisSheet);
        }

        UIManager.Instance.EnableUI(menuCanvasGroup);
        menuCanvasGroup.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void ExitMenu(CanvasGroup menuCanvasGroup)
    {
        UIManager.Instance.DisableUI(menuCanvasGroup);
        UIManager.Instance.EnableInteraction(SceneManager.Instance.currentCanvasGroup);
        if ((SceneManager.Instance.onDesk) && (GameManager.Instance.ailmentChosen))
        {
            UIManager.Instance.EnableInteraction(SceneManager.Instance.diagnosisSheet);
        }
    }

    public void SetupInitialScene()
    {
        foreach (CanvasGroup c in allCanvasesMenus)
        {
            UIManager.Instance.DisableUI(c);
        }
    }
}
