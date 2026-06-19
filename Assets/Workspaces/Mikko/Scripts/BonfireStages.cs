using UnityEngine;

public class BonfireStages : MonoBehaviour
{
    [SerializeField] public GameObject[] bonfireStages;
    [SerializeField] public int[] stageThreshold;
    [SerializeField] public ScoreManager scoreManager;
    [SerializeField] private int currentStage;
    bool lit = false;

    BonfireVFXController vfxController;

    private void Awake() {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        vfxController = GetComponentInChildren<BonfireVFXController>();
    }
    
    public bool LightUp() { // returns true if lit now or earlier
        if (currentStage <= 0) {
            return false;
        }
        lit = true;
        vfxController.SetBonfireStage(currentStage);
        return true;
    }

    public void checkStage() {
        if (currentStage < stageThreshold[stageThreshold.Length-1]) {
            if (bonfireStages[currentStage+1] != null && scoreManager.score >= stageThreshold[currentStage]) {
                bonfireStages[currentStage].SetActive(false);
                currentStage++;
                bonfireStages[currentStage].SetActive(true);
                if (lit) {
                    vfxController.SetBonfireStage(currentStage);
                }
            }
        }
    }
}
