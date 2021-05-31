using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThugAnimationEventHandler : MonoBehaviour
{
    #region Public Methods
    
    public void KOBucky()
    {
        CharacterObjects.Instance.Ragdoll.SetActive(true);
        CharacterObjects.Instance.Bucky.SetActive(false);
    }

    #endregion
}
