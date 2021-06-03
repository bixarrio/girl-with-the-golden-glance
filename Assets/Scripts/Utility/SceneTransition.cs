using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    #region Properties and Fields

    private static SceneTransition _instance;
    public static SceneTransition Instance => _instance;

    [SerializeField] Image _transitionImage;
    [SerializeField] CanvasGroup _transitionGroup;
    [SerializeField] float _transitionSpeed;

    private Coroutine _transitionRoutine;

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

    private void Start() => _transitionImage.enabled = false;

    #endregion

    #region Public Methods

    public void DoTransition(string targetScene, System.Action callback, TransitionType type = TransitionType.Radial)
    {
        if (_transitionRoutine != null) StopCoroutine(_transitionRoutine);

        // This could've been better but I was getting pressed for time
        if (type == TransitionType.Radial)
            _transitionRoutine = StartCoroutine(DoRadialTransitionRoutine(targetScene, callback));
        else
            _transitionRoutine = StartCoroutine(DoFadeTransitionRoutine(targetScene, callback));
    }

    #endregion

    #region Private Methods

    private IEnumerator DoRadialTransitionRoutine(string targetScene, System.Action callback)
    {
        _transitionImage.fillAmount = 0f;
        _transitionImage.enabled = true;

        yield return RadialTransitionFrom(0f, 1f);
        _transitionImage.fillClockwise = !_transitionImage.fillClockwise;

        callback?.Invoke();
        yield return new WaitForSeconds(0.1f); // give the character a moment to 'settle' before changing the scene
        SceneManager.LoadScene(targetScene);

        yield return RadialTransitionFrom(1f, 0f);
        _transitionImage.fillClockwise = !_transitionImage.fillClockwise;

        _transitionImage.enabled = false;
    }
    private IEnumerator RadialTransitionFrom(float start, float end)
    {
        var timer = 0f;
        _transitionImage.fillAmount = start;
        while (timer / _transitionSpeed <= 1f)
        {
            _transitionImage.fillAmount = Mathf.Lerp(start, end, timer / _transitionSpeed);
            timer += Time.deltaTime;
            yield return null;
        }
        _transitionImage.fillAmount = end;
    }


    private IEnumerator DoFadeTransitionRoutine(string targetScene, System.Action callback)
    {
        _transitionImage.fillAmount = 1f;
        _transitionImage.enabled = true;

        yield return FadeTransitionFrom(0f, 1f);

        callback?.Invoke();
        yield return new WaitForSeconds(0.1f); // give the character a moment to 'settle' before changing the scene
        SceneManager.LoadScene(targetScene);

        yield return FadeTransitionFrom(1f, 0f);
        _transitionImage.enabled = false;
    }
    private IEnumerator FadeTransitionFrom(float start, float end)
    {
        var timer = 0f;
        _transitionGroup.alpha = start;
        while (timer / _transitionSpeed <= 1f)
        {
            _transitionGroup.alpha = Mathf.Lerp(start, end, timer / _transitionSpeed);
            timer += Time.deltaTime;
            yield return null;
        }
        _transitionGroup.alpha = end;
    }

    #endregion
}

public enum TransitionType { Radial, Fade }
