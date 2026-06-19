using UnityEngine;

[DefaultExecutionOrder(-100)]
public class FPSCap : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
