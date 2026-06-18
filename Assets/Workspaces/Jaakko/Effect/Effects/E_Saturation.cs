using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Effects/Volume Saturation")]
public class E_Saturation : EffectDefinition
{
    [SerializeField] private float targetValue;
    [SerializeField] private float duration;

    public override IEffectInstance Create(EffectContext ctx)
    {        
        return new Instance(ctx.GlobalVolume, targetValue, duration);
    }
    private class Instance : IEffectInstance 
    {
        private readonly ColorAdjustments color;
        private readonly float m_target;
        private readonly float m_duration;

        private float t;
        private float start;

        public bool IsFinished { get; private set; }

        public void OnEnter() { }
        public void OnExit() { }

        public Instance(Volume volume, float target, float duration) 
        {
            if (volume != null)
            if (!volume.profile.TryGet(out color)) 
            {
                Debug.LogError("E_Saturation: No color override on volume");
                return;
            }

            m_target = target;
            m_duration = duration;

            start = color.saturation.value;
        }
        public void Tick(float dt) 
        {
            if (color == null) 
            {
                Debug.LogError("E_Saturation: Color == null");
                IsFinished = true;
                return;
            }
            if (IsFinished) return;

            t += dt;
            float a = Mathf.Clamp01(t / m_duration);

            color.saturation.value = Mathf.Lerp(start, m_target, a);

            if (t >= m_duration)
                IsFinished = true;
        }
    }
}
