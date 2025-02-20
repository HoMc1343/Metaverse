using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> inventory = new List<string>();

    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
        Debug.Log($"아이템 획득: {itemName}");
    }

    public void RemoveItem(string itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
            Debug.Log($"아이템 사용 또는 삭제: {itemName}");
        }
        else
        {
            Debug.Log($"인벤토리에 {itemName}이(가) 없습니다.");
        }
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }

    public void ShowInventory()
    {
        Debug.Log("=== 인벤토리 ===");
        foreach (string item in inventory)
        {
            Debug.Log(item);
        }
    }
}
