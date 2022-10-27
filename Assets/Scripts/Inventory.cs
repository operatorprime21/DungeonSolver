using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject bagTile;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition.x + ", " + Input.mousePosition.y);
        }
    }

    public void OpenInventory()
    {
        for (float row = 0f; row < 5f; row = row+1f)
        {
            for(float width = 0f; width < 3f; width = width+1f)
            {
                Vector3 camSpace = cam.ScreenToWorldPoint(new Vector3(row, width, 0f));
                GameObject tile = Instantiate(bagTile, camSpace, Quaternion.identity);
            }
        }
    }
}
