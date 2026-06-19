using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class BonfireVFXController : MonoBehaviour {
    [SerializeField] private int bonfireStage;
    [SerializeField] private int maxStage = 3;
    [Header("Debug display only")]
    [SerializeField] private float percent;

    private VisualEffect[] vfx;

    void Awake() {
        vfx = GetComponentsInChildren<VisualEffect>();
        SetBonfireStage(0);
    }
    public void SetBonfireStage(int stage) {
        bonfireStage = stage;
        float per = (float)stage / maxStage;
        SetBonfirePercent(per);
    }

    void SetBonfirePercent(float percent) {
        this.percent = percent;
        foreach (var effect in vfx) {
            effect.SetFloat("PercentActive", percent);
        }
    }

    private void Update() {
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
