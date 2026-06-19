using Unity.VisualScripting;
using UnityEngine;
public class SD_GameStart : SequenceDefinition 
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Transform m_endPos;
    [SerializeField] private float m_duration;
    [SerializeField] private float m_stopTime;
    
    public override Sequence Create(SequenceContext ctx)
    {
        m_target.position += new Vector3(0f, 1.7f, 0f);
        return new S_EasterEgg_Cave(ctx, m_target, m_endPos, m_duration, m_stopTime, null, false);
    }
}