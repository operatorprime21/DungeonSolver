using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBase : MonoBehaviour
{
    public int survStatPos;
    public int survStatNeg;

    public int dmgReduce;
    public Armor armor;

    // Start is called before the first frame update
    public enum Armor
    {
        sweater,
        bulletProofVest,
        hazmatSuit,
        footballSuit,
        heavyJacket,
    }

    private void Start()
    {
        InitStat();
    }
    private void InitStat()
    {
        switch (this.armor)
        {
            case ArmorBase.Armor.sweater:
                dmgReduce = 1;
                break;
            case ArmorBase.Armor.bulletProofVest:
                dmgReduce = 5;
                break;
            case ArmorBase.Armor.hazmatSuit:
                dmgReduce = 4;
                break;
            case ArmorBase.Armor.footballSuit:
                dmgReduce = 6;
                break;
            case ArmorBase.Armor.heavyJacket:
                dmgReduce = 2;
                break;
        }

    }

    public void ChangeStat()
    {
        //Check the equip-er to see if this is the player or a survivor
        //if its a survivor, change their stats
    }
}
