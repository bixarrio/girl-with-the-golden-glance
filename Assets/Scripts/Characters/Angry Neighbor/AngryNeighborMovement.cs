using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AngryNeighborMovement : MonoBehaviour
{
    #region Properties and Fields

    private static AngryNeighborMovement _instance;
    public static AngryNeighborMovement Instance => _instance;

    [SerializeField] Animator _animator;
    [SerializeField] Transform _targetLocation;

    private bool _isWalking = false;
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
        if (!_isWalking) return;
        if (!_agent.isStopped && AgentHasArrived())
        {
            _isWalking = false;
            _agent.isStopped = true;
            _animator.SetTrigger("IsShooting");
            _animator.SetBool("IsWalking", false);
            RotateTowardsPlayer();
        }
    }

    #endregion

    #region Public Methods

    public void StartWalking()
    {
        _agent.SetDestination(_targetLocation.position);
        _agent.isStopped = false;
        _isWalking = true;
        _animator.SetBool("IsWalking", true);
    }

    #endregion

    #region Private Methods

    private bool AgentHasArrived()
        => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;

    private void RotateTowardsPlayer()
    {
        var playerPos = CharacterMovementController.Instance.transform.position;
        var direction = (playerPos - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    #endregion
}
