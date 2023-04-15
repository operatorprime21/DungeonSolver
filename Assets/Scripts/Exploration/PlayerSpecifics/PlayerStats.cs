using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float startHealth;
    public float maxHealth;
    public float currentHealth;
    public float currentHunger;

    public float maxHunger;

    public bool hasArmor;

    public Slider healthM;
    public Slider hungerM;


    private void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;

        //healthM.maxValue = maxHealth;

        healthM.value = currentHealth;

        //hungerM.value = currentHunger;

        
        //StartCoroutine(DrainThirst(0.5f));
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(20);
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    ConsumeEnergy(15);
        //}

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float d)
    {
        Equipment equipped = this.gameObject.transform.Find("Equipment").GetComponent<Equipment>();
        if(equipped.bodyGear != null) //Armor reduction is a simple calculation where the damage done to the player is reduced by a single amount
        {
            ArmorBase armor = equipped.bodyGear.GetComponent<ArmorBase>();
            d -= armor.dmgReduce;
        }
        currentHealth = currentHealth - d;
        healthM.value = currentHealth;
        
    }
    //private void ConsumeEnergy(float e)
    //{
    //    currentHunger = currentHunger - e;
    //    hungerM.value = currentHunger;
    //}
    //IEnumerator DrainThirst(float t)
    //{
    //    for(int s = 0; s < 3; s++)
    //    {
    //        currentHunger = currentHunger - t;
    //        hungerM.value = currentHunger;
    //        yield return new WaitForSeconds(1f);
    //        s--;
    //    }
    //}

    public void Death()
    {
        Debug.Log("You Died");
    }
}
