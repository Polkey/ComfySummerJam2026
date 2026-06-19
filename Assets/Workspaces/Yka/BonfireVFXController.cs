using System;
using UnityEngine;
using UnityEngine.VFX;

public class BonfireVFXController : MonoBehaviour
{
    [SerializeField] private int bonfireStage;
    [SerializeField] private int maxStage = 3;
    [Header("Debug display only")]
    [SerializeField] private float percent;

    private VisualEffect vfx;

    void Awake()
    {
        vfx = GetComponent<VisualEffect>();
    }
    public void SetBonfireStage(int stage)
    {
        float per = (float)stage / maxStage;
        SetBonfirePercent(per);
    }

    void SetBonfirePercent(float percent)
    {
        this.percent = percent;
        vfx.SetFloat("PercentActive", percent);
    }

    private void OnValidate()
    {
        if (!vfx)
            vfx = GetComponent<VisualEffect>();
        SetBonfireStage(bonfireStage);
    }
}
