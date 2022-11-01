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
        //BoxCollider2D tileCollider = this.GetComponent<BoxCollider2D>();
        //if (isValid == true)
        //{
        //    isValid = false;
        //    tileCollider.size = new Vector2(0.001f, 0.001f);
        //}
        //else
        //{
        //    isValid = true;
        //    tileCollider.size = new Vector2(0.95f, 0.95f);
        //}
        isValid = !isValid;
        
    }
}
