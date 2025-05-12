using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;



public class InventoryItems : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI ItemText;
    public TextMeshProUGUI StackAmountText;

    public void ResetSlots()
    {
        icon.enabled = false;
        ItemText.enabled = false;
        StackAmountText.enabled = false;
    }
    public void ShowItems()
    {
        icon.enabled = true;
        //ItemText.enabled = true;
        StackAmountText.enabled = true;
    }
    public void DisplayItem(InventoryItem item)
    {
        if(item ==null)
        {
            ResetSlots();
            return;
        }
        ShowItems();

        icon.sprite = item.itemData.FishSprite;
        ItemText.text = item.itemData.FishName;
        StackAmountText.text = item.stackSize.ToString();
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
