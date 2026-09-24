using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class _PrototypeHerbGuideMockupController : MonoBehaviour
{
    private List<TMP_Text> leafletTitlesList;
    private List<_PrototypeLeafletsOnHover> leafletsOnHoverReferenceList;
    
    private float minX;
    private float minY;
    private float maxX;
    private float maxY;
    [SerializeField] private float boundingBoxOffset = 100f;
    private Rect rect;
    
    // *TAG* - For use on slider: Maybe if roller is open, interaction blocker prevents touching the drawers? idk.
    
    
    void Awake()
    {
        rect = CalculateRect();
        InitialiseHGMockupController();
    }

    private Rect CalculateRect()
    {
        Vector3[] corners = new Vector3[4];
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.GetWorldCorners(corners);

        Vector2 min = corners[0];
        Vector2 max = new Vector2(corners[2].x + boundingBoxOffset, corners[2].y);
        Vector2 size = max - min;

        return new Rect(min, size);
    }

    void InitialiseHGMockupController()
    {
        List<string> titles = new List<string> { "Fortify", "Heal", "Ease", "Mind", "Body" };
            TMP_Text[] tmpArray = GetComponentsInChildren<TMP_Text>();
            leafletTitlesList = new List<TMP_Text>(tmpArray);
            
        leafletsOnHoverReferenceList = new List<_PrototypeLeafletsOnHover>();
        
        for (int i = 0; i < titles.Count; i++)
        {
            // Assigns the title from the list to the TMPro element at the same position in the relevant list
            leafletTitlesList[i].text = titles[i];
            leafletTitlesList[i].raycastTarget = false;
            
            leafletsOnHoverReferenceList.Add(leafletTitlesList[i].GetComponentInParent<_PrototypeLeafletsOnHover>());
            leafletsOnHoverReferenceList[i].InitialisePrototypeLeaflet();
            leafletsOnHoverReferenceList[i].AssignRect(rect);
        }
    }

}
