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

    void SetupTile() //Spawns tile at 1 intervals in the x and y. Tiles are sized 1x1 so it fits perfectly.
    {
        for(int row = -50; row < 50; row++)
        {
            for(int col = -50; col < 50; col++)
            {
                GameObject tile = Instantiate(singleTile, new Vector3(row, col, 0), Quaternion.identity);
                tile.name = row + "." + col;
                tile.transform.parent = GameObject.Find("Tiles").transform;
            }
        }
    }
}
