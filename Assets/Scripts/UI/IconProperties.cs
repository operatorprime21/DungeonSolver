using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconProperties : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text count;
    public int c =0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCount()
    {
        count.text = c.ToString();
    }

}
