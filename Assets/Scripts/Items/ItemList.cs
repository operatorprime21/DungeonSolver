using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    // Start is called before the first frame update
    public List<InventoryItem> items = new List<InventoryItem>();
    public GameObject UItoToggle;
    private bool UIisToggled;
    public void Toggle()
    {
        if (UItoToggle != null)
        {
            if (UIisToggled == false)
            {
                UItoToggle.SetActive(true);
                UIisToggled = true;
                Inventory inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>();
                inventory.ToggleInventory();
            }
            else
            {
                UItoToggle.SetActive(false);
                UIisToggled = false;
                Inventory inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>();
                inventory.ToggleInventory();
            }
        }
        else
        {
            //Not near anything to search
        }    
    }
}


