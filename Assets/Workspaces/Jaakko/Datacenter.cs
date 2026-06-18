using System.Collections;
using UnityEngine;

public class Datacenter : MonoBehaviour
{
    public float time = 1f;
    public float delay = 0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Destroy();
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
        AudioManager.instance.PlayOneShot(FMODEvents.instance.datacenterPopSFX, transform.position);

        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < time) 
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time;

            // transform.position += new Vector3(0f, 1.0f, 0f) * Time.deltaTime * 5f;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

            yield return null;
        }
        AudioManager.instance.SetMusicParameter("MuteMusic", 0);
        Destroy(gameObject);
    }
}
