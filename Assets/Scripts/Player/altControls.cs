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
        AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audio.Play("button1");
        if(playerScript.canMove == false && GameObject.Find("LevelSetup").GetComponent<LevelManager>().steps > 0)
        {
            playerScript.tapPos = new Vector3(0, 0, 0);
            playerScript.releasePos = new Vector3(0, -1, 0);
            playerScript.ReturnDirection();
        }
        
    }
    public void MoveRight()
    {
        AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audio.Play("button1");
        if (playerScript.canMove == false && GameObject.Find("LevelSetup").GetComponent<LevelManager>().steps > 0)
        {
            playerScript.tapPos = new Vector3(0, 0, 0);
            playerScript.releasePos = new Vector3(1, 0, 0);
            playerScript.ReturnDirection();
        }
        
    }
    public void MoveLeft()
    {
        AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audio.Play("button1");
        if (playerScript.canMove == false && GameObject.Find("LevelSetup").GetComponent<LevelManager>().steps > 0)
        {
            playerScript.tapPos = new Vector3(0, 0, 0);
            playerScript.releasePos = new Vector3(-1, 0, 0);
            playerScript.ReturnDirection();
        }
        
    }
    public void MoveUp()
    {
        if (playerScript.canMove == false && GameObject.Find("LevelSetup").GetComponent<LevelManager>().steps > 0)
        {
            AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audio.Play("button1");
            playerScript.tapPos = new Vector3(0, 0, 0);
            playerScript.releasePos = new Vector3(0, 1, 0);
            playerScript.ReturnDirection();
        }
        
    }
}
