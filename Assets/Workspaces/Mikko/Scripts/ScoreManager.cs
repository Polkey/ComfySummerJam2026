using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public BonfireStages bonfire;

    private void Awake() {
        bonfire = FindAnyObjectByType<BonfireStages>();
    }

    public void addScore() {
        score++;
        bonfire.checkStage();
    }
}
