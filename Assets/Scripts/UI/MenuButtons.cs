using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject levels;
    public GameObject mainMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSelect()
    {
        mainMenu.SetActive(false);
        levels.SetActive(true);
    }

    public void ReturnToMenu()
    {
        mainMenu.SetActive(true);
        levels.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        string levelName = EventSystem.current.currentSelectedGameObject.name;
        if(levelName == "Level1")
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogError("Level Not Available");
        }
        
    }
}
