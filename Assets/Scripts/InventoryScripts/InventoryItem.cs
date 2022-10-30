using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Item itemType;
    public int maxCountPerSlot;

    public int currentCount;
    private Camera cam;
    private GameObject slot;
    private GameObject canvas;
    public TMP_Text count;
    private GameObject player;

    public Transform lastSlot;

    public enum Item
    {
        wood,
        cloth,
        ammo,
        nail,
        emptyBottle
    }

    private void Start()
    {
        InitItemVariables();
        canvas = GameObject.Find("Main Canvas");
        player = GameObject.Find("Player");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        count.text = currentCount.ToString();
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
            Debug.Log("No slots asigned");
        }
    }

    private void OnMouseDown()
    {
        if (slot != null)
        {
            Slot SlotInfo = slot.GetComponent<Slot>();
            SlotInfo.RemoveItem();
            this.transform.parent = canvas.transform;
        }
    }

    public void DenyStacking()
    {
        this.transform.position = lastSlot.transform.position;
        this.transform.parent = lastSlot.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Slot")
        {
            slot = collision.gameObject;
            //eDebug.Log(slot.name);
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
