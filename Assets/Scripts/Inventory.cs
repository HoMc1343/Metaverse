using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> inventory = new List<string>();
    public static Inventory instance;
    private InventoryUI inventoryUI;

    public int gold = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 변경 시 삭제되지 않음
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }

    private void Start()
    {
        gold = 100;
        if (!inventoryUI)
        {
            inventoryUI = GetComponentInChildren<InventoryUI>();
        }
    }

    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
        inventoryUI.UpdateInventoryUI();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        FindObjectOfType<InventoryUI>().UpdateGoldUI();
    }

    public void RemoveItem(string itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
            inventoryUI.UpdateInventoryUI(); 
        }
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }
}