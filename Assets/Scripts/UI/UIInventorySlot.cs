using UnityEngine;

public class UIInventorySlot : MonoBehaviour
{
    #region Properties and Fields

    public UIInventoryItem CurrentInventoryItem;
    public bool Filled => CurrentInventoryItem != null;

    #endregion

    #region Public Methods

    public bool TryRemoveItem()
    {
        if (CurrentInventoryItem == null) return false;
        CurrentInventoryItem.CurrentSlot = null;
        CurrentInventoryItem.transform.SetParent(null);
        Destroy(CurrentInventoryItem.gameObject);
        return true;
    }

    #endregion
}
