using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float finalTotalPrice;
    public float totalPrice;
    public static event Action<List<InventoryItem>> OnInventoryChange;

    public List<InventoryItem> inventory;
    private Dictionary<Fish, InventoryItem> itemDictionary;

    private void OnEnable()
    {
        FishScript.OnFishDropping += Add;
    }
    private void OnDisable()
    {
        FishScript.OnFishDropping -= Add;
    }

    public void Start()
    {
        itemDictionary = new Dictionary<Fish, InventoryItem>();
        inventory = new List<InventoryItem>();
    }


    public void Add(Fish itemData)
    {
        if (itemData == null)
        {
            Debug.LogError("itemData is null. Cannot add to inventory.");
            return;
        }
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            Debug.Log($"{item.itemData.FishName} total stack is { item.stackSize }");
            OnInventoryChange?.Invoke(inventory);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            OnInventoryChange?.Invoke(inventory);
        }
    }
    public void Remove(Fish itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if(item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(inventory);
        }
    }

    public float CountFishPrice()
    {
        
        totalPrice = 0f;
        foreach (InventoryItem item in inventory)
        {
            if (item.itemData != null)
            {
                totalPrice += item.itemData.FishPrice * item.stackSize;
            }

        }

        return finalTotalPrice += totalPrice;

    }

    
}
