using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

// I just learned this, which is why the other cutscenes/timeline don't have it in
// I may clean them up at some point
public class NarrativeControlBehaviour : PlayableBehaviour
{
    #region Properties and Fields

    public string Narrative;

    private Text _narrativeText;

    #endregion

    #region Override Methods

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (_narrativeText == null) _narrativeText = playerData as Text;
        if (_narrativeText == null) return;

        _narrativeText.text = Narrative;
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (_narrativeText == null) return;

        _narrativeText.text = string.Empty;
    }

    #endregion
}
