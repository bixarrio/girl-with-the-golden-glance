using UnityEngine;
using UnityEngine.UI;

public class ShearText : Text
{
    #region Properties and Fields

    [SerializeField] Vector2 _shear;

    #endregion

    #region Override Methods
    
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);

        var width = rectTransform.rect.width;
        var height = rectTransform.rect.height;
        var skew = new Vector2(height * Mathf.Tan(Mathf.Deg2Rad * _shear.x), width * Mathf.Tan(Mathf.Deg2Rad * _shear.y));

        var yMin = rectTransform.rect.yMin;
        var xMin = rectTransform.rect.xMin;

        var v = new UIVertex();
        for (int i = 0; i < toFill.currentVertCount; i++)
        {
            toFill.PopulateUIVertex(ref v, i);
            v.position += new Vector3(Mathf.Lerp(0, skew.x, (v.position.y - yMin) / height), Mathf.Lerp(0, skew.y, (v.position.x - xMin) / width), 0);
            toFill.SetUIVertex(v, i);
        }
    }

    #endregion
}
