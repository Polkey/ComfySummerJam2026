using System.Collections;
using UnityEngine;

public class BadVibesOnlySignPickup : MonoBehaviour, IInteractable {
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

            EffectController ec = EffectController.I;
            if (ec == null) {
                Debug.LogWarning($"UIC_MainMenu: No EffectController found in the scene.");
                return;
            }
            ec.PlayEffect(ec.Get("V_CA_Saturation"));
            ec.PlayEffect(ec.Get("A_MV_FadeIn"));

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
