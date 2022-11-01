using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isValid = true;
    // Start is called before the first frame update

    public bool ReturnValid()
    {
        return isValid;
    }

    public void SwitchValid()
    {
        isValid = !isValid;
    }

}
