using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 playerPos = player.transform.position;
        this.transform.position = new Vector3(playerPos.x, playerPos.y, -10);
    }
}
