using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Inventory
{
    #region Properties and Fields

    public int MaxSlots = 5;
    public bool CanInventoryShuffle = true;

    private List<InventorySlot> _slots = new List<InventorySlot>();
    public List<UIInventorySlot> UISlots = new List<UIInventorySlot>();

    #endregion

    #region Public Methods

    public void Init()
    {
        for (int i = 0; i < MaxSlots; i++)
            _slots.Add(new InventorySlot());
        AssignUISlots();
    }

    public void SetItemPrefab(UIInventoryItem itemPrefab)
    {
        foreach (var slot in _slots) slot.ItemPrefab = itemPrefab;
    }

    public bool HasFreeSlot() => _slots.Any(s => s.UIInventorySlot.CurrentInventoryItem == null);

    public bool ContainsItem(Item item)
        => _slots.Any(s => s.UIInventorySlot.CurrentInventoryItem?.HasItem(item) ?? false);

    public void AddItemToInventory(Item item)
    {
        _slots.First(s => s.UIInventorySlot.CurrentInventoryItem == null).AddItemToSlot(item);
    }


    public bool TryRemoveItem(Item item, bool checkConditions = false)
    {
        var slot = _slots.FirstOrDefault(s => s.UIInventorySlot.CurrentInventoryItem?.HasItem(item) ?? false);
        if (slot == null) return false;
        slot.RemoveItemFromSlot();
        return slot.UIInventorySlot.TryRemoveItem(checkConditions);
    }

    public void Clear()
    {
        _slots.ForEach(s =>
        {
            s.UIInventorySlot.TryRemoveItem();
            s.UpdateUI();
        });
    }

    public IEnumerable<InventorySlot> IterateSlots()
    {
        foreach (var slot in _slots)
            yield return slot;
    }

    public void ShuffleInventory() => ShuffleItems();

    #endregion

    #region Private Methods

    // Fischer-Yates shuffling algorithm
    private void ShuffleItems()
    {
        if (!CanInventoryShuffle) return;

        for (var i = 0; i < _slots.Count - 2; i++)
        {
            var j = UnityEngine.Random.Range(i, _slots.Count);
            ExchangeSlots(_slots[i], _slots[j]);
        }
    }

    private void ExchangeSlots(InventorySlot slotA, InventorySlot slotB)
    {
        (slotA.UIInventorySlot.CurrentInventoryItem.CurrentSlot, slotB.UIInventorySlot.CurrentInventoryItem.CurrentSlot)
            = (slotB.UIInventorySlot.CurrentInventoryItem.CurrentSlot, slotA.UIInventorySlot.CurrentInventoryItem.CurrentSlot);
        (slotA.UIInventorySlot.CurrentInventoryItem, slotB.UIInventorySlot.CurrentInventoryItem)
            = (slotB.UIInventorySlot.CurrentInventoryItem, slotA.UIInventorySlot.CurrentInventoryItem);
    }

    private void AssignUISlots()
    {
        for (int i = 0; i < MaxSlots; i++)
        {
            _slots[i].UIInventorySlot = UISlots[i];
            _slots[i].UpdateUI();
        }
    }

    #endregion
}
