using UnityEngine;
using UnityEngine.Video;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    private Inventory<ItemData> inventory = new Inventory<ItemData>();

    [SerializeField] private InventoryUI inventoryUI;
    public Inventory<ItemData> Inventory => inventory;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (inventoryUI != null)
        {
            inventoryUI.Bind(this);
        }
    }

    private void Update()
    {
        if (InventoryTogglePressed() && inventoryUI != null)
        {
            inventoryUI.Toggle();
        }
    }

    public void AddItem(ItemData item, int amount = 1)
    {
        inventory.AddItem(item, amount);
    }

    public bool RemoveItem(string itemId, int amount = 1)
    {
        return inventory.RemoveItem(itemId, amount);
    }
    public bool HasItem(string itemId, int amount = 1)
    {
        return inventory.HasItem(itemId, amount);
    }
    private bool InventoryTogglePressed()
    {
        return Input.GetKeyDown(KeyCode.I);
    }
}