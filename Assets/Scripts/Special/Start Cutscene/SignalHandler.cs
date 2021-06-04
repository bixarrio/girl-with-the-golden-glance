using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SignalHandler : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] GameObject[] _uiShot;
    [SerializeField] Transform[] _position;
    [SerializeField] PlayableDirector _director;

    #endregion

    #region Public Methods

    public void ShowUIShot(int idx)
    {
        _uiShot[idx].SetActive(true);
    }

    public void HideUIShot(int idx)
    {
        _uiShot[idx].SetActive(false);
    }

    public void MoveToPosition(int idx)
    {
        CharacterMovementController.Instance.SetDestination(_position[idx].position);
    }

    public void StopTimelineIfPlayed()
    {
        if (GameEventController.Instance.GameEventIsSet("Opening Scene Played"))
        {
            _director.Stop();
        }
        else
        {
            Messaging<GameEvent>.Trigger?.Invoke("Opening Scene Played", true);
        }
    }

    public void SaySomething(string narrative) => Messaging<ShowNarrative>.Trigger?.Invoke(narrative);

    public void RelinquishControl() => CharacterController.Instance.RelinquishControl();
    public void RegainControl() => CharacterController.Instance.RegainControl();

    #endregion

}
