using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public GameObject handGear;
    public GameObject bodyGear;

    // Start is called before the first frame update
    private void Start()
    {
        EquipWeapon(); //For testing purposes
        //Need to implement equipping and unequipping better
    }

    public void EquipWeapon()
    {
        this.gameObject.transform.parent.GetComponent<PlayerAttack>().slot = handGear;
    }
}
