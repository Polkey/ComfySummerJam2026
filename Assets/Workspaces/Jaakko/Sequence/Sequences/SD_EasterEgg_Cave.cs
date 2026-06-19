using UnityEngine;
public class SD_EasterEgg_Cave : SequenceDefinition 
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Transform m_endPos;
    [SerializeField] private float m_duration;
    [SerializeField] private float m_stopDelay;
    [SerializeField] private UIV_PopUP m_popUP;

    public override Sequence Create(SequenceContext ctx)
    {
        return new S_EasterEgg_Cave(ctx, m_target, m_endPos, m_duration, m_stopDelay, m_popUP, true);
    }
}