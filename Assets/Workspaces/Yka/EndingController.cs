using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class EndingController : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [Header("Exposed for debug, will be found by script")]
    [SerializeField] private CinemachineBrain brain;
    public void RunEndingTimeline()
    {
        brain = FindAnyObjectByType<CinemachineBrain>();
        if (!brain)
        {
            Debug.LogError("No Cinemachine brain found, can't play ending");
            return;
        }
        var timeline = director.playableAsset as TimelineAsset;

        foreach (var track in timeline.GetOutputTracks())
        {
            if (track is CinemachineTrack)
            {
                director.SetGenericBinding(track, brain);
            }
        }

        director.Play();
    }

    // for debug press F10
    private void Update()
    {
        if (Keyboard.current.f10Key.wasPressedThisFrame)
        {
            RunEndingTimeline();
        }
    }
}
