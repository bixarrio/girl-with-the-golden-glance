using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    #region Public Methods

    public abstract void Execute();

    #endregion
}

public static class ActionExtensions
{
    public static void RunActions(this IEnumerable<Action> actions)
    {
        foreach (var action in actions)
            action.Execute();
    }
}
