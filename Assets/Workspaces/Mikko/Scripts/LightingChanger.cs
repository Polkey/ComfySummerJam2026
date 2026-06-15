using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingChanger : MonoBehaviour
{
    [SerializeField] Material skyboxMaterial;
    [SerializeField] GameObject prefabToDisable;
    [SerializeField] GameObject prefabToEnable;


    private void Awake() {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            int y = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("BakeScene_Dawn", LoadSceneMode.Single);
        }
    }
}
