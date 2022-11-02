using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public List<InventoryItem.Item> resourceToLook = new List<InventoryItem.Item>();
    public List<int> resourceToCount = new List<int>();
    public GameObject itemToMake;
    private void OnMouseDown()
    {
        Crafting craft = GameObject.Find("InventoryManager").GetComponent<Crafting>();
        craft.Craft(resourceToLook, resourceToCount, itemToMake);
    }
}
