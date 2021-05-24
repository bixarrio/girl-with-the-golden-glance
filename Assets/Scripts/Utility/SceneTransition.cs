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

    public void DoTransition(string targetScene, Action callback)
    {
        if (_transitionRoutine != null) StopCoroutine(_transitionRoutine);
        _transitionRoutine = StartCoroutine(DoTransitionRoutine(targetScene, callback));
    }

    #endregion

    #region Private Methods

    private IEnumerator DoTransitionRoutine(string targetScene, Action callback)
    {
        _transitionImage.enabled = true;

        yield return TransitionFrom(0f, 1f);
        _transitionImage.fillClockwise = !_transitionImage.fillClockwise;

        SceneManager.LoadScene(targetScene);
        yield return new WaitForSeconds(0.1f); // give the scene a moment to 'settle' before moving the character
        callback?.Invoke();

        yield return TransitionFrom(1f, 0f);
        _transitionImage.fillClockwise = !_transitionImage.fillClockwise;

        _transitionImage.enabled = false;
    }

    private IEnumerator TransitionFrom(float start, float end)
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

    #endregion
}
