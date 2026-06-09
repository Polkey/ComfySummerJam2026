using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class ExamplePickupScript : MonoBehaviour
{
    private StudioEventEmitter emitter;

    void Start()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.pickupIdle, this.gameObject);
        emitter.Play();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // pickup
        {
            emitter.Stop();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.pickupSFX, transform.position);
        }
    }
}
