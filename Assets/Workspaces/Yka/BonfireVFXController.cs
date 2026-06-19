using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class BonfireVFXController : MonoBehaviour {
    [SerializeField] private int bonfireStage;
    [SerializeField] private int maxStage = 3;
    [SerializeField] private AnimationCurve lightCurve;
    [SerializeField] private float maxLightIntensity;
    [SerializeField] private float wobbleSpeedFactor = 4;
    [Range(0, 1)][SerializeField] private float lightWobblePercent = 0.2f;
    [Header("Debug display only")]
    [SerializeField] private float percent;

    private VisualEffect[] vfx;
    private Light light;
    private float currentIntensityLevel;

    void Awake() {
        vfx = GetComponentsInChildren<VisualEffect>();
        light = GetComponentInChildren<Light>();
        SetBonfireStage(0);
    }
    public void SetBonfireStage(int stage) {
        bonfireStage = stage;
        float per = Mathf.Clamp01((float)stage / maxStage);
        SetBonfirePercent(per);
    }

    void SetBonfirePercent(float percent) {
        this.percent = percent;
        foreach (var effect in vfx) {
            effect.SetFloat("PercentActive", percent);
        }
        currentIntensityLevel = lightCurve.Evaluate(percent) * maxLightIntensity;
    }

    private void Update() {
        var wobble = (Mathf.PerlinNoise1D(Time.time * wobbleSpeedFactor) * 2 - 1) * lightWobblePercent;
        light.intensity = Mathf.Max(0, currentIntensityLevel * (1 + wobble));
        
        // DEBUG
        if (Keyboard.current.digit1Key.wasPressedThisFrame) {
            SetBonfireStage(0);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame) {
            SetBonfireStage(1);
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame) {
            SetBonfireStage(2);
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame) {
            SetBonfireStage(3);
        }
    }

    //private void OnValidate() {
    //    if (vfx == null || vfx.Length == 0)
    //        vfx = GetComponentsInChildren<VisualEffect>();
    //    SetBonfireStage(bonfireStage);
    //}
}
