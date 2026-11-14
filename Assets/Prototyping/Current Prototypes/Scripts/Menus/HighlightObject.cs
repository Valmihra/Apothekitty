using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightObject : MonoBehaviour
{
    private Image image;
    private Color pulseColour;
    private Color defaultColour;
    private float time = 0.5f;

    void Start()
    {
        image = GetComponent<Image>();
        pulseColour = new Color(138f/255f, 30f/255f, 30f/255f);
        defaultColour = image.color;
    }

    void PulseColour()
    {
        image.color = pulseColour;
    }

    void PulseDefault()
    {
        image.color = defaultColour;
    }

    public void PerformPulse()
    {
        float timer = time;
        Invoke(nameof(PulseColour), timer);
        timer = timer + time;
        Invoke(nameof(PulseDefault), timer);
        timer = timer + time;
        Invoke(nameof(PulseColour), timer);
        timer = timer + time;
        Invoke(nameof(PulseDefault), timer);
    }
}
