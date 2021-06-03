using System.Collections;
using UnityEngine;

public class SceneTimer : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] float _duration;
    [SerializeField] string _nextScene;
    [SerializeField] TransitionType _transition;

    #endregion

    #region Unity Methods

    private void Start() => StartCoroutine(WaitAndChange());

    #endregion

    #region Private Methods

    private IEnumerator WaitAndChange()
    {
        yield return new WaitForSeconds(_duration);
        SceneTransition.Instance.DoTransition(_nextScene, null, _transition);
    }

    #endregion
}
