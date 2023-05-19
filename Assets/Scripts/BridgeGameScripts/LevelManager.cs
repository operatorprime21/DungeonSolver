using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 playerStart;
    public Tile startTile;
    public Tile endTile;
    public Tile startDoorTile;
    public int camSize;
    [SerializeField] private bool levelStarted = true;
    public int steps;

    public Sprite doorOpen;
    public Sprite doorClose;

    public GameObject playerObj;

    public TMP_Text textSteps;
    void Start()
    {
        Camera cam = Camera.main;
        cam.orthographicSize = camSize;
        BeginSequence();
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
        StartCoroutine(ChangeTileSpriteOnTime(doorSprite, doorClose, 2f));
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
    }

    public void PlayerStep(Tile playerTile) //Reads everything that can happen when the player makes a move
    {
        if(steps > 0)
        {
            steps--;
            textSteps.text = steps.ToString();
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
        GameObject loseScreen = GameObject.Find("Canvas").transform.Find("LostScreen").gameObject;
        GameObject levelUI = GameObject.Find("Canvas").transform.Find("LevelUI").gameObject;
        GameObject playerControls = GameObject.Find("Canvas").transform.Find("PlayerControls").gameObject;
        playerControls.SetActive(false);
        levelUI.SetActive(false);
        loseScreen.SetActive(true);

    }

    public void ReachedGoal()
    {
        GameObject levelUI = GameObject.Find("Canvas").transform.Find("LevelUI").gameObject;
        levelUI.SetActive(false);
        GameObject playerControls = GameObject.Find("Canvas").transform.Find("PlayerControls").gameObject;
        playerControls.SetActive(false);
        GameObject winScreen = GameObject.Find("Canvas").transform.Find("WinScreen").gameObject;
        winScreen.SetActive(true);
    }
}
