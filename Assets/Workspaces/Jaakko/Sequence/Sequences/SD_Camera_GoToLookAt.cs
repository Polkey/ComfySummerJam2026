using UnityEngine;

public class SD_Camera_GoToLookAt : SequenceDefinition 
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Transform m_lookat;
    [SerializeField] private float m_duration;
    public override Sequence Create(SequenceContext ctx) 
    {
        return new S_Camera_GoToLookAt(ctx, m_target.position, m_lookat.position, m_duration, m_target.localPosition);
    }
}