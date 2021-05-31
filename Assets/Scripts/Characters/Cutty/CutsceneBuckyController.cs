using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CutsceneBuckyController : MonoBehaviour
{
    #region Properties and Fields

    private static CutsceneBuckyController _instance;
    public static CutsceneBuckyController Instance => _instance;

    [SerializeField] Animator _animator;

    private NavMeshAgent _agent;

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
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        Debug.Assert(_agent != null);
    }

    private void Update()
    {
        if (!_agent.isStopped && AgentHasArrived())
        {
            _agent.isStopped = true;
        }
    }

    private void LateUpdate()
    {
        _animator.SetBool("IsWalking", _agent.velocity.magnitude > 0f);
    }

    #endregion

    #region Public Methods

    public void SetTarget(Vector3 target)
    {
        _agent.SetDestination(target);
        _agent.isStopped = false;
    }

    public void LightUp()
    {
        _animator.SetTrigger("Light Up");
    }

    #endregion

    #region Private Methods

    private bool AgentHasArrived()
        => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;

    #endregion
}
