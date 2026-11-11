using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotanicalCodexIconUpdater : MonoBehaviour
{
    // Only for diet currently
    private UnityEngine.UI.Image botanicalCodexIcon;
    [SerializeField] private Sprite botanicalCodexIconDesaturated;
    [SerializeField] private Sprite botanicalCodexIconColoured;
    private bool botanicalCodexIconIsDesaturated;

    public void InitialiseBotanicalCodexIconUpdater()
    {
        botanicalCodexIcon = gameObject.GetComponent<UnityEngine.UI.Image>();
        botanicalCodexIconIsDesaturated = true;
    }

    public void ToggleBotanicalCodexIcon()
    {
        Sprite spriteToShift = botanicalCodexIconIsDesaturated ? botanicalCodexIconColoured : botanicalCodexIconDesaturated;
        UIManager.Instance.SpriteShift(botanicalCodexIcon, spriteToShift);

        botanicalCodexIconIsDesaturated = !botanicalCodexIconIsDesaturated;
    }

    public void ResetBotanicalCodexIcon()
    {
        UIManager.Instance.SpriteShift(botanicalCodexIcon, botanicalCodexIconDesaturated);
        botanicalCodexIconIsDesaturated = true;
    }
}
