using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistAcrossScenes : MonoBehaviour
{
    // Just here to keep things like lights, etc. persistent.
    // Helps with the world building using multiple, additive scenes

    // I could put these in a scene instead and keep it loaded. But I didn't. My bad

    private static PersistAcrossScenes _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
