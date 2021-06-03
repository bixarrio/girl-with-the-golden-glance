using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    #region Properties and Fields

    private static Countdown _instance;
    public static Countdown Instance => _instance;

    [SerializeField] Text _timerText;

    private float _timer = 0f;
    private bool _isRunning = false;
    private int _startSeconds = 0;

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

    private void Start() => StopCountdown();

    private void Update()
    {
        if (!_isRunning) return;

        _timer += Time.deltaTime;
        var remainingSeconds = Mathf.Max(0, Mathf.CeilToInt(_startSeconds - _timer));
        _timerText.text = remainingSeconds.ToString();
        _isRunning = remainingSeconds > 0;
    }

    #endregion

    #region Public Methods

    public void StartCountdownTimer(int seconds)
    {
        _startSeconds = seconds;
        _isRunning = true;
    }

    public void StopCountdown()
    {
        _startSeconds = 0;
        _isRunning = false;
        _timerText.text = string.Empty;
    }

    #endregion
}
