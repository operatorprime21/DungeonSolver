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
            firstClickedPoint = Input.mousePosition.x;
        }

        if(Input.GetMouseButton(0) && touched == true)
        {
            float currentClickPoint = Input.mousePosition.x;
            float xMultiplier = firstClickedPoint - currentClickPoint;
            xMultiplier = Mathf.Clamp(xMultiplier, -100, 100);
            Vector3 playerPos = player.transform.position;
            player.transform.Rotate(new Vector3(0, 0, xMultiplier*0.01f));
        }
    }

    private void OnMouseDown()
    {
        touched = true;
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
