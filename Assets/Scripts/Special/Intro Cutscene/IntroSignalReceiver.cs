using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class IntroSignalReceiver : SignalReceiver
{
    #region Properties and Fields

    [SerializeField] Transform[] _buckyPositions;
    [SerializeField] Transform[] _thugPositions;
    [SerializeField] AudioClip _music;
    [SerializeField] Volume _postProcessingVolume;

    #endregion

    #region Unity Methods

    private void Start()
    {
        if (CharacterMovementController.Instance != null)
            Destroy(CharacterMovementController.Instance.gameObject);
        if (UIInventoryGroup.Instance != null)
            Destroy(UIInventoryGroup.Instance.gameObject);
    }

    #endregion

    #region Public Methods

    public void BuckyGoToPosition(int idx)
        => CutsceneBuckyController.Instance.SetTarget(_buckyPositions[idx].position);

    public void ThugGoToPosition(int idx)
        => ThugMovement.Instance.SetTarget(_thugPositions[idx].position);

    public void ChangeScene(string sceneName)
        => SceneTransition.Instance.DoTransition(sceneName, null, TransitionType.Fade);

    public void StartMusic()
        => Messaging<PlayAudio>.Trigger?.Invoke(_music, AudioGroups.Music);

    public void DoBrainFart(float restoreTime)
        => StartCoroutine(BrainFartRoutine(restoreTime));

    public void LightUp()
        => CutsceneBuckyController.Instance.LightUp();

    #endregion

    #region Private Methods

    private IEnumerator BrainFartRoutine(float restoreTime)
    {
        _postProcessingVolume.profile.TryGet(out Vignette vignette);
        vignette.intensity.value = 0f;

        _postProcessingVolume.profile.TryGet(out ChromaticAberration aberration);
        aberration.intensity.value = 1f;

        var timer = 0f;
        while (timer / restoreTime <= 1f)
        {
            vignette.intensity.value = Mathf.Lerp(0f, 1f, timer / restoreTime);
            aberration.intensity.value = Mathf.Lerp(1f, 0f, timer / restoreTime);
            
            timer += Time.deltaTime;
            yield return null;
        }

        vignette.intensity.value = 1f;
        aberration.intensity.value = 0f;
    }

    #endregion
}
