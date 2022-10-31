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
            }
        }
    }
}
