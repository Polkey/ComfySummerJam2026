using UnityEngine;

public abstract class SequenceDefinition : MonoBehaviour 
{
    public abstract Sequence Create(SequenceContext ctx);
}