using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public GameObject sizeChecker; //Holds the prefab
    private Recipe recipe; //Recipe holding the necessary resource to build the building

    public void CheckResource()
    {
        //Loop through Inventory Holder to check the availability and correct amount of resource to build that building
        recipe = this.gameObject.transform.Find("Recipe").GetComponent<Recipe>();
        recipe.BeginInventoryScan();
    }

    public void Build()
    {
        //Gets the camera for checking mouse point
            Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 itemPos = cam.ScreenToWorldPoint(mousePos);
        //Spawns the size checker prefab at the point of click
            Instantiate(sizeChecker, new Vector3(itemPos.x, itemPos.y, 0f), Quaternion.identity);
        //Turns off the list of building blueprints and the exit button
            GameObject BPListUI = GameObject.Find("Main Canvas").transform.Find("BlueprintList").gameObject;
            BPListUI.SetActive(false);
            GameObject buttonExit = GameObject.Find("Main Canvas").transform.Find("button_x").gameObject;
            buttonExit.SetActive(false);
    }
}
