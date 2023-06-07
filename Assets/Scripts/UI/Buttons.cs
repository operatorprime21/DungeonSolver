using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{
    public List<GameObject> uiToggleWhenPause = new List<GameObject>();

    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject craftMenu;
    public GameObject craftButton;

    public GameObject buttonControlsOff;
    public GameObject buttonControlsOn;
    public GameObject buttonControls;

    public altControls altControlScript;
    public GameObject confirmMenu;

    public GameObject loseScreen;

    public TMP_Text cWood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        PlayerMovement playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerScript.enabled = false;
        foreach(GameObject ui in uiToggleWhenPause)
        {
            ui.SetActive(false);
        }
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ToggleButtonControlsOff()
    {
        buttonControls.SetActive(false);
        buttonControlsOff.SetActive(false);
        buttonControlsOn.SetActive(true);
        altControlScript.Toggle();        
    }
    public void ToggleButtonControlsOn()
    {
        buttonControls.SetActive(true);
        buttonControlsOn.SetActive(false);
        buttonControlsOff.SetActive(true);
        altControlScript.Toggle();
    }

    public void Resume()
    {
        PlayerMovement playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerScript.enabled = true;
        foreach (GameObject ui in uiToggleWhenPause)
        {
            ui.SetActive(true);
        }
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    public void CraftMenu()
    {
        PlayerMovement playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerScript.enabled = false;
        foreach (GameObject ui in uiToggleWhenPause)
        {
            ui.SetActive(false);
        }
        craftMenu.SetActive(true);
        craftButton.SetActive(false);
    }

    public void ExitCraftMenu()
    {
        PlayerMovement playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerScript.enabled = true;
        foreach (GameObject ui in uiToggleWhenPause)
        {
            ui.SetActive(true);
        }
        craftMenu.SetActive(false);
        craftButton.SetActive(true);
    }

    public void OpenConfirm()
    {
        confirmMenu.SetActive(true);
        //This
    }
    public void Yes()
    {
        LevelManager manager = GameObject.Find("LevelSetup").GetComponent<LevelManager>();
        confirmMenu.SetActive(false);
        loseScreen.SetActive(false);
        manager.coin -= 50;
        manager.steps += 5;
        //Need a bit more work on this and the No button as well
        //Okay fuck it the rest of my notes here
        //Do the rest of the main menu and level menu animations like you did for the stuff in level
        //ask people on scriptable objects and how to do that for Coins
        //Also remember to show that in level
        //Animation for pick up also?
        //if theres spare time do mechanics
    }
}
