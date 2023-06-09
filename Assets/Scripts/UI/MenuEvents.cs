using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject levels;
    public Animator levelsAnim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLevelSelect()
    {
        levels.SetActive(true);
        levelsAnim.Play("levels_fade_in");    
    }

    public void OpenMenu()
    {
        levels.SetActive(false);
        GameObject.Find("Canvas").GetComponent<MenuButtons>().offTransition();
    }
}
