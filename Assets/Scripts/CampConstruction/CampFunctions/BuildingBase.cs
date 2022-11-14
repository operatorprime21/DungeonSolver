using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public int residentCap;

    public float timeToBuild;

    public float timeToGenerate;
    public int energyCost;

    public GameObject sizeChecker;

    private bool CheckResource()
    {
        //Loop through Inventory Holder to check the availability and correct amount of resource
        return true;
    }

    public void Build()
    {
        if(CheckResource() == true)
        {

            //Instantiate holo-prefab to check 
            Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 itemPos = cam.ScreenToWorldPoint(mousePos);
            Instantiate(sizeChecker, new Vector3(itemPos.x, itemPos.y, 0f), Quaternion.identity);

            GameObject BPListUI = GameObject.Find("Main Canvas").transform.Find("BlueprintList").gameObject;
            BPListUI.SetActive(false);
            GameObject buttonExit = GameObject.Find("Main Canvas").transform.Find("ExitMenu").gameObject;
            buttonExit.SetActive(false);
        }
        else
        {
            //Do warning
        }
    }
}
