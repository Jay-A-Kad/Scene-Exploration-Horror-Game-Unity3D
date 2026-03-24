using UnityEngine;

public interface IInventoryItem
{
    string Id { get; }
    string DisplayName { get; }
    bool Stackable { get; }
}
