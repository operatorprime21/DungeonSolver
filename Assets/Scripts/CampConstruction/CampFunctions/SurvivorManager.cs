using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorManager : MonoBehaviour
{
    public List<SurvivorBase> survivorList = new List<SurvivorBase>();

    private void Start()
    {
         foreach(SurvivorBase survInfo in survivorList)
         {
            if(survInfo.isSaved == true)
            survInfo.UI.SetActive(true);
         }
    }
}
