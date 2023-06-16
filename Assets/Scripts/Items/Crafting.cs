using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    //private GameObject canvas;
    public Recipe recipe;
    private Camera cam;
    public GameObject dragDrop;

    [SerializeField] private GameObject objectToMake;
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        dragDrop = GameObject.Find("Canvas").transform.Find("DragDrop").gameObject;
    }
    bool CheckResource(InventoryItem.Item resource, int reqAmount) //Check a specific resource and the amount of said resource the recipe wants
    {
        Inventory inventory = GameObject.Find("Player").GetComponent<Inventory>(); //Get reference to the manager
        //Get everything the player has, both in inventory and other storages. Note: This isnt supposed to work in Expeditions, only player inventory
        int countToAdd = 0; //Create a new variable to add the total available amount
        foreach (InventoryItem item in inventory.inventory)
        {
            InventoryItem type = item.GetComponent<InventoryItem>();
            if (type.item == resource) //Check if item is the same one that we need
            {
                //int count = type.currentCount; //Then adds its count in the stack
                countToAdd += 1;  //Adds up all the counts to the variable
            }
        }
        if (countToAdd < reqAmount) //Check if the required amount is met
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
        Inventory inventory = GameObject.Find("Player").GetComponent<Inventory>(); //Get reference to the manager
        //Get everything the player has, both in inventory and other storages. Note: This isnt supposed to work in Expeditions, only player inventory
        int countToAdd = 0;//Create a new variable to add the total available amount
        //foreach (InventoryItem invenItem in inventory.inventory)
        //{
        //    InventoryItem.Item type = invenItem.GetComponent<InventoryItem>().item;
        //    if (type == resource && countToAdd <= reqAmount)
        //    {
                
        //    }
            
        //}
        for (int c = 0; c < inventory.inventory.Count; c++)
        {
            InventoryItem.Item type = inventory.inventory[c].item;
            if (type == resource && countToAdd < reqAmount)
            {
                countToAdd ++;
                inventory.inventory[c].FindCorrectUI(-1);
                inventory.inventory.Remove(inventory.inventory[c]);
                c--;
            }
            else if (countToAdd >= reqAmount)
            {
                break;
            }
        }
            

        //Each case takes into account if the amount left to consume is greater or equal to the current count of the item in the stack or not, if so it destroys the item and deduce that count from the amount to consume
        //It goes on until the remaining to consume is less than a stack of an item. In which case it deduces the remaining amount from the stack and sets the amount to consume to 0, stopping the loop
        //{
        //    int count = type.currentCount;
        //    if (countToAdd == 0)
        //    {
        //        if (reqAmount < count)
        //        {
        //            countToAdd = reqAmount;
        //            //type.currentCount -= reqAmount;
        //            //type.count.text = type.currentCount.ToString(); 
        //            break;
        //        }
        //        else if (reqAmount == count)
        //        {
        //            countToAdd = reqAmount;
        //            //type.removeBool();
        //            Destroy(item);
        //            break;
        //        }
        //        else
        //        {
        //            countToAdd += count;
        //            //type.removeBool();
        //            Destroy(item);
        //        }
        //    }
        //    else
        //    {
        //        int amountLeft = reqAmount - countToAdd;
        //        if (amountLeft < count)
        //        {
        //            countToAdd = reqAmount;
        //            //type.currentCount -= amountLeft;
        //            //type.count.text = type.currentCount.ToString();
        //            break;
        //        }
        //        if (amountLeft == count)
        //        {
        //            countToAdd = reqAmount;
        //            //type.removeBool();
        //            Destroy(item);
        //            break;
        //        }
        //        else
        //        {
        //            countToAdd += count;
        //            //type.removeBool();
        //            Destroy(item);
        //        }
        //    }
        //}

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&dragDrop.activeInHierarchy)
        {
            TapToPlace();
        }
    }
    public void CheckAllResource(List<InventoryItem.Item> item, List<int> countReq, GameObject result)
    {
        for (int r = 0; r < item.Count+1; r++)
        {
            if(r<item.Count)
            {
                if (CheckResource(item[r], countReq[r]) == false) //Going through the two lists (the items to find and the accordingly ordered amount to find), the crafting runs the bool function for every one of them
                {
                    Debug.Log("Not enough resources");  //The moment one bool returns false, break and discontinue the process
                    break;
                }
            }
            if(r == item.Count)
            {
                dragDrop.SetActive(true);
                objectToMake = result;
                GameObject craftMenu = GameObject.Find("Canvas").transform.Find("CraftMenu").gameObject;
                craftMenu.SetActive(false);
                ConsumeAllResource(item, countReq, result); //if all bool returns true, begin consuming everything
            }
        }
    }

    public void ConsumeAllResource(List<InventoryItem.Item> type, List<int> typeReq, GameObject result)
    {
        for (int c = 0; c < type.Count; c++)
        {
            ConsumeResource(type[c], typeReq[c]);  //Consumes every resource, using the two list 
        }
        AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audio.Play("wood");
    }
        //Inventory inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>(); //Depends on what the type of recipe it is, do things differently
        //if (recipe.timeToMake == Recipe.RecipeType.building) 
        //{
        //    BuildingBase building = recipe.gameObject.transform.parent.GetComponent<BuildingBase>();
        //    inventory.inventory = new List<GameObject>();
        //    building.Build();
        //}
        //else if (recipe.timeToMake == Recipe.RecipeType.units || recipe.timeToMake == Recipe.RecipeType.timed) //These tells the game that a building is handling these and it would take time to go through that
        //{
        //    BuildingFunctions building = recipe.recipeOwner.GetComponent<BuildingFunctions>();
        //    building.StartCoroutine(building.TimeToFinishFunction(building.timeToGen));
        //}
        //else if (recipe.timeToMake == Recipe.RecipeType.instant) //This is the version of recipes that the player can craft straight from their inventory
        //{
        //    if(FindEmptySlot(inventory)!= null)
        //    {
        //        MakeItem(result, FindEmptySlot(inventory), recipe.amountToMake);
        //        inventory.inventory = new List<GameObject>();
        //    }
        //}
    

    private void TapToPlace()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 itemPos = cam.ScreenToWorldPoint(mousePos);
        Instantiate(objectToMake, new Vector3(itemPos.x, itemPos.y, 0f), Quaternion.identity);
        dragDrop.SetActive(false);
        GameObject.Find("Canvas").GetComponent<Buttons>().ExitCraftMenu();
        objectToMake = null;
        AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audio.Play("wood");
        PlacingAnim();
    }

    private void PlacingAnim()
    {
        Animator playerAnim = this.gameObject.GetComponent<Animator>();
        PlayerMovement movementScript = this.gameObject.GetComponent<PlayerMovement>();
        playerAnim.Play(movementScript.orientation + "_action");
    }
    //public Slot FindEmptySlot(Inventory inventory)
    //{
    //    for (int i = 0; i < 20; i++) //Loops through player inventory to check for an empty slot
    //    {
    //        GameObject playerInventory = inventory.inventoryUI;
    //        GameObject nextSlot = playerInventory.transform.Find("Slot (" + i + ")").gameObject;
    //        Slot slot = nextSlot.GetComponent<Slot>();
    //        if (slot.hasItem == false)
    //        {
    //            return slot;
    //        }
    //    }
    //    return null;
    //}

    //public void MakeItem(GameObject result, Slot slot, int amount) //Creates an item in an empty slot and fill in all the info an item and the slot needs
    //{
    //    canvas = GameObject.Find("Main Canvas");
    //    Vector3 spawnSlot = slot.transform.position;
    //    GameObject itemMake = Instantiate(result, spawnSlot, Quaternion.identity, slot.transform);
    //    slot.hasItem = true;
    //    InventoryItem itemInfo = itemMake.GetComponent<InventoryItem>();
    //    //slot.itemInThisSlot = itemInfo;
    //    //slot.maxCapForItem = itemInfo.maxCountPerSlot;
    //    //itemInfo.currentCount = amount;
    //    //itemInfo.transform.Find("Canvas").GetComponent<Canvas>().overrideSorting = true;
    //    //itemInfo.lastSlot = slot.transform;
    //    //itemInfo.slot = slot.gameObject;
    //}
}

