using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBuildTile : MonoBehaviour
{
    public GameObject singleTile;
    // Start is called before the first frame update
    void Start()
    {
        SetupTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupTile()
    {
        for(int row = 0; row < 10; row++)
        {
            for(int col = 0; col < 10; col++)
            {
                GameObject tile = Instantiate(singleTile, new Vector3(row, col, 0), Quaternion.identity);
                tile.name = row + "." + col;
                tile.transform.parent = GameObject.Find("Tiles").transform;
            }
        }
    }

    /*Aight, how the fuck do I plan to do this
    First, every tile and every structure has a collider, we know that much
    The assign position .ie the empty object that hold the whole structure is where we need to start to add tiles from (tile list at element 0)
    */
}
