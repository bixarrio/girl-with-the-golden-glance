using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioButtonController : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] Transform _pivotTransform;
    [SerializeField] string _description;
    [SerializeField] AudioMixerGroup _mixerGroup;
    [SerializeField] string _groupVolumeParameter;
    [SerializeField] float _rotationLimit = 270f;
    [Header("UI")]
    [SerializeField] Sprite _overCursor;
    [SerializeField] Text _descriptionText;
    [SerializeField] Text _valueText;

    private float _offset;
    private float _audioValue;
    private float _startValue;
    private Vector3 _mousePos = Vector3.zero;
    private bool _isDragging = false;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _offset = (_rotationLimit / 2f);
        _audioValue = GetAudioValue();
        _pivotTransform.localRotation = Quaternion.AngleAxis(GetVolumeAngle(_audioValue), Vector3.left);
    }

    private void OnMouseOver()
    {
        ShowText();
        // Change the cursor
        if (_overCursor == null) return;
        Messaging<SetCursor>.Trigger?.Invoke(_overCursor);
    }
    private void OnMouseExit()
    {
        if (_isDragging) return;
        ClearText();
        Messaging<SetCursor>.Trigger?.Invoke(null);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) CheckBeginDrag();
        if (Input.GetMouseButton(0)) CheckDrag();
        if (Input.GetMouseButtonUp(0)) CheckEndDrag();
    }

    #endregion

    #region Private Methods

    private void CheckBeginDrag()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (!hit.transform.CompareTag("Audio Buttons")) return;
            if (hit.transform != transform) return;
            _isDragging = true;
            _startValue = _audioValue;
            _mousePos = Utilities.GetMousePosition() + Vector3.up * _startValue;
        }
    }
    private void CheckDrag()
    {
        if (!_isDragging) return;
        var newPos = Utilities.GetMousePosition();
        var delta = _mousePos - newPos;
        Drag(delta);
    }
    private void CheckEndDrag()
    {
        if (!_isDragging) return;
        ClearText();
        _isDragging = false;
        Messaging<SetCursor>.Trigger?.Invoke(null);
    }

    public void Drag(Vector3 delta)
    {
        _audioValue = Mathf.Clamp(delta.y, 0.00001f, 1f);
        _pivotTransform.localRotation = Quaternion.AngleAxis(GetVolumeAngle(_audioValue), Vector3.left);
        SetAudioValue(_audioValue);
        ShowText();
    }

    private void ShowText()
    {
        _descriptionText.text = _description;
        _valueText.text = Mathf.RoundToInt(GetAudioValue() * 10f).ToString();
    }
    private void ClearText()
    {
        _descriptionText.text = "";
        _valueText.text = "";
    }

    private float GetVolumeAngle(float volume) => (_offset - Mathf.Lerp(0, _rotationLimit, volume)) * -1f;
    private float GetAudioValue()
    {
        _mixerGroup.audioMixer.GetFloat(_groupVolumeParameter, out float volume);
        volume = Mathf.Clamp(Mathf.Pow(10f, volume / 20f), 0.00001f, 1f);
        return Mathf.Clamp(PlayerPrefs.GetFloat(_groupVolumeParameter, volume), 0.00001f, 1f);
    }
    private void SetAudioValue(float value)
    {
        value = Mathf.Clamp(value, 0.00001f, 1f);
        _mixerGroup.audioMixer.SetFloat(_groupVolumeParameter, Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat(_groupVolumeParameter, value);
    }

    #endregion
}
