using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public TextMeshProUGUI itemText; // 아이템 이름
    public Button itemButton; // 아이템 클릭

    private string itemName; // 슬롯 해당 아이템 이름

    public void SetItem(string name)
    {
        itemName = name; 
        itemText.text = name; // 아이템 이름
    }

    public void OnClick()
    {
        PlayerManager.Instance.GetComponent<Inventory>().RemoveItem(itemName);
        PlayerManager.Instance.GetComponent<InventoryUI>().UpdateInventoryUI();
    }

    private void Start()
    {
        itemButton.onClick.AddListener(OnClick);
    }
}