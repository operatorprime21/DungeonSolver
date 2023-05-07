using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool canMoveOn = true;
    public bool canBuildOn;
    public bool hasItem;
    public List<InventoryItem> item = new List<InventoryItem>();
    public Vector3 tilePosition;

    public Sprite noItemSprite;
    // Simply handles different bools to let tile checkers know if tiles can be built on or not

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
}
