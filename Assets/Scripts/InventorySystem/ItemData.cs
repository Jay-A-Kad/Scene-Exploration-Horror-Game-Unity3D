using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item_Data")]
public class ItemData : ScriptableObject, IInventoryItem
{
    [SerializeField] private string id;
    [SerializeField] private string displayName;
    [SerializeField] private bool stackable;
    [SerializeField] private Sprite icon;

    public string Id => id;

    public string DisplayName => DisplayName;

    public bool IsStackable => stackable;

    public Sprite Icon => icon;


}