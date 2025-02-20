using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> inventory = new List<string>();
    private InventoryUI inventoryUI;

    private void Start()
    {
        if (!inventoryUI)
        {
            inventoryUI = GetComponentInChildren<InventoryUI>();
        }
    }

    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
        Debug.Log($"아이템 획득: {itemName}");
        inventoryUI.UpdateInventoryUI();
    }

    public void RemoveItem(string itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
            Debug.Log($"아이템 사용: {itemName}");
            inventoryUI.UpdateInventoryUI(); 
        }
        else
        {
            Debug.Log($"{itemName}이(가) 인벤토리에 없습니다.");
        }
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }
}