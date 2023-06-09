using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour
{
    public bool canMoveOn = true;
    public bool canBuildOn;

    public List<InventoryItem> itemWithin = new List<InventoryItem>();
    public Sprite itemSprite;
    public Tile connectedTile;
    public Vector3 tilePosition;

    public Sprite otherSprite;
    public tileSpecial tileType;
    // Simply handles different bools to let tile checkers know if tiles can be built on or not
    public enum tileSpecial 
    {
        //Generic
        none, //
        hasItem, //
        vent, //
        //Chests
        ironChest,
        lockedIronChest,
        goldChest,//
        onFire,  //
        //Door
        //Hazard
        ironDoor, //
        //Barrel
        barrel,  // 

    };
    public bool ReturnCanMove()
    {
        return canMoveOn;
    }
    public bool ReturnCanBuild()
    {
        return canBuildOn;
    }
    public void SwitchCanBuild()
    {
        canBuildOn = !canBuildOn;
    }
    public void SwitchCanMove()
    {
        canMoveOn = !canMoveOn;
    }
   
    public void Interaction()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().ReturnToIdle();
        switch (tileType)
        {
            case tileSpecial.hasItem:
                Inventory inventory = GameObject.Find("Player").GetComponent<Inventory>();

                SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
                sprite.sprite = otherSprite;
                int c = 0;
                foreach (InventoryItem item in itemWithin)
                {
                    inventory.inventory.Add(item);
                    item.FindCorrectUI(+1);
                    c++;
                }
                PopUpUI(c);
                itemWithin = null;
                tileType = tileSpecial.none;
                break;
            case tileSpecial.vent:
                GameObject player = GameObject.Find("Player");
                SpriteRenderer openedsprite = this.GetComponent<SpriteRenderer>();
                openedsprite.sprite = otherSprite;
                Vector3 newPos = new Vector3(connectedTile.tilePosition.x, connectedTile.tilePosition.y + 0.5f, 1f);
                player.transform.position = newPos;
                player.GetComponent<PlayerMovement>().currentTile = connectedTile;
                connectedTile.GetComponent<SpriteRenderer>().sprite = connectedTile.otherSprite;
                break;
            case tileSpecial.ironDoor:
                Inventory invent = GameObject.Find("Player").GetComponent<Inventory>();
                foreach (InventoryItem item in invent.inventory)
                {
                    if(item.item == InventoryItem.Item.ironKey)
                    {
                        connectedTile.canMoveOn = true;
                        connectedTile.gameObject.GetComponent<SpriteRenderer>().sprite = connectedTile.otherSprite;
                        invent.inventory.Remove(item);
                        item.FindCorrectUI(-1);
                        tileType = tileSpecial.none;
                        break;
                    }
                }
                break;
            case tileSpecial.onFire:
                //Dont need to do anything, potion buff handles this
                break;
            case tileSpecial.barrel:
                Inventory inven = GameObject.Find("Player").GetComponent<Inventory>();
                foreach (InventoryItem item in inven.inventory)
                {
                    if (item.item == InventoryItem.Item.emptyBottle)
                    {
                        item.FindCorrectUI(-1);
                        inven.inventory.Remove(item);
                        inven.inventory.Add(itemWithin[0]);
                        itemWithin[0].FindCorrectUI(+1);
                        itemWithin = null;
                        break;
                    }
                }
                PopUpUI(1);
                SpriteRenderer barrelSprite = this.GetComponent<SpriteRenderer>();
                barrelSprite.sprite = otherSprite;
                tileType = tileSpecial.none;
                break;
            case tileSpecial.ironChest:
                SpriteRenderer chestsprite = connectedTile.GetComponent<SpriteRenderer>();
                chestsprite.sprite = otherSprite;
                LevelManager manager = GameObject.Find("LevelSetup").GetComponent<LevelManager>();
                manager.chest--;
                manager.CheckChest();
                tileType = tileSpecial.none;
                break;
            case tileSpecial.lockedIronChest:
                Inventory inve = GameObject.Find("Player").GetComponent<Inventory>();
                foreach (InventoryItem item in inve.inventory)
                {
                    if (item.item == InventoryItem.Item.silverKey)
                    {
                        item.FindCorrectUI(-1);
                        connectedTile.gameObject.GetComponent<SpriteRenderer>().sprite = connectedTile.otherSprite;
                        inve.inventory.Remove(item);
                        LevelManager Lmanager = GameObject.Find("LevelSetup").GetComponent<LevelManager>();
                        Lmanager.chest--;
                        Lmanager.CheckChest();
                        tileType = tileSpecial.none;
                        break;
                    }
                }
                break;
            case tileSpecial.goldChest:
                Inventory inv = GameObject.Find("Player").GetComponent<Inventory>();
                foreach (InventoryItem item in inv.inventory)
                {
                    if (item.item == InventoryItem.Item.goldKey)
                    {
                        item.FindCorrectUI(-1);
                        connectedTile.gameObject.GetComponent<SpriteRenderer>().sprite = connectedTile.otherSprite;
                        inv.inventory.Remove(item);
                        LevelManager Lmanager = GameObject.Find("LevelSetup").GetComponent<LevelManager>();
                        Lmanager.chest--;
                        Lmanager.CheckChest();
                        tileType = tileSpecial.none;
                        break;
                    }
                }
                break;
        }
    }

    private void PopUpUI(int c)
    {
        GameObject popUp = GameObject.Find("Player").transform.Find("PlayerCanvas").transform.Find("popUp").gameObject;
        popUp.transform.Find("icon").GetComponent<Image>().sprite = itemSprite;
        popUp.transform.Find("number").GetComponent<TMP_Text>().text = "+" + c.ToString();
        Animator pop = popUp.GetComponent<Animator>();
        pop.Play("pop_up");
    }

    //private IEnumerator EndLevel()
    //{
    //    yield return new WaitForSeconds(2f);
    //    LevelManager manager = GameObject.Find("LevelSetup").GetComponent<LevelManager>();
    //    manager.Win();
    //}
}
