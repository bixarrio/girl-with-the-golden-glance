using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovementController : MonoBehaviour
{
    #region Properties and Fields

    private static CharacterMovementController _instance;
    public static CharacterMovementController Instance => _instance;

    [SerializeField, Tooltip("The speed at which the character must turn towards an interactable when interacting")] float _interactLookRotationSpeed = 15f;
    [SerializeField, Tooltip("How close to the required rotation can we get before we're happy")] float _interactLookRotationTolerance = 0.1f;

    private NavMeshAgent _agent;

    private bool _isTurning = false;
    private Quaternion _targetRotation;

    private Interactable _currentInteractable;
    private InteractionMenuOption _currentInteractionMenuOption;

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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        Debug.Assert(_agent != null);
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            OnClick();

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

    public void SetDestination(Vector3 position)
    {
        _isTurning = false;
        _agent.isStopped = false;
        _agent.SetDestination(position);
    }

    public void SetInteractionTarget(Interactable interactable, InteractionMenuOption menuOption)
    {
        _currentInteractable = interactable;
        _currentInteractionMenuOption = menuOption;
        SetDestination(interactable.InteractTarget(transform));
    }

    public void SetLookDirection(Vector3 direction)
    {
        _isTurning = true;
        _targetRotation = Quaternion.LookRotation(direction - transform.position);
    }

    public void Teleport(Vector3 position, Vector3 lookDirection)
    {
        _agent.Warp(position);
        SetLookDirection(lookDirection);
    }

    #endregion

    #region Private Methods

    private void OnClick()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("Navigation"))
            {
                // Tell the system to close the menu if it is open
                Messaging<CloseMenu>.Trigger?.Invoke();
                SetDestination(hit.point);
            }
        }
    }


    private void StartInteraction()
    {
        _currentInteractable.StartInteraction(this, _currentInteractionMenuOption);
        _currentInteractable = null;
        _currentInteractionMenuOption = null;
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
