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
        AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        foreach(InventoryItem item in inv.inventory)
        {
            if (item.item == InventoryItem.Item.redPotion)
            {
                item.FindCorrectUI(-1);
                inv.inventory.Remove(item);
                this.gameObject.GetComponent<PlayerMovement>().turnsRed += 5;
                audio.Play("drink");
                break;
            }
        }
    }

    public void bluePotion()
    {
        foreach (InventoryItem item in this.gameObject.GetComponent<Inventory>().inventory)
        {
            AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            if (item.item == InventoryItem.Item.bluePotion)
            {
                item.FindCorrectUI(-1);
                inv.inventory.Remove(item);
                this.gameObject.GetComponent<PlayerMovement>().turnsBlue += 5;
                audio.Play("drink");
                break;
            }
        }
    }

    public void greenPotion()
    {
        foreach (InventoryItem item in this.gameObject.GetComponent<Inventory>().inventory)
        {
            AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            if (item.item == InventoryItem.Item.greenPotion)
            {
                item.FindCorrectUI(-1);
                inv.inventory.Remove(item);
                this.gameObject.GetComponent<PlayerMovement>().turnsGreen += 5;
                audio.Play("drink");
                break;
            }
        }
    }
}
