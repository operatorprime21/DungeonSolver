using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBuffs : MonoBehaviour
{
    // Start is called before the first frame update
    Inventory inv; 
    void Start()
    {
        inv = this.gameObject.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void redPotion()
    {
        foreach(InventoryItem item in inv.inventory)
        {
            if (item.item == InventoryItem.Item.redPotion)
            {
                item.FindCorrectUI(-1);
                inv.inventory.Remove(item);
                this.gameObject.GetComponent<PlayerMovement>().turnsRed += 3;
                break;
            }
        }
    }

    public void bluePotion()
    {
        foreach (InventoryItem item in this.gameObject.GetComponent<Inventory>().inventory)
        {
            if (item.item == InventoryItem.Item.bluePotion)
            {
                item.FindCorrectUI(-1);
                inv.inventory.Remove(item);
                this.gameObject.GetComponent<PlayerMovement>().turnsBlue += 3;
                break;
            }
        }
    }

    public void greenPotion()
    {
        foreach (InventoryItem item in this.gameObject.GetComponent<Inventory>().inventory)
        {
            if (item.item == InventoryItem.Item.greenPotion)
            {
                item.FindCorrectUI(-1);
                inv.inventory.Remove(item);
                this.gameObject.GetComponent<PlayerMovement>().turnsGreen += 3;
                break;
            }
        }
    }
}
