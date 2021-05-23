using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovementController : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField, Tooltip("The speed at which the character must turn towards an interactable when interacting")] float _interactLookRotationSpeed = 15f;
    [SerializeField, Tooltip("How close to the required rotation can we get before we're happy")] float _interactLookRotationTolerance = 0.1f;
    [SerializeField] LayerMask _interactionMask;

    private NavMeshAgent _agent;

    private bool _isTurning = false;
    private Quaternion _targetRotation;

    private Interactable _currentInteractable;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        Debug.Assert(_agent != null);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) OnClick();

        if (!_agent.isStopped && AgentHasArrived())
        {
            _agent.isStopped = true;
            if (_currentInteractable != null) StartInteraction();
        }

        if (_isTurning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _interactLookRotationSpeed * Time.deltaTime);
            _isTurning = KeepTurning();
        }
    }


    #endregion

    #region Public Methods

    public void SetLookDirection(Vector3 direction)
    {
        _isTurning = true;
        _targetRotation = Quaternion.LookRotation(direction - transform.position);
    }

    #endregion

    #region Private Methods

    private void OnClick()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _interactionMask))
        {
            var target = hit.point;

            _currentInteractable = hit.transform.GetInteractable();
            if (_currentInteractable != null)
                target = _currentInteractable.InteractTarget(transform);

            MoveTo(target);
        }
    }

    private void MoveTo(Vector3 position)
    {
        _isTurning = false;
        _agent.isStopped = false;
        _agent.SetDestination(position);
    }

    private void StartInteraction()
    {
        _currentInteractable.StartInteraction(this);
        _currentInteractable = null;
    }

    private bool AgentHasArrived()
        => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;

    private bool KeepTurning()
    {
        var myRot = transform.rotation.eulerAngles.y;
        var trgRot = _targetRotation.eulerAngles.y;
        return Mathf.Abs(myRot - trgRot) > _interactLookRotationTolerance;
    }

    #endregion
}
