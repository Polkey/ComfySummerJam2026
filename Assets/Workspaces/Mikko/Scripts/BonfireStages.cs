using UnityEngine;

public class BonfireStages : MonoBehaviour
{
    [SerializeField] public GameObject[] bonfireStages;
    [SerializeField] public int[] stageThreshold;
    [SerializeField] public ScoreManager scoreManager;
    [SerializeField] private int currentStage;


    private void Awake() {
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }
    
    public void checkStage() {
        if (currentStage < stageThreshold[stageThreshold.Length-1]) {
            if (bonfireStages[currentStage+1] != null && scoreManager.score >= stageThreshold[currentStage]) {
                bonfireStages[currentStage].SetActive(false);
                currentStage++;
                bonfireStages[currentStage].SetActive(true);
            }
        }
    }
}
