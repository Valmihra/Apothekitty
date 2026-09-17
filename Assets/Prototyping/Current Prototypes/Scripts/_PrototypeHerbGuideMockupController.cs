using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class _PrototypeHerbGuideMockupController : MonoBehaviour, IDropHandler
{
    private List<TMP_Text> leafletTitlesList;
    private List<_PrototypeLeafletsOnHover> leafletsOnHoverReferenceList;
    
    private float minX;
    private float minY;
    private float maxX;
    private float maxY;
    
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3[] corners = new Vector3[4];
        
        rectTransform.GetWorldCorners(corners);
        minX = corners[0].x;
        maxX = corners[2].x;
        minY = corners[0].y;
        maxY = corners[2].y;
        
        InitialiseHGMockupController();
        // Debug.Log(corners[2].y);
    }

    void InitialiseHGMockupController()
    {
        List<string> titles = new List<string> { "Fortify", "Heal", "Ease", "Mind", "Body" };
        TMP_Text[] tmpArray = GetComponentsInChildren<TMP_Text>();
        leafletTitlesList = new List<TMP_Text>(tmpArray);
        leafletsOnHoverReferenceList = new List<_PrototypeLeafletsOnHover>();
        
        for (int i = 0; i < titles.Count; i++)
        {
            leafletTitlesList[i].text = titles[i];
            _PrototypeLeafletsOnHover tempLeaflet = leafletTitlesList[i].GetComponentInParent<_PrototypeLeafletsOnHover>();
            leafletsOnHoverReferenceList.Add(leafletTitlesList[i].GetComponentInParent<_PrototypeLeafletsOnHover>());
            
            leafletTitlesList[i].raycastTarget = false;
            /* if (leafletTitlesList[i].gameObject.TryGetComponentInParent<_PrototypeLeafletsOnHover>(out _PrototypeLeafletsOnHover tempLeaflet))
            {
                leafletsOnHoverReference.Add(tempLeaflet);
            }*/
        }

        for (int i = 0; i < titles.Count; i++)
        {
            // Image childImage = leafletsOnHoverReferenceList[i].gameObject.FindInChildren("Leaflet").GetComponent<Image>();
            // leafletsOnHoverReferenceList[i].GetInformation(leafletsOnHoverReferenceList[i].gameObject.GetComponentInChildren<Image>());
            // GameObject childObject = leafletsOnHoverReferenceList[i].transform.Find("Leaflet");
            
            /*Transform childObject = leafletsOnHoverReferenceList[i].transform.GetChild(0);
            // Debug.Log(childObject);
            Image childImage = childObject.GetComponent<Image>();
            leafletsOnHoverReferenceList[i].GetInformation(childImage);*/
            leafletsOnHoverReferenceList[i].GetInformation(leafletsOnHoverReferenceList[i].GetComponent<Image>());
            leafletsOnHoverReferenceList[i].SetBoundingBox(minX, minY, maxX, maxY);
        }
        
        // leafletsOnHoverReference = GetComponent<_PrototypeLeafletsOnHover>();
        
    }

    public static void DisableInteraction()
    {
        // find parent object and foreach child, blocks raycasts = false
        
        // and then enable would be the opposite
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent<_PrototypeLeafletsOnHover>(out _PrototypeLeafletsOnHover leaflet))
        {
            leaflet.StartResize(leaflet.smallestSize);
            leaflet.HideInformation();
        }
    }
}
