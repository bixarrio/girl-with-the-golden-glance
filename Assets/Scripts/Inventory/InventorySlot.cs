using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    #region Properties and Fields

    public UIInventorySlot UIInventorySlot;

    private Item _item;
    public Item Item => _item;

    public UIInventoryItem ItemPrefab { get; set; }

    #endregion

    #region Public Methods

    public void UpdateUI()
    {
        if (UIInventorySlot.Filled && Item == null)
        {
            GameObject.Destroy(UIInventorySlot.CurrentInventoryItem);
        }

        else if (!UIInventorySlot.Filled && Item != null)
        {
            var item = GameObject.Instantiate(ItemPrefab, UIInventorySlot.transform);
            item.CurrentSlot = UIInventorySlot;
            UIInventorySlot.CurrentInventoryItem = item;

            item.transform.localPosition = Vector3.zero;
            item.SetItem(Item);
        }
    }

    public void AddItemToSlot(Item item)
    {
        _item = item;
        UpdateUI();
    }

    #endregion
}
