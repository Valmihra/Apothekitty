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

    //private bool pulsing;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        //string hex = "#8A1E1E";

        /*if (ColorUtility.TryParseHTMLString(hex, out Color output))
        {
            Debug.Log("Colour found!");
            pulseColour = output;
        }*/
        pulseColour = new Color(138f/255f, 30f/255f, 30f/255f);
        defaultColour = image.color;
    }

    /*// Update is called once per frame
    void Update()
    {
        if (pulsing)
    }*/

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
