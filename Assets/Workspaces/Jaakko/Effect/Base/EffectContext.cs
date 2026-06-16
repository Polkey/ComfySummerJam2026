using UnityEngine.Rendering;
public class EffectContext 
{
    public EffectContext(Volume globalVolume)
    {
        GlobalVolume = globalVolume;
    }
    public Volume GlobalVolume { get; private set; }
}