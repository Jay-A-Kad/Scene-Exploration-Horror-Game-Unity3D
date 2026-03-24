using System;
using System.Collections.Generic;

public class Inventory<T> where T : IInventoryItem
{
    private List<InventorySlot<T>> slots = new List<InventorySlot<T>>();
    public IReadOnlyList<InventorySlot<T>> Slots => slots;

    //even on inventory change detected
    public Action OnInventoryChanged;

    public void AddItem(T item, int amount = 1)
    {
        if (item == null || amount <= 0)
        {
            return;
        }
        if (item.Stackable)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].Item.Id == item.Id)
                {
                    slots[i].Add(amount);
                    OnInventoryChanged?.Invoke();
                    return;
                }
            }
        }
        slots.Add(new InventorySlot<T>(item, amount));
        OnInventoryChanged?.Invoke();
    }

    public bool RemoveItem(string itemId, int amount = 1)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].Item.Id == itemId)
            {
                if (slots[i].Quantity < amount) return false;

                slots[i].Remove(amount);

                if (slots[i].Quantity == 0)
                {
                    slots.RemoveAt(i);
                }
                OnInventoryChanged?.Invoke();
                return true;


            }
        }
        return false;
    }

    public bool HasItem(string itemId, int amount = 1)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].Item.Id == itemId && slots[i].Quantity >= amount)
            {
                return true;
            }
        }
        return false;

    }

    public InventorySlot<T> GetSlot(string itemId)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].Item.Id == itemId)
            {
                return slots[i];
            }
        }
        return null;
    }


}