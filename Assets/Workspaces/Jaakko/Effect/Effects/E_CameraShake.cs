using Unity.Cinemachine;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/CameraShake")]
public class E_CameraShake : EffectDefinition
{
    [SerializeField] private float shakeRadius;
    [SerializeField] private float duration;

    public override IEffectInstance Create(EffectContext ctx)
    {
        return new Instance(ctx.Camera, duration, shakeRadius);
    }
    private class Instance : IEffectInstance
    {
        public bool IsFinished { get; private set; }

        private CinemachineCamera m_camera;
        private Vector3 m_originalPosition;
        private float m_duration;
        private float m_radius;
        float m_elapsed;

        public void OnEnter() 
        {
            m_originalPosition = m_camera.transform.position;
        }
        public void OnExit() 
        {
            m_camera.transform.localPosition = Vector3.zero;
        }

        public Instance(CinemachineCamera camera, float duration, float radius)
        {
            m_camera = camera;
            m_duration = duration;
            m_radius = radius;
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

            m_camera.transform.position =
                m_camera.transform.position + Random.insideUnitSphere * currentRadius;
        }
    }
}
