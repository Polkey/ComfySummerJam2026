using Unity.Cinemachine;
using UnityEngine.Rendering;
public class EffectContext 
{    
    public EffectContext(Volume globalVolume, CinemachineCamera camera)
    {
        GlobalVolume = globalVolume;
        Camera = camera;
    }
    public Volume GlobalVolume { get; private set; }
    public CinemachineCamera Camera { get; private set; }
}