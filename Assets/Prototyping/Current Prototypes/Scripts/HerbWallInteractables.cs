using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerbWallInteractables : MonoBehaviour
{
    public Button drawer01;
    public Button drawer02;
    public Button drawer03;
    public Button drawer04;
    public Button drawer05;
    public Button drawer06;
    public Button drawer07;
    public Button drawer08;
    public Button drawer09;
    public Button drawer10;
    public Button drawer11;
    public Button drawer12;

    // Start is called before the first frame update
    void Start()
    {
        drawer01.onClick.AddListener(delegate { OnPress(drawer01); });
        drawer02.onClick.AddListener(delegate { OnPress(drawer02); });
        drawer03.onClick.AddListener(delegate { OnPress(drawer03); });
        drawer04.onClick.AddListener(delegate { OnPress(drawer04); });
        drawer05.onClick.AddListener(delegate { OnPress(drawer05); });
        drawer06.onClick.AddListener(delegate { OnPress(drawer06); });
        drawer07.onClick.AddListener(delegate { OnPress(drawer07); });
        drawer08.onClick.AddListener(delegate { OnPress(drawer08); });
        drawer09.onClick.AddListener(delegate { OnPress(drawer09); });
        drawer10.onClick.AddListener(delegate { OnPress(drawer10); });
        drawer11.onClick.AddListener(delegate { OnPress(drawer11); });
        drawer12.onClick.AddListener(delegate { OnPress(drawer12); });

    }

    void OnPress(Button button)
    {
        /*if (button.GetComponent<DrawerSensor>().isActive)
        {
            // closes
            
        }
        else
        {
            // opens
        }
        
        !button.GetComponent<DrawerSensor>().isActive;*/
        button.GetComponent<DrawerSensor>().ToggleActivity();
    }
}
