using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [Header("Volume")]
    [Range(0,1)]
    public float masterVolume = 1;
    [Range(0,1)]
    public float musicVolume = 1;
    [Range(0,1)]
    public float sfxVolume = 1;
    [Range(0,1)]
    public float ambienceVolume = 1;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;
    private Bus ambienceBus;

    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;
    private EventInstance footstepsEventInstance;
    private EventInstance mainAmbienceEventInstance;
    public EventInstance darkAmbienceEventInstance;
    public EventInstance reverbSSEventInstance;
    public EventInstance sleepSSEventInstance;
    public EventInstance pausedSSEventInstance;
    public EventInstance datacenterDestructEventInstance;
    public EventInstance fireEventInstance;
    private EventInstance musicEventInstance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
    }

    void Start()
    {
        InitializeMainAmbience(FMODEvents.instance.mainAmbience);
        InitializeDarkAmbience(FMODEvents.instance.darkAmbience);
    }

    void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
        ambienceBus.setVolume(ambienceVolume);
    }
    public void InitializeMainAmbience(EventReference ambienceEventReference)
    {
        mainAmbienceEventInstance = CreateInstance(ambienceEventReference);
        mainAmbienceEventInstance.start();
    }

    public void InitializeDarkAmbience(EventReference ambienceEventReference)
    {
        darkAmbienceEventInstance = CreateInstance(ambienceEventReference);
        darkAmbienceEventInstance.start();
    }

    public void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();

    }

    public void InitializeDatacenterDestruct(EventReference destructEventReference)
    {
        datacenterDestructEventInstance = CreateInstance(destructEventReference);
        datacenterDestructEventInstance.start();
    }

    public void InitializeFireSFX(EventReference fireEventReference)
    {
        fireEventInstance = CreateInstance(fireEventReference);
        fireEventInstance.start();
    }

    public void InitializeReverbSnapshot(EventReference reverbEventReference)
    {
        reverbSSEventInstance = CreateInstance(reverbEventReference);
        reverbSSEventInstance.start();
    }

    public void InitializeSleepSnapshot(EventReference sleepEventReference)
    {
        sleepSSEventInstance = CreateInstance(sleepEventReference);
        sleepSSEventInstance.start();
    }

    public void InitializePauseSnapshot(EventReference pausedEventReference)
    {
        pausedSSEventInstance = CreateInstance(pausedEventReference);
        pausedSSEventInstance.start();
    }


    public void SetAmbienceParameter(string parameterName, float parameterValue)
    {
        mainAmbienceEventInstance.setParameterByName(parameterName, parameterValue);
    }

    public void SetMusicParameter(string parameterName, float parameterValue)
    {
        musicEventInstance.setParameterByName(parameterName, parameterValue);
    }

    public void SetFootstepParameter(string parameterName, float parameterValue)
    {
        footstepsEventInstance.setParameterByName(parameterName, parameterValue);
    }

    public void PlayOneShotWithPos(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }


    public void PlayOneShotWithParameters(EventReference eventReference, Vector3 worldPos, params (string name, float value)[] parameters)
    {
        EventInstance instance = RuntimeManager.CreateInstance(eventReference);

        foreach (var param in parameters)
        {
            instance.setParameterByName(param.name, param.value);
        }

        instance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));

        instance.start();
        instance.release();
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    private void Oestroy()
    {
        CleanUp();
    }

}
