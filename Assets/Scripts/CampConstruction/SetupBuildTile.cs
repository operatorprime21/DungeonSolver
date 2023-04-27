using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBuildTile : MonoBehaviour
{
    public GameObject singleTile;
    public int tileStartX;
    public int tileStartY;
    public int tileEndX;
    public int tileEndY;
    public int tileLimit;
    public List<Vector2> levelBorder = new List<Vector2>();
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
        for(int row = -tileLimit; row <= tileLimit; row++)
        {
            for(int col = -tileLimit; col <= tileLimit; col++)
            {
                Vector3 tilePos = new Vector3(row, col, 0);
                GameObject tile = Instantiate(singleTile, tilePos, Quaternion.identity);
                tile.name = row + "." + col;
                tile.transform.parent = GameObject.Find("Tiles").transform;
                tile.GetComponent<Tile>().tilePosition = tilePos;
                if(row == tileStartX && col == tileStartY)
                {
                    LevelManager manager = this.GetComponentInParent<LevelManager>();
                    manager.playerStart = tilePos;
                    manager.startTile = tile.GetComponent<Tile>();
                    tile.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f, 0.5f);
                }
                if (row == tileEndX && col == tileEndY)
                {
                    LevelManager manager = this.GetComponentInParent<LevelManager>();
                    manager.endTile = tile.GetComponent<Tile>();
                    tile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 0.5f);
                }
                foreach (Vector2 border in levelBorder)
                {
                    if(border == new Vector2(row, col))
                    {
                        tile.GetComponent<Tile>().canMoveOn = false;
                        tile.GetComponent<Tile>().canBuildOn = false;
                        tile.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
                    }
                }
            }
        }
    }
}
