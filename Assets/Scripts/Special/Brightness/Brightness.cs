using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] Slider _slider;
    [SerializeField] Volume _globalVolume;

    private LiftGammaGain _liftGammaGain;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _globalVolume.sharedProfile.TryGet(out _liftGammaGain);
        Debug.Assert(_liftGammaGain != null);

        var val = PlayerPrefs.GetFloat("Brightness", 0f);
        SetBrightness(val);
        _slider.value = val;
    }

    #endregion

    #region Public Methods

    public void AdjustBrightness(float value)
    {
        SetBrightness(value);
        PlayerPrefs.SetFloat("Brightness", value);
    }

    #endregion

    #region Private Methods

    private void SetBrightness(float value)
    {
        var val = _liftGammaGain.gamma.value;
        val.w = value;
        _liftGammaGain.gamma.value = val;
    }

    #endregion
}
