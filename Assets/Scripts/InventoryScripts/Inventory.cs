using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private bool toggled = false;
    public GameObject inventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    public void ToggleInventory()
    {
        if (toggled == false)
        {
            inventoryUI.gameObject.SetActive(true);
            toggled = true;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            inventoryUI.gameObject.SetActive(false);
            toggled = false;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
