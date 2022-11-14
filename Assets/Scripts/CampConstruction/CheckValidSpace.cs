using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckValidSpace : MonoBehaviour
{
    public List<GameObject> tileToCheck = new List<GameObject>();
    private GameObject startTile;
    public List<GameObject> sprites = new List<GameObject>();
    public int tileCost;
    private bool allSlotsFree;

    public GameObject buildingPrefab;
    //For testing only
    public Vector3 startingPos;
    //For testing only

    private bool isChecking;
    private void Awake()
    {
        isChecking = false;
    }
    private void OnMouseDrag()
    {
        
        if(isChecking == true)
        {
            Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 itemPos = cam.ScreenToWorldPoint(mousePos);
            TileManager manager = GameObject.Find("Tiles").GetComponent<TileManager>();
            if (manager.TileHovered != null)
            {
                GameObject tile = GameObject.Find(manager.TileHovered);
                startTile = tile;
                Vector3 offSet = new Vector3(tile.transform.position.x - 0.5f, tile.transform.position.y - 0.5f, 0);
                this.transform.position = offSet;
                if (tileToCheck.Count == tileCost)
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
    }
    void CheckTileValid()
    {
        foreach(GameObject tile in tileToCheck)
        {
            Tile validity = tile.GetComponent<Tile>();
            if (validity.ReturnValid() == true)
            {
                foreach(GameObject square in sprites)
                {
                    square.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0.5f);
                }
                allSlotsFree = true;
            }
            else
            {
                foreach (GameObject square in sprites)
                {
                    square.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
                }
                allSlotsFree = false;
                break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tile" && isChecking == true)
        {
            tileToCheck.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tile" && isChecking == true)
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
            GameObject buttonChangeMode = GameObject.Find("Main Canvas").transform.Find("ChangeCamMode").gameObject;
            buttonChangeMode.SetActive(true);
            GameObject buttonOpenBP = GameObject.Find("Main Canvas").transform.Find("OpenBPList").gameObject;
            buttonOpenBP.SetActive(true);

            Destroy(this.gameObject);
            //Start coroutine using build time
            //Spawn the actual building prefab

        }
        else
        {
            Debug.Log("Not enough slots to place");
            this.transform.position = startingPos;

            GameObject buttonChangeMode = GameObject.Find("Main Canvas").transform.Find("ChangeCamMode").gameObject;
            buttonChangeMode.SetActive(true);

            GameObject buttonOpenBP = GameObject.Find("Main Canvas").transform.Find("OpenBPList").gameObject;
            buttonOpenBP.SetActive(true);

            Destroy(this.gameObject);
        }
    }
    private void OnMouseDown()
    {
        isChecking = true;
        foreach (GameObject slot in tileToCheck)
        {
            slot.GetComponent<Tile>().SwitchValid();
        }
    }
    
}
