using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 playerStart;
    public Tile startTile;
    public Tile endTile;
    public int camSize;

    public int steps;
    void Start()
    {
        Camera cam = Camera.main;
        cam.orthographicSize = camSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerStep(Tile playerTile) //Reads everything that can happen when the player makes a move
    {
        if(steps > 0)
        {
            steps--;
        }
        else
        {
            RanOutOfSteps();
        }
        if(playerTile == endTile)
        {
            ReachedGoal();
        }

    }
    public void RanOutOfSteps()
    {
        Debug.Log("Game Over!");
    }

    public void ReachedGoal()
    {
        SceneManager.LoadScene(0);
    }
}
