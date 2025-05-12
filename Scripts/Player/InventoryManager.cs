using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public List<InventoryItems> inventoryItems = new List<InventoryItems> (12);

    void ClearInventory()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        inventoryItems = new List<InventoryItems> (12);
    }
    void ShowInventory(List<InventoryItem> inventory)
    {
        ClearInventory();

        for (int i = 0; i < inventoryItems.Capacity; i++)
        {
            CreateSlot();
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItems[i].DisplayItem(inventory[i]);
        }
    }
    void CreateSlot()
    {
        GameObject Slot = Instantiate(itemPrefab);
        Slot.transform.SetParent(transform, false);

        InventoryItems SlotComponent = Slot.GetComponent<InventoryItems>();
        SlotComponent.ResetSlots();

        inventoryItems.Add(SlotComponent);
    }

    private void OnEnable()
    {
        Inventory.OnInventoryChange += ShowInventory;
    }
    private void OnDisable()
    {
        Inventory.OnInventoryChange -= ShowInventory;
    }
    // Start is      once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
