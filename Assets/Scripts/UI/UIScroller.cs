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

    private float _scaledEndOffset;
    private float _scaledScrollSpeed;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        Debug.Assert(_rectTransform != null);

        CalculateScaledValues();
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
        pos.y += _scaledScrollSpeed * Time.deltaTime;
        _rectTransform.position = pos;

        // end if we're done
        if (_rectTransform.position.y > _scaledEndOffset && !_done)
        {
            _done = true;
            SceneTransition.Instance.DoTransition(_targetScene, null, _transitionType);
        }
    }

    #endregion

    #region Private Methods

    private void CalculateScaledValues()
    {
        // This is horrible, but it works and this is a jam
        // get a reference size
        var corners = new Vector3[4];

        _rectTransform.GetLocalCorners(corners);
        var referenceHeight = corners[1].y - corners[0].y;

        // get the current height
        _rectTransform.GetWorldCorners(corners);
        var scaledHeight = corners[1].y - corners[0].y;

        Debug.Log($"Reference Height: {referenceHeight}");
        Debug.Log($"Scaled Height: {scaledHeight}");

        var ratio = scaledHeight / referenceHeight;
        _scaledScrollSpeed = _scrollSpeed * ratio;
        _scaledEndOffset = _scrollEndOffset * ratio;
    }

    #endregion
}
