using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f); //Dictate life time if the arrow fires into nothing
    }

    // Update is called once per frame
    void Update()
    {
        //Constant forward movement
        this.transform.position += transform.up * Time.deltaTime * 10f;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy")
        {
            collider.GetComponent<Enemy>().ReceiveDamage(25); //Deal damage on colliding with an enemy
            Destroy(this.gameObject); //Destroys itself upon colliding with any enemy
        }
        if(collider.tag == "Player")
        {

        }
        else
        {
            Destroy(this.gameObject); //Destroys itself upon colliding with any obstacle
        }
    }
}
