using Unity.Cinemachine;
using UnityEngine;

public class S_Camera_GoToLookAt : Sequence
{
    private Transform m_camera;
    private BasicFPCC m_player;
    private Vector3 m_lookAt;
    private Vector3 m_target;
    private float m_duration;
    private Vector3 m_endLocalPos;

    private float m_elapsed;
    private Vector3 m_cameraLocal;
    private Vector3 m_startPos;
    private Quaternion m_startRot;

    private Transform m_cameraRoot;

    public override bool IsStackable => false;
    public S_Camera_GoToLookAt(SequenceContext ctx, Vector3 target, Vector3 lookAt, float duration
        , Vector3 endLocalPos)
    {
        m_cameraRoot = ctx.CameraRoot;
        m_player = ctx.Player;
        m_camera = ctx.Camera;
        m_lookAt = lookAt;
        m_target = target;
        m_duration = duration;
        m_endLocalPos = endLocalPos;
    }
    public override void _Start()
    {
        base._Start();
        m_elapsed = 0f;
        m_player.SetState(PlayerState.Sequence);
        m_cameraLocal = m_cameraRoot.localPosition;
        m_startPos = m_cameraRoot.position;
        m_startRot = m_cameraRoot.rotation;
    }
    public override void _Stop()
    {
        IsFinished = true;
        m_player.SetState(PlayerState.Default);
    }
    public override void _Tick()
    {
        base._Tick();

        m_elapsed += Time.deltaTime;

        float t = Mathf.Clamp01(m_elapsed / m_duration);

        m_cameraRoot.position = Vector3
            .Lerp(m_startPos, m_target, t);

        Quaternion targetRot = Quaternion.LookRotation(
            m_lookAt - m_camera.transform.position
);
        m_cameraRoot.rotation = Quaternion.Slerp(
            m_startRot,
            targetRot,
            t
        );
        if (t >= 1f)
        {
            _Stop();
        }
    }
}