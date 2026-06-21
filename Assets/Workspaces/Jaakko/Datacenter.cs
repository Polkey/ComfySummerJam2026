using System.Collections;
using UnityEngine;

public class Datacenter : MonoBehaviour
{
    public float time = 1f;
    public float delay = 0f;
    [SerializeField] private Transform m_shakeRoot;

    private bool shaking;

    public void _Shake() 
    {
        shaking = true;
        StartCoroutine(Shake());

    }
    public void Destroy()
    {        

        StartCoroutine(DestroyCo());
    }

    IEnumerator DestroyCo()
    {  
        yield return new WaitForSeconds(delay);

        EventEmitter[] emitters = FindObjectsByType<EventEmitter>();
        foreach (var emitter in emitters)
        {
            if (emitter.gameObject.CompareTag("DatacenterEmitter"))
            {
                emitter.gameObject.SetActive(false);
            }
        }

        AudioManager.instance.datacenterDestructEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.datacenterPopSFX);

        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time;

            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

            yield return null;
        }

        shaking = false;
        m_shakeRoot.localPosition = Vector3.zero;

        AudioManager.instance.SetMusicParameter("MuteMusic", 0);
        Destroy(gameObject);
    }

    IEnumerator Shake()
    {
        while (shaking)
        {
            float strength = 1f; // shake amount

            m_shakeRoot.localPosition = Random.insideUnitSphere * strength;

            yield return null;
        }
    }
}