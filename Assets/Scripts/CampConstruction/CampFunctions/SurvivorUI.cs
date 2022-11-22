using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivorUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject survivor;
    public GameObject buttonAsign;

    public GameObject buttonUnassign;
    void Start()
    {
        buttonAsign = this.transform.Find("assignBuilding").gameObject;
        buttonUnassign = this.transform.Find("unassignBuilding").gameObject;
       
        buttonUnassign.SetActive(false);
        SurvivorBase surv = survivor.GetComponent<SurvivorBase>();
        for (int s = 0; s < 3; s++)
        {
            TMP_Text stat = this.transform.Find("stat (" + s + ")").GetComponent<TMP_Text>();
            if(s == 0)
            {
                stat.text = "Perserverance: " + surv.perseverance.ToString();
            }
            if(s == 1)
            {
                stat.text = "Adaptability: " + surv.adaptability.ToString();
            }
            if(s == 2)
            {
                stat.text = "Iron Will: " + surv.ironWill.ToString();
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
        //show something that tells the player that they are in "select building mode"
        //
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

    public void UnassignPlace()
    {
        SurvivorBase survInfo = survivor.GetComponent<SurvivorBase>();
        survInfo.assignedBuilding.GetComponent<BuildingFunctions>().survivor.Remove(survivor);
        survInfo.assignedBuilding = null;
        
        CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
        cam.buttonAssign = buttonAsign;
        cam.buttonUnassign = buttonUnassign;
        cam.SurvList.SetActive(false);
        cam.CloseUI();
        cam.TurnOnAssignButton();
    }
}
