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
        thisBoxUI = Instantiate(boxUI, UIpos, Quaternion.identity, canvas.transform);
        thisBoxUI.SetActive(false);
        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                int randomItem = Random.Range(0, itemlist.items.Count);
                if (randomItem != 0)
                {
                    int rItemCount = Random.Range(1, itemlist.items[randomItem].maxCountPerSlot+1);
                    currentItemCount =+ rItemCount;
                    if(currentItemCount >= maxItemSpawn)
                    {//Currenntly not functional. Item spawning are still overexceeding the limit of the box. Need further investigationn
                        break;
                    }
                    else
                    {
                        InventoryItem newItem = itemlist.items[randomItem];
                        newItem.currentCount = rItemCount;
                        TestHolding.Add(newItem);
                        Transform slotToFill = thisBoxUI.transform.Find("Slot (" + r + ", " + c + ")");
                        InventoryItem itemUI = Instantiate(newItem, slotToFill.transform.position, Quaternion.identity, canvas.transform);
                        itemUI.transform.parent = canvas.transform;
                        itemUI.transform.Find("Canvas").GetComponent<Canvas>().overrideSorting = true;
                        Slot SlotInfo = slotToFill.GetComponent<Slot>();
                        SlotInfo.AddItem(itemUI);
                    }
                }
            }
        }
        //TestHolding = generate;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ItemList list = GameObject.Find("ItemList").GetComponent<ItemList>();
            list.UItoToggle = thisBoxUI;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ItemList list = GameObject.Find("ItemList").GetComponent<ItemList>();
            list.UItoToggle = null;
        }
    }

}
