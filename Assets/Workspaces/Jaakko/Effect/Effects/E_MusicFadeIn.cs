using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Music Fade In")]
public class E_MusicFadeIn : EffectDefinition
{
    [SerializeField] private float duration;

    public override IEffectInstance Create(EffectContext ctx)
    {
        return new Instance(duration);
    }
    private class Instance : IEffectInstance
    {
        private AudioManager m_audio;
        private float t;
        private float m_start;
        private float m_duration;
        private float m_target;

        public bool IsFinished { get; private set; }

        public Instance(float duration)
        {
            m_audio = AudioManager.instance;
            if (m_audio == null) 
            {
                Debug.LogError("E_MusicFadeIn: No AudioManager Instance Found");
                return;
            }
            m_start = 0f;
            m_duration = duration;
            m_target = m_audio.musicVolume;
            m_audio.musicVolume = m_start;
        }
        public void Tick(float dt)
        {
            if (IsFinished || m_audio == null) return;

            t += dt;
            float a = Mathf.Clamp01(t / m_duration);

            m_audio.musicVolume = Mathf.Lerp(m_start, m_target, a);

            if (t >= m_duration)
                IsFinished = true;
        }
    }
}
