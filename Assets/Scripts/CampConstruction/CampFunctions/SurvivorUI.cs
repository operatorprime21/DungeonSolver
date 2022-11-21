using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivorUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject survivor;
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }

}
