using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    #region Public Methods

    public abstract void Execute();

    #endregion
}

public static class InteractionExtensions
{
    public static void RunInteractions(this IEnumerable<Interaction> interactions)
    {
        foreach (var interaction in interactions)
            interaction.Execute();
    }
}
