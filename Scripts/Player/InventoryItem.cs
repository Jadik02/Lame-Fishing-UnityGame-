using System;
using UnityEngine;
[Serializable]
public class InventoryItem
{
    public Fish itemData;
    public int stackSize;

    public InventoryItem(Fish item)
    {
        itemData = item;
        AddToStack();
    }
    public void AddToStack()
    {
        stackSize++;
    }
    public void RemoveFromStack()
    {
        //stackSize--;
        stackSize = 0;
    }
}
