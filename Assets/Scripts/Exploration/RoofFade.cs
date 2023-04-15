using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofFade : MonoBehaviour
{
    private Animator roof;
    //Supposed to simulate fading in and out of a roof but it just ends up causing a lot of problems. 
    private void Start()
    {
        roof = this.GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            roof.Play("roof_fade_out");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            roof.Play("roof_fade_in");
        }
    }

    public void StayVeiled()
    {
        roof.Play("roof_veiled");
    }
    public void StayUnveiled()
    {
        roof.Play("roof_unveiled");
    }
}
