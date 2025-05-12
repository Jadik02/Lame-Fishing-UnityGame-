using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] MoneyDisplay moneyDisplay;
    [SerializeField] Inventory _Inventory;
    [SerializeField] GameObject storeUi;
    [SerializeField] PlayerController _PlayerController;
    private float BaitCost = 5;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            
            storeUi.SetActive(true);
            
        }

            
            Debug.Log("EnterShop");
        }
        public void SellFishButton()
        {
        moneyDisplay.DisplayMoney();
        if (_Inventory != null)
        {
            List<InventoryItem> FishToRemove = new List<InventoryItem>(_Inventory.inventory);

            foreach (InventoryItem item in FishToRemove)
            {
                _Inventory.Remove(item.itemData);
            }
        }
        
    }
    public void BuyBaitButton()
    {
        if (_PlayerController.BaitCount < _PlayerController.maxBaitCount && _Inventory.finalTotalPrice >= BaitCost)
        {
            _Inventory.finalTotalPrice -= BaitCost;
            _PlayerController.BaitCount++;
            moneyDisplay.UpdateMoney();
            moneyDisplay.UpdateBait();
        }
        moneyDisplay.UpdateBait();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            storeUi.SetActive(false);
            Debug.Log("ExitShop");
        }
    }
}
