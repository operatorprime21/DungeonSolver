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
    public GameObject slot;
    private GameObject canvas;
    public TMP_Text count;
    private GameObject player;

    public Transform lastSlot;

    public enum Item
    {
        //Necessities/Crafting materials/Resources
        wood,
        cloth,
        ammo,
        nail,
        emptyBottle,
        arrow,
        bandage,
        gunPowder,
        scrapMetal,
        ductTape,
        rope,
        pipe,
        plasticScrap,
        potato,
        cannedMeat,
        cannedFish,
        bread,
        glass,
        egg,
        batteryCell,
        gasTank,
        wires,
        motherboards,
        cogs,
        wheat,
        rawMeat,
        medicalKit,
        painKillers,
        rubberStraps,
        lockpick,
        copperNickle,

        //Melee
        woodenPlank,
        crowBar,
        policeBaton,
        knife,
        shovel,
        machete,
        bigKnife,
        rake,
        hammer,
        largeHammer,

        //Ranged 
        pistol,
        crossbow,
        levelActionRifle,
        sawedOffShotgun,
        automaticPistol,

        //Armor
        sweater,
        bulletProofVest,
        hazmatSuit,
        footballSuit,
        heavyJacket,

        //Misc
        bagExtender1,
        bagExtender2,
        flashlight1,
        flashlight2,

    }

    private void Start()
    {
        InitItemVariables();
        canvas = GameObject.Find("Main Canvas");
        player = GameObject.Find("Player");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        count.text = currentCount.ToString();
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
                maxCountPerSlot = 8;
                break;
            case InventoryItem.Item.nail:
                maxCountPerSlot = 15;
                break;
            case InventoryItem.Item.emptyBottle:
                maxCountPerSlot = 3;
                break;
            case InventoryItem.Item.arrow:
                maxCountPerSlot = 2;
                break;
            case InventoryItem.Item.bandage:
                maxCountPerSlot = 4;
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
            SlotInfo.hasItem = false;
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Slot")
        {
            slot = null;
        }
    }

    public void removeBool()
    {
        Slot SlotInfo = slot.GetComponent<Slot>();
        SlotInfo.hasItem = false;
    }

}
