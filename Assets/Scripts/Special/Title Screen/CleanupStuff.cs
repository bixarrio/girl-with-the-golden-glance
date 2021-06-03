using UnityEngine;

public class CleanupStuff : MonoBehaviour
{
    #region Properties and Fields

    [SerializeField] GameObject _buckyPrefab;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        // Remove Cutscene Jones
        var cutty = GameObject.Find("Cutscene Jones");
        Destroy(cutty);
    }

    private void OnDisable()
    {
        // Create Bucky
        Instantiate(_buckyPrefab, Vector3.zero, Quaternion.identity, transform);
    }

    #endregion
}
