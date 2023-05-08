using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructModeCam : MonoBehaviour
{
    public Vector3 tappedPos;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) //Gets the first point of click
        {
            Camera cam = this.GetComponent<Camera>();
            Vector3 FirstmousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            tappedPos = cam.ScreenToWorldPoint(FirstmousePos);
        }

        else if(Input.GetMouseButton(0)) //Dragging the mouse while clicking moves the camera according to how the current mouse position is to the first clicked position
        {
            Camera cam = this.GetComponent<Camera>();
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 posToMove = cam.ScreenToWorldPoint(mousePos);
            Vector3 transformV3 = tappedPos - posToMove;
            Vector3 camTransform = new Vector3(transformV3.x, transformV3.y, 0);

            this.gameObject.transform.position += camTransform;
        }
    }
}
