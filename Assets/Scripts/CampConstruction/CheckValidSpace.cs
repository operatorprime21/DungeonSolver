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
                GameObject buttonOpenSurvList = GameObject.Find("Main Canvas").transform.Find("ManageSurvivor").gameObject;
                buttonOpenSurvList.SetActive(true);

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
                GameObject buttonOpenSurvList = GameObject.Find("Main Canvas").transform.Find("ManageSurvivor").gameObject;
                buttonOpenSurvList.SetActive(true);

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
            GameObject.Find("Main Canvas").GetComponent<Canvas>().transform.Find("SpeedUpBuilding").gameObject.SetActive(true);
            TMP_Text confirm = GameObject.Find("Main Canvas").transform.Find("SpeedUpBuilding").transform.Find("confirm").GetComponent<TMP_Text>();
            confirm.text = "Speed up build using " + fruitCost.ToString() + " Cornea Fruits?";
            GameObject.Find("UIManager").GetComponent<CamMode>().AssignSpeedUpBuild(buildingPrefab, -fruitCost, this.gameObject);
        }
    }

    IEnumerator BuildTimer(float time)
    {
        this.transform.Find("Canvas").GetComponent<Canvas>().overrideSorting = true;
        StartCoroutine(SetTimer(time));
        yield return new WaitForSeconds(time);
        CompleteBuilding();
    }

    public void CompleteBuilding()
    {
        GameObject newBuilding = Instantiate(buildingPrefab, this.transform.position, Quaternion.identity);
        BuildingFunctions function = newBuilding.GetComponent<BuildingFunctions>();
        if (function.type == BuildingFunctions.BuildType.storage)
        {
            GameObject box = CreateNewUI(function, function.storageToSpawn);
            function.AssignStorage(box);
            Inventory inven = GameObject.Find("InventoryManager").GetComponent<Inventory>();
            inven.storages.Add(box); 
        }
        else if (function.type == BuildingFunctions.BuildType.foodGenerator || function.type == BuildingFunctions.BuildType.armoryWorkshop || function.type == BuildingFunctions.BuildType.meleeWorkShop || function.type == BuildingFunctions.BuildType.firearmWorkshop)
        {
            GameObject box = CreateNewUI(function, function.recipeListToSpawn);
            function.AssignRecipe(box);
        }
        Destroy(this.gameObject);
    }

    private GameObject CreateNewUI(BuildingFunctions function, GameObject uiToMake)
    {
        Canvas mainCv = GameObject.Find("Main Canvas").GetComponent<Canvas>();
        Vector3 UIpos = mainCv.gameObject.transform.Find("boxUIpos").transform.position;
        GameObject box = Instantiate(uiToMake, UIpos, Quaternion.identity, mainCv.transform);
        
        box.SetActive(false);
        return box;
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
