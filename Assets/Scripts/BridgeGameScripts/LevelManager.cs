using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 playerStart;
    public Tile startTile;
    public Tile endTile;
    public Tile startDoorTile;
    public int camSize;
    private bool levelStarted = true;

    private int maxSteps;
    private int tierProgressSteps;
    public int steps;
    [SerializeField] private int stepsUsed = 0;
    public Slider tierProgress;

    public Sprite doorOpen;
    public Sprite doorClose;

    public GameObject playerObj;

    public int reward;
    public int rewardTier3;
    public int rewardTier2;
    public int rewardTier1;
    public int rewardTier0;

    private float startTime;
    private bool lerpingTier = false;
    private float currentBar = 1f;
    private float destinationBar;

    [SerializeField] private int tier = 3;
    private float tierThreshold;

    public int chest;
    public TMP_Text chestCount;
    public TMP_Text stepUsed;
    public CoinScript coin;
    public TMP_Text endReward;
    public TMP_Text stepPop;

    public List<GameObject> Stars = new List<GameObject>();
    public TMP_Text textSteps;

    public LevelInfo thisLevel;
    void Start()
    {
        Camera cam = Camera.main;
        cam.orthographicSize = camSize;
        BeginSequence();
        maxSteps = steps;
        tierProgressSteps = maxSteps;
        startTime = 0f;
        chestCount.text = chest.ToString();
        coin.amount = PlayerPrefs.GetInt("CoinAmount");
        AudioManager manager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        manager.Play("level_bg");
        manager.Play("door");
        textSteps.text = steps.ToString();
    }

    // Update is called once per frame
    public void BeginSequence()
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = playerStart;
        playerObj = player;

        Animator playerAnim = player.GetComponent<Animator>();
        playerAnim.Play("down_walk");

        PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
        playerScript.currentTile = startTile;

        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        StartCoroutine(PlayerStartAnim(playerSprite));

        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        playerScript.cam = cam;

        SpriteRenderer doorSprite = startDoorTile.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeTileSpriteOnTime(doorSprite, doorOpen, 0.2f));
        StartCoroutine(ChangeTileSpriteOnTime(doorSprite, doorClose, 1.0f));
    }

    IEnumerator ChangeTileSpriteOnTime(SpriteRenderer renderer, Sprite sprite, float dur)
    {
        yield return new WaitForSeconds(dur);
        renderer.sprite = sprite;
    }
    IEnumerator PlayerStartAnim(SpriteRenderer player)
    {
        player.enabled = false;
        yield return new WaitForSeconds(0.3f);
        player.enabled = true;
        levelStarted = false;
        
    }

    private void Update()
    {
        if(!levelStarted)
        {
            float movingTime = +Time.time;
            Vector3 trueStart = new Vector3(playerStart.x, playerStart.y - 0.5f, playerStart.z);
            playerObj.transform.position = Vector3.Lerp(playerStart, trueStart, movingTime);
            if(playerObj.transform.position == trueStart)
            {
                levelStarted = true;
                playerObj.GetComponent<PlayerMovement>().enabled = true;
                playerObj.GetComponent<Animator>().Play("down_idle");
            }
        }

        float moving = +(Time.time - startTime) * 2f;
        if (lerpingTier ==true)
        {
            tierProgress.value = Mathf.Lerp(currentBar, destinationBar, moving);
        }
        if(tierProgress.value == destinationBar && lerpingTier == true)
        {
            if(tierProgress.value != 0)
            {
                lerpingTier = false;
                currentBar = tierProgress.value;
            }
            else
            {
                if(tier!=0)
                {
                    startTime = Time.time;
                    currentBar = 0f;
                    destinationBar = 1f;
                    Stars[tier - 1].SetActive(false);
                    Stars[tier + 2].SetActive(false);
                    AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
                    audio.Play("star");
                }
                else
                {
                    lerpingTier = false;
                    Lose();
                }
            }
        }
    }

    public void PlayerStep() //Reads everything that can happen when the player makes a move
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if (steps >= 1 && player.turnsRed ==0)
        {
                steps--;
                stepPop.text = "-1";
                stepPop.GetComponent<Animator>().Play("steps_pop");
                stepsUsed++;
                textSteps.text = steps.ToString();
                lerpingTier = true;
                startTime = Time.time;

                if (WinTier() == 3)
                {
                    tierThreshold = 0.6f;
                }
                else if (WinTier() == 2)
                {
                    tierThreshold = 0.4f;
                }
                else if (WinTier() == 1)
                {
                    tierThreshold = 0.2f;
                }
                else
                {
                    tierThreshold = 0.0f;
                    stepPop.color = Color.red;
                    textSteps.color = Color.red;
                }
                destinationBar = TierBarProgress(tierThreshold);
        }
        else if(steps == 1 && player.turnsRed == 0)
        {
            steps--;
            stepPop.text = "-1";
            stepPop.GetComponent<Animator>().Play("steps_pop");
            stepsUsed++;
            textSteps.text = steps.ToString();
            lerpingTier = true;
            startTime = Time.time;
            Lose();
        }
    }

    public void Lose()
    {
        GameObject loseScreen = GameObject.Find("Canvas").transform.Find("LostScreen").gameObject;
        GameObject levelUI = GameObject.Find("Canvas").transform.Find("LevelUI").gameObject;
        GameObject playerControls = GameObject.Find("Canvas").transform.Find("PlayerControls").gameObject;
        playerControls.SetActive(false);
        levelUI.SetActive(false);
        loseScreen.SetActive(true);
        loseScreen.GetComponent<Animator>().Play("lose_screen");
        AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audio.Play("lose");
        audio.Stop("level_bg");
    }

    public void Win()
    {
        GameObject levelUI = GameObject.Find("Canvas").transform.Find("LevelUI").gameObject;
        levelUI.SetActive(false);
        GameObject playerControls = GameObject.Find("Canvas").transform.Find("PlayerControls").gameObject;
        playerControls.SetActive(false);
        GameObject winScreen = GameObject.Find("Canvas").transform.Find("WinScreen").gameObject;
        coin.amount = coin.amount + reward;
        PlayerPrefs.SetInt("CoinAmount", coin.amount);
        PlayerPrefs.SetString(thisLevel.name.ToString() + " completion", "Completed");
        thisLevel.levelStatus = "Completed";
        endReward.text = reward.ToString();
        stepUsed.text = stepsUsed.ToString();
        winScreen.SetActive(true);
        winScreen.GetComponent<Animator>().Play("win_screen");
        AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audio.Play("win");
        audio.Stop("level_bg");
        int starsEarned = 0;
        int i = -1;
        foreach (GameObject star in Stars)
        {
            i++;
            if (i < 3 && Stars[i].activeSelf)
            {
                starsEarned++;
                Stars[i + 3].GetComponent<Animator>().Play("star_show");
            }
        }
        Debug.Log(starsEarned);
        if (starsEarned >= thisLevel.star)
        {
            thisLevel.star = starsEarned;
            PlayerPrefs.SetInt(thisLevel.name + " star", starsEarned); 
        }
    }

    public int WinTier()
    {
        float percentage = (float)steps / (float)maxSteps;
        if (percentage >= 0.6f)
        {
            tier = 3;
            reward = rewardTier3;
        }
        else if (percentage >= 0.4f)
        {
            tier = 2;
            reward = rewardTier2;
        }
        else if (percentage >= 0.2f)
        {
            tier = 1;
            reward = rewardTier1;
        }
        else
        {
            tier = 0;
            reward = rewardTier0;
        }
        return tier;
    }

    public float TierBarProgress(float tierThres)
    {
        int stepsProgress = tierProgressSteps - Mathf.FloorToInt(maxSteps * tierThres);
        int currentProgress = steps - Mathf.FloorToInt(maxSteps * tierThres);
        float progress = (float)currentProgress / (float)stepsProgress;
        if(progress == 0)
        {
            tierProgressSteps -= stepsProgress;
        }
        return progress;
    }

    public void CheckChest()
    {
        chestCount.text = chest.ToString();
        if(chest == 0 && steps > 0)
        {
            Win();
        }
    }
}
