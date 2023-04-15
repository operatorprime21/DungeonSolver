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

    public float health;
    public float attackRange;
    public float attackDamage;
    private bool inAttackAnim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); //Get reference to player
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        if(seeingPlayer == true)
        {
            
        }

        if(health <= 0)
        {
            Death();
        }
    }

    private void FindPlayer() //Called in update, FindPlayer() does a lot of things to check player in LoS
    {
        Collider2D playerInView = Physics2D.OverlapCircle(this.transform.position, viewRadius, playerMask); //Produce a circle overlap around the enemy checking for player using player layermask
        
        if(playerInView != null) //Begins going through conditions if player gets within view
        {
            Vector3 playerPos = player.transform.position; //Get player position
            Vector3 directionToPlayer = (playerPos - this.transform.position).normalized; //Get direction from enemy to player using normalized
            if(Vector2.Angle(this.transform.up, directionToPlayer) < viewAngle/2) //Check anything within a radius infront of the enemy. The /2 is because the angle is counted from the forward vector
            {
                float distanceToPlayer = Vector2.Distance(this.transform.position, playerPos); //Calculate distance between player and enemy if the player is within the FoV cone
                if (!Physics2D.Raycast(this.transform.position, directionToPlayer, distanceToPlayer, obstacleMask)) 
                    //Check if there is no wall between the player using a Raycast
                    //that shoots from the enemy, going into the direction of the player, only if the player is within viewing range from the FoV cone
                    //If not true, means there is no obstacle and the player is within viewing cone, distance, and clear LoS
                {
                    seeingPlayer = true; //seeingPlayer sets bool to make the enemy move towards the player as long as bool applies. 
                    LockOnPlayer();
                    if(distanceToPlayer <= attackRange && inAttackAnim == false)
                    {
                        StartCoroutine(DoDamage(attackDamage));
                    }
                }
                else seeingPlayer = false;
            }
            else seeingPlayer = false;
        }
        else seeingPlayer = false;
    }

    public void OnMouseDown() //Lets the player lock on to a certain enemy. Sets bool to change the view direction functions already existing in the movement script
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

    private void Death() //Might change this to disable and enable instead to save resource
    {
        PlayerMovement playerController = player.GetComponent<PlayerMovement>();
        playerController.lockedOnEnemy = null;
        playerController.manualAim = false;
        this.gameObject.SetActive(false);
        Debug.Log("Enemy killed, dropping loot");
    }
    private void LockOnPlayer() //Lock on the player as long as the player checks all the FoV conditions
    {
        Vector3 aimedAt = player.transform.position; //Get player position
        Vector3 vectorDifference = aimedAt - this.transform.position; //Get Vector difference to grab angle difference
        float angle = Mathf.Atan2(vectorDifference.y, vectorDifference.x) * Mathf.Rad2Deg - angleMod; //Calculate angle difference using the difference in x and y coordinates, 
        //using said difference in x and y to calculate angle to turn using tangent to degrees formulas
        Quaternion p = Quaternion.AngleAxis(angle, Vector3.forward); //Get the angle and turn it in a Quaternion to use in rotation
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, p, Time.deltaTime *4); //Rotate using Slerp
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 1*Time.deltaTime); //Move towards the player
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        Debug.Log(health+"HP left");
    }

    IEnumerator DoDamage(float damage) //Doing damage, setting cooldowns, windup, all the stop
    {
        inAttackAnim = true;
        yield return new WaitForSeconds(0.5f);
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.TakeDamage(damage);
        yield return new WaitForSeconds(2f);
        inAttackAnim = false;
    }
}
