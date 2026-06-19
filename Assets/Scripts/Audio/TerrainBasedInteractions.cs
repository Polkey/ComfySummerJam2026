using System.Collections;
using UnityEngine;

public class TerrainBasedInteractions : MonoBehaviour
{
    BasicFPCC fpcc;
    [Header("Parameter Change")]
    [SerializeField] private string parameterName;
    [Range(0.01f, 1f)][SerializeField] private float footstepbuffer;
    [Range(0.01f, 2f)][SerializeField] private float bushSoundBuffer;
    private bool bushBuffer;
    private int currentTerrain = 1;

    void Awake()
    {
        fpcc = GetComponent<BasicFPCC>();
    }

    void Start()
    {
        StartCoroutine(Footsteps());
    }

    private IEnumerator Footsteps()
    {
        while (true)
        {
            if (fpcc.isGrounded && fpcc.moving)
            {
                AudioManager.instance.PlayOneShotWithParameters(
                FMODEvents.instance.playerFootsteps,
                transform.position,
                (parameterName, currentTerrain)
            );
            }

            yield return new WaitForSeconds(fpcc.running == false ? footstepbuffer : footstepbuffer / 2);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Grass":
                currentTerrain = 0;
                break;

            case "Rock":
                currentTerrain = 3;
                break;

            case "WetSand":
                currentTerrain = 2;
                break;

            default:
                currentTerrain = 1;
                break;
        }

        if (other.CompareTag("Bush") && fpcc.moving && (!bushBuffer))
        {
            StartCoroutine(BushBuffer());
            AudioManager.instance.PlayOneShotWithPos(FMODEvents.instance.bushRustleSFX, other.gameObject.transform.position);
        }

        if (other.CompareTag("Cave"))
        {
            AudioManager.instance.InitializeReverbSnapshot(FMODEvents.instance.reverbSnapshot);
        }
    }

    void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Grass":
                currentTerrain = 0;
                break;

            case "Rock":
                currentTerrain = 3;
                break;

            case "WetSand":
                currentTerrain = 2;
                break;

            default:
                currentTerrain = 1;
                break;
        }

        if (other.CompareTag("Bush") && fpcc.moving && (!bushBuffer))
        {
            StartCoroutine(BushBuffer());
            AudioManager.instance.PlayOneShotWithPos(FMODEvents.instance.bushRustleSFX, other.gameObject.transform.position);
        }
    }

    private IEnumerator BushBuffer()
    {
        bushBuffer = true;
        yield return new WaitForSeconds(bushSoundBuffer);
        bushBuffer = false;
    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Cave"))
        {
            AudioManager.instance.reverbSSEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        
        currentTerrain = 1;
    }


}
