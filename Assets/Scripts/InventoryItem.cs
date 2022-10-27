using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Item itemType;
    public int maxCountPerSlot;

    public int currentCount;
    public Camera cam;
    private GameObject slot;
    private GameObject canvas;
    private GameObject player;

    public enum Item
    {
        wood,
        cloth,
        ammo,
        nail,
        emptyBottle
    }

    public Sprite sprite;

    private void Start()
    {
        InitItemVariables();
        canvas = GameObject.Find("Canvas");
        player = GameObject.Find("Player");
    }

    private void Update()
    {

    }

    private void InitItemVariables()
    {
        switch (this.itemType)
        {
            case InventoryItem.Item.wood:
                maxCountPerSlot = 5;
                break;
            case InventoryItem.Item.cloth:
                maxCountPerSlot = 12;
                break;
            case InventoryItem.Item.ammo:
                maxCountPerSlot = 20;
                break;
            case InventoryItem.Item.nail:
                maxCountPerSlot = 15;
                break;
            case InventoryItem.Item.emptyBottle:
                maxCountPerSlot = 3;
                break;
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 itemPos = cam.ScreenToWorldPoint(mousePos);
        this.transform.position = new Vector3(itemPos.x, itemPos.y, 0f);
    }

    private void OnMouseUp()
    {
        if (slot != null)
        {
            Slot SlotInfo = slot.GetComponent<Slot>();
            SlotInfo.AddItem(this);
        }
        if (slot == null)
        {
            DenyStacking();
        }
    }

    public void DenyStacking()
    {
        this.transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Slot")
        {
            slot = collision.gameObject;
            Debug.Log(slot.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Slot")
        {
            slot = null;
        }
    }

}
