using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public List<InventoryItem.Item> resourceToLook = new List<InventoryItem.Item>();
    public List<int> resourceToCount = new List<int>();
    public GameObject itemToMake;
    public RecipeType timeToMake;
    public int amountToMake;
    public float time;

    public GameObject recipeOwner;
    public enum RecipeType
    {
        instant,
        timed,
        building,
        units,
    }
    private void OnMouseDown()
    {
        BeginInventoryScan();
        Debug.Log("clicked");
    }

    public void BeginInventoryScan()
    {
        Crafting craft = GameObject.Find("InventoryManager").GetComponent<Crafting>();
        craft.recipe = this;
        craft.CheckAllResource(resourceToLook, resourceToCount, itemToMake);
    }
}
