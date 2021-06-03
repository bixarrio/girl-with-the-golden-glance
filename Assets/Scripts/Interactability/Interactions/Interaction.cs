using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : Action
{
    // I should've named these Action from the start, but
    // I didn't and I don't have time to change them now
}

public static class InteractionExtensions
{
    public static void RunInteractions(this IEnumerable<Interaction> interactions)
    {
        foreach (var interaction in interactions)
            interaction.Execute();
    }
}
