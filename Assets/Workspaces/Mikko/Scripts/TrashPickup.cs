using System.Collections;
using UnityEngine;

public class TrashPickup : MonoBehaviour, IInteractable {
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
            scoreManager.addScore();
            StartCoroutine(fade());
            interacted = true;
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
