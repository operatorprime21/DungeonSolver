using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Item item;
    public int maxCountPerSlot;
    private Type itemType;
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
        stick,
        plank,
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
        plasticScrap,//

        potato,
        cannedMeat,
        cannedFish,
        bread,
        glass,//
        egg,
        wheat,
        rawMeat,
        medicalKit,
        painKillers,//
        lockpick,//

        batteryCell,
        gasTank,//
        wires,
        motherboards,
        cogs,
        rubberStraps,
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

    private enum Type
    { 
        resource,
        consumable,
        misc,
        weapon,
        armor
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
        switch (this.item)
        {
            case InventoryItem.Item.wood:
                maxCountPerSlot = 5;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.stick:
                maxCountPerSlot = 20;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.plank:
                maxCountPerSlot = 10;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.cloth:
                maxCountPerSlot = 12;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.ammo:
                maxCountPerSlot = 8;
                itemType = Type.consumable;
                break;
            case InventoryItem.Item.nail:
                maxCountPerSlot = 15;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.emptyBottle:
                maxCountPerSlot = 3;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.arrow:
                maxCountPerSlot = 2;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.bandage:
                maxCountPerSlot = 4;
                itemType = Type.consumable;
                break;

            case InventoryItem.Item.gunPowder:
                maxCountPerSlot = 4;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.scrapMetal:
                maxCountPerSlot = 4;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.ductTape:
                maxCountPerSlot = 4;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.rope:
                maxCountPerSlot = 4;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.pipe:
                maxCountPerSlot = 4;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.plasticScrap:
                maxCountPerSlot = 4;
                itemType = Type.resource;
                break;

            case InventoryItem.Item.potato:
                maxCountPerSlot = 2;
                itemType = Type.consumable;
                break;
            case InventoryItem.Item.cannedMeat:
                maxCountPerSlot = 2;
                itemType = Type.consumable;
                break;
            case InventoryItem.Item.cannedFish:
                maxCountPerSlot = 2;
                itemType = Type.consumable;
                break;
            case InventoryItem.Item.bread:
                maxCountPerSlot = 2;
                itemType = Type.consumable;
                break;
            case InventoryItem.Item.glass:
                maxCountPerSlot = 2;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.egg:
                maxCountPerSlot = 2;
                itemType = Type.consumable;
                break;
            case InventoryItem.Item.wheat:
                maxCountPerSlot = 2;
                itemType = Type.consumable;
                break;
            case InventoryItem.Item.rawMeat:
                maxCountPerSlot = 2;
                itemType = Type.consumable;
                break;
            case InventoryItem.Item.medicalKit:
                maxCountPerSlot = 2;
                itemType = Type.consumable;
                break;
            case InventoryItem.Item.painKillers:
                maxCountPerSlot = 2;
                itemType = Type.consumable;
                break;
            case InventoryItem.Item.lockpick:
                maxCountPerSlot = 2;
                itemType = Type.consumable;
                break;

            case InventoryItem.Item.batteryCell:
                maxCountPerSlot = 2;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.gasTank:
                maxCountPerSlot = 2;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.wires:
                maxCountPerSlot = 2;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.motherboards:
                maxCountPerSlot = 2;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.cogs:
                maxCountPerSlot = 2;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.rubberStraps:
                maxCountPerSlot = 2;
                itemType = Type.resource;
                break;
            case InventoryItem.Item.copperNickle:
                maxCountPerSlot = 2;
                itemType = Type.resource;
                break;

            case InventoryItem.Item.woodenPlank:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.crowBar:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.policeBaton:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.knife:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.shovel:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.machete:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.rake:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.hammer:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.largeHammer:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.bigKnife:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.pistol:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.crossbow:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.levelActionRifle:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.sawedOffShotgun:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;
            case InventoryItem.Item.automaticPistol:
                maxCountPerSlot = 1;
                itemType = Type.weapon;
                break;

            case InventoryItem.Item.sweater:
                maxCountPerSlot = 1;
                itemType = Type.armor;
                break;
            case InventoryItem.Item.bulletProofVest:
                maxCountPerSlot = 1;
                itemType = Type.armor;
                break;
            case InventoryItem.Item.hazmatSuit:
                maxCountPerSlot = 1;
                itemType = Type.armor;
                break;
            case InventoryItem.Item.footballSuit:
                maxCountPerSlot = 1;
                itemType = Type.armor;
                break;
            case InventoryItem.Item.heavyJacket:
                maxCountPerSlot = 1;
                itemType = Type.armor;
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
