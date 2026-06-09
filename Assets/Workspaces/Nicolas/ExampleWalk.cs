using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class ExampleWalk : MonoBehaviour
{

    private EventInstance playerFootsteps;

    void Start()
    {
        playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.playerFootsteps);
        playerFootsteps.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
    }
    void Update()
    {
        UpdateSound();
    }

    private void UpdateSound()
    {
        if (Input.GetMouseButton(1))
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }

        else
        {
            playerFootsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
