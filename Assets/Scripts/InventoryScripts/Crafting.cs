using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    private GameObject canvas;
    public Recipe recipe;
    bool CheckResource(InventoryItem.Item resource, int reqAmount)
    {
        Inventory inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>();
        List<GameObject> items = inventory.ReturnInventory();
        int countToAdd = 0;
        foreach (GameObject item in items)
        {
            InventoryItem type = item.GetComponent<InventoryItem>();
            if (type.item == resource)
            {
                int count = type.currentCount;
                countToAdd += count;
            }
        }
        if (countToAdd < reqAmount)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    void ConsumeResource(InventoryItem.Item resource, int reqAmount)
    {
        Inventory inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>();
        List<GameObject> items = inventory.ReturnInventory();
        int countToAdd = 0;
        foreach (GameObject item in items)
        {
            InventoryItem type = item.GetComponent<InventoryItem>();
            if (type.item == resource)
            {
                int count = type.currentCount;
                if (countToAdd == 0)
                {
                    if (reqAmount < count)
                    {
                        countToAdd = reqAmount;
                        type.currentCount -= reqAmount;
                        type.count.text = type.currentCount.ToString();
                        break;
                    }
                    else if (reqAmount == count)
                    {
                        countToAdd = reqAmount;
                        type.removeBool();
                        Destroy(item);
                        break;
                    }
                    else
                    {
                        countToAdd += count;
                        type.removeBool();
                        Destroy(item);
                    }
                }
                else
                {
                    int amountLeft = reqAmount - countToAdd;
                    if (amountLeft < count)
                    {
                        countToAdd = reqAmount;
                        type.currentCount -= amountLeft;
                        type.count.text = type.currentCount.ToString();
                        break;
                    }
                    if (amountLeft == count)
                    {
                        countToAdd = reqAmount;
                        type.removeBool();
                        Destroy(item);
                        break;
                    }
                    else
                    {
                        countToAdd += count;
                        type.removeBool();
                        Destroy(item);
                    }
                }
            }
        }
    }

    public void CheckAllResource(List<InventoryItem.Item> type, List<int> typeReq, GameObject result)
    {
        for (int r = 0; r < type.Count + 1; r++)
        {
            if (r < type.Count )
            {
                if (CheckResource(type[r], typeReq[r]) == false)
                {
                    Debug.Log("Not enough resources");
                    break;
                }
            }
            if (r == type.Count )
            {
                ConsumeAllResource(type, typeReq, result);
            }
        }
    }

    private void ConsumeAllResource(List<InventoryItem.Item> type, List<int> typeReq, GameObject result)
    {
        GameObject inventory = GameObject.Find("InventoryUI");

        for (int i = 0; i < 8; i++)
        {
            GameObject nextSlot = inventory.transform.Find("Slot (" + i + ")").gameObject;
            Slot slot = nextSlot.GetComponent<Slot>();
            if (slot.hasItem == false)
            {
                for (int c = 0; c < type.Count; c++)
                {
                    ConsumeResource(type[c], typeReq[c]);
                }
                if(recipe.timeToMake == Recipe.RecipeType.instant)
                {
                    MakeItem(result, slot);
                    break;
                }
                else if(recipe.timeToMake == Recipe.RecipeType.timed)
                {
                    StartCoroutine(recipe.WaitUntilFinish(recipe.time, result, slot));
                    break;
                }
            }
            
        }
    }



    public void MakeItem(GameObject result, Slot slot)
    {
        canvas = GameObject.Find("Main Canvas");
        Vector3 spawnSlot = slot.transform.position;
        GameObject itemMake = Instantiate(result, spawnSlot, Quaternion.identity, slot.transform);
        slot.hasItem = true;
        InventoryItem itemInfo = itemMake.GetComponent<InventoryItem>();
        slot.itemInThisSlot = itemInfo;
        slot.maxCapForItem = itemInfo.maxCountPerSlot;
        itemInfo.currentCount = 1;
        itemInfo.transform.Find("Canvas").GetComponent<Canvas>().overrideSorting = true;
        itemInfo.lastSlot = slot.transform;
        itemInfo.slot = slot.gameObject;
    }
}
   
