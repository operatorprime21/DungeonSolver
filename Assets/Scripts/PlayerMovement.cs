using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Camera cam;
    public Vector3 destination;
    public Vector3 aimedAt;
    public float angleMod;
    public bool manualAim;
    public Enemy lockedOnEnemy;

    public LayerMask obstacleMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && manualAim == false)
        {
            destination = ReturnDestination();
        }
        
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);

        if (lockedOnEnemy == null)
        {
            aimedAt = destination;
        }
        else
        {
            LockOn();
        }
        
        Rotate();
    }

    public Vector3 ReturnDestination()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 destination = cam.ScreenToWorldPoint(mousePos);
        return destination;
    }

    private void LockOn()
    {
        aimedAt = lockedOnEnemy.transform.position;
        manualAim = false;
    }

    private void Rotate()
    {
        Vector3 vectorDifference = aimedAt - this.transform.position;
        float angle = Mathf.Atan2(vectorDifference.y, vectorDifference.x) * Mathf.Rad2Deg - angleMod;
        Quaternion p = Quaternion.AngleAxis(angle, Vector3.forward);
        if (this.transform.position != aimedAt && manualAim == false)
        {
            transform.rotation = Quaternion.Slerp(this.transform.rotation, p, Time.deltaTime * speed * 4);
        }
    }

    private void OnEnter2D(Collider2D collider)
    {
       if(collider.tag == "Wall")
        {
            destination = this.transform.position;
        }
    }

}
