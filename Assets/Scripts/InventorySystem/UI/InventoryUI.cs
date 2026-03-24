using System.Text;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TMP_Text inventoryText;

    private InventoryManager inventoryManager;
    private bool isOpen;

    public void Bind(InventoryManager manager)
    {
        inventoryManager = manager;
        if (inventoryManager != null)
        {
            inventoryManager.Inventory.OnInventoryChanged += Refresh;
        }
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
        Refresh();

    }
    public void OnDestroy()
    {
        if (inventoryManager != null)
        {
            inventoryManager.Inventory.OnInventoryChanged -= Refresh;
        }
    }
    public void Toggle()
    {
        isOpen = !isOpen;
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(isOpen);
        }
        if (isOpen)
        {
            Refresh();
        }
    }
    public void Refresh()
    {
        if (inventoryText == null || inventoryManager == null) return;
        var slots = inventoryManager.Inventory.Slots;
        if (slots.Count == 0)
        {
            inventoryText.text = "Inventory is empty.";
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Inventory:");
        sb.AppendLine("-----------");
        foreach (var slot in slots)
        {
            sb.AppendLine($"{slot.Item.DisplayName} x{slot.Quantity}");
        }
        inventoryText.text = sb.ToString();
    }
}