using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu]
public class LevelInfo : ScriptableObject
{
    // Start is called before the first frame update

    public int star;
    public int candle;

    public bool levelIsUnlocked;
    
    public int unlockCost;

    public string levelStatus;
    

    
    void Start()
    {
        //SetLevelUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    
}
