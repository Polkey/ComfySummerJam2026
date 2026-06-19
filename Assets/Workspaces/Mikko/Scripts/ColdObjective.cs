using System.Collections;
using UnityEngine;

public class ColdObjective : MonoBehaviour, IInteractable {
    private bool interacted = false;
    private bool highlighted = false;
    ScoreManager scoreManager;

    public Material mat;
    public int indexOfMat = 1;
    private void Awake() {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        mat = GetComponent<Renderer>().materials[indexOfMat];
    }
    public void Interact() {
        if (!interacted) {
            var bonfire = FindAnyObjectByType<BonfireStages>();
            bool couldLightUp = bonfire.LightUp();
            if (!couldLightUp) {
                // couldn't yet light up the fire
                // TODO: audio/visual feedback
                return;
            }
            AudioManager.instance.InitializeFireSFX(FMODEvents.instance.fireSFX, gameObject);

            scoreManager.addScore();
            StartCoroutine(fade());
            interacted = true;
            var player = FindAnyObjectByType<BasicFPCC>();
            ObjectiveManager.CompleteObjective("t_coldObjective");
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
        float timeToFadeAway = 2;
        float fadeSpeed = 3;
        float timer = 0;
        while (timer < timeToFadeAway) {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 0, Time.deltaTime * fadeSpeed);
            yield return null;
        }
        Destroy(gameObject);
    }
}
