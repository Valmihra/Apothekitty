using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableHerbs : Draggable
{
    // *TAG* - woof, I should revisit this. LocateRect seems SUPER convoluted. could clean up.
    [HideInInspector]
    public Image cuttingImage;
    private RectTransform cuttingRectTransform;
    private GameObject cuttingObject;
    private CanvasGroup movableCutting;
    private Vector2 initialPositionOnDrag;
    private Vector2 drag;
    [HideInInspector]
    public string herbType;


    protected override void Awake()
    {
        LocateRect();
        AssignCanvasGroup();
        initialPositionOnDrag = cuttingObject.transform.position;
    }

    void Start()
    {
        UIManager.Instance.DisableUI(movableCutting);
    }

    // Sets the initial position of the UI prior to movement and brings the selected panel to the front on the screen
    public override void OnBeginDrag(PointerEventData eventData)
    {
        cuttingObject.transform.position = eventData.pressPosition;
        UIManager.Instance.EnableUI(movableCutting);
        UIManager.Instance.DisableInteraction(SceneManager.Instance.canvasGroupHerbDrawers);
                // maybe just change disable appearence in the inspector for the buttons?? uerghhhh idk,,
    }

    // Updates the UI position according to the mouse's movement
    public override void OnDrag(PointerEventData eventData)
    {
        var x = eventData.position.x / Screen.width;
        var y = eventData.position.y / Screen.height;
            //try to prevent dragging the element offscreen
            if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;
        
            cuttingObject.transform.position = eventData.position;// - drag;
    }

    // Hides the cutting and moves it back to the drawer
    public override void OnEndDrag(PointerEventData eventData)
    {
        UIManager.Instance.DisableUI(movableCutting);
        cuttingObject.transform.position = initialPositionOnDrag;
        UIManager.Instance.EnableInteraction(SceneManager.Instance.canvasGroupHerbDrawers);        
    }

    void LocateRect()
    {
        RectTransform r = GetComponent<RectTransform>();
        RectTransform[] findChild = GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < findChild.Length; i++)
        {
            if (findChild[i].gameObject != gameObject)
            {
                cuttingRectTransform = findChild[i];
                cuttingObject = cuttingRectTransform.gameObject;
                //Debug.Log(cuttingRectTransform.gameObject.name);
                return;
            }
        }
    }

    void AssignCanvasGroup()
    {
        CanvasGroup[] getCanvasGroup = GetComponentsInChildren<CanvasGroup>();
        if (getCanvasGroup[0].gameObject != gameObject)
        {
            movableCutting = getCanvasGroup[0];
            cuttingImage = movableCutting.gameObject.GetComponent<Image>();
            return;
        }
    }

    public void UpdateHerbType(string herbName)
    {
        herbType = herbName;
    }
}
