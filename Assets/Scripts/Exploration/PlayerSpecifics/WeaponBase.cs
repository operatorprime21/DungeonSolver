using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public Weapon weapon;
    public enum Weapon
    { 
        woodenPlank,
        shovel,
        policeBaton,
        knife,
        pistol,
        crossbow,
        leverActionRifle
    }
    public WeaponType type; 
    public enum WeaponType
    { 
        melee,
        rangedHitscan,
        rangedProjectile,
    }
    public float range;
    public int damage;
    public float windup;
    public float recovery;

    public int clipSize;
    public int ammoInClip;
    public float reloadTime;
    public GameObject projPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InitProperties();
    }

    // Update is called once per frame
    void InitProperties()
    {
        switch (this.weapon)
        {
            case WeaponBase.Weapon.woodenPlank:
                this.type = WeaponType.melee;
                this.range = 1.5f;
                this.damage = 10;
                this.windup = 1.5f;
                this.recovery = 1f;
                break;
            case WeaponBase.Weapon.shovel:
                this.type = WeaponType.melee;
                this.range = 1.2f;
                this.damage = 15;
                this.windup = 2f;
                this.recovery = 1.5f;
                break;
            case WeaponBase.Weapon.policeBaton:
                this.type = WeaponType.melee;
                this.range = 1.0f;
                this.damage = 20;
                this.windup = 1.2f;
                this.recovery = 1.5f;
                break;
            case WeaponBase.Weapon.knife:
                this.type = WeaponType.melee;
                this.range = .5f;
                this.damage = 5;
                this.windup = 0.3f;
                this.recovery = 0.2f;
                break;
            case WeaponBase.Weapon.pistol: 
                this.type = WeaponType.rangedHitscan;
                this.range = 10;
                this.damage = 30;
                this.windup = 3f;
                this.recovery = 2.5f;
                this.clipSize = 6;
                this.ammoInClip = Random.Range(0, clipSize + 1);
                break;
            case WeaponBase.Weapon.leverActionRifle:
                this.type = WeaponType.rangedHitscan;
                this.range = 15;
                this.damage = 40;
                this.windup = 3.6f;
                this.recovery = 2.5f;
                this.clipSize = 4;
                this.ammoInClip = Random.Range(0, clipSize + 1);
                break;
            case WeaponBase.Weapon.crossbow:
                this.type = WeaponType.rangedProjectile;
                this.range = 0f;
                this.damage = 25;
                this.windup = 2f;
                this.recovery = 3f;
                this.clipSize = 1;
                this.ammoInClip = Random.Range(0, clipSize + 1);
                break;
        }
    }
}
