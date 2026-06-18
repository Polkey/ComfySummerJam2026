using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/CameraShake")]
public class E_CameraShake : EffectDefinition
{
    [SerializeField] private float shakeRadius;
    [SerializeField] private float duration;

    public override IEffectInstance Create(EffectContext ctx)
    {
        return new Instance(ctx.CameraRoot, duration, shakeRadius);
    }
    private class Instance : IEffectInstance
    {
        public bool IsFinished { get; private set; }

        private Vector3 m_originalPosition;
        private float m_duration;
        private float m_radius;
        float m_elapsed;
        private Transform m_transform;

        public void OnEnter() 
        {
            m_originalPosition = m_transform.localPosition;
        }
        public void OnExit() 
        {
            m_transform.localPosition = m_originalPosition;
        }

        public Instance(Transform transform, float duration, float radius)
        {
            m_duration = duration;
            m_radius = radius;
            m_transform = transform;
        }
        public void Tick(float dt)
        {
            m_elapsed += dt;

            if (m_elapsed >= m_duration)
            {
                IsFinished = true;
                return;
            }
            

            float decay = 1f - (m_elapsed / m_duration);
            float currentRadius = m_radius * decay;

            m_transform.localPosition =
                m_originalPosition + Random.insideUnitSphere * currentRadius;
        }
    }
}
