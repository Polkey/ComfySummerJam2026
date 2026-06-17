using Unity.Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineCamera m_camera;

    private float m_defaultFOV;
    private void Awake()
    {
        if (m_camera != null)
        {
            m_defaultFOV = m_camera.Lens.FieldOfView;
        }
    }

    private void Update()
    {
        if (m_camera == null) return;
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            m_camera.Lens.FieldOfView -= scrollInput * 10f;
            m_camera.Lens.FieldOfView = Mathf.Clamp(m_camera.Lens.FieldOfView, 20f, m_defaultFOV);
        }
    }
}
