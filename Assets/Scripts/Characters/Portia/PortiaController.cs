using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PortiaController : MonoBehaviour
{
    #region Properties and Fields

    private static PortiaController _instance;
    public static PortiaController Instance => _instance;

    [SerializeField] Animator _animator;
    [SerializeField] float _lookRotationSpeed = 15f;
    [SerializeField] float _lookRotationTolerance = 0.1f;

    private bool _isTurning;
    private NavMeshAgent _agent;
    private Quaternion _targetRotation;

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
        if (_isTurning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _lookRotationSpeed * Time.deltaTime);
            _isTurning = KeepTurning();
        }
    }

    private void LateUpdate()
    {
        _animator.SetBool("IsWalking", _agent.velocity.magnitude > 0f);
    }

    #endregion

    #region Public Methods

    public void SetDestination(Vector3 target)
    {
        _agent.SetDestination(target);
        _agent.isStopped = false;
    }

    public void SetLookDirection(Vector3 direction)
    {
        _isTurning = true;
        _targetRotation = Quaternion.LookRotation(direction);
    }

    #endregion

    #region Private Methods

    private bool AgentHasArrived()
        => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;

    private bool KeepTurning()
    {
        var myRot = transform.rotation.eulerAngles.y;
        var trgRot = _targetRotation.eulerAngles.y;
        return Mathf.Abs(myRot - trgRot) > _lookRotationTolerance;
    }

    #endregion
}
