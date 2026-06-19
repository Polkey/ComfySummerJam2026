using UnityEngine;

public class S_Camera_GoToLookAt : Sequence
{
    public override bool IsStackable => false;

    private Transform m_cameraRoot;
    private BasicFPCC m_player;

    private Vector3 m_lookAt;
    private Vector3 m_target;
    private Vector3 m_endLocalPos;

    private float m_duration;
    private float m_elapsed;

    private Vector3 m_startPos;
    private Quaternion m_startRot;

    public S_Camera_GoToLookAt(
        SequenceContext ctx,
        Vector3 target,
        Vector3 lookAt,
        float duration,
        Vector3 endLocalPos
    )
    {
        m_cameraRoot = ctx.CameraRoot;
        m_player = ctx.Player;

        m_target = target;
        m_lookAt = lookAt;
        m_duration = duration;
        m_endLocalPos = endLocalPos;
    }

    public override void _Start()
    {
        m_elapsed = 0f;

        m_player.SetState(PlayerState.Sequence);

        m_startPos = m_cameraRoot.position;
        m_startRot = m_cameraRoot.rotation;
    }

    public override void _Tick()
    {
        m_elapsed += Time.deltaTime;

        float t = Mathf.Clamp01(m_elapsed / m_duration);

        Vector3 pos = Vector3.Lerp(m_startPos, m_target, t);

        Quaternion rot = Quaternion.LookRotation(m_lookAt - pos, Vector3.up);

        m_cameraRoot.position = pos;
        m_cameraRoot.rotation = Quaternion.Slerp(m_startRot, rot, t);

        if (t >= 1f)
            _Stop();
    }

    public override void _Stop()
    {
        IsFinished = true;

        m_cameraRoot.position = m_target;
        m_cameraRoot.rotation = Quaternion.LookRotation(m_lookAt - m_target, Vector3.up);

        m_player.SetState(PlayerState.Default);
    }
}