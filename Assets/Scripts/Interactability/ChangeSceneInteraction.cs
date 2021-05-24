using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneInteraction : Interaction
{
    #region Properties and Fields

    [SerializeField] string _targetScene;
    [SerializeField] Vector3 _characterSpawnPosition;
    [SerializeField] Vector3 _characterLookDirection;

    #endregion

    #region Unity Methods

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, _characterSpawnPosition);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_characterSpawnPosition, 0.125f);
        Gizmos.DrawRay(_characterSpawnPosition, (_characterLookDirection - _characterSpawnPosition).normalized);
    }

    #endregion

    #region Public Methods

    public override void Execute()
    {
        SceneTransition.Instance.DoTransition(_targetScene,
            () => CharacterMovementController.Instance.Teleport(_characterSpawnPosition, _characterLookDirection));
    }

    #endregion
}
