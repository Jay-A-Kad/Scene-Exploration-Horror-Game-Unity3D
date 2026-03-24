using System.Xml.Serialization;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int quantity = 1;
    [SerializeField] private string PlayerTag = "Player";
    [SerializeField] private GameObject pickupPrompt;
    [SerializeField] private TMP_Text pickUpText;

    private bool isPlayerInRange = false;

    private void Reset()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }
    private void Start()
    {
        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(false);
        }
        if (pickUpText != null && itemData != null)
        {
            pickUpText.text = $"Press 'E' to pick up {itemData.DisplayName}";
        }

    }

    private void Update()
    {
        if (!isPlayerInRange) return;
        if (PickUpPresseed())
        {
            PickUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerTag)) return;
        isPlayerInRange = true;
        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(true);
        }
        if (pickUpText != null && itemData != null)
        {
            pickUpText.text = $"Press 'E' to pick up {itemData.DisplayName}";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(PlayerTag)) return;
        isPlayerInRange = false;
        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(false);
        }
    }

    private void PickUp()
    {
        if (itemData == null)
        {
            Debug.LogWarning("ItemData is not assigned on " + gameObject.name);
            return;
        }
        if (InventoryManager.Instance == null)
        {
            Debug.LogWarning("No InventoryManager found in the scene.");
            return;
        }
        InventoryManager.Instance.AddItem(itemData, quantity);
        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(false);
        }
        Destroy(gameObject);

    }
    private bool PickUpPresseed()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}