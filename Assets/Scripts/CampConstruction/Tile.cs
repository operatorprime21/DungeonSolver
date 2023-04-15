using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isValid = true;
    // Simply handles different bools to let tile checkers know if tiles can be built on or not

    public bool ReturnValid()
    {
        return isValid;
    }

    public void SwitchValid()
    {
        isValid = !isValid;
    }
}
