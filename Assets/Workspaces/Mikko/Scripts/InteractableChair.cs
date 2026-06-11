using System.Collections;
using UnityEditor;
using UnityEngine;

public class InteractableChair : MonoBehaviour, IInteractable {
    public GameObject player;
    public GameObject playerCamera;
    public BasicFPCC BasicFPCC;
    public Transform lookTarget;
    public Transform positionTarget;
    public Transform exitPosition;

    private MeshCollider mCollider;
    private bool highlighted = false;
    private bool seated = false;

    public Material mat;
    public int indexOfMat = 1;

    //float startXTest;

    private void Awake() {
        player = FindAnyObjectByType<BasicFPCC>().gameObject;
        BasicFPCC = FindAnyObjectByType<BasicFPCC>();
        mat = GetComponent<Renderer>().materials[indexOfMat];
        mCollider = GetComponent<MeshCollider>();
    }
    public void Interact() {
        if (!seated) {
            BasicFPCC.movementLocked = true;
            mCollider.enabled = false;
            StartCoroutine(movePlayer());
            StartCoroutine(playerLook());
        }
    }


    public void Highlight() {
        if (!highlighted && !seated) {
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

    IEnumerator movePlayer() {

        float timeToMove = 1.5f;
        float moveSpeed = 2;
        float timer = 0;
        var startPos = player.transform.position;
        while (timer < timeToMove) {
            timer += Time.deltaTime;
            float t = timer / 1;
            player.transform.position = Vector3.Lerp(startPos, positionTarget.position, t * moveSpeed);
            yield return null;
        }
    }
    IEnumerator exitPlayer() {

        float timeToMove = 0.7f;
        float moveSpeed = 2;
        float timer = 0;
        var startPos = player.transform.position;
        while (timer < timeToMove) {
            timer += Time.deltaTime;
            float t = timer / 1;
            player.transform.position = Vector3.Lerp(startPos, exitPosition.position, t * moveSpeed);
            yield return null;
        }
        BasicFPCC.movementLocked = false;
        seated = false;
    }
    IEnumerator playerLook() {
        {
            Vector3 direction = lookTarget.position - player.transform.position;

            float startY = player.transform.eulerAngles.y;
            //float startX = playerCamera.transform.eulerAngles.x;
            float targetY = Quaternion.LookRotation(direction).eulerAngles.y;

            float timer = 0f;
            float duration = 1f;

            while (timer < duration) {
                timer += Time.deltaTime;

                float t = Mathf.Clamp01(timer / duration);
                //ADJUST MAGICAL NUMBERS HERE IF NEEDED, Y = L/R, X = U/D
                float y = Mathf.LerpAngle(startY, 90, t);
                //float x = Mathf.LerpAngle(startX, 0, t); 

                player.transform.rotation = Quaternion.Euler(0f, y, 0f);
                yield return null;
            }
        }
    }

    private void OnTriggerExit(Collider collision) {
        if (collision.gameObject.name == "Player") {
            mCollider.enabled = true;
        }
    }
    private void OnTriggerStay(Collider collision) {
        if (collision.gameObject.name == "Player") {
            seated = true;
        }
    }

    private void Start() {
        float startXTest = playerCamera.transform.eulerAngles.x;
    }
    private void Update() {
       
        if (seated == true && Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(exitPlayer());
        }
    }
}
