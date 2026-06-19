using UnityEngine;

public class CaveTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cave"))
        {
            AudioManager.instance.InitializeReverbSnapshot(FMODEvents.instance.reverbSnapshot);
        }
    }
    
    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Cave"))
        {
            AudioManager.instance.reverbSSEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
