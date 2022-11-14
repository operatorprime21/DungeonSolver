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

    private List<GameObject> UItoClose = new List<GameObject>();
    private List<GameObject> UItoOpen = new List<GameObject>();

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
}
