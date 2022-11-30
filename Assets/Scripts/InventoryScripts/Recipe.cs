using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public List<InventoryItem.Item> resourceToLook = new List<InventoryItem.Item>();
    public List<int> resourceToCount = new List<int>();
    public GameObject itemToMake;
    public Time time;
    public enum Time
    { 
        simple,
        complex,
    }

    private void OnMouseDown()
    {
        Crafting craft = GameObject.Find("InventoryManager").GetComponent<Crafting>();
        if (this.time == Time.simple)
        {  
            craft.Craft(resourceToLook, resourceToCount, itemToMake);
        }
        else if (this.time == Time.complex)
        {
            //Implement the timer into craft here
            //Need to check have enough for recipe first, thenn start coroutine if possible
        }
    }

    public IEnumerator CraftTimer(float time)
    {
        yield return new WaitForSeconds(time);

    }
}
