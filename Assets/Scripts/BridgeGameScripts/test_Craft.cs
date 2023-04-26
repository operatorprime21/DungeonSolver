using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_Craft : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject testBridge;
    private Camera cam;
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestSpawnBridge()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 itemPos = cam.ScreenToWorldPoint(mousePos);
        Instantiate(testBridge, new Vector3(itemPos.x, itemPos.y, 0f), Quaternion.identity);
    }
}
