using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public int residentCap;

    public float timeToBuild;

    public float timeToGenerate;
    public int energyCost;

    public void GenerateResource()
    {
        //Consume energy from each member
        //Wait for time
        //Add resource (and count) to inventory
        //Loop through each survivor using resident cap

    }

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
        }
        else
        {
            //Do warning
        }
    }
}
