using UnityEngine.Timeline;

public class DoggieTimelineSignalReceiver : SignalReceiver
{
    #region Signal Reactions

    public void PlayTextNarrative(string narrative) => Messaging<ShowNarrative>.Trigger?.Invoke(narrative);

    public void StartCountdown(int seconds) => Countdown.Instance.StartCountdownTimer(seconds);

    public void Activate(Activatable activatable) => activatable.Activate(true);

    public void RelinquishControl() => CharacterController.Instance.RelinquishControl();

    public void ChangeScene(string sceneName) => SceneTransition.Instance.DoTransition(sceneName, null);

    #endregion
}
