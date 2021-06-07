using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class NarrativeControlAsset : PlayableAsset, ITimelineClipAsset
{
    #region Properties and Fields
    
    [SerializeField] string _narrative;

    public ClipCaps clipCaps => ClipCaps.None;

    #endregion

    #region Override Methods

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<NarrativeControlBehaviour>.Create(graph);

        var narrativeControlBehaviour = playable.GetBehaviour();
        narrativeControlBehaviour.Narrative = _narrative;

        return playable;
    }

    #endregion
}
