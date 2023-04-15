using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script purely made to hold slots that are on any object that has loots, representing its contents
public class SlotHolder : MonoBehaviour
{
    public int slotCount;
    public List<Slot> slots = new List<Slot>();
}
