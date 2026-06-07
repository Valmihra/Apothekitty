using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AilmentIconColourController : MonoBehaviour
{
    private Image ailmentIconBackground;

    private static AilmentIconColourController _instance;
    public static AilmentIconColourController Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        ailmentIconBackground = GetComponent<Image>();
    }

    public void ResetAilmentIconBackground()
    {
        //UIManager.Instance.HideImageElement()
        ailmentIconBackground.enabled = false;
    }

    public void ShowAilmentIconBackground()
    {
        //UIManager.Instance.ShowImageElement
        ailmentIconBackground.enabled = true;
    }
}
