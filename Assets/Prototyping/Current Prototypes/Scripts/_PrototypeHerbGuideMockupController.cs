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
    
    /*private float minX;
    private float minY;
    private float maxX;
    private float maxY;*/
    
    // *TAG* - For use on slider: Maybe if roller is open, interaction blocker prevents touching the drawers? idk.
    
    
    void Awake()
    {
        InitialiseHGMockupController();
    }

    void InitialiseHGMockupController()
    {
        Vector3[] corners = new Vector3[4];
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.GetWorldCorners(corners);
            float minX = corners[0].x;
            float maxX = corners[2].x;
            float minY = corners[0].y;
            float maxY = corners[2].y;
            
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
            leafletsOnHoverReferenceList[i].SetBoundingBox(minX, minY, maxX, maxY);
            // leafletsOnHoverReferenceList[i].AssignResizeSpeed(resizeSpeedVariable);
        }
    }

}
