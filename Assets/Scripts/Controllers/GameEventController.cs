using System.Collections.Generic;
using UnityEngine;

public class GameEventController : MonoBehaviour
{
    #region Properties and Fields

    private static GameEventController _instance;
    public static GameEventController Instance => _instance;

    private Dictionary<string, bool> _gameEvents = new Dictionary<string, bool>();
    private Dictionary<string, Dictionary<string, bool>> _itemEvents = new Dictionary<string, Dictionary<string, bool>>();

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() => HookMessages();
    private void OnDisable() => UnhookMessages();

    #endregion

    #region Public Methods

    public bool GameEventIsSet(string eventName)
    {
        if (!_gameEvents.ContainsKey(eventName)) return false;
        return _gameEvents[eventName];
    }
    public bool ItemEventIsSet(string itemName, string eventName)
    {
        if (!_itemEvents.ContainsKey(itemName)) return false;
        if (!_itemEvents[itemName].ContainsKey(eventName)) return false;
        return _itemEvents[itemName][eventName];
    }

    #endregion

    #region Private Methods

    private void HookMessages()
    {
        Messaging<GameEvent>.Register(OnGameEvent);
        Messaging<ItemEvent>.Register(OnItemEvent);
    }
    private void UnhookMessages()
    {
        Messaging<GameEvent>.Unregister(OnGameEvent);
        Messaging<ItemEvent>.Unregister(OnItemEvent);
    }

    private void OnGameEvent(string eventName, bool value) => _gameEvents[eventName] = value;
    private void OnItemEvent(string itemName, string eventName, bool value)
    {
        if (!_itemEvents.ContainsKey(itemName)) _itemEvents[itemName] = new Dictionary<string, bool>();
        _itemEvents[itemName][eventName] = value;
    }

    #endregion
}
