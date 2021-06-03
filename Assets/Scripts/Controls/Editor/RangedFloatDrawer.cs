using UnityEngine;
using UnityEditor;

// This is from Richard Fine
[CustomPropertyDrawer(typeof(RangedFloat), true)]
public class RangedFloatDrawer : PropertyDrawer
{
    #region Override Functions

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        var minProp = property.FindPropertyRelative("MinValue");
        var maxProp = property.FindPropertyRelative("MaxValue");

        var minValue = minProp.floatValue;
        var maxValue = maxProp.floatValue;

        var rangeMin = 0f;
        var rangeMax = 1f;

        var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof(MinMaxRangeAttribute), true);
        if (ranges.Length > 0)
        {
            rangeMin = ranges[0].Min;
            rangeMax = ranges[0].Max;
        }

        const float rangeBoundsLabelWidth = 40f;

        var rangeBoundsLabelMinRect = new Rect(position);
        rangeBoundsLabelMinRect.width = rangeBoundsLabelWidth;
        GUI.Label(rangeBoundsLabelMinRect, new GUIContent(minValue.ToString("F2")));
        position.xMin += rangeBoundsLabelWidth;

        var rangeBoundsLabelMaxRect = new Rect(position);
        rangeBoundsLabelMaxRect.xMin = rangeBoundsLabelMaxRect.xMax - rangeBoundsLabelWidth;
        GUI.Label(rangeBoundsLabelMaxRect, new GUIContent(maxValue.ToString("F2")));
        position.xMax -= rangeBoundsLabelWidth;

        EditorGUI.BeginChangeCheck();
        EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);
        if (EditorGUI.EndChangeCheck())
        {
            minProp.floatValue = minValue;
            maxProp.floatValue = maxValue;
        }

        EditorGUI.EndProperty();
    }

    #endregion
}