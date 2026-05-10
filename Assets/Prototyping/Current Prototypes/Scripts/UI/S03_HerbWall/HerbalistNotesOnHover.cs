using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HerbalistNotesOnHover : MonoBehaviour
{
    private Vector2 storedPosition;
    private DrawerSensor drawerSensor;

    public CanvasGroup herbalistNoteCanvasGroup;
    public TMP_Text herbalistNoteName;
    public TMP_Text herbalistNoteDescription;
    public TMP_Text herbalistNoteExtras;
    
    private static HerbalistNotesOnHover _instance;
    public static HerbalistNotesOnHover Instance
    {
        get
        {
            return _instance;
        }
    }

    void Start()
    {
        UIManager.Instance.DisableUI(herbalistNoteCanvasGroup);
        SetRespawnPosition();
    }

    void SetRespawnPosition()
    {
        RectTransform rectTransform = herbalistNoteCanvasGroup.GetComponent<RectTransform>();
        storedPosition = rectTransform.anchoredPosition;
    }

    // Updates the text displayed on the note popup
    public void ReceiveInformation(string displayName, string displayDescription, string displayExtras)
    {
        herbalistNoteName.text = displayName;
        herbalistNoteDescription.text = displayDescription;
        herbalistNoteExtras.text = displayExtras;

        PlaceUI();
    }

    // determines where best to place the UI before enabling it again
    public void PlaceUI()
    {
        //Vector3 mousePosition = Input.mousePosition;

        //herbalistNoteCanvasGroup.anchoredPosition =


        UIManager.Instance.EnableUI(herbalistNoteCanvasGroup);
    }

    // storedPosition is where the note will spawn at on hover
}
