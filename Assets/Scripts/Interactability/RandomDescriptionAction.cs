using UnityEngine;

public class RandomDescriptionAction : InteractionGroup
{
    public override void Execute() 
        => _interactions[Random.Range(0, _interactions.Length)].Execute();
}
