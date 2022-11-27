using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float startHealth;
    public float maxHealth;
    public float currentHealth;

    public float maxThirst;
    public float currentThirst;

    public float maxHunger;
    public float currentHunger;

    public Slider healthM;
    public Slider thirstM;
    public Slider hungerM;

    private void Start()
    {
        currentHealth = maxHealth;
        currentThirst = maxThirst;
        currentHunger = maxHunger;

        healthM.maxValue = maxHealth;
        thirstM.maxValue = maxThirst;
        hungerM.maxValue = maxHunger;

        healthM.value = currentHealth;
        hungerM.value = currentHunger;
        thirstM.value = currentThirst;

        StartCoroutine(DrainThirst(0.5f));
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
        currentHealth = currentHealth - d;
        healthM.value = currentHealth;
    }
    private void ConsumeEnergy(float e)
    {
        currentHunger = currentHunger - e;
        hungerM.value = currentHunger;
    }
    IEnumerator DrainThirst(float t)
    {
        for(int s = 0; s < 3; s++)
        {
            currentThirst = currentThirst - t;
            thirstM.value = currentThirst;
            yield return new WaitForSeconds(1f);
            s--;
        }
    }

    public void Death()
    {
        Debug.Log("You Died");
    }
}
