using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public List<GameObject> storages = new List<GameObject>();
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

    public List<GameObject> ReturnInventory()
    { 
        List<GameObject> itemInSlot = new List<GameObject>();
        for(int i = 0; i < 8; i++)
        {
            GameObject nextSlot = inventoryUI.transform.Find("Slot ("+i+")").gameObject;
            Slot slot = nextSlot.GetComponent<Slot>();
            if(slot.hasItem == true)
            {
                GameObject item = nextSlot.transform.GetChild(0).gameObject;
                itemInSlot.Add(item);
            }
        }
        return itemInSlot;
    }

    public List<GameObject> ReturnTotalInventory()
    {
        List<GameObject> item = new List<GameObject>();
        if(ReturnInventory()!= null)
        {
            item.Union(ReturnInventory()).ToList();
        }
        foreach (GameObject storage in storages)
        {
            //storage.GetComponent<>
            //Try to get the inventories of each storage house
            //Then loop through each slot
            //Check if it has item or not
            //Add
        }
        return item;
    }
}
