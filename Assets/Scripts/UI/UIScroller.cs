using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScroller : MonoBehaviour
{
    #region Properties and Fields

    [Header("Scrolling")]
    [SerializeField] float _scrollSpeed;
    [SerializeField] float _startDelay;
    [SerializeField] float _scrollEndOffset;
    [Header("Transition")]
    [SerializeField] string _targetScene;
    [SerializeField] TransitionType _transitionType;

    private RectTransform _rectTransform;
    private bool _done = false;
    private float _timer = 0f;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        Debug.Assert(_rectTransform != null);
    }

    private void Update()
    {
        // wait through delay before we start scrolling
        if (_timer <= _startDelay)
        {
            _timer += Time.deltaTime;
            return;
        }

        // scroll
        var pos = _rectTransform.position;
        pos.y += _scrollSpeed * Time.deltaTime;
        _rectTransform.position = pos;

        // end if we're done
        if (_rectTransform.position.y > _scrollEndOffset && !_done)
        {
            _done = true;
            SceneTransition.Instance.DoTransition(_targetScene, null, _transitionType);
        }
    }

    #endregion
}
