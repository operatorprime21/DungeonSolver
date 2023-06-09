using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Item item;
    public string itemName;
    //public int maxCountPerSlot;
    //public Type itemType;
    //public int currentCount;
    //private Camera cam;
    //public GameObject slot;
    //private GameObject canvas;
    //public TMP_Text count;
    //private GameObject player;

    //public Transform lastSlot;

    public enum Item
    {
        wood,         //Ingredients to build tile
        cloth,        //Ingredient to build tile
        emptyBottle,  //Used to get potions
        bluePotion,   //Consume to walk on air
        redPotion,    //Consume to ignore fire
        greenPotion,  //Consume to save steps
        ironKey,      //Used to open locked Silver Chests
        silverKey,    //Used to open Iron Doors
        goldKey,      //Used to open locked Gold Chests
    }


    private void Start()
    {

    }

    public void FindCorrectUI(int add)
    {
        switch(this.item)
        {
            case Item.wood: 
                itemName = "wood";
                break;
            case Item.cloth:
                itemName = "cloth";
                break;
            case Item.emptyBottle:
                itemName = "emptyBottle";
                break;
            case Item.bluePotion:
                itemName = "bluePotion";
                break;
            case Item.redPotion:
                itemName = "redPotion";
                break;
            case Item.greenPotion:
                itemName = "greenPotion";
                break;
            case Item.ironKey:
                itemName = "ironKey";
                break;
            case Item.silverKey:
                itemName = "silverKey";
                break;
            case Item.goldKey:
                itemName = "goldKey";
                break;
        }
        GameObject itemUI = GameObject.Find("Canvas").transform.Find("CraftMenu").transform.Find("Page").gameObject.transform.Find(itemName).gameObject;
        itemUI.GetComponent<IconProperties>().c += add;
        itemUI.GetComponent<IconProperties>().SetCount();
    }




    //private void OnMouseDrag()
    //{
    //    Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    //    Vector3 itemPos = cam.ScreenToWorldPoint(mousePos);
    //    this.transform.position = new Vector3(itemPos.x, itemPos.y, 0f);
    //}

    //private void OnMouseUp()
    //{
    //    if (slot != null)
    //    {
    //        Slot SlotInfo = slot.GetComponent<Slot>();
    //        SlotInfo.AddItem(this);
    //    }
    //    if (slot == null)
    //    {
    //        DenyStacking();
    //        Debug.Log("No slots asigned");
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    if (slot != null)
    //    {
    //        Slot SlotInfo = slot.GetComponent<Slot>();
    //        SlotInfo.RemoveItem();
    //        this.transform.parent = canvas.transform;
    //        SlotInfo.hasItem = false;
    //    }
    //}

    //public void DenyStacking()
    //{
    //    this.transform.position = lastSlot.transform.position;
    //    this.transform.parent = lastSlot.transform;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "Slot")
    //    {
    //        slot = collision.gameObject;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Slot")
    //    {
    //        slot = null;
    //    }
    //}

    //public void removeBool()
    //{
    //    Slot SlotInfo = slot.GetComponent<Slot>();
    //    SlotInfo.hasItem = false;
    //}

}
