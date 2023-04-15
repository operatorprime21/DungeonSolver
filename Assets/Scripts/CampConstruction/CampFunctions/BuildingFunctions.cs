using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class BuildingFunctions : MonoBehaviour
{
    //All Stats required for the building to work
    public int reqBody;
    public int reqSoul;
    public int reqMind; 
    
    public int reqCell; //Amount of consumed Power Cell every time the building begins working
    public List<GameObject> survivor = new List<GameObject>(); //Occupant list
    public int maxOccupant; //Maximum number of survivors that can fit in a single building
    public float timeToGen; //Time required to do any ability
    public int amountToIncrease; //Any changes to units from the ability
    public int fruitCost; //cost to speed up ability
    public BuildType type; //What the building is
    public TMP_Text timer; //Text
    public int hungerDrain; //Amount of Hunger drained from Survivors 

    public GameObject storageToSpawn; //spawns a pre-determined storage size
    public GameObject storageUI; //Makes the building the owner of the contents

    public GameObject recipeListToSpawn; //Spawns a pre-determined list of recipes
    public GameObject recipeList; //Makes the building the owner of the recipe.
    //The reason to spawn multiple UIs like this is that the UI themselves hold the functions to craft the items
    //Having only one that does the work for every building of the same type results in clashing processes
    public Recipe recipeWorking; //Make sure the game knows which recipe the building is working on
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
        trainer,
        storage,
        others,
        cellGen,
    }

    public bool isWorking; //Ensures the building cant run two different processes at once
    void Start()
    {
        timer = this.gameObject.transform.Find("Canvas").transform.Find("timer").GetComponent<TMP_Text>(); //Gets reference to the time disply
        if(this.type == BuildType.storage)
        {
            Inventory inven = GameObject.Find("InventoryManager").GetComponent<Inventory>(); //Gets reference to the inventory manager
            inven.storages.Add(storageUI); //Adds the storage of this to the master inventory for automatic grabbing of any item
            storageUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {

        CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>(); //Get Reference to the UI manager
        cam.buildingToAssign = this.gameObject; //Let the game know that the player wants to do something with this building specifically
        if (isWorking == false)
        {
            if(cam.isInPlacementMode == true)
            {
                if(cam.survivorToPlace != null)
                {
                    AddOccupant(cam.survivorToPlace); //Assign the survivor 
                    cam.survivorToPlace = null; //Reset variables
                    cam.buildingToAssign = null; //Reset variables
                    cam.isInPlacementMode = false; //Exit survivor placing mode
                }
            }
            else //Opens all the UI elements necessary
            {
                cam.buildingInfo.SetActive(true); 
                cam.buttonExitMenu.SetActive(true);
                cam.buttonCamMode.SetActive(false);
                cam.buttonOpenBPList.SetActive(false);
                cam.buttonOpenSurvList.SetActive(false); 
                ChangeInfoUI(); 

                cam.UItoClose.Add(cam.buildingInfo);
                cam.UItoClose.Add(cam.buttonExitMenu);
                cam.UItoOpen.Add(cam.buttonOpenSurvList);
                cam.UItoOpen.Add(cam.buttonOpenBPList);
                cam.UItoOpen.Add(cam.buttonCamMode);
            }
        }
        else
        {
            TMP_Text confirmText = GameObject.Find("Main Canvas").transform.Find("SpeedUpFunction").gameObject.transform.Find("confirm").GetComponent<TMP_Text>();  //Opens UI pop up to confirm spending fruits
            cam.fruitCost = fruitCost; //Assign cell consumption
            confirmText.text = "Speed up process by " + fruitCost.ToString() + " Cornea Fruits?";
            cam.confirmSpeedUpFunction.SetActive(true); //Turn off the pop up
        }
    }

    void ChangeInfoUI()  //Changes all the info in the UI including the sum of all Survivor stats, the number of survivors in the building and what ability the building has
    {
        CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
        int sumBody = 0; 
        int sumSoul = 0;
        int sumMind = 0; 
        Text occupants = cam.buildingInfo.transform.Find("occupancy").GetComponent<Text>();
        occupants.text = "Occupancy: " + survivor.Count.ToString() + "/" + maxOccupant.ToString();
        Text func = cam.buildingInfo.transform.Find("button_function").transform.GetChild(0).GetComponent<Text>();
        if (this.fType == functionType.craft)
        {
            func.text = "Choose Item to build";
        }
        else if(this.fType == functionType.storage)
        {
            func.text = "Open storage";
        }
        else if (this.fType == functionType.trainer)
        {
            func.text = "Train survivor";
        }
        else if (this.fType == functionType.cellGen)
        {
            func.text = "Generate power";
        }

        foreach (GameObject surv in survivor)
        {
            if (surv != null)
            {
                SurvivorBase survStat = surv.GetComponent<SurvivorBase>();
                sumBody += survStat.body;
                sumMind += survStat.mind;
                sumSoul += survStat.soul;
            }
        }

        for (int s = 1; s<4; s++)
        {
            Text statReq = cam.buildingInfo.transform.Find("statReq ("+ s +")").GetComponent<Text>();
            Text sumStat = cam.buildingInfo.transform.Find("statCur ("+ s +")").GetComponent<Text>();
            if (s == 1)
            {
                statReq.text = "/" + reqBody.ToString();
                sumStat.text = "Total Body: " + sumBody.ToString();
            }
            if (s == 2)
            {
                statReq.text = "/" + reqSoul.ToString();
                sumStat.text = "Total Body: " + sumSoul.ToString();
            }
            if (s == 3)
            {
                statReq.text = "/" + reqMind.ToString();
                sumStat.text = "Total Body: " + sumMind.ToString();
            }
        }
    }

    public bool CheckStatReq() //Checks total sum of stats to make sure the building has enough stats to do anything
    {
        int sumBody = 0;
        int sumSoul = 0;
        int sumMind = 0;
        foreach (GameObject surv in survivor)
        {
            if (surv != null)
            {
                SurvivorBase survStat = surv.GetComponent<SurvivorBase>(); //Count total sum of every stat of every survivor
                sumBody += survStat.body;
                sumMind += survStat.mind;
                sumSoul += survStat.soul; 
            }
            else return false;
        }

        if (sumBody >= reqBody)
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

    public void AddOccupant(GameObject surv) //Assigning a survivor to this building
    {
        if(survivor.Count < maxOccupant)
        {
            survivor.Add(surv);  //Adds the survivor to the list
            CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
            cam.TurnOffAssignButton();
            SurvivorBase survInfo = surv.GetComponent<SurvivorBase>();
            survInfo.assignedBuilding = this.gameObject; //Make sure the survivor is assigned from both ends
        }
        else
        {
            Debug.Log("Full");
        }
    }


    public void CheckFunctionCost()
    {
        ResourceHolder inv = GameObject.Find("InventoryManager").GetComponent<ResourceHolder>(); //Get access to the resource holding essential units
        
        if (CheckStatReq() == true && inv.powerCell >= reqCell) //Goes through stat checking and cell checking
        {
            inv.ChangeCell(-reqCell);  //Consumes cells
            foreach(GameObject surv in survivor) 
            {
                SurvivorBase survInfo = surv.GetComponent<SurvivorBase>();
                survInfo.currentHunger -= hungerDrain; //Consumes hunger meter on everyone in the building
                survInfo.UI.transform.Find("unassignBuilding").gameObject.SetActive(false); 
            }
            
            CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
            cam.buildingInfo.SetActive(false); //Turn off the UI
            if(this.fType == functionType.craft)
            {
                recipeList.SetActive(true);  //Turn on the recipe list and other UI things

                cam.buttonExitMenu.SetActive(true);
                cam.UItoClose.Add(recipeList);
                cam.UItoClose.Add(cam.buttonExitMenu);
            }
            else
            {
                StartCoroutine(TimeToFinishFunction(timeToGen)); //Begins a timer until the building finishes whatever it needs to do
            }
            
        }
        else
        {
            Debug.Log("Not enough stats");
        }
    }

    public IEnumerator TimeToFinishFunction(float time)//Sets a flag on and off to make sure the building cant do more than 1 thing at any time

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
            survInfo.UI.transform.Find("unassignBuilding").gameObject.SetActive(true); //Make survivors available to be assigned else where
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
            case BuildType.storage: //Opens the storage UI and sets all the according buttons assignment. 
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
            case BuildType.trainingStation1: //Increases the stat for every Survivor hosted in the building
                foreach (GameObject surv in survivor)
                {
                    SurvivorBase survStat = surv.GetComponent<SurvivorBase>();
                    survStat.soul += amountToIncrease;
                    SurvivorUI survUI = survStat.UI.GetComponent<SurvivorUI>();
                    survUI.SetUIstat();
                }    
                break;
            case BuildType.trainingStation2: //Increases the stat for every Survivor hosted in the building
                foreach (GameObject surv in survivor)
                {
                    SurvivorBase survStat = surv.GetComponent<SurvivorBase>();
                    survStat.body += amountToIncrease;
                    SurvivorUI survUI = survStat.UI.GetComponent<SurvivorUI>();
                    survUI.SetUIstat();
                }
                break;
            case BuildType.trainingStation3: //Increases the stat for every Survivor hosted in the building
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

    public IEnumerator SetTimer(float time) //Change the timer in the UI
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
