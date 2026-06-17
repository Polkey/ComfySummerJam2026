using Unity.Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 2f;
    [SerializeField] private CinemachineCamera m_camera;
    private BasicFPCC m_player;

    private float m_defaultFOV;
    private void Awake()
    {
        if (m_camera != null)
        {
            m_defaultFOV = m_camera.Lens.FieldOfView;
        }
        m_player = GetComponent<BasicFPCC>();
    }

    private void Update()
    {
        if (m_camera == null || m_player == null) return;
        if (m_player.State == PlayerState.Seated ||
            m_player.State == PlayerState.Paused)
            return;

        float newFov = m_camera.Lens.FieldOfView;
        const float minFov = 20;

        if (Input.GetMouseButton(1)) 
        {
            newFov = Mathf.MoveTowards(
                newFov,
                minFov,
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
