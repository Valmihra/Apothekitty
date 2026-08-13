using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolutionManager : MonoBehaviour
{
    private static ScreenResolutionManager _instance;
    public static ScreenResolutionManager Instance
    {
        get
        {
            return _instance;
        }
    }
    
    
    void Awake()
    {
        // _instance = this;
        
        // Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }
    
    // Accessibility options for builds -- windowed vs fullscreen, dif resolutions, etc.
    //void ChangeResolutionSettings()
    //{
    //    
    //}
}
