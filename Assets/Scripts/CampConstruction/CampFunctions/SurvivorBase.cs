using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorBase : MonoBehaviour
{
    //Holds all info on Survivor
    public int currentHealth;
    public int maxHealth;

    public int currentHunger;
    public int maxHunger;

    public int perseverance;
    public int adaptability;
    public int ironWill;

    public void Heal(int healAmount)
    {
        if (currentHealth == maxHealth)
        {
            //Health full.
        }
        else
        {
            int remainingHP = maxHealth - currentHealth;
            if (remainingHP <= healAmount)
            {
                currentHealth = maxHealth;
            }
            else if (remainingHP > healAmount)
            {
                currentHealth += healAmount;
            }
        }
    }

    public void Feed(int restoreAmount)
    {
        if (currentHunger == maxHunger)
        {
            //Hunger full.
        }
        else
        {
            int remainingHP = maxHunger - currentHunger;
            if (remainingHP <= restoreAmount)
            {
                currentHunger = maxHunger;
            }
            else if (remainingHP > restoreAmount)
            {
                currentHunger += restoreAmount;
            }
        }
    }

}
