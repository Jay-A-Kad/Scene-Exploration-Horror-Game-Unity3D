


using Unity.VisualScripting;

public class InventorySlot<T> where T : IInventoryItem
{
    public T Item { get; private set; }
    public int Quantity { get; private set; }

    public InventorySlot(T item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public void Add(int amount)
    {
        Quantity += amount;
    }

    public void Remove(int amount)
    {
        Quantity -= amount;
        if (Quantity < 0)
        {
            Quantity = 0;
        }
    }
}