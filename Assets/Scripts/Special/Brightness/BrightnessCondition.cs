using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessCondition : InteractionCondition
{
    public override bool ConditionMet()
    {
        var val = PlayerPrefs.GetFloat("Brightness", -2f);
        return EvaluateWithNot(() => val < -1f);
    }
}
