using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuildingFunctions : MonoBehaviour
{
    // Start is called before the first frame update

    public int redBody;
    public int reqSoul;
    public int reqMind;

    public int reqCell;
    public List<GameObject> survivor = new List<GameObject>();
    public int maxOccupant;
    public float timeToGen;
    public int amountToIncrease;
    public int fruitCost;
    public BuildType type;
    public TMP_Text timer;
    public int hungerDrain;

    public List<InventoryItem> resReqFor = new List<InventoryItem>();
    public List<int> amountResReq = new List<int>();

    public GameObject storageToSpawn;
    public GameObject storageUI;

    public GameObject recipeListToSpawn;
    public GameObject recipeList;
    public Recipe recipeWorking;
    public enum BuildType
    { 
        host,             //Done
        cellGenerator,    //Done
        foodGenerator,    //Crafting done, just need to make the actual recipes now
        fanStations,      //For later progression
        fence,            //Might actually skip this
        storage,          //Done
        meleeWorkShop,    //Crafting done, just need to make the actual recipes now
        firearmWorkshop,  //Crafting done, just need to make the actual recipes now
        armoryWorkshop,   //Crafting done, just need to make the actual recipes now
        trainingStation1, //Done
        trainingStation2, //Done
        trainingStation3, //Done
    }

    public functionType fType;
    public enum functionType
    {
        craft,
        others,
    }



    public bool isWorking;
    void Start()
    {
        timer = this.gameObject.transform.Find("Canvas").transform.Find("timer").GetComponent<TMP_Text>();
        if(this.type == BuildType.storage)
        {
            Inventory inven = GameObject.Find("InventoryManager").GetComponent<Inventory>();
            inven.storages.Add(storageUI);
            storageUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        //Open the building functionality menu (and exit button)
        //
        CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
        cam.buildingToAssign = this.gameObject;
        if (isWorking == false)
        {
            if(cam.isInPlacementMode == true)
            {
                if(cam.survivorToPlace != null)
                {
                    AddOccupant(cam.survivorToPlace);
                    cam.survivorToPlace = null;
                    cam.buildingToAssign = null;
                    cam.isInPlacementMode = false;
                }
            }
            else
            {
                cam.buildingInfo.SetActive(true);
                cam.buttonExitMenu.SetActive(true);
                cam.buttonCamMode.SetActive(false);
                cam.buttonOpenBPList.SetActive(false);
                cam.buttonOpenSurvList.SetActive(false);

                cam.UItoClose.Add(cam.buildingInfo);
                cam.UItoClose.Add(cam.buttonExitMenu);
                cam.UItoOpen.Add(cam.buttonOpenSurvList);
                cam.UItoOpen.Add(cam.buttonOpenBPList);
                cam.UItoOpen.Add(cam.buttonCamMode);
            }
        }
        else
        {
            TMP_Text confirmText = GameObject.Find("Main Canvas").transform.Find("SpeedUpFunction").gameObject.transform.Find("confirm").GetComponent<TMP_Text>();
            cam.fruitCost = fruitCost;
            confirmText.text = "Speed up process by " + fruitCost.ToString() + " Cornea Fruits?";
            cam.confirmSpeedUpFunction.SetActive(true);
        }
    }

    public bool CheckStatReq()
    {
        int sumBody = 0;
        int sumSoul = 0;
        int sumMind = 0;
        foreach (GameObject surv in survivor)
        {
            if (surv != null)
            {
                SurvivorBase survStat = surv.GetComponent<SurvivorBase>();
                sumBody += survStat.body;
                sumMind += survStat.mind;
                sumSoul += survStat.soul;
            }
            else return false;
        }

        if (sumBody >= redBody)
        {
            if (sumSoul >= reqSoul)
            {
                if (sumMind >= reqMind)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        else return false;
    }

    public void AddOccupant(GameObject surv)
    {
        if(survivor.Count < maxOccupant)
        {
            survivor.Add(surv);
            CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
            cam.TurnOffAssignButton();
            SurvivorBase survInfo = surv.GetComponent<SurvivorBase>();
            survInfo.assignedBuilding = this.gameObject;
        }
        else
        {
            Debug.Log("Full");
        }
    }


    public void CheckFunctionCost()
    {
        ResourceHolder inv = GameObject.Find("InventoryManager").GetComponent<ResourceHolder>();
        
        if (CheckStatReq() == true && inv.powerCell >= reqCell)
        {
            inv.ChangeCell(-reqCell);
            foreach(GameObject surv in survivor)
            {
                SurvivorBase survInfo = surv.GetComponent<SurvivorBase>();
                survInfo.currentHunger -= hungerDrain;
                survInfo.UI.transform.Find("unassignBuilding").gameObject.SetActive(false);
            }
            
            CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
            cam.buildingInfo.SetActive(false);
            if(this.fType == functionType.craft)
            {
                recipeList.SetActive(true);

                cam.buttonExitMenu.SetActive(true);
                cam.UItoClose.Add(recipeList);
                cam.UItoClose.Add(cam.buttonExitMenu);
            }
            else
            {
                StartCoroutine(TimeToFinishFunction(timeToGen));
            }
            
        }
        else
        {
            Debug.Log("Not enough stats");
        }
    }

    public IEnumerator TimeToFinishFunction(float time)
    {
        isWorking = true;
        StartCoroutine(SetTimer(time));
        yield return new WaitForSeconds(time);
        isWorking = false;
        Function();

    }

    public void Function()
    {
        foreach (GameObject surv in survivor)
        {
            SurvivorBase survInfo = surv.GetComponent<SurvivorBase>();
            survInfo.UI.transform.Find("unassignBuilding").gameObject.SetActive(true);
        }
        ResourceHolder inv = GameObject.Find("InventoryManager").GetComponent<ResourceHolder>();
        Inventory totalInv = GameObject.Find("InventoryManager").GetComponent<Inventory>();
        CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
        Crafting crafter = GameObject.Find("InventoryManager").GetComponent<Crafting>();
        Slot slot = crafter.FindEmptySlot(totalInv);
        switch (this.type)
        {
            case BuildType.cellGenerator:
                inv.ChangeCell(amountToIncrease);
                break;
            case BuildType.foodGenerator:
                inv.ChangeFood(amountToIncrease);
                break;
            case BuildType.fanStations:
                //Clears fog from a fixed number of tiles (enable validity). 
                break;
            case BuildType.storage:
                storageUI.SetActive(true);
                totalInv.ToggleInventory();
                cam.buttonExitMenu.SetActive(true);

                cam.UItoClose.Add(totalInv.inventoryUI);
                cam.UItoClose.Add(storageUI);
                cam.UItoClose.Add(cam.buttonExitMenu);
                //Opens the box ui
                break;
            case BuildType.meleeWorkShop:
                //Works like crafting. Requires special recipes only accessible from these buildings. Consumes resources and makes only 1 at a time
                crafter.MakeItem(recipeWorking.itemToMake, slot, 1);
                break;
            case BuildType.firearmWorkshop:
                //Works like crafting. Requires special recipes only accessible from these buildings. Consumes resources and makes only 1 at a time
                crafter.MakeItem(recipeWorking.itemToMake, slot, 1);
                break;
            case BuildType.armoryWorkshop:
                //Works like crafting. Requires special recipes only accessible from these buildings. Consumes resources and makes only 1 at a time
                crafter.MakeItem(recipeWorking.itemToMake, slot, 1);
                break;
            case BuildType.trainingStation1:
                foreach (GameObject surv in survivor)
                {
                    SurvivorBase survStat = surv.GetComponent<SurvivorBase>();
                    survStat.soul += amountToIncrease;
                    SurvivorUI survUI = survStat.UI.GetComponent<SurvivorUI>();
                    survUI.SetUIstat();
                }    
                break;
            case BuildType.trainingStation2:
                foreach (GameObject surv in survivor)
                {
                    SurvivorBase survStat = surv.GetComponent<SurvivorBase>();
                    survStat.body += amountToIncrease;
                    SurvivorUI survUI = survStat.UI.GetComponent<SurvivorUI>();
                    survUI.SetUIstat();
                }
                break;
            case BuildType.trainingStation3:
                foreach (GameObject surv in survivor)
                {
                    SurvivorBase survStat = surv.GetComponent<SurvivorBase>();
                    survStat.mind += amountToIncrease;
                    SurvivorUI survUI = survStat.UI.GetComponent<SurvivorUI>();
                    survUI.SetUIstat();
                }
                break;
        }
    }

    public IEnumerator SetTimer(float time)
    {
        if (time >= 0 && isWorking == true)
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

    public void AssignStorage(GameObject ui)
    {
        storageUI = ui;
    }

    public void AssignRecipe(GameObject ui)
    {
        recipeList = ui;
    }
}
