using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DoggieBehaviour : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] float _freakOutStrength = 1f;
    [SerializeField] float _targetOffset = 1f;
    [SerializeField] DoggiePatrolArea _patrolArea;
    [SerializeField] AudioClip _doggieBark;

    private NavMeshAgent _agent;

    private Vector3 _homePosition;
    private bool _isAngry = true;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _homePosition = transform.position;
        _agent = GetComponent<NavMeshAgent>();
        _isAngry = !GameEventController.Instance.GameEventIsSet("Doggie fed");
        if (_isAngry) StartCoroutine(PlayBark());
        else DontBeAngry();
    }

    private void Update()
    {
        if (!_agent.pathPending)
        {
            if (_isAngry)
            {
                var playerPos = CharacterMovementController.Instance.transform.position;
                playerPos = new Vector3(playerPos.x, 0f, playerPos.z);

                var targetPos = _patrolArea.GetAggressionPoint(playerPos);
                var freakOutZone = (transform.forward * _targetOffset) + (Random.onUnitSphere * _freakOutStrength);
                freakOutZone.y = 0f;
                targetPos += freakOutZone;

                _agent.SetDestination(targetPos);
            }
            else
            {
                _agent.SetDestination(_homePosition);
            }
        }
    }

    #endregion

    #region Private Methods

    private IEnumerator PlayBark()
    {
        while (_isAngry)
        {
            Messaging<PlayAudio>.Trigger?.Invoke(_doggieBark, AudioGroups.SFX, transform);
            yield return new WaitForSeconds(4f);
            _isAngry = !GameEventController.Instance.GameEventIsSet("Doggie fed");
        }

        // We're not angry anymore. This is a weird place to do this, but I am tired
        DontBeAngry();
    }

    private void DontBeAngry()
    {
        Messaging<StopTimeline>.Trigger?.Invoke();
        Countdown.Instance.StopCountdown();
    }

    #endregion
}
