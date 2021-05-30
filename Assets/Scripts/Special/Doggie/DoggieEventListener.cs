using UnityEngine;
using UnityEngine.Playables;

public class DoggieEventListener : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] PlayableDirector _playableDirector;

    #endregion

    #region Unity Methods

    private void OnEnable() => HookMessages();
    private void OnDisable() => UnhookMessages();

    #endregion

    #region Private Methods

    private void HookMessages()
    {
        Messaging<StopTimeline>.Register(StopTimeline);
    }
    private void UnhookMessages()
    {
        Messaging<StopTimeline>.Unregister(StopTimeline);
    }

    private void StopTimeline() => _playableDirector.Stop();

    #endregion
}
