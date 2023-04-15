using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    // Start is called before the first frame update
    private ItemList itemlist;
    public int row;
    public int col;
    public GameObject boxUI;
    private GameObject thisBoxUI;
    private GameObject canvas;

    public int maxItemSpawn;
    private int currentItemCount;

    public List<InventoryItem> TestHolding = new List<InventoryItem>();
    void Start()
    {
        itemlist = GameObject.Find("ItemList").GetComponent<ItemList>();
        canvas = GameObject.Find("Main Canvas");
        InitRandomItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitRandomItems()
    {
        Vector3 UIpos = GameObject.Find("boxUIpos").transform.position;
        thisBoxUI = Instantiate(boxUI, UIpos, Quaternion.identity, canvas.transform); //Creates a new box UI of slots
        thisBoxUI.SetActive(false); 
        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++) //Loop through each slot
            {
                int randomItem = Random.Range(0, itemlist.items.Count); //Pick a random item
                if (randomItem != 0)
                {
                    int rItemCount = Random.Range(1, 4); //Randomize stack count
                    //currentItemCount =+ rItemCount;
                    //if(currentItemCount >= maxItemSpawn)
                    //{
                    //    break;
                    //}
                    //else
                    //{
                        InventoryItem newItem = itemlist.items[randomItem]; 
                        newItem.currentCount = rItemCount; //Generate an item based on the two randomized variables
                        TestHolding.Add(newItem); //What did I need this for again?
                        Transform slotToFill = thisBoxUI.transform.Find("Slot (" + r + ", " + c + ")"); //Find the slot according to the loop
                        InventoryItem itemUI = Instantiate(newItem, slotToFill.transform.position, Quaternion.identity, canvas.transform); //Spawns the new item into a slot
                        itemUI.transform.parent = canvas.transform; //Change the item's and the slot's variables as if it were to be inserted into the slot naturally
                        itemUI.transform.Find("Canvas").GetComponent<Canvas>().overrideSorting = true;
                        Slot SlotInfo = slotToFill.GetComponent<Slot>();
                        SlotInfo.AddItem(itemUI);
                        SlotInfo.hasItem = true;
                    //}
                }
            }
        }
        //TestHolding = generate;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ItemList list = GameObject.Find("searcher").GetComponent<ItemList>();
            list.UItoToggle = thisBoxUI; //Make sure the player gets to open this box when near
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ItemList list = GameObject.Find("searcher").GetComponent<ItemList>();
            list.UItoToggle = null; //Make sure the player gets to open this box when near
        }
    }

}
