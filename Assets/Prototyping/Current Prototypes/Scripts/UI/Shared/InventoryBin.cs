using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBin : MonoBehaviour
{
    private Button binButton;
    public bool canDelete;

    //public Button 
    // Start is called before the first frame update
    void Start()
    {
        binButton = GetComponent<Button>();
        canDelete = false;
        binButton.onClick.AddListener(delegate { ActivateBin(); });
    }

    // The player will now remove the next herb they click on from their inventory.
    void ActivateBin()
    {
        canDelete = true;
    }
}
