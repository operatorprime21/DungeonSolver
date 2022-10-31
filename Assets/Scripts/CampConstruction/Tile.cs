using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isValid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ReturnValid()
    {
        return isValid;
    }

    public void SwitchValid()
    {
        isValid = !isValid;
    }




}
