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

        DrainThirst(5);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ConsumeEnergy(15);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            DrainThirst(10);
        }
    }

    private void TakeDamage(float d)
    {
        currentHealth = currentHealth - d;
        healthM.value = currentHealth;
    }
    private void ConsumeEnergy(float e)
    {
        currentHunger = currentHunger - e;
        hungerM.value = currentHunger;
    }
    private void DrainThirst(float t)
    {
        currentThirst = currentThirst - t;
        thirstM.value = currentThirst;
    }
}
