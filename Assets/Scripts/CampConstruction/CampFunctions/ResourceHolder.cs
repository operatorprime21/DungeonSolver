using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public int powerCell;
    public int corneaFruit;
    public int foodUnits;

    public TMP_Text cell;
    public TMP_Text unit;
    public TMP_Text fruit;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start()
    {
        cell.text = powerCell.ToString();
        unit.text = foodUnits.ToString();
        fruit.text = corneaFruit.ToString();
    }
}
