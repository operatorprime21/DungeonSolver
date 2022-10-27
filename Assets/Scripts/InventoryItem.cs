using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Item itemType;
    public int maxCountPerSlot;

    public enum Item
    {
        wood,
        cloth,
        ammo,
        nail,
        emptyBottle
        
    }
    private void Start()
    {
        InitItemVariables();
    }

    private void InitItemVariables()
    {
        switch (this.itemType)
        {
            case InventoryItem.Item.wood:
                maxCountPerSlot = 5;
                break;
            case InventoryItem.Item.cloth:
                maxCountPerSlot = 12;
                break;
            case InventoryItem.Item.ammo:
                maxCountPerSlot = 20;
                break;
            case InventoryItem.Item.nail:
                maxCountPerSlot = 15;
                break;
            case InventoryItem.Item.emptyBottle:
                maxCountPerSlot = 3;
                break;
        }

            

    }

}
