using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageScroller : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 startPos;
    private Vector3 curPos;
    private Vector3 destPos;

    private float startTime;

    public bool moving = false;
    public int curSelectedLevel = 0;
    public GameObject unlocker;

    public CoinScript coins;
    public TMP_Text coinT;
    public TMP_Text costText;
    public TMP_Text status;
    public List<LevelInfo> levels = new List<LevelInfo>();
    [SerializeField] public LevelInfo selectedLevel;

    public List<GameObject> iconStar = new List<GameObject>();
    public List<GameObject> iconCandle = new List<GameObject>();
    void Start()
    {

        startPos = this.transform.position;
        selectedLevel = levels[0];
        foreach (LevelInfo level in levels)
        {
            if(PlayerPrefs.GetString(level.name + " unlock") == "Unlocked")
            {
                level.levelIsUnlocked = true;
            }
            //level.levelStatus = "Not Unlocked";
            //PlayerPrefs.SetString(level.name + " completion", "Not Unlocked");
            level.levelStatus = PlayerPrefs.GetString(level.name + " completion");
            level.star = PlayerPrefs.GetInt(level.name + " star");
        }
        SetLevelUI();
    }

    // Update is called once per frame
    void Update()
    {
        float movingTime = +(Time.time - startTime) * 10;
        if (moving)
        {
            this.transform.position = Vector3.Lerp(curPos, destPos, movingTime);
            if(this.transform.position == destPos)
            {
                moving = false;
            }
        }
    }

    public void ScrollLeft()
    {
        if(!moving && FindLevel(-1)==true)
        {
            
            curSelectedLevel--;
            AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audio.Play("page");
            curPos = this.transform.position;
            destPos = new Vector3(this.transform.position.x + 1500, this.transform.position.y, this.transform.position.z);
            moving = true;
            startTime = Time.time;
            if (selectedLevel != null)
            {
                selectedLevel = levels[curSelectedLevel - 1];
                if (selectedLevel.levelIsUnlocked == false)
                {
                    unlocker.SetActive(true);
                }
                else
                {
                    unlocker.SetActive(false);
                }
                foreach (GameObject star in iconStar)
                {
                    star.SetActive(false);
                }
                foreach (GameObject candle in iconCandle)
                {
                    candle.SetActive(false);
                }
                SetLevelUI();
            }
        }
    }

    public void ScrollRight()
    {
        if (!moving && FindLevel(1)==true)
        {
            curSelectedLevel++;
            AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audio.Play("page");
            curPos = this.transform.position;
            destPos = new Vector3(this.transform.position.x - 1500, this.transform.position.y, this.transform.position.z);
            moving = true;
            startTime = Time.time;
            if (selectedLevel != null)
            {
                selectedLevel = levels[curSelectedLevel - 1];
                if (selectedLevel.levelIsUnlocked == false)
                {
                    unlocker.SetActive(true);
                }
                else
                {
                    unlocker.SetActive(false);
                }
                foreach (GameObject star in iconStar)
                {
                    star.SetActive(false);
                }
                foreach (GameObject candle in iconCandle)
                {
                    candle.SetActive(false);
                }
                SetLevelUI();
            }
        }
    }

    public bool FindLevel(int change)
    {
        int levelFind = curSelectedLevel + change;
        GameObject lvlButton = this.transform.Find(levelFind.ToString()).gameObject;
        if (lvlButton != null)
        {
            return true;
        }
        if (lvlButton == null)
        {
            Debug.Log("no object");
            return false;
        }
        else return false;
    }

        private void OnDisable()
    {
        this.transform.position = startPos;
        curSelectedLevel = 1;
        if(selectedLevel!= null)
        {
            selectedLevel = levels[0];
        }
    }

    public void UnlockLevel()
    {
        if (coins.amount >= selectedLevel.unlockCost && selectedLevel.levelIsUnlocked == false)
        {
            unlocker.SetActive(false);
            coins.amount -= selectedLevel.unlockCost;
            coinT.text = coins.amount.ToString();
            PlayerPrefs.SetInt("CoinAmount", coins.amount);
            PlayerPrefs.SetString(selectedLevel.name + " unlock", "Unlocked");
            selectedLevel.levelIsUnlocked = true;
            selectedLevel.levelStatus = "Incomplete";
            PlayerPrefs.SetString(selectedLevel.name + " completion", "Incomplete");
            AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audio.Play("coin");
        }
    }

    public void SetLevelUI()
    {
        costText.text = selectedLevel.unlockCost.ToString();

        if (selectedLevel.levelIsUnlocked)
        {
            unlocker.SetActive(false);
        }
        else
        {
            unlocker.SetActive(true);
        }
        
        for (int c = -1; c < selectedLevel.star; c++)
        {
            if (c != -1)
            {
                iconStar[c].SetActive(true);
            }
        }
        
        for (int c = -1; c < selectedLevel.candle; c++)
        {
            if (c != -1)
            {
                iconCandle[c].SetActive(true);
            }
        }
        status.text = selectedLevel.levelStatus;
    }
}
