using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class EventEmitter : MonoBehaviour
{
    private StudioEventEmitter emitter;

    void Start()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.pickupIdle, this.gameObject);
        emitter.Play();
    }
    void OnDestroy()
    {
        emitter.Stop();
    }
}
