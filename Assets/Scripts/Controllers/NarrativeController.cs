using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeController : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] float _defaultNarrativeDisplaySeconds = 5f;
    [SerializeField] Text _narrativeText;

    private Coroutine _narrativeRoutine;

    #endregion

    #region Unity Methods

    private void OnEnable() => HookMessages();
    private void OnDisable() => UnhookMessages();

    #endregion

    #region Private Methods

    private void HookMessages()
    {
        Messaging<ShowNarrative>.Register(OnNarrative);
    }
    private void UnhookMessages()
    {
        Messaging<ShowNarrative>.Unregister(OnNarrative);
    }

    private void OnNarrative(string richTextMessage, float displaySeconds)
    {
        if (_narrativeRoutine != null) StopCoroutine(_narrativeRoutine);
        _narrativeText.text = richTextMessage;
        var displayTime = displaySeconds == -1 ? _defaultNarrativeDisplaySeconds : displaySeconds;
        _narrativeRoutine = StartCoroutine(ClearNarrativeAfter(displayTime));
    }

    private IEnumerator ClearNarrativeAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _narrativeText.text = string.Empty;
    }

    #endregion
}
