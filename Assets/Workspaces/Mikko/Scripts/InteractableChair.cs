using System.Collections;
using Unity.Multiplayer.Center.Common.Analytics;
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
    public bool interacted = false;

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
        if (!seated && !interacted) {
            interacted = true;
            mCollider.enabled = false;
            StartCoroutine(movePlayer());
            StartCoroutine(playerLook());

            AudioManager.instance.PlayOneShot(FMODEvents.instance.sitDownSFX, transform.position);
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

        BasicFPCC.SetState(PlayerState.Seated);
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
        AudioManager.instance.SetMusicParameter("MuteMusic", 1);
        AudioManager.instance.InitializeSleepSnapshot(FMODEvents.instance.sleepFilterSnapshot);
        seated = true;
        BasicFPCC.movementLocked = true;        
    }
    IEnumerator exitPlayer() {
        //yield return new WaitForSeconds(0.7f);
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
        BasicFPCC.SetState(PlayerState.Default);
        AudioManager.instance.SetMusicParameter("MuteMusic", 0);
        AudioManager.instance.sleepSSEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        
        seated = false;
    }
    IEnumerator playerLook() {
        {
            Vector3 direction = lookTarget.position - player.transform.position;

            float startY = player.transform.eulerAngles.y;
            //float startX = playerCamera.transform.eulerAngles.x;
            float targetY = Quaternion.LookRotation(direction).eulerAngles.y;

            float timer = 0f;
            float duration = 0.7f;

            while (timer < duration) {
                timer += Time.deltaTime;

                float t = Mathf.Clamp01(timer / duration);
                //ADJUST MAGICAL NUMBERS HERE IF NEEDED, Y = L/R, X = U/D
                float y = Mathf.LerpAngle(startY, 180, t);
                //float x = Mathf.LerpAngle(startX, 0, t); 

                player.transform.rotation = Quaternion.Euler(0f, y, 0f);
                yield return null;
            }
        }
    }

    private void OnTriggerExit(Collider collision) {
        if (collision.gameObject.name == "Player") {
            mCollider.enabled = true;
            seated = false;
            interacted = false;
        }
    }

    private void Start() {
        float startXTest = playerCamera.transform.eulerAngles.x;
    }
    private void Update() {
       
        if (seated == true && interacted == true && Input.GetKeyDown(KeyCode.Mouse1)) {
            
            BasicFPCC.fadeOut();
            StartCoroutine(exitPlayer());
            interacted = false;
        }
        if (seated == true && interacted == true && Input.GetKeyDown(KeyCode.Mouse0)) {
            if (ObjectiveManager.HasObjective("t_coldObjective") == true && ObjectiveManager.HasObjective("t_noisyObjective")) {
                
                BasicFPCC.fadeOut();
                BasicFPCC.playEnding();
                AudioManager.instance.sleepSSEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                interacted = false;
            }
        }

    }
}
