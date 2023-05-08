using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Item item;
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
        //Necessities/Crafting materials/Resources
        wood,     //Found
        //cloth,   //Found
        //stick,   //Crafted or Found
        //plank,   //Crafted or Found
        //ammo,     //Crafted or Found
        //nail,  //Found 
        //emptyBottle,   //Late Release
        //arrow,    //Crafted
        //bandage,    //Crafted

        //gunPowder,    //Found
        //scrapMetal,   //Found
        //ductTape,     //Late Release
        //rope,   //Found 
        //pipe,   //Found or Crafted
        //plasticScrap,    //Found

        //potato,   //Found
        //cannedMeat,    //Found or Built
        //cannedFish,   //Late Release
        //bread,   //Found
        //glass,    //Late Release 
        //egg,    //Late Release
        //wheat,    //Late Release
        //rawMeat,   //Late Release
        //medicalKit,   //Found or Built
        //painKillers,    //Late Release
        //lockpick,    //Late Release

        //batteryCell,  //Found
        //gasTank,   //Late Release
        //wires,    //Found
        //circuitBoard,   //Crafted
        //cogs,   //Late Release
        //rubberStraps,   //Late Release
        //copperNickle,    //Late Release

        ////Melee
        //woodenPlank,  //Crafted
        //crowBar,   //Late Release
        //policeBaton,  //Found
        //knife,   //Found or Built
        //shovel,    //Built
        //machete,    //Late Release
        //bigKnife,    //Late Release
        //rake,     //Late Release
        //hammer,   //Late Release
        //largeHammer,    //Late Release

        ////Ranged 
        //pistol, //Found or Built
        //crossbow, //Crafted
        //leverActionRifle,  //Built
        //sawedOffShotgun,  //Late Release
        //automaticPistol,   //Late Release

        ////Armor
        //sweater,   //Late Release
        //bulletProofVest,   //Found
        //hazmatSuit,   //Late Release
        //footballSuit,  //Late Release
        //heavyJacket,   //Late Release

        ////Misc. Late Release
        //bagExtender1,  
        //bagExtender2,
        //flashlight1,
        //flashlight2,

    }

    //public enum Type
    //{ 
    //    resource,
    //    consumable,
    //    misc,
    //    weapon,
    //    armor
    //}


    private void Start()
    {
        //canvas = GameObject.Find("Main Canvas");
        //player = GameObject.Find("Player");
        //cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        //count.text = currentCount.ToString();
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
