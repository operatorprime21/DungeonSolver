using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureMove : MonoBehaviour
{
    public List<Tile> tileToFill = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 itemPos = cam.ScreenToWorldPoint(mousePos);
        this.transform.position = new Vector3(itemPos.x, itemPos.y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tile")
        {
            Tile tile = collision.GetComponent<Tile>();
            if(tile.ReturnValid() == true)
            {
                tileToFill.Add(tile);
            }
            else
            {

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tile")
        {
            Tile tile = collision.GetComponent<Tile>();
            if(tile.ReturnValid() == true)
            {
                tileToFill.Remove(tile);
            }
        }
    }
}
