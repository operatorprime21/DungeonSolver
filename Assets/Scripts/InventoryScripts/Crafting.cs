using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    bool CheckResource(InventoryItem.Item resource, int reqAmount)
    {
        Inventory inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>();
        List<GameObject> items = inventory.ReturnInventory();
        int countToAdd = 0;
        foreach (GameObject item in items)
        {
            InventoryItem type = item.GetComponent<InventoryItem>();
            if (type.itemType == resource)
            {
                int count = type.currentCount;
                countToAdd += count;
            }
        }
        Debug.Log(countToAdd);
        if(countToAdd < reqAmount)
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
            if (type.itemType == resource)
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

    void CraftFrom1(InventoryItem.Item type1, int type1Req)
    {
        if (CheckResource(type1, type1Req) == false)
        {
            Debug.Log("Not enough resources");
        }
        else
        {
            ConsumeResource(type1, type1Req);
            Debug.Log("Successfully crafted");
        }
    }
    void CraftFrom2(InventoryItem.Item type1, int type1Req, InventoryItem.Item type2, int type2Req)
    {
        for (int r = 0; r < 3; r++)
        {
            if (r == 0)
            {
                if (CheckResource(type1, type1Req) == false)
                {
                    Debug.Log("Not enough resources");
                    break;
                }
            }
            if (r == 1)
            {
                if (CheckResource(type2, type2Req) == false)
                {
                    Debug.Log("Not enough resources");
                    break;
                }
            }
            if (r == 2)
            {
                ConsumeResource(type1, type1Req);
                ConsumeResource(type2, type2Req);
                Debug.Log("Successfully crafted");
            }
        }
    }
    void CraftFrom3(InventoryItem.Item type1, int type1Req, InventoryItem.Item type2, int type2Req, InventoryItem.Item type3, int type3Req)
    {
        for (int r = 0; r < 4; r++)
        {
            if (r == 0)
            {
                if (CheckResource(type1, type1Req) == false)
                {
                    Debug.Log("Not enough resources");
                    break;
                }
            }
            if (r == 1)
            {
                if (CheckResource(type2, type2Req) == false)
                {
                    Debug.Log("Not enough resources");
                    break;
                }
            }
            if (r == 2)
            {
                if (CheckResource(type3, type3Req) == false)
                {
                    Debug.Log("Not enough resources");
                    break;
                }
            }
            if (r == 3)
            {
                ConsumeResource(type1, type1Req);
                ConsumeResource(type2, type2Req);
                ConsumeResource(type3, type3Req);
                Debug.Log("Successfully crafted");
            }
        }
    }
    void CraftFrom4(InventoryItem.Item type1, int type1Req, InventoryItem.Item type2, int type2Req, InventoryItem.Item type3, int type3Req, InventoryItem.Item type4, int type4Req)
    {
        for (int r = 0; r < 5; r++)
        {
            if (r == 0)
            {
                if (CheckResource(type1, type1Req) == false)
                {
                    Debug.Log("Not enough resources");
                    break;
                }
            }
            if (r == 1)
            {
                if (CheckResource(type2, type2Req) == false)
                {
                    Debug.Log("Not enough resources");
                    break;
                }
            }
            if (r == 2)
            {
                if (CheckResource(type3, type3Req) == false)
                {
                    Debug.Log("Not enough resources");
                    break;
                }
            }
            if (r == 3)
            {
                if (CheckResource(type4, type4Req) == false)
                {
                    Debug.Log("Not enough resources");
                    break;
                }
            }
            if ( r == 4)
            {
                ConsumeResource(type1, type1Req);
                ConsumeResource(type2, type2Req);
                ConsumeResource(type3, type3Req);
                ConsumeResource(type4, type4Req);
                Debug.Log("Successfully crafted");
            }
        }
    }
}
