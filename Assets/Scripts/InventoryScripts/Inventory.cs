using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    //public List<GameObject> storages = new List<GameObject>();
    private bool toggled = false;
    public GameObject inventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Need to keep InventoryUI (as a whole) not destroyed loading through levels along with some buttons


    public void ToggleInventory() //Simply opens the inventory UI and closes it
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

    public List<InventoryItem> ReturnInventory() //Loops through all slots thats in the inventory UI to grab any item within. Adds them to a list
    { 
        return inventory;
    }

    //public List<GameObject> ReturnTotalInventory()  //Including the items from ReturnInventory(), this returns every single item from every Storage building the player has built.
    //    //This is done through looping the list of storages that the storage buildings added their own contents to upon finish construction
    //{
    //    List<GameObject> itemInSlot = new List<GameObject>();
    //    List<GameObject> playerInventory = ReturnInventory();
    //    inventory = itemInSlot.Union(playerInventory).ToList();
        
    //    //foreach (GameObject storage in storages)
    //    //{
    //    //     if(storage!= null)
    //    //     {
    //    //          SlotHolder storageInfo = storage.GetComponent<SlotHolder>();
    //    //          foreach (Slot slot in storageInfo.slots)
    //    //          { 
    //    //               if (slot.hasItem == true)
    //    //               {
    //    //                   GameObject item = slot.transform.GetChild(0).gameObject;
    //    //                   inventory.Add(item);
    //    //               }
    //    //          }
    //    //     }  
    //    //} 
    //    return inventory;
    //}
}
