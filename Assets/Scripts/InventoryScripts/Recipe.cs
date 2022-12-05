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

    public IEnumerator WaitUntilFinish(float time, GameObject result, Slot slot)
    {
        Crafting craft = GameObject.Find("InventoryManager").GetComponent<Crafting>();
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        //Change this down the line so that it activates a "claim" UI first. Same goes for every other buildings really. Clicking claim will *then* spawn in the item.
        craft.MakeItem(result, slot, amountToMake);
        this.gameObject.SetActive(true);
    }
}
