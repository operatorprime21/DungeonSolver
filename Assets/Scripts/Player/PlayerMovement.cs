using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 tapPos;
    [SerializeField] private Vector3 releasePos;
    [SerializeField] private Vector3 tileToMoveTo;
    [SerializeField] private Tile currentTile;
    public string orientation = "down";
    [SerializeField] private Animator playerAnim;
    private float startTime;
    [SerializeField] private bool canMove = false;
    [SerializeField] private Vector3 tileToMoveFrom;
    //public Vector3 destination;
    //public Vector3 aimedAt;
    //public float angleMod;
    //public bool manualAim;
    //public Enemy lockedOnEnemy;

    //public LayerMask uiMask;
    //public LayerMask blockingMask;
    // Start is called before the first frame update

    public LevelManager levelManager;
    void Start()
    {
        playerAnim = this.gameObject.GetComponent<Animator>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        levelManager = GameObject.Find("LevelSetup").GetComponent<LevelManager>();
        this.transform.position = levelManager.playerStart;
        currentTile = levelManager.startTile;
        
        //destination = this.transform.position;
        //CamMode cam = GameObject.Find("UIManager").GetComponent<CamMode>();
        //if (cam.playerExists == false)
        //{
        //    cam.playerExists = true;
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}
    }

    // Update is called once per frame
    void Update()
    {  
        if (Input.GetMouseButtonDown(0))
        {
            tapPos = TappedPos();
        }
        if (Input.GetMouseButtonUp(0) && canMove == false)
        {
            releasePos = ReleasePos();
            ReturnDirection();
        }
        float movingTime =+ (Time.time - startTime) * speed *10;
        if(canMove==true)
        {
            this.transform.position = Vector3.Lerp(tileToMoveFrom, tileToMoveTo, movingTime);
        }
        if(this.transform.position == tileToMoveTo && canMove == true)
        {
            canMove = false;
            this.transform.position = tileToMoveTo;
            currentTile = GameObject.Find(this.transform.position.x + "." + (this.transform.position.y-0.5f)).GetComponent<Tile>();
            levelManager.PlayerStep(currentTile);
            if (currentTile.hasItem == true)
            {
                playerAnim.Play("side_grab");
            }
            else
            {
                playerAnim.Play(orientation + "_idle");
            }
        }
        //Note: Since move distance is always 1, does not require calculating fraction
        //this.transform.position = Vector3.MoveTowards(this.transform.position, destination, speed * Time.deltaTime); //Player is constantly trying to move to the destination vector variable
        ////Movement highly depends on how this variable is handled, but mostly just tap to set.

        //if (lockedOnEnemy == null)
        //{
        //    aimedAt = destination;
        //}
        //else
        //{
        //    LockOn();
        //}
    }

    public Vector3 TappedPos()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 tappedPoint = cam.ScreenToWorldPoint(mousePos);
        
        return tappedPoint;
    }

    public Vector3 ReleasePos()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 releasePoint = cam.ScreenToWorldPoint(mousePos);
        return releasePoint;
    }

    private void ReturnDirection()
    {
        
        float x = releasePos.x - tapPos.x;
        float y = releasePos.y - tapPos.y;
        GameObject tiles = GameObject.Find("level1");
        float tileX = this.transform.position.x;
        float tileY = this.transform.position.y-0.5f;
        if (Mathf.Abs(x) < Mathf.Abs(y))
        {
            if (y > 0)
            {
                float newTileY = tileY + 1;
                GameObject tileToCheck = tiles.transform.Find(tileX+"."+newTileY).gameObject;
                if (tileToCheck != null)
                {
                    Tile tileScript = tileToCheck.GetComponent<Tile>();
                    if (tileScript.canMoveOn == true)
                    {
                        orientation = "up";
                        MoveToTile(newTileY, tileX, orientation, 1);
                    }
                    else
                    {
                        Debug.LogError("cannot move up");
                    }
                }
                else
                {
                    Debug.LogError("Out of bounds");
                }
            }
            else if (y < 0)
            {
                float newTileY = tileY - 1;
                GameObject tileToCheck = tiles.transform.Find(tileX + "." + newTileY).gameObject;
                if (tileToCheck != null)
                {
                    Tile tileScript = tileToCheck.GetComponent<Tile>();
                    if (tileScript.canMoveOn == true)
                    {
                        orientation = "down";
                        MoveToTile(newTileY, tileX, orientation, 1);
                    }
                    else
                    {
                        Debug.LogError("cannot move down");
                    }
                }
                else
                {
                    Debug.LogError("Out of bounds");
                }
            }
        }
        else if (Mathf.Abs(y) < Mathf.Abs(x))
        {
            if (x > 0)
            {
                float newTileX = tileX + 1;
                GameObject tileToCheck = tiles.transform.Find(newTileX + "." + tileY).gameObject;
                if (tileToCheck != null)
                {
                    Tile tileScript = tileToCheck.GetComponent<Tile>();
                    if (tileScript.canMoveOn == true)
                    {
                        orientation = "side";
                        MoveToTile(tileY, newTileX, orientation, -1);
                    }
                    else
                    {
                        Debug.LogError("cannot move right");
                    }
                }
                else
                {
                    Debug.LogError("Out of bounds");
                }
            }
            else if (x < 0)
            {
                float newTileX = tileX - 1;
                GameObject tileToCheck = tiles.transform.Find(newTileX + "." + tileY).gameObject;
                if (tileToCheck != null)
                {
                    Tile tileScript = tileToCheck.GetComponent<Tile>();
                    if (tileScript.canMoveOn == true)
                    {
                        orientation = "side";
                        MoveToTile(tileY, newTileX, orientation, 1);
                    }
                    else
                    {
                        Debug.LogError("cannot move left");
                    }
                }
                else
                {
                    Debug.LogError("Out of bounds");
                }
            }
        }
        else
        {
            Debug.LogError("Abort Moving");
        }
        tapPos = new Vector3(0,0,0);
        releasePos = new Vector3(0, 0, 0);
    }

    private void MoveToTile(float tileY, float newTileX, string orientation, int side)
    {
        playerAnim.Play(orientation + "_walk");
        tileToMoveFrom = new Vector3(currentTile.tilePosition.x, currentTile.tilePosition.y + 0.5f, 0);
        tileToMoveTo = new Vector3(newTileX, tileY + 0.5f, 0);
        if(orientation == "side")
        {
            this.transform.localScale = new Vector3(side, 1, 1);
        }
        startTime = Time.time;
        canMove = true;
    }

    public void GetItem()
    {
        SpriteRenderer sprite = currentTile.GetComponent<SpriteRenderer>();
        sprite.sprite = currentTile.noItemSprite;
        Inventory inventory = this.gameObject.GetComponent<Inventory>();
        foreach (InventoryItem item in currentTile.item)
        {
            inventory.inventory.Add(item);
        }
        currentTile.item = null;
        currentTile.hasItem = false;
        ReturnToIdle();
    }

    private void ReturnToIdle()
    {
        playerAnim.Play(orientation + "_idle");
    }

    //Some poor attempts at preventing the player from moving if the player click on a UI element or click on somewhere that is blocked.
    //        if (Physics2D.Raycast(destination, destination - this.transform.position, Mathf.Infinity))
    //    {
    //        Debug.Log("clicked UI");
    //        destination = this.transform.position;
    //        return destination;
    //    }
    //    if (Physics2D.Raycast(this.transform.position, destination - this.transform.position, .5f))
    //    {
    //        Debug.Log("Hitting a wall");
    //        return this.transform.position;
    //    }
    //    else if (Physics2D.Raycast(this.transform.position, destination - this.transform.position, 5f))
    //    {
    //        Debug.Log("Returning wall");
    //        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, destination - this.transform.position, Mathf.Infinity, blockingMask);
    //        return hit.point;
    //    }
    //    else
    //    {
    //        return destination;
    //    }
    //}

    //private void LockOn()
    //{
    //    aimedAt = lockedOnEnemy.transform.position;
    //    manualAim = false;
    //}

    //private void Rotate() //Rotate uses angle modifiers and multiplies everything by a difference in x 
    //{
    //    Vector3 vectorDifference = aimedAt - this.transform.position;
    //    float angle = Mathf.Atan2(vectorDifference.y, vectorDifference.x) * Mathf.Rad2Deg - angleMod;
    //    Quaternion p = Quaternion.AngleAxis(angle, Vector3.forward);
    //    if (this.transform.position != aimedAt && manualAim == false)
    //    {
    //        transform.rotation = Quaternion.Slerp(this.transform.rotation, p, Time.deltaTime * speed * 4);
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //   if(collider.tag == "Wall")
    //    {
    //        destination = this.transform.position;
    //    }
    //}

}
