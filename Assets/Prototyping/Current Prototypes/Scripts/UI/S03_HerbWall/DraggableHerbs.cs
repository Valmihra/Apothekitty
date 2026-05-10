using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableHerbs : Draggable
{
    // References for the child gameObject that will appear and get dragged
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
        //initialPositionOnDrag = cuttingRectTransform.anchoredPosition;
        initialPositionOnDrag = cuttingObject.transform.position;

        //UIManager.Instance.DisableUI(movableCutting);
    }

    void Start()
    {
        UIManager.Instance.DisableUI(movableCutting);
    }

    // Sets the initial position of the UI prior to movement and brings the selected panel to the front on the screen
    public override void OnBeginDrag(PointerEventData eventData)
    {
        drag = eventData.pressPosition - (Vector2)transform.position;

            //rectTransform.SetAsLastSibling();

        UIManager.Instance.EnableUI(movableCutting);
        UIManager.Instance.DisableInteraction(SceneManager.Instance.herbDrawers);
    }

    // Updates the UI position according to the mouse's movement
    public override void OnDrag(PointerEventData eventData)
    {
        var x = eventData.position.x / Screen.width;
        var y = eventData.position.y / Screen.height;
            //try to prevent dragging the element offscreen
            if(x is < 0.02f or > 0.98f || y is < 0.02f or > 0.98f) return;
        
            cuttingObject.transform.position = eventData.position - drag;
                // MIGHT HAVE TO EDIT THE SORT ORDER TO BRING IT IN FRONT OF THE INVENTORY
        //cuttingRectTransform.position = eventData.pressEventCamera.ScreenToWorldPoint(eventData.position);
    }

    // Checks to see if the UI is within the set boundaries, and places it accordingly
    public override void OnEndDrag(PointerEventData eventData)
    {
        UIManager.Instance.DisableUI(movableCutting);
        cuttingObject.transform.position = initialPositionOnDrag;
            //cuttingRectTransform.anchoredPosition = initialPositionOnDrag;
        UIManager.Instance.EnableInteraction(SceneManager.Instance.herbDrawers);        
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
        //if (cuttingRectTransform != null)
        //{
            //Debug.Log(cuttingRectTransform.gameObject.name);
        //}
        
        CanvasGroup[] getCanvasGroup = GetComponentsInChildren<CanvasGroup>();
        if (getCanvasGroup[0].gameObject != gameObject)
        {
            movableCutting = getCanvasGroup[0];
            cuttingImage = movableCutting.gameObject.GetComponent<Image>();

            //UIManager.Instance.DisableUI(movableCutting);
            //Debug.Log(movableCutting.gameObject.name);
            //UIManager.Instance.DisableUI(this.movableCutting);
            return;
        }
    }

    public void UpdateHerbType(string herbName)
    {
        herbType = herbName;
    }
}
