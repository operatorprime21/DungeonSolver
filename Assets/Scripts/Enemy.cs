using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float viewRadius; [Range(0,360)]
    public float viewAngle;
    public float angleMod;

    public LayerMask playerMask;
    public LayerMask obstacleMask;

    public bool seeingPlayer;
    public string state;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        if(seeingPlayer == true)
        {
            
        }
    }

    private void FindPlayer()
    {
        Collider2D playerInView = Physics2D.OverlapCircle(this.transform.position, viewRadius, playerMask);
        
        if(playerInView != null)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 directionToPlayer = (playerPos - this.transform.position).normalized;
            if(Vector2.Angle(this.transform.up, directionToPlayer) < viewAngle/2)
            {
                float distanceToPlayer = Vector2.Distance(this.transform.position, playerPos);
                if (!Physics2D.Raycast(this.transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                    seeingPlayer = true;
                    LockOnPlayer();
                }
                else seeingPlayer = false;
            }
            else seeingPlayer = false;
        }
        else seeingPlayer = false;
    }

    public void OnMouseDown()
    {
        PlayerMovement playerController = player.GetComponent<PlayerMovement>();
        if(playerController.lockedOnEnemy == this)
        {
            playerController.lockedOnEnemy = null;
            playerController.manualAim = false;
        }
        else
        {
            playerController.lockedOnEnemy = this;
            playerController.manualAim = true;
        }
    }

    private void OnDestroy()
    {
        PlayerMovement playerController = player.GetComponent<PlayerMovement>();
        playerController.lockedOnEnemy = null;
        playerController.manualAim = false;
    }
    private void LockOnPlayer()
    {
        Vector3 aimedAt = player.transform.position;
        Vector3 vectorDifference = aimedAt - this.transform.position;
        float angle = Mathf.Atan2(vectorDifference.y, vectorDifference.x) * Mathf.Rad2Deg - angleMod;
        Quaternion p = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, p, Time.deltaTime *4);
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 1*Time.deltaTime);
    }
}
