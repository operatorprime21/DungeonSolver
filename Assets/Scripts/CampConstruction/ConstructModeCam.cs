using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructModeCam : MonoBehaviour
{
    public Vector3 tappedPos;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Camera cam = this.GetComponent<Camera>();
            Vector3 FirstmousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            tappedPos = cam.ScreenToWorldPoint(FirstmousePos);
        }

        if(Input.GetMouseButton(0))
        {
            Camera cam = this.GetComponent<Camera>();
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 posToMove = cam.ScreenToWorldPoint(mousePos);
            Vector3 camTransform = mousePos - posToMove;
            this.gameObject.transform.position = camTransform;
        }
    }
}
