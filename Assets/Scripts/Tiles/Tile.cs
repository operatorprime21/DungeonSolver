using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool canMoveOn = true;
    public bool canBuildOn;

    public List<InventoryItem> item = new List<InventoryItem>();
    public Tile connectedTile;
    public Vector3 tilePosition;

    public Sprite otherSprite;
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
        end,
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
        switch (tileType)
        {
            case tileSpecial.hasItem:
                Inventory inventory = GameObject.Find("Player").GetComponent<Inventory>();
                SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
                sprite.sprite = otherSprite;
                GameObject.Find("Player").GetComponent<PlayerMovement>().ReturnToIdle();
                foreach (InventoryItem item in item)
                {
                    inventory.inventory.Add(item);
                    item.FindCorrectUI(+1);
                }
                item = null;
                tileType = tileSpecial.none;
                Destroy(count);
                break;
            case tileSpecial.vent:
                GameObject player = GameObject.Find("Player");
                SpriteRenderer openedsprite = this.GetComponent<SpriteRenderer>();
                openedsprite.sprite = otherSprite;
                Vector3 newPos = new Vector3(connectedTile.tilePosition.x, connectedTile.tilePosition.y + 0.5f, 1f);
                player.transform.position = newPos;
                player.GetComponent<PlayerMovement>().currentTile = connectedTile;
                connectedTile.GetComponent<SpriteRenderer>().sprite = connectedTile.otherSprite;
                GameObject.Find("Player").GetComponent<PlayerMovement>().ReturnToIdle();
                break;
            case tileSpecial.end:
                GameObject.Find("Player").GetComponent<PlayerMovement>().ReturnToIdle();
                SpriteRenderer chestsprite = connectedTile.GetComponent<SpriteRenderer>();
                chestsprite.sprite = otherSprite;
                StartCoroutine(EndLevel());
                break;

        }
    }

    private IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(2f);
        LevelManager manager = GameObject.Find("LevelSetup").GetComponent<LevelManager>();
        manager.ReachedGoal();
    }
}
