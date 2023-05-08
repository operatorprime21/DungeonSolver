using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    public List<InventoryItem.Item> resourceToLook = new List<InventoryItem.Item>();   //Holds a list of items a recipe should look for
    public List<int> resourceToCount = new List<int>();   //Holds a list of numbers thats ordered accordingly to the amount of resource that the recipe looks for 
    public GameObject itemToMake; //The result, if the recipe were to generate an item
    //public RecipeType timeToMake; //Time to make, mostly used in buildings
    //public int amountToMake; //time to make the item, mostly for buildings

    //public GameObject recipeOwner; //Need to determine owner to make sure multiple timed form of crafting doesnt take place using the same object
    //public enum RecipeType
    //{
    //    instant, //Items the players can craft manually  from their own inventory
    //    timed, //Items only buildings can make, taking time
    //    building, //Recipe to construct buildings
    //    units, //Recipes for simple units
    //}
    //private void OnMouseDown()
    //{
    //    BeginInventoryScan(); //This is only for instant form of crafts
    //}

    public void BeginInventoryScan() //This is only for instant form of crafts
    {
        Crafting craft = GameObject.Find("Player").GetComponent<Crafting>();
        craft.recipe = this;
        craft.CheckAllResource(resourceToLook, resourceToCount, itemToMake);
    }
}
