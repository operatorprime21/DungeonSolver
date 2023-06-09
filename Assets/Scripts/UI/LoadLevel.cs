using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public int level = 9999;
    public GameObject menu;
    public GameObject levelPage;
    public Animator menuAnim;

    public void LoadOrTransition()
    {
        if (SceneManager.GetSceneByBuildIndex(level) != null)
        {
            SceneManager.LoadScene(level);
            this.gameObject.SetActive(false);
            level = 9999;
        }
        else 
        {
            menu.SetActive(true);
            menuAnim.Play("menu_fade_in");
        }
    }

    public void OpenPage()
    {
        menu.SetActive(false);
        levelPage.SetActive(true);
        GameObject.Find("Canvas").GetComponent<MenuButtons>().offTransition();
    }
}
