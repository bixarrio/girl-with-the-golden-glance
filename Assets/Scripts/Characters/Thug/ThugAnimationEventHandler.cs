using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThugAnimationEventHandler : MonoBehaviour
{
    #region Public Methods
    
    public void KOBucky()
    {
        CuttyObjects.Instance.Ragdoll.SetActive(true);
        CuttyObjects.Instance.Bucky.SetActive(false);
    }

    #endregion
}
