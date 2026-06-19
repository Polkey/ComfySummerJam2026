using System.Collections;
using UnityEngine;

public class AreaBlockerSignPickup : MonoBehaviour, IInteractable {
    private bool interacted = false;
    private bool highlighted = false;
    private DestroyWallOnSignPickup[] walls;
    ScoreManager scoreManager;

    public Material mat;
    public int indexOfMat = 1;
    private void Awake() {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        mat = GetComponent<Renderer>().materials[indexOfMat];
        walls = GetComponentsInChildren<DestroyWallOnSignPickup>();
        
    }
    public void Interact() {
        if (!interacted) {
            foreach (var wall in walls)
            {
                Destroy(wall.gameObject);
            }
            scoreManager.addScore();
            StartCoroutine(fade());
            interacted = true;

            AudioManager.instance.PlayOneShotWithPos(FMODEvents.instance.pickupSFX, transform.position);
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
        float timeToFadeAway = 1;
        float fadeSpeed = 4;
        float timer = 0;
        while (timer < timeToFadeAway) {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 0, Time.deltaTime * fadeSpeed);
            yield return null;
        }
        Destroy(gameObject);
    }
}
