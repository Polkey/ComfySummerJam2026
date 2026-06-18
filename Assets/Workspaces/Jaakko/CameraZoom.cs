using Unity.Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 2f;
    public float targetFov = 30f;
    [SerializeField] private CinemachineCamera m_camera;
    private PlayerState m_playerState;

    public static float s_defaultFov;
    public static float s_maxFov;

    private float m_defaultFOV;
    private void Awake()
    {
        if (m_camera != null)
        {
            m_defaultFOV = m_camera.Lens.FieldOfView;
        }
        GameEvents.OnPlayerStateChanged += PlayerStateChanged;
    }
    private void PlayerStateChanged(PlayerState state) 
    {
        m_playerState = state;
    }

    private void Update()
    {
        if (m_camera == null) return;
        if (m_playerState == PlayerState.Seated ||
            m_playerState == PlayerState.Paused)
            return;

        float newFov = m_camera.Lens.FieldOfView;

        if (Input.GetMouseButton(1)) 
        {
            newFov = Mathf.MoveTowards(
                newFov,
                targetFov,
                zoomSpeed * Time.deltaTime);
        }
        else 
        {
            newFov = Mathf.MoveTowards(
                newFov,
                m_defaultFOV,
                zoomSpeed * Time.deltaTime);
        }
        m_camera.Lens.FieldOfView = newFov;
    }
}
