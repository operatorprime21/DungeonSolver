using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public List<InventoryItem> itemInThisSlot = new List<InventoryItem>();
    public int maxCapForItem;
    public Text count;

    public void AddItem(InventoryItem item)
    {
        if(itemInThisSlot.Count == 0) //Check if the list is empty
        {
            AssignListCap(item);
            for (int i = 0; i < item.currentCount; i++)
            {
                itemInThisSlot.Add(item);
            }
        }
        else if (itemInThisSlot.Count > 0 && item.itemType == itemInThisSlot[0].itemType) //Check if the list is not empty and already has at least one item occupied
        {
            if (itemInThisSlot.Count < maxCapForItem)//Check if the item cap has not reached for this particular type of item
            {
                int capLeft = maxCapForItem - itemInThisSlot.Count;
                if(capLeft >= item.currentCount)
                {
                    for (int i = 0; i < item.currentCount; i++)
                    {
                        itemInThisSlot.Add(item);
                    }
                }
                else
                {
                    for (int i = 0; i < capLeft; i++)
                    {
                        itemInThisSlot.Add(item);
                    }
                    item.currentCount -= capLeft;
                    item.DenyStacking();
                }
                
            }
            else 
            {
                item.DenyStacking();
            }
        }
        else 
        {
            item.DenyStacking();
        }
    }

    public void AssignListCap(InventoryItem item)
    {
        maxCapForItem = item.maxCountPerSlot;
    }
}
