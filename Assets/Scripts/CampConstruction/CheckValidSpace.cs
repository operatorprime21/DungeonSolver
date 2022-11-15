using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private bool buildIsStarted;
    public float timeToBuild;
    public TMP_Text timer;

    public int fruitCost;
    private void Awake()
    {
        isChecking = false;
        buildIsStarted = false;
    }
    private void OnMouseDrag()
    {
        if(isChecking == true && buildIsStarted == false)
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
        if(buildIsStarted == false)
        {
            if (allSlotsFree == true)
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
                StartCoroutine(BuildTimer(timeToBuild));
                buildIsStarted = true;
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
    }
    private void OnMouseDown()
    {
        if(buildIsStarted == false)
        {
            isChecking = true;
            foreach (GameObject slot in tileToCheck)
            {
                slot.GetComponent<Tile>().SwitchValid();
            }
        }
        else
        {
            GameObject.Find("Main Canvas").GetComponent<Canvas>().transform.Find("SpeedUp").gameObject.SetActive(true);
            TMP_Text confirm = GameObject.Find("Main Canvas").transform.Find("SpeedUp").transform.Find("confirm").GetComponent<TMP_Text>();
            confirm.text = "Speed up build using " + fruitCost.ToString() + " Cornea Fruits?";
            GameObject.Find("UIManager").GetComponent<CamMode>().AssignSpeedUpBuild(buildingPrefab, -fruitCost, this.gameObject);
        }
    }

    IEnumerator BuildTimer(float time)
    {
        this.transform.Find("Canvas").GetComponent<Canvas>().overrideSorting = true;
        StartCoroutine(SetTimer(time));
        yield return new WaitForSeconds(time);
        Debug.Log("Finished Building!");
        Instantiate(buildingPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    IEnumerator SetTimer(float time)
    {
        if (time >= 0)
        {
            timer.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time -= 1;
            StartCoroutine(SetTimer(time));
        }
        else
        {
            timer.text = null;
        }
    }
    
}
