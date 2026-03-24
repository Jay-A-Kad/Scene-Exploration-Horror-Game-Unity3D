using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject, IInventoryItem
{
    [SerializeField] private string id;
    [SerializeField] private string displayName;
    [SerializeField] private bool stackable;
    [SerializeField] private Sprite icon;

    public string Id => id;

    public string DisplayName => displayName;

    public bool Stackable => stackable;

    public Sprite Icon => icon;


}