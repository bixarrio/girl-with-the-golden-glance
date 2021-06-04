using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalHandler : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] GameObject[] _uiShot;
    [SerializeField] Transform[] _position;

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

    public void SaySomething(string narrative) => Messaging<ShowNarrative>.Trigger?.Invoke(narrative);

    public void RelinquishControl() => CharacterController.Instance.RelinquishControl();
    public void RegainControl() => CharacterController.Instance.RegainControl();

    #endregion

}
