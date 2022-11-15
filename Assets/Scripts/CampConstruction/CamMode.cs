using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamMode : MonoBehaviour
{
    public bool isInBuildMode;
    public GameObject cam;
    public GameObject player;

    public GameObject buttonOpenBPList;
    public GameObject BPList;

    public GameObject buttonCamMode;

    public GameObject buttonExitMenu;

    public GameObject confirmSpeedUp;

    private List<GameObject> UItoClose = new List<GameObject>();
    private List<GameObject> UItoOpen = new List<GameObject>();

    public GameObject buildingPrefab;
    public GameObject tileChecker;

    public int fruitCost;

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
            cam.GetComponent<Camera>().orthographicSize = 4;
            player.SetActive(true);

            buttonOpenBPList.SetActive(false);
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

            buttonOpenBPList.SetActive(true);
        }
    }

    public void OpenBPList()
    {
        buttonOpenBPList.SetActive(false);
        BPList.SetActive(true);
        ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
        buildCamScript.enabled = false;
        buttonCamMode.SetActive(false);
        buttonExitMenu.SetActive(true);
    }

    public void CloseBPList()
    {
        BPList.SetActive(false);
        buttonOpenBPList.SetActive(true);
        ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
        buildCamScript.enabled = true;
        buttonCamMode.SetActive(true);
        buttonExitMenu.SetActive(false);
    }

    public void ConfirmSpeedUp()
    {
        GameObject.Find("InventoryManager").GetComponent<ResourceHolder>().ChangeFruit(fruitCost);
        Debug.Log("Finished Building!");
        confirmSpeedUp.SetActive(false);
        Instantiate(buildingPrefab, tileChecker.transform.position, Quaternion.identity);
        Destroy(tileChecker);
    }
    public void DoNotSpeedUp()
    {
        confirmSpeedUp.SetActive(false);
    }

    public void AssignSpeedUpBuild(GameObject building, int fruit, GameObject checker)
    {
        buildingPrefab = building;
        fruitCost = fruit;
        tileChecker = checker;
    }
}
