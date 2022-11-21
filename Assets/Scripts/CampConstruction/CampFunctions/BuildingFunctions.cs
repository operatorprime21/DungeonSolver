using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFunctions : MonoBehaviour
{
    // Start is called before the first frame update

    public int reqPer;
    public int reqAdp;
    public int reqInw;

    List<GameObject> survivor = new List<GameObject>();
    public int maxOccupant;
    public float timeToGen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        //Open the building functionality menu (and exit button)
        //
        
    }

    public bool CheckReq()
    {
        int sumPer = 0;
        int sumAdp = 0;
        int sumInw = 0;
        foreach (GameObject surv in survivor)
        {
            if(surv != null)
            {
                SurvivorBase survStat = surv.GetComponent<SurvivorBase>();
                sumPer += survStat.perseverance;
                sumInw += survStat.ironWill;
                sumAdp += survStat.adaptability;
            }
        }

        if (sumPer >= reqPer)
        {
            if (sumAdp >= reqAdp)
            {
                if (sumInw >= reqInw)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        else return false;
    }

    public void AddOccupant(GameObject surv)
    {
        if(survivor.Count < maxOccupant)
        {
            survivor.Add(surv);
        }
    }

    public void ResourceGeneration()
    {
        if (CheckReq() == true)
        {
            StartCoroutine(TimeToGenerate(timeToGen));
        }
    }

    IEnumerator TimeToGenerate(float time)
    {
        yield return new WaitForSeconds(time);
        
    }

}
