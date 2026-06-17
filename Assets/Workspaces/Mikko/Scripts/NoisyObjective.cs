using System.Collections;
using UnityEngine;

public class NoisyObjective : MonoBehaviour, IInteractable {
    private bool interacted = false;
    private bool highlighted = false;
    ScoreManager scoreManager;

    [SerializeField] private Datacenter datacenter;

    public Material mat;
    public int indexOfMat = 1;
    private void Awake() {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        mat = GetComponent<Renderer>().materials[indexOfMat];
    }
    public void Interact() {
        if (!interacted) {
            scoreManager.addScore();
            StartCoroutine(fade());
            interacted = true;
            var player = FindAnyObjectByType<BasicFPCC>();
            player.noisyObjective = true;
        }
    }
    public void Highlight() {
        if (!highlighted) {
            mat.SetFloat("_showOutline", 1);
            highlighted = true;
        }
    }
    public void Unhighlight() {
        if (highlighted) {
            mat.SetFloat("_showOutline", 0);
        }
        highlighted = false;
    }
    IEnumerator fade() {
        EffectController.I.PlayEffect(EffectController.I.Get("C_Shake"));
        AudioManager.instance.InitializeDatacenterDestruct(FMODEvents.instance.datacenterDestructSFX);
        float timeToFadeAway = 2;
        float fadeSpeed = 3;
        float timer = 0;
        while (timer < timeToFadeAway) {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 0, Time.deltaTime * fadeSpeed);
            yield return null;
        }
        datacenter?.Destroy();

        EventEmitter[] emitters = FindObjectsByType<EventEmitter>();
        foreach (var emitter in emitters)
        {
            if (emitter.gameObject.CompareTag("DatacenterEmitter"))
            {
                emitter.gameObject.SetActive(false);
            } 
        }
        
        AudioManager.instance.datacenterDestructEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        Destroy(gameObject);
    }
}
