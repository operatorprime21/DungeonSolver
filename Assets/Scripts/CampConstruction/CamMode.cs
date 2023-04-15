using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamMode : MonoBehaviour //Manages camera modes at first but now became the manager of the entirety of every UI
{
    public bool isInBuildMode;
    public GameObject cam;
    public GameObject player;

    public GameObject buttonOpenBPList;
    public GameObject buttonOpenSurvList;
    public GameObject BPList;
    public GameObject SurvList;
    public GameObject confirmSpeedUpFunction;
    public GameObject buttonCamMode;
    public GameObject buttonAttack;

    public GameObject buttonToggleInventory;
    public GameObject buttonInteract;
    public GameObject buttonExitMenu;

    public GameObject confirmSpeedUpBuilding;
    public GameObject buildingInfo;

    public List<GameObject> UItoClose = new List<GameObject>();
    public List<GameObject> UItoOpen = new List<GameObject>();

    public GameObject buildingPrefab;
    public GameObject tileChecker;
    public GameObject survivorToPlace;
    public GameObject buildingToAssign;

    public GameObject buttonAssign;
    public GameObject buttonUnassign;

    public GameObject buttonExplore;
    public GameObject buttonReturnHome;

    public GameObject healthSlider;
    public GameObject hungerSlider;
    public GameObject status;
    public GameObject currencies;

    public bool playerExists;
    public int fruitCost;
    public bool isInPlacementMode;

    // Start is called before the first frame update
    public void ChangeCamMode()
    {
        if (isInBuildMode == true)
        {
            isInBuildMode = false;
            CameraControl camScript = cam.GetComponent<CameraControl>();
            camScript.enabled = true;
            ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
            buildCamScript.enabled = false;
            cam.GetComponent<Camera>().orthographicSize = 5;
            player.SetActive(true);

            buttonToggleInventory.SetActive(true);
            buttonInteract.SetActive(true);
            buttonOpenBPList.SetActive(false);
            buttonOpenSurvList.SetActive(false);
            buttonExplore.SetActive(true);
        }
        else
        {
            isInBuildMode = true;
            CameraControl camScript = cam.GetComponent<CameraControl>();
            camScript.enabled = false;
            ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
            buildCamScript.enabled = true;
            cam.GetComponent<Camera>().orthographicSize = 10;
            player.SetActive(false);

            buttonToggleInventory.SetActive(false);
            buttonInteract.SetActive(false);
            buttonOpenBPList.SetActive(true);
            buttonOpenSurvList.SetActive(true);
            buttonExplore.SetActive(false);
        }
    }

    public void OpenBPList()
    {
        buttonOpenBPList.SetActive(false);
        ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
        buildCamScript.enabled = false;
        buttonCamMode.SetActive(false);
        buttonOpenSurvList.SetActive(false);

        BPList.SetActive(true);
        buttonExitMenu.SetActive(true);

        UItoClose.Add(BPList);
        UItoClose.Add(buttonExitMenu);
        UItoOpen.Add(buttonOpenBPList);
        UItoOpen.Add(buttonCamMode);
        UItoOpen.Add(buttonOpenSurvList);
    }

    public void OpenSurvList()
    {
        buttonOpenSurvList.SetActive(false);
        ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
        buildCamScript.enabled = false;
        buttonCamMode.SetActive(false);
        buttonOpenBPList.SetActive(false);

        SurvList.SetActive(true);
        buttonExitMenu.SetActive(true);

        UItoClose.Add(SurvList);
        UItoClose.Add(buttonExitMenu);
        UItoOpen.Add(buttonOpenSurvList);
        UItoOpen.Add(buttonCamMode);
        UItoOpen.Add(buttonOpenBPList);
    }

    public void CloseUI() //To make the exit button reusable, it closes and opens every button that enters a list of buttons and UI according to the situation. So when every
        //time the exit button shows up, all UI elements that needs to be turned on and off are passed into these according lists.
    {
        foreach (GameObject uiClose in UItoClose)
        {
            uiClose.SetActive(false);
        }
        UItoClose = new List<GameObject>();
        foreach (GameObject uiOpen in UItoOpen)
        {
            uiOpen.SetActive(true);
        }
        UItoOpen = new List<GameObject>();

        ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
        buildCamScript.enabled = true;
    }

    public void ConfirmSpeedUpBuilding()  //Consumes Fruit and stops coroutines, finishing whatever the objects are doing
    {
        GameObject.Find("InventoryManager").GetComponent<ResourceHolder>().ChangeFruit(fruitCost);
        confirmSpeedUpBuilding.SetActive(false); 

        tileChecker.GetComponent<CheckValidSpace>().CompleteBuilding();

        tileChecker = null;
        ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
        buildCamScript.enabled = true;
    }

    public void ConfirmSpeedUpFunction()  //Consumes Fruit and stops coroutines, finishing whatever the objects are doing
    {
        GameObject.Find("InventoryManager").GetComponent<ResourceHolder>().ChangeFruit(fruitCost);
        confirmSpeedUpFunction.SetActive(false);

        BuildingFunctions function = buildingToAssign.GetComponent<BuildingFunctions>();
        function.StopAllCoroutines();
        function.Function();
        function.timer.text = null;
        function.isWorking = false;
        buildingToAssign = null;
        ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
        buildCamScript.enabled = true;
    }


    public void DoNotSpeedUp()
    {
        confirmSpeedUpBuilding.SetActive(false);
        confirmSpeedUpFunction.SetActive(false);
        ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
        buildCamScript.enabled = true;
    }

    public void AssignSpeedUpBuild(GameObject building, int fruit, GameObject checker)
    {
        buildingPrefab = building;
        fruitCost = fruit;
        tileChecker = checker;
    }

    public void TurnOffAssignButton()
    {
        buttonAssign.SetActive(false);
        buttonUnassign.SetActive(true);
    }
    public void TurnOnAssignButton()
    {
        buttonAssign.SetActive(true);
        buttonUnassign.SetActive(false);
    }


    public void AssignSurvivor() //Similar principles to if the player selects from the building first. 
    {
        if (isInPlacementMode == true)
        {
            if (survivorToPlace != null)
            {
                buildingToAssign.GetComponent<BuildingFunctions>().AddOccupant(survivorToPlace);
                survivorToPlace = null;
                buildingToAssign = null;
                isInPlacementMode = false;
            }
        }
        else
        {
            isInPlacementMode = true;
            OpenSurvList();
        }
        buildingInfo.SetActive(false);
    }
    public void ConfirmFunction()
    {
        buildingToAssign.GetComponent<BuildingFunctions>().CheckFunctionCost();
    }

    public void Explore()  //Poor attempt at making the two sections connect. 
    {
        SceneManager.LoadScene(1);
        healthSlider.SetActive(true);
        //hungerSlider.SetActive(true);
        buttonCamMode.SetActive(false);
        currencies.SetActive(false);
        buttonExplore.SetActive(false);
        buttonReturnHome.SetActive(true);
        buttonAttack.SetActive(true);
    }

    public void ReturnHome() //Poor attempt at making the two sections connect. 
    {
        SceneManager.LoadScene(0);
        healthSlider.SetActive(false);
        //hungerSlider.SetActive(false);
        buttonCamMode.SetActive(true);
        currencies.SetActive(true);
        buttonExplore.SetActive(true);
        buttonReturnHome.SetActive(false);
        buttonAttack.SetActive(false);
    }
}
