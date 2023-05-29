using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool canMoveOn = true;
    public bool canBuildOn;

    public List<InventoryItem> item = new List<InventoryItem>();
    public Vector3 tilePosition;

    public Sprite inactiveSprite;
    public tileSpecial tileType;
    public GameObject count;
    // Simply handles different bools to let tile checkers know if tiles can be built on or not
    public enum tileSpecial 
    {
        none,
        hasItem,
        lever,
        vent,
        interactable,
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
        if(tileType == tileSpecial.hasItem)
        {
            Inventory inventory = GameObject.Find("Player").GetComponent<Inventory>();
            SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
            sprite.sprite = inactiveSprite;
            GameObject.Find("Player").GetComponent<PlayerMovement>().ReturnToIdle();
            foreach (InventoryItem item in item)
            {
                inventory.inventory.Add(item);
            }
            item = null;
            tileType = tileSpecial.none;
            Destroy(count);
        }
        if(tileType == tileSpecial.vent)
        {
            GameObject player = GameObject.Find("Player");
            //change sprite
            //change tile to move to and current tile
            //need new Tile variable to define the other end
            //relocate player
        }
    }
}
