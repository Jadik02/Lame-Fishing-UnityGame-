using TMPro;
using UnityEngine;


public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] Inventory _inventory;
    [SerializeField] PlayerController _playerController;
    [SerializeField] TMP_Text _text;
    [SerializeField] TMP_Text BaitCountText;

    public void DisplayMoney()
    {
        _text.text =_inventory.CountFishPrice().ToString();
        

    }
    public void UpdateMoney()
    {
        _text.text = _inventory.finalTotalPrice.ToString();
    }
    public void UpdateBait()
    {
        BaitCountText.text = _playerController.BaitCount.ToString();
        
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
