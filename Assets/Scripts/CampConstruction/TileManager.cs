using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public string TileHovered;
    private Camera mainCam;
    public LayerMask mask;
    private void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePos);
            RaycastHit2D hitPoint = Physics2D.Raycast(worldPos, new Vector2(0, 0), Mathf.Infinity, mask);
            if(hitPoint)
            {
                if (hitPoint.collider.tag == "Tile")
                {
                    TileHovered = hitPoint.collider.name;
                }
                else
                {
                    TileHovered = null;
                }
            }
            else
            {
                TileHovered = null;
            } 
        }
    }
}
