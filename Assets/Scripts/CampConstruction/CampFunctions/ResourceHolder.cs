using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHolder : MonoBehaviour //Handles the three core resources and changing their UI accordingly
{
    // Start is called before the first frame update
    public int powerCell;
    public int corneaFruit;
    public int foodUnits;
    public List<InventoryItem> resources = new List<InventoryItem>();

    public Text cell;
    public Text unit;
    public Text fruit;
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

    public void ChangeFruit(int fruitChange)
    {
        corneaFruit += fruitChange;
        fruit.text = corneaFruit.ToString();
    }

    public void ChangeCell(int cellChange)
    {
        powerCell += cellChange;
        cell.text = powerCell.ToString();
    }

    public void ChangeFood(int foodChange)
    {
        foodUnits += foodChange;
        unit.text = foodUnits.ToString();
    }

}
