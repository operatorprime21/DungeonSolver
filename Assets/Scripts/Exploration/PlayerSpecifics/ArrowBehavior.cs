using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += transform.up * Time.deltaTime * 10f;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy")
        {
            collider.GetComponent<Enemy>().ReceiveDamage(25);
            Destroy(this.gameObject);
        }
        if(collider.tag == "Player")
        {

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
