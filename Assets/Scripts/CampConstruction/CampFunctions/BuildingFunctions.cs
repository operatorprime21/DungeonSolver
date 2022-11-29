using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuildingFunctions : MonoBehaviour
{
    // Start is called before the first frame update

    public int reqPer;
    public int reqAdp;
    public int reqInw;

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

    public GameObject storageUI;
    public GameObject recipeList;
    public enum BuildType
    { 
        host,             //Done
        cellGenerator,    //Done
        foodGenerator,    //Basic done, need to implement Inventory for more depth
        fanStations,      //For later progression
        fence,            //Need to Implement Invasion Events first
        storage,          //Need to Implement Inventory first
        meleeWorkShop,    //Need to Implement Inventory/Storage buildings first
        firearmWorkshop,  //Need to Implement Inventory/Storage buildings first
        armoryWorkshop,   //Need to Implement Inventory/Storage buildings first
        trainingStation1, //Done
        trainingStation2, //Done
        trainingStation3, //Done
    }


    public bool isWorking;
    void Start()
    {
        timer = this.gameObject.transform.Find("Canvas").transform.Find("timer").GetComponent<TMP_Text>();
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
        int sumPer = 0;
        int sumAdp = 0;
        int sumInw = 0;
        foreach (GameObject surv in survivor)
        {
            if (surv != null)
            {
                SurvivorBase survStat = surv.GetComponent<SurvivorBase>();
                sumPer += survStat.body;
                sumInw += survStat.mind;
                sumAdp += survStat.soul;
            }
            else return false;
        }

        if (sumPer >= reqPer)
        {
            if (sumAdp >= reqAdp)
            {
                if (sumInw >= reqInw)
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
            isWorking = true;
            CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
            cam.buildingInfo.SetActive(false);
            StartCoroutine(TimeToFinishFunction(timeToGen));
        }
        else
        {
            Debug.Log("Not enough stats");
        }
    }

    public IEnumerator TimeToFinishFunction(float time)
    {
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
        switch (this.type)
        {
            case BuildType.cellGenerator:
                inv.ChangeCell(amountToIncrease);
                break;
            case BuildType.foodGenerator:
                inv.ChangeFood(amountToIncrease);
                //Consume different type of resources from a recipe to produce higher amounts. 
                break;
            case BuildType.fanStations:
                //Clears fog from a fixed number of tiles (enable validity). 
                break;
            case BuildType.storage:
                //Spawns a new box inventory
                break;
            case BuildType.meleeWorkShop:
                //Works like crafting. Requires special recipes only accessible from these buildings. Consumes resources and makes only 1 at a time
                break;
            case BuildType.firearmWorkshop:
                //Works like crafting. Requires special recipes only accessible from these buildings. Consumes resources and makes only 1 at a time
                break;
            case BuildType.armoryWorkshop:
                //Works like crafting. Requires special recipes only accessible from these buildings. Consumes resources and makes only 1 at a time
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
}
