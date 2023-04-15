using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SurvivorUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject survivor;
    public GameObject buttonAsign;

    public GameObject buttonUnassign;
    void Start()
    {
        buttonAsign = this.transform.Find("button_assign").gameObject;
        buttonUnassign = this.transform.Find("button_unassign").gameObject;

        buttonUnassign.SetActive(false);
        SetUIstat();
    }

    public void SetUIstat()
    {
        SurvivorBase surv = survivor.GetComponent<SurvivorBase>();
        for (int s = 0; s < 3; s++)
        {
            Text stat = this.transform.Find("stat (" + s + ")").GetComponent<Text>();
            if (s == 0)
            {
                stat.text = "Body: " + surv.body.ToString();
            }
            if (s == 1)
            {
                stat.text = "Soul: " + surv.soul.ToString();
            }
            if (s == 2)
            {
                stat.text = "Mind: " + surv.mind.ToString();
            }
        }
    }

    private void Awake()
    {
        //if (buildingAssigned == null)
        //{
        //    buttonUnassign.SetActive(false);
        //}
        //else
        //{
        //    buttonUnassign.SetActive(true);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignPlace()
    {
        //Turn off list ui and other buttons
        //show something that tells the player that they are in "select building mode". Too late for that
        //Inserts this survivor into the variable in a manager to assign it to a building
        CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
        cam.survivorToPlace = survivor;
        cam.buttonAssign = buttonAsign;
        cam.buttonUnassign = buttonUnassign;
        cam.SurvList.SetActive(false);
        cam.CloseUI();
        if (cam.isInPlacementMode == false)
        {
            cam.isInPlacementMode = true;
        }
        else
        {
            if(cam.buildingToAssign != null)
            {
                BuildingFunctions building = cam.buildingToAssign.GetComponent<BuildingFunctions>();
                building.AddOccupant(cam.survivorToPlace);
                cam.survivorToPlace = null;
                cam.buildingToAssign = null;
                cam.isInPlacementMode = false;
            }
        }
    }

    public void UnassignPlace() // Remove the survivor from the building's list and set every other variable accordingly
    {
        SurvivorBase survInfo = survivor.GetComponent<SurvivorBase>();
        survInfo.assignedBuilding.GetComponent<BuildingFunctions>().survivor.Remove(survivor);
        survInfo.assignedBuilding = null;
        
        CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
        cam.isInPlacementMode = false;
        cam.buttonAssign = buttonAsign;
        cam.buttonUnassign = buttonUnassign;
        cam.SurvList.SetActive(false);
        cam.CloseUI();
        cam.TurnOnAssignButton();
    }
}
