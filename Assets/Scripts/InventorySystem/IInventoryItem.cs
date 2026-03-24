using UnityEngine;

public interface IInventoryItem
{
    string Id { get; }
    string DisplayName { get; }
    bool IsStackable { get; }
}
