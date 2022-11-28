using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamMode : MonoBehaviour
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

    public void CloseUI()
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

    public void ConfirmSpeedUpBuilding()
    {
        GameObject.Find("InventoryManager").GetComponent<ResourceHolder>().ChangeFruit(fruitCost);
        confirmSpeedUpBuilding.SetActive(false);

        tileChecker.GetComponent<CheckValidSpace>().CompleteBuilding();

        tileChecker = null;
        ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
        buildCamScript.enabled = true;
    }

    public void ConfirmSpeedUpFunction()
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


    public void AssignSurvivor()
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
}
