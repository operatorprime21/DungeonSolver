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

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Equipment")
        {
            InventoryItem equip = collision.gameObject.GetComponent<InventoryItem>();
           if(equip.itemType == InventoryItem.Type.weapon)
            {
                handGear = collision.gameObject;
            }
           else if (equip.itemType == InventoryItem.Type.armor)
            {
                bodyGear = collision.gameObject;
            }
        }
    }
}
