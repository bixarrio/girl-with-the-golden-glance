using System.Collections.Generic;
using UnityEngine;

public class ItemEventManager : MonoBehaviour
{
    #region Properties and Fields

    private Dictionary<string, bool> _itemEvent = new Dictionary<string, bool>();

    #endregion

    #region Public Methods

    public void SetItemEvent(string eventName, bool value = true) => _itemEvent[eventName] = value;
    public bool ItemEventIsSet(string eventName)
    {
        if (!_itemEvent.ContainsKey(eventName)) return false;
        return _itemEvent[eventName];
    }

    #endregion

}
