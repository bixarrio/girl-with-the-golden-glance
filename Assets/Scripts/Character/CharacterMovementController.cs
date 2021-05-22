using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovementController : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] LayerMask _interactionMask;

    private NavMeshAgent _agent;

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
            _agent.isStopped = true;
    }


    #endregion
    #region Private Methods
    
    private void OnClick()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _interactionMask))
        {
            Debug.Log("hit");
            _agent.isStopped = false;
            _agent.SetDestination(hit.point);
        }
    }

    private bool AgentHasArrived()
        => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;

    #endregion
}
