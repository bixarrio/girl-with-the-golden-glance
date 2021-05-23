using UnityEngine;

public class RandomDescriptionAction : MultiDescriptionAction
{
    public override void Execute() 
        => _descriptions[Random.Range(0, _descriptions.Length)].Execute();
}
