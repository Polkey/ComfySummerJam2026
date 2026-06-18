using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
public class EffectContext 
{    
    public EffectContext(Volume globalVolume, CinemachineCamera camera, Transform cameraRoot)
    {
        GlobalVolume = globalVolume;
        Camera = camera;
        CameraRoot = cameraRoot;
    }
    public Volume GlobalVolume { get; private set; }
    public CinemachineCamera Camera { get; private set; }
    public Transform CameraRoot { get; private set; }
}