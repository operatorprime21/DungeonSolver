using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject levels;
    public GameObject levelPages;
    public GameObject mainMenu;
    public bool transitioning = false;

    public Animator menuAnim;
    public Animator levelSelectorAnim;

    public CoinScript coins;
    public TMP_Text coinT;
    void Start()
    {
        coins.amount = PlayerPrefs.GetInt("CoinAmount");
        coinT.text = coins.amount.ToString();
        AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audio.Play("menu_bg");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void offTransition()
    {
        transitioning = false;
    }
    public void SetCoinText()
    {
        coinT.text = coins.amount.ToString();
    }

    public void LevelSelect()
    {
        if (!transitioning)
        {
            menuAnim.Play("menu_fade_out");
            AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audio.Play("button1");
            transitioning = true;
        }
    }

    public void ReturnToMenu()
    {
        if(!transitioning)
        {
            AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audio.Play("button1");
            levelPages.SetActive(false);
            levelSelectorAnim.Play("levelselect_fade_out");
            transitioning = true;
        }
    }

    public void Quit()
    {
        if(!transitioning)
        {
            AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audio.Play("button1");
            Application.Quit();
        }
    }

    public void LoadLevel()
    {
        GameObject info = levelPages.transform.Find("LevelInfo").gameObject;
        if (info.GetComponent<PageScroller>().moving == false)
        {
            if(levelPages.transform.Find("LevelInfo").gameObject.GetComponent<PageScroller>().selectedLevel.levelIsUnlocked == true && !transitioning)
            {
                AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                audio.Play("level_enter");
                audio.Stop("menu_bg");
                transitioning = true;
                levelPages.SetActive(false);
                string levelName = EventSystem.current.currentSelectedGameObject.name;
                int level;
                int.TryParse(levelName, out level);
                levels.GetComponent<LoadLevel>().level = level;
                levelSelectorAnim.Play("levelselect_fade_out");
            }
        }
    }
}
