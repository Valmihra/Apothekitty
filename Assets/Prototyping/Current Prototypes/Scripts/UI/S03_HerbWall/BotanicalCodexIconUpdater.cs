using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class BotanicalCodexIconUpdater : MonoBehaviour
{
    private UnityEngine.UI.Image botanicalCodexIcon;
    [SerializeField] private Sprite botanicalCodexIconDesaturated;
    [SerializeField] private Sprite botanicalCodexIconColoured;
    private bool botanicalCodexIconIsDesaturated;

    public void InitialiseBotanicalCodexIconUpdater()
    {
        botanicalCodexIcon = gameObject.GetComponent<UnityEngine.UI.Image>();
        botanicalCodexIconIsDesaturated = false;
    }

    public void ToggleBotanicalCodexIcon()
    {
        Sprite spriteToShift = botanicalCodexIconIsDesaturated ? botanicalCodexIconColoured : botanicalCodexIconDesaturated;
        UIManager.Instance.SpriteShift(botanicalCodexIcon, spriteToShift);
    }

    public void ResetBotanicalCodexIcon()
    {
        UIManager.Instance.SpriteShift(botanicalCodexIcon, botanicalCodexIconDesaturated);
    }
}
