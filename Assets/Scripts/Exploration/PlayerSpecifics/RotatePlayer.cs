using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public GameObject player;
    public bool touched = false;
    private float firstClickedPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && touched == true)
        {
            firstClickedPoint = Input.mousePosition.x;  //Get input point at x
        }

        if(Input.GetMouseButton(0) && touched == true)
        {
            float currentClickPoint = Input.mousePosition.x; //Get second input point as the mouse moves at x
            float xMultiplier = firstClickedPoint - currentClickPoint;  //find the difference
            xMultiplier = Mathf.Clamp(xMultiplier, -100, 100); //give it max values
            Vector3 playerPos = player.transform.position; 
            player.transform.Rotate(new Vector3(0, 0, xMultiplier*0.01f)); //then change rotation accordingly with the x 
        }
    }

    private void OnMouseDown()
    {
        touched = true; //Change variables to make sure manual and auto aiming doesnt clash
        player.GetComponent<PlayerMovement>().manualAim = true;
        player.GetComponent<PlayerMovement>().lockedOnEnemy = null;
        player.GetComponent<PlayerMovement>().aimedAt = player.transform.position; 
    }

    private void OnMouseUp()
    {
        touched = false;
        player.GetComponent<PlayerMovement>().manualAim = false;
    }
}
