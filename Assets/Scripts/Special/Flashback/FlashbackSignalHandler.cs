using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlashbackSignalHandler : MonoBehaviour
{
    #region Properties and Fields

    [Header("Positions")]
    [SerializeField] Transform[] _buckyPositions;
    [SerializeField] Transform[] _portiaPositions;

    [Header("Dialog")]
    [SerializeField] string[] _buckyDialog;
    [SerializeField] string[] _portiaDialog;

    #endregion

    #region Public Methods

    public void KillHUD() => Destroy(UIHud.Instance.gameObject);

    public void RelinquishControl() => CharacterController.Instance.RelinquishControl(true);
    public void RegainControl() => CharacterController.Instance.RegainControl(true);

    public void MoveBucky(int idx) => CharacterMovementController.Instance.SetDestination(_buckyPositions[idx].position);
    public void MovePortia(int idx) => PortiaController.Instance.SetDestination(_portiaPositions[idx].position);

    public void MakeBuckyTalk(int idx) => Messaging<ShowNarrative>.Trigger?.Invoke(_buckyDialog[idx]);
    public void MakePortiaTalk(int idx) => Messaging<ShowNarrative>.Trigger?.Invoke($"<i>{_portiaDialog[idx]}</i>");

    public void LookAtBucky()
    {
        var direction = CharacterMovementController.Instance.transform.position
            - PortiaController.Instance.transform.position;
        PortiaController.Instance.SetLookDirection(direction);
    }
    public void LookAtPortia()
    {
        var direction = PortiaController.Instance.transform.position
            - CharacterMovementController.Instance.transform.position;
        CharacterMovementController.Instance.SetLookDirection(direction);
    }

    public void LoadScene(string sceneName) => SceneTransition.Instance.DoTransition(sceneName, null, TransitionType.Fade);

    #endregion
}
