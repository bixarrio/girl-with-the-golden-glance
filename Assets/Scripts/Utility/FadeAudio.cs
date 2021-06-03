using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class FadeAudio : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] AudioMixerGroup _group;
    [SerializeField] string _exposedVolumeName;
    [SerializeField] float _delay;
    [SerializeField] float _fadeDuration;

    #endregion

    #region Unity Methods

    private void Start() => StartCoroutine(WaitAndFade());

    #endregion

    #region Private Methods

    private IEnumerator WaitAndFade()
    {
        yield return new WaitForSeconds(_delay);

        var timer = 0f;

        var mixer = _group.audioMixer;

        mixer.GetFloat(_exposedVolumeName, out float currentVolumeDb);
        var start = currentVolumeDb;
        var target = -80f;
        while (timer / _fadeDuration <= 1f)
        {
            var newV = Mathf.Lerp(start, target, timer / _fadeDuration);
            mixer.SetFloat(_exposedVolumeName, newV);
            timer += Time.deltaTime;
            yield return null;
        }
        mixer.SetFloat(_exposedVolumeName, target);
    }

    #endregion
}
