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
    public int amountResToGen;
    public int fruitCost;
    public BuildType type;
    public TMP_Text timer;

    public List<InventoryItem> resReqFor = new List<InventoryItem>();
    public enum BuildType
    { 
        host,
        cellGenerator,
        foodGenerator,
        fanStations,
        fence,
        storage,
        meleeWorkShop,
        firearmWorkshop,
        trainingStation1,
        trainingStation2,
        trainingStation3,

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
            if(surv != null)
            {
                SurvivorBase survStat = surv.GetComponent<SurvivorBase>();
                sumPer += survStat.perseverance;
                sumInw += survStat.ironWill;
                sumAdp += survStat.adaptability;
            }
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
        ResourceHolder inv = GameObject.Find("InventoryManager").GetComponent<ResourceHolder>();
        switch (this.type)
        {
            case BuildingFunctions.BuildType.cellGenerator:
                inv.ChangeCell(amountResToGen);
                break;
            case BuildingFunctions.BuildType.foodGenerator:
                inv.ChangeFood(amountResToGen);
                break;
            case BuildingFunctions.BuildType.fanStations:
                //Clears fog from a fixed area. 
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
