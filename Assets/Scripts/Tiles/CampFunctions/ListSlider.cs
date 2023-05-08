using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSlider : MonoBehaviour
{
    public Vector3 tappedPos;
    // Start is called before the first frame update
    private void Update()
    { //Summary: Similar to other sliders, this just takes the first point of tapping and the point the player drags and change the location of the item according to the x coordinates difference of said two points
        if (Input.GetMouseButtonDown(0))
        {
            Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 FirstmousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            tappedPos = cam.ScreenToWorldPoint(FirstmousePos);
        }

        else if (Input.GetMouseButton(0))
        {
            Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 posToMove = cam.ScreenToWorldPoint(mousePos);
            Vector3 transformV3 = posToMove - tappedPos;
            Vector3 camTransform = new Vector3(transformV3.x, 0, 0);

            this.gameObject.transform.position += camTransform*0.01f;
        }
    }
}
