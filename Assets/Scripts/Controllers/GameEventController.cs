using System.Collections.Generic;
using UnityEngine;

public class GameEventController : MonoBehaviour
{
    #region Properties and Fields

    private static GameEventController _instance;
    public static GameEventController Instance => _instance;

    private Dictionary<string, bool> _gameEvents = new Dictionary<string, bool>();

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

    public bool GameEventOccurred(string eventName)
    {
        // The events are dynamically added so these will always be true
        // but I'm just storing a boolean anyway. Perhaps one day I will make events
        // 'unhappen' again and then it will be useful. Like picking up a quest item
        // will set the event, but dropping it again will unset it. Although I'll manage
        // inventory things differently
        if (!_gameEvents.ContainsKey(eventName)) return false;
        return _gameEvents[eventName];
    }

    #endregion

    #region Private Methods

    private void HookMessages()
    {
        Messaging<GameEvent>.Register(OnGameEvent);
    }
    private void UnhookMessages()
    {
        Messaging<GameEvent>.Unregister(OnGameEvent);
    }

    private void OnGameEvent(string eventName) => _gameEvents[eventName] = true;

    #endregion
}
