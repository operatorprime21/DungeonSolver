using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureMove : MonoBehaviour
{
    public List<GameObject> tileToCheck = new List<GameObject>();
    private GameObject startTile;
    public int tileCost;
    private bool allSlotsFree;
    //For testing only
    public Vector3 startingPos;
    //For testing only
    
    private void OnMouseDrag()
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 itemPos = cam.ScreenToWorldPoint(mousePos);
        TileManager manager = GameObject.Find("Tiles").GetComponent<TileManager>();
        if(manager.TileHovered != null)
        {
            GameObject tile = GameObject.Find(manager.TileHovered);
            startTile = tile;
            Vector3 offSet = new Vector3(tile.transform.position.x - 0.5f, tile.transform.position.y -0.5f, 0);
            this.transform.position = offSet;
            if(tileToCheck.Count == tileCost)
            {
                CheckTileValid();
            }
        }
        else
        {
            startTile = null;
            this.transform.position = new Vector3(itemPos.x, itemPos.y, 0f);
            allSlotsFree = false;
        }
    }
    void CheckTileValid()
    {
        foreach(GameObject tile in tileToCheck)
        {
            Tile validity = tile.GetComponent<Tile>();
            if (validity.ReturnValid() == true)
            {
                this.transform.Find("Square").GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0.5f);

                allSlotsFree = true;
            }
            else
            {
                this.transform.Find("Square").GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
                allSlotsFree = false;
                break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tile")
        {
            tileToCheck.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tile")
        {
            tileToCheck.Remove(collision.gameObject);
        }
    }

    private void OnMouseUp()
    {
        if(allSlotsFree == true)
        {
            Debug.Log("Successfully placed");
            foreach (GameObject slot in tileToCheck)
            {
                slot.GetComponent<Tile>().SwitchValid();
            }
        }
        else
        {
            Debug.Log("Not enough slots to place");
            this.transform.position = startingPos;
        }
    }
    private void OnMouseDown()
    {
        foreach (GameObject slot in tileToCheck)
        {
            slot.GetComponent<Tile>().SwitchValid();
        }
    }
    
}
