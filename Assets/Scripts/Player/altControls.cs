using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altControls : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMovement playerScript;
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    public void Toggle()
    {
        playerScript.altControlOn = !playerScript.altControlOn;
    }

    public void MoveDown()
    {
        if(playerScript.canMove == false)
        {
            playerScript.tapPos = new Vector3(0, 0, 0);
            playerScript.releasePos = new Vector3(0, -1, 0);
            playerScript.ReturnDirection();
        }
        
    }
    public void MoveRight()
    {
        if (playerScript.canMove == false)
        {
            playerScript.tapPos = new Vector3(0, 0, 0);
            playerScript.releasePos = new Vector3(1, 0, 0);
            playerScript.ReturnDirection();
        }
        
    }
    public void MoveLeft()
    {
        if (playerScript.canMove == false)
        {
            playerScript.tapPos = new Vector3(0, 0, 0);
            playerScript.releasePos = new Vector3(-1, 0, 0);
            playerScript.ReturnDirection();
        }
        
    }
    public void MoveUp()
    {
        if (playerScript.canMove == false)
        {
            playerScript.tapPos = new Vector3(0, 0, 0);
            playerScript.releasePos = new Vector3(0, 1, 0);
            playerScript.ReturnDirection();
        }
        
    }
}
