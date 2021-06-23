using UnityEngine;

public class WebGLButtonKiller : MonoBehaviour
{
    #region Unity Methods

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            Destroy(gameObject);
    }

    #endregion
}
