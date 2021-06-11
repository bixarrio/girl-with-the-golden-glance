using UnityEngine;

public class UIInventorySlot : MonoBehaviour
{
    #region Properties and Fields

    public UIInventoryItem CurrentInventoryItem;
    public bool Filled => CurrentInventoryItem != null;

    #endregion

    #region Public Methods

    public bool CanRemoveItem(bool checkConditions = false)
    {
        if (CurrentInventoryItem == null) return false;
        if (checkConditions && !CurrentInventoryItem.CanDropItem()) return false;
        return true;
    }

    public bool TryRemoveItem(bool checkConditions = false)
    {
        if (!CanRemoveItem(checkConditions)) return false;
        CurrentInventoryItem.CurrentSlot = null;
        CurrentInventoryItem.transform.SetParent(null);
        Destroy(CurrentInventoryItem.gameObject);
        return true;
    }

    #endregion
}
