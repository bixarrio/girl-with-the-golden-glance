using UnityEngine;

public class ItemEventManager : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] string _uniqueName;

    #endregion

    #region Public Methods

    public void SetItemEvent(string eventName, bool value = true)
        => Messaging<ItemEvent>.Trigger?.Invoke(_uniqueName, eventName, value);
    public bool ItemEventIsSet(string eventName)
        => GameEventController.Instance.ItemEventIsSet(_uniqueName, eventName);

    #endregion

}
