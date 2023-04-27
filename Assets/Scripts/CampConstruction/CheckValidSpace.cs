using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckValidSpace : MonoBehaviour
{
    public List<GameObject> tileToCheck = new List<GameObject>(); //List of Tiles that needs to be go through different processes to check
    private GameObject startTile; //Determines the most left, lowest tile to place the building.
    public List<GameObject> sprites = new List<GameObject>(); //Low alpha images of the buildings to simulate the shape of the buildings as visual feedback when checking tiles
    public int tileCost; //Makes sure the tiles within are handled properly
    private bool allSlotsFree; //Final check to see if there is enough space to build

    public GameObject buildingPrefab;
    //For testing only
    public Vector3 startingPos;
    //For testing only

    private bool isChecking; //Just to make sure the checker prefab doesnt mess up on spawn
    //private bool buildIsStarted; //Begins build time
    //public float timeToBuild; //Build time
    public TMP_Text timer;

    public int fruitCost;
    private void Awake()
    {
        isChecking = false;
        //buildIsStarted = false;
    }
    private void OnMouseDrag()
    {
        if(isChecking == true /*buildIsStarted == false*/)
        {
            Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 itemPos = cam.ScreenToWorldPoint(mousePos);  //Detect mouse position
            TileManager manager = GameObject.Find("Tiles").GetComponent<TileManager>();
            if (manager.TileHovered != null) //Finds the tile mouse is hovering over
            {
                GameObject tile = GameObject.Find(manager.TileHovered);
                startTile = tile; //Set tile hovered over as the place to move the checker
                Vector3 offSet = new Vector3(tile.transform.position.x - 0.5f, tile.transform.position.y - 0.5f, 0); //Offsets the position to the lowest left corner
                this.transform.position = offSet; //Snaps the checker in place to ensure the building is in its correct tiles
                if (tileToCheck.Count == tileCost)
                {
                    CheckTileValid();
                }
            }
            else
            {
                startTile = null;
                this.transform.position = new Vector3(itemPos.x, itemPos.y, 0f);
                allSlotsFree = false;  //Makes the checker move with the mouse even without any tile
            }
        }
    }
    void CheckTileValid()
    {
        foreach(GameObject tile in tileToCheck)
        {
            Tile validity = tile.GetComponent<Tile>();
            if (validity.ReturnCanBuild() == true) //Returns if the tile is free, if it does, continue for every other tile
            {
                foreach(GameObject square in sprites)
                {
                    square.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);  //as long as all tiles within the list is free, make the image green
                }
                allSlotsFree = true; //Only sets after every tile is free
            }
            else
            {
                foreach (GameObject square in sprites) //If just one tile isn't free, make the image red and break to prevent any later tile making it green
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
            tileToCheck.Add(collision.gameObject);  //Adds tiles to the list on collision
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tile" && isChecking == true)
        {
            tileToCheck.Remove(collision.gameObject); //Removes tiles out of the list on exitting the collider
        }
    }
    private void OnMouseUp()
    {
        if (allSlotsFree == true)
        {
            foreach (GameObject slot in tileToCheck)
            {
                slot.GetComponent<Tile>().SwitchCanBuild(); //Make sure every tile that it is placed on are no longer available
                slot.GetComponent<Tile>().canMoveOn = true;
            }
            foreach (GameObject square in sprites)
            {
                square.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            Debug.Log("Cannot place here");
        }
            
    }
    //private void OnMouseUp() 
    //{
    //    if(buildIsStarted == false)
    //    {
    //        if (allSlotsFree == true)  //Deciding to let go lets the checker begin the build timer
    //        {
    //            Debug.Log("Successfully placed");
    //            foreach (GameObject slot in tileToCheck)
    //            {
    //                slot.GetComponent<Tile>().SwitchValid(); //Make sure every tile that it is placed on are no longer available
    //            }
    //            //Find all relevant UI and toggle them accordingly
    //            GameObject buttonChangeMode = GameObject.Find("Main Canvas").transform.Find("button_changeCam").gameObject;
    //            buttonChangeMode.SetActive(true);
    //            GameObject buttonOpenBP = GameObject.Find("Main Canvas").transform.Find("button_building_blueprints").gameObject;
    //            buttonOpenBP.SetActive(true);
    //            GameObject buttonOpenSurvList = GameObject.Find("Main Canvas").transform.Find("button_manage_survivor").gameObject;
    //            buttonOpenSurvList.SetActive(true);

    //            StartCoroutine(BuildTimer(timeToBuild));
    //            buildIsStarted = true; //Begins timer
    //        }
    //        else
    //        {
    //            Debug.Log("Not enough slots to place");
    //            this.transform.position = startingPos;
    //            //Find all relevant UI and toggle them accordingly
    //            GameObject buttonChangeMode = GameObject.Find("Main Canvas").transform.Find("button_changeCam").gameObject;
    //            buttonChangeMode.SetActive(true);
    //            GameObject buttonOpenBP = GameObject.Find("Main Canvas").transform.Find("button_building_blueprints").gameObject;
    //            buttonOpenBP.SetActive(true);
    //            GameObject buttonOpenSurvList = GameObject.Find("Main Canvas").transform.Find("button_manage_survivor").gameObject;
    //            buttonOpenSurvList.SetActive(true);

    //            Destroy(this.gameObject); //Removes the checker on failure
    //        }
    //    }
    //}
    private void OnMouseDown()
    {
          isChecking = true;
          foreach (GameObject slot in tileToCheck)
          {
              slot.GetComponent<Tile>().SwitchCanBuild(); 
          }
        //else //Opens the according UI to ask the user if they want to speed up
        //{
        //    GameObject.Find("Main Canvas").GetComponent<Canvas>().transform.Find("SpeedUpBuild").gameObject.SetActive(true);
        //    Text confirm = GameObject.Find("Main Canvas").transform.Find("SpeedUpBuild").transform.Find("confirm").GetComponent<Text>();
        //    confirm.text = "Speed up build using " + fruitCost.ToString() + " Cornea Fruits?";
        //    GameObject.Find("UIManager").GetComponent<CamMode>().AssignSpeedUpBuild(buildingPrefab, -fruitCost, this.gameObject);
        //}
    }

    //IEnumerator BuildTimer(float time)  //Timer to build the building object. 
    //{
    //    this.transform.Find("Canvas").GetComponent<Canvas>().overrideSorting = true;
    //    StartCoroutine(SetTimer(time));
    //    yield return new WaitForSeconds(time);
    //    CompleteBuilding();
    //}

    //public void CompleteBuilding()
    //{
    //    GameObject newBuilding = Instantiate(buildingPrefab, this.transform.position, Quaternion.identity); //Builds the building on the exact location of the checker. Building is identical to checker  in size
    //    BuildingFunctions function = newBuilding.GetComponent<BuildingFunctions>();
    //    if (function.type == BuildingFunctions.BuildType.storage) //Spawns a new storage and puts it in the master inventory list
    //    { 
    //        GameObject box = CreateNewUI(function, function.storageToSpawn);  
    //        function.AssignStorage(box);
    //        Inventory inven = GameObject.Find("InventoryManager").GetComponent<Inventory>();
    //        inven.storages.Add(box); 
    //    }
    //    else if (function.fType == BuildingFunctions.functionType.craft) //Spawns a new recipe  list and make sure the building owner is correct
    //    {
    //        GameObject box = CreateNewUI(function, function.recipeListToSpawn);
    //        function.AssignRecipe(box);
    //        RecipeHolder recs = box.gameObject.GetComponent<RecipeHolder>();
    //        foreach(Recipe rec in recs.recipes)
    //        {
    //            rec.recipeOwner = newBuilding;
    //        }
    //    }
    //    Destroy(this.gameObject);
    //}

    //private GameObject CreateNewUI(BuildingFunctions function, GameObject uiToMake) //Creating UI elements so that they work properly in the canvas
    //{
    //    Canvas mainCv = GameObject.Find("Main Canvas").GetComponent<Canvas>();
    //    Vector3 UIpos = mainCv.gameObject.transform.Find("boxUIpos").transform.position;
    //    GameObject box = Instantiate(uiToMake, UIpos, Quaternion.identity, mainCv.transform);
        
    //    box.SetActive(false);
    //    return box;
    //}

    //IEnumerator SetTimer(float time) //Timer indicator
    //{
    //    if (time >= 0)
    //    {
    //        timer.text = time.ToString();
    //        yield return new WaitForSeconds(1f);
    //        time -= 1;
    //        StartCoroutine(SetTimer(time));
    //    }
    //    else
    //    {
    //        timer.text = null;
    //    }
    //}
    
}
