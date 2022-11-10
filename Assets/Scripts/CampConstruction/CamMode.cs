using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMode : MonoBehaviour
{
    public bool isInBuildMode;
    public GameObject cam;
    public GameObject player;
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
        }
        else
        {
            isInBuildMode = true;
            CameraControl camScript = cam.GetComponent<CameraControl>();
            camScript.enabled = false;
            ConstructModeCam buildCamScript = cam.GetComponent<ConstructModeCam>();
            buildCamScript.enabled = true;
            cam.GetComponent<Camera>().orthographicSize = 15;
            player.SetActive(false);
        }
    }
}
