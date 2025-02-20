using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; // 인벤토리 전체 UI 패널
    public GameObject itemSlotPrefab; // 아이템 슬롯 프리팹
    public Transform itemGrid; // 아이템이 배치될 그리드
    public TextMeshProUGUI goldText; // 보유 골드
    private Inventory inventory; // 인벤토리 데이터

    public static InventoryUI instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventory = GetComponentInParent<Inventory>(); // 부모(Player)에서 Inventory 가져오기
        inventoryPanel.SetActive(false); // 시작 시 비활성화
        UpdateInventoryUI();
        UpdateGoldUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);

        if (isActive)
        {
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (string item in inventory.inventory)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemGrid);
            slot.GetComponentInChildren<TextMeshProUGUI>().text = item;
        }
    }

    public void UpdateGoldUI()
    {
        if (goldText != null && inventory != null)
        {
            goldText.text = "Gold: " + inventory.gold.ToString();
        }
    }
}