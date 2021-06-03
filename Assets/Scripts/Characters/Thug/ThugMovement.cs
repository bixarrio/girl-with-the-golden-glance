using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ThugMovement : MonoBehaviour
{
    #region Properties and Fields

    private static ThugMovement _instance;
    public static ThugMovement Instance => _instance;

    [SerializeField] Animator _animator;

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
            _animator.SetTrigger("Attack");
            _animator.SetBool("IsWalking", false);
        }
        RotateTowardsPlayer();
    }

    #endregion

    #region Public Methods

    public void SetTarget(Vector3 target)
    {
        _agent.SetDestination(target);
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
        var playerPos = CutsceneBuckyController.Instance.transform.position;
        var direction = (playerPos - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    #endregion
}
