using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isAttacking = false;

    public LayerMask enemyMask;
    public LayerMask obstacleMask;
    public LayerMask mask;
    public GameObject forward;

    private GameObject slot;
    public GameObject inventory;


    public void BeginAttack() //Called by a button press
    {
        Equipment equipped = GameObject.Find("Status").GetComponent<Equipment>();
        slot = equipped.handGear;
        WeaponBase weapon = slot.GetComponent<WeaponBase>();
        if(weapon != null)
        {
            StartCoroutine(Attack(weapon, weapon.range, weapon.damage, weapon.windup, weapon.recovery));
        }
    }

    public void AttackCooldownReset() //Called by end of animation frame and coroutine
    {
        isAttacking = false;
    }

    IEnumerator Attack(WeaponBase weapon, int range, int damage, float windup, float recovery) 
    {
        if (isAttacking == false)
        {
            isAttacking = true;
            yield return new WaitForSeconds(windup); //Simulate an attack windup
            switch (weapon.type)
            {
                case WeaponBase.WeaponType.melee:
                    CreateMeleeHitbox(range, damage);
                    Debug.Log(weapon.weapon);
                    break;
                case WeaponBase.WeaponType.rangedHitscan:
                    HitscanBullet(weapon, range, damage);
                    break;
                case WeaponBase.WeaponType.rangedProjectile:
                    SpawnProjectile(weapon);
                    break;
            }
            yield return new WaitForSeconds(recovery);
            AttackCooldownReset();
        }
    }

    public void CreateMeleeHitbox(int range, int damage)
    {
        Collider2D enemyInRange = Physics2D.OverlapCircle(this.transform.position, range, enemyMask); //Produce a circle overlap around the player detecting anything with the enemy layermask
        List<Collider2D> enemiesInRange = new List<Collider2D>(); //Create a new list to consider multiple enemies within the hitbox range
        if (enemyInRange != null) //Begins going through conditions if enemies gets within range
        {
            Vector3 enemyPos = enemyInRange.transform.position; //Get enemy position
            Vector3 directionToEnemy = (enemyPos - this.transform.position).normalized; //Get direction to enemy using normalized
            if (Vector2.Angle(this.transform.up, directionToEnemy) < range / 2) //Check if the enemy is within the hitbox range
            {
                float distanceToEnemy = Vector2.Distance(this.transform.position, enemyPos); //Calculate distance between player and enemy to see if they are under the attack range
                if (!Physics2D.Raycast(this.transform.position, directionToEnemy, distanceToEnemy, obstacleMask))
                //Check if there is no wall between the player and the enemy within the attack range
                {
                    enemiesInRange.Add(enemyInRange); //Add all the enemies that passed all the conditions to a list
                    foreach (Collider2D enemy in enemiesInRange)
                    {
                        Enemy enemyScript = enemy.gameObject.GetComponent<Enemy>(); //Get enemy script reference
                        enemyScript.ReceiveDamage(damage); //Do damage to every enemy in the list
                    }
                }
            }
        }
    }


    public void HitscanBullet(WeaponBase gun, int range, int damage)
    {
        //Check current clip first
        if(gun.ammoInClip > 0)
        {
            gun.ammoInClip--;
            Vector3 aimDir = (forward.transform.position - this.transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector3.forward + aimDir, range, mask);
            if (hit.collider.tag == "Enemy")
            {
                Debug.DrawRay(this.transform.position, (Vector3.forward + aimDir) * range, Color.green, 2f);

                Enemy enemyScript = hit.collider.GetComponent<Enemy>(); //Get enemy script reference
                enemyScript.ReceiveDamage(damage); //Do damage to the first enemy thats hit
            }
            else
            {
                Debug.DrawRay(this.transform.position, (Vector3.forward + aimDir) * range, Color.red, 2f);
            }
        }
        else
        {
            Reload(gun, InventoryItem.Item.ammo);
        }
    }
    //Note: This function will serve as a baseline for any future crafting/building related behaviors that require automatic grabbing from inventory
    private static void Reload(WeaponBase gun, InventoryItem.Item ammoType) 
    {
        Debug.Log("EmptyClip");
        Inventory inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>();
        List<GameObject> items = inventory.ReturnInventory();
        foreach (GameObject item in items)
        {
            InventoryItem type = item.GetComponent<InventoryItem>();
            if (type.item == ammoType)
            {
                int count = type.currentCount;
                if (gun.ammoInClip == 0)
                {
                    if (count > gun.clipSize)
                    {
                        type.currentCount -= gun.clipSize;
                        type.count.text = type.currentCount.ToString();
                        gun.ammoInClip = gun.clipSize;
                        break;
                    }
                    else if (count == gun.clipSize)
                    {
                        gun.ammoInClip = gun.clipSize;
                        type.removeBool();
                        Destroy(item);
                        break;
                    }
                    else
                    {
                        gun.ammoInClip += count;
                        type.removeBool();
                        Destroy(item);
                    }
                }
                else
                {
                    int clipLeft = gun.clipSize - gun.ammoInClip;
                    if (clipLeft < count)
                    {
                        gun.ammoInClip = gun.clipSize;
                        type.currentCount -= clipLeft;
                        type.count.text = type.currentCount.ToString();
                        break;
                    }
                    if (clipLeft == count)
                    {
                        gun.ammoInClip = gun.clipSize;
                        type.removeBool();
                        Destroy(item);
                        break;
                    }
                    if (clipLeft > count)
                    {
                        gun.ammoInClip += count;
                        type.removeBool();
                        Destroy(item);
                    }
                }
            }
        }
    }

    public void SpawnProjectile(WeaponBase weapon)
    {
        if(weapon.ammoInClip > 0)
        {
            weapon.ammoInClip--;
            GameObject projPref = weapon.projPrefab;
            Vector3 aimDir = (forward.transform.position - this.transform.position).normalized;
            float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 90;
            Quaternion p = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject arrow = Instantiate(projPref, this.transform.position, p);
        }
        else
        {
            Reload(weapon, InventoryItem.Item.arrow);
        }
    }
}

