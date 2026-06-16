using UnityEngine;

public abstract class EffectDefinition : ScriptableObject 
{
    public abstract IEffectInstance Create(EffectContext ctx);
}