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

    //Need to keep InventoryUI (as a whole) not destroyed loading through levels along with some buttons


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
        List<GameObject> itemInSlot = new List<GameObject>();
        if(ReturnInventory()!= null)
        {
            itemInSlot.Union(ReturnInventory()).ToList();
        }

        if(storages != null)
        {
            foreach (GameObject storage in storages)
            {
                SlotHolder storageInfo = storage.GetComponent<SlotHolder>();
                foreach (Slot slot in storageInfo.slots)
                {
                    if (slot.hasItem == true)
                    {
                        GameObject item = slot.gameObject.transform.GetChild(0).gameObject;
                        itemInSlot.Add(item);
                    }
                }
            }
        }
        return itemInSlot;
    }
}
