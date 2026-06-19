using UnityEngine;

public class S_EasterEgg_Cave : Sequence
{
    public override bool IsStackable => false;

    private SequenceContext m_context;

    private Transform m_cameraRoot;
    private Transform m_cameraRootParent;

    private Transform m_target;
    private Transform m_endPos;

    private Vector3 m_originCamPos;
    private Quaternion m_originCamRot;

    private Vector3 m_holdCamPos;
    private Quaternion m_holdCamRot;

    private float m_duration;
    private float m_stopDelay;
    private float m_timer;

    private enum State
    {
        MovingToTarget,
        Waiting,
        Returning
    }

    private State m_state;
    private UIV_PopUP m_popUp;
    private bool m_return;
    public S_EasterEgg_Cave(
        SequenceContext ctx,
        Transform target,
        Transform endPos,
        float duration,
        float stopDelay,
        UIV_PopUP popUp,
        bool returnToOrigin = true
    )
    {
        m_return = returnToOrigin;
        m_context = ctx;

        m_cameraRoot = ctx.CameraRoot;
        m_target = target;
        m_endPos = endPos;

        m_duration = duration;
        m_stopDelay = stopDelay;
        m_popUp = popUp;

        m_state = State.MovingToTarget;
    }

    public override void _Start()
    {
        m_context.Player.SetState(PlayerState.Sequence);

        m_cameraRootParent = m_cameraRoot.parent;
        m_cameraRoot.SetParent(null, true);

        m_originCamPos = m_cameraRoot.position;
        m_originCamRot = m_cameraRoot.rotation;

        m_timer = 0f;
        IsFinished = false;
    }

    public override void _Stop()
    {
        if (m_cameraRootParent != null)
            m_cameraRoot.SetParent(m_cameraRootParent, true);

        m_context.Player.SetState(PlayerState.Default);
        IsFinished = true;
    }

    public override void _Tick()
    {
        if (IsFinished)
            return;

        m_timer += Time.deltaTime;

        switch (m_state)
        {
            case State.MovingToTarget:
                {
                    float t = Mathf.Clamp01(m_timer / m_duration);

                    m_cameraRoot.position = Vector3.Lerp(m_originCamPos, m_target.position, t);
                    m_cameraRoot.rotation = Quaternion.Slerp(m_originCamRot, m_target.rotation, t);

                    if (t >= 1f)
                    {
                        m_holdCamPos = m_cameraRoot.position;
                        m_holdCamRot = m_cameraRoot.rotation;

                        m_timer = 0f;
                        m_state = State.Waiting;

                        if (m_popUp != null) 
                        {
                            m_popUp.Bind(PopupDatabase.T_Default);
                            m_popUp.Show();
                        }
                        
                    }
                    break;
                }

            case State.Waiting:
                {
                    if (m_timer >= m_stopDelay)
                    {
                        m_context.Player.transform.SetPositionAndRotation(
                            m_endPos.position,
                            m_endPos.rotation
                        );

                        m_cameraRoot.SetParent(m_cameraRootParent, true);

                        m_timer = 0f;
                        m_state = State.Returning;
                    }
                    break;
                }

            case State.Returning:
                {
                    if (!m_return) 
                    {
                        _Stop();
                    }
                    float r = Mathf.Clamp01(m_timer / m_duration);

                    Vector3 targetLocalPos = new Vector3(0f, 1.7f, 0f);
                    Quaternion targetLocalRot = Quaternion.identity;

                    Vector3 startPos = m_holdCamPos;
                    Quaternion startRot = m_holdCamRot;

                    Vector3 endPos = m_cameraRootParent.TransformPoint(targetLocalPos);
                    Quaternion endRot = m_cameraRootParent.rotation * targetLocalRot;

                    m_cameraRoot.position = Vector3.Lerp(startPos, endPos, r);
                    m_cameraRoot.rotation = Quaternion.Slerp(startRot, endRot, r);

                    if (r >= 1f)
                        _Stop();

                    break;
                }
        }
    }
}