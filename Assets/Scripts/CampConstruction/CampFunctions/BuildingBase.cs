using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public GameObject sizeChecker;
    private Recipe recipe; 

    public void CheckResource()
    {
        //Loop through Inventory Holder to check the availability and correct amount of resource to build that building
        recipe = this.gameObject.transform.Find("Recipe").GetComponent<Recipe>();
        recipe.BeginInventoryScan();
    }

    public void Build()
    {
            Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 itemPos = cam.ScreenToWorldPoint(mousePos);
            Instantiate(sizeChecker, new Vector3(itemPos.x, itemPos.y, 0f), Quaternion.identity);
            GameObject BPListUI = GameObject.Find("Main Canvas").transform.Find("BlueprintList").gameObject;
            BPListUI.SetActive(false);
            GameObject buttonExit = GameObject.Find("Main Canvas").transform.Find("ExitMenu").gameObject;
            buttonExit.SetActive(false);
    }
}
