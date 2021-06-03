using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTimer : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] float _duration;
    [SerializeField] string _fallBackScene;
    [SerializeField] ConditionalScene[] _scenes;
    [SerializeField] TransitionType _transition;

    #endregion

    #region Unity Methods

    private void Start() => StartCoroutine(WaitAndChange());

    #endregion

    #region Private Methods

    private IEnumerator WaitAndChange()
    {
        yield return new WaitForSeconds(_duration);
        var sceneToLoad = _fallBackScene;
        foreach (var scene in _scenes)
        {
            if (scene.Condition.ConditionMet())
            {
                sceneToLoad = scene.Scene;
                break;
            }
        }
        SceneTransition.Instance.DoTransition(sceneToLoad, null, _transition);
    }

    #endregion
}

[System.Serializable]
public struct ConditionalScene
{
    public string Scene;
    public InteractionCondition Condition;
}