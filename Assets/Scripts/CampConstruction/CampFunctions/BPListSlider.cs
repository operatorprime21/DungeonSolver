using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPListSlider : MonoBehaviour
{
    public Vector3 tappedPos;
    // Start is called before the first frame update
    private void Update()
    {
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

            this.gameObject.transform.position += camTransform;
        }
    }
}
