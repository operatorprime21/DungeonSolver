using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public InventoryItem itemInThisSlot;
    public int maxCapForItem;
    public bool hasItem;

    public void AddItem(InventoryItem itemToAdd)
    {
        if(itemInThisSlot == null) //Check if the list is empty
        {
            AssignListCap(itemToAdd);
            itemInThisSlot = itemToAdd;
            itemToAdd.transform.position = this.transform.position;
            itemToAdd.lastSlot = this.transform;
            itemToAdd.transform.parent = this.transform;
            hasItem = true;
        }
        else if (itemToAdd.itemType == itemInThisSlot.itemType) //Check if the list is not empty and already has at least one item occupied
        {
            if (itemInThisSlot.currentCount < maxCapForItem)//Check if the item cap has not reached for this particular type of item
            {
                int capLeft = maxCapForItem - itemInThisSlot.currentCount;
                if(capLeft >= itemToAdd.currentCount)
                {
                    Destroy(itemToAdd.gameObject);
                    itemInThisSlot.currentCount += itemToAdd.currentCount;
                    itemInThisSlot.count.text = itemInThisSlot.currentCount.ToString();

                }
                else
                {
                    itemInThisSlot.currentCount = maxCapForItem;
                    itemInThisSlot.count.text = itemInThisSlot.currentCount.ToString();
                    itemToAdd.currentCount -= capLeft;
                    itemToAdd.count.text = itemToAdd.currentCount.ToString();
                    itemToAdd.DenyStacking();
                    Debug.Log("stacked to max, remaining denied");
                }
            }
            else 
            {
                itemToAdd.DenyStacking();
                Debug.Log("stacks maxed");
            }
        }
        else 
        {
            itemToAdd.DenyStacking();
            Debug.Log("incorrect stack");
        }
    }

    public void AssignListCap(InventoryItem item)
    {
        maxCapForItem = item.maxCountPerSlot;
    }

    public void RemoveItem()
    {
        itemInThisSlot = null;
        maxCapForItem = 0;

    }
}
