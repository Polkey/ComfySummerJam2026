using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SequenceTrigger : MonoBehaviour 
{
    private BoxCollider m_collider;
    private Sequencer m_sequencer;

    [SerializeField] private float duration;
    [SerializeField] private Transform target;
    [SerializeField] private Transform lookAt;
    [SerializeField] private BasicFPCC m_player;

    private CinemachineCamera m_camera;

    [SerializeField] private bool m_disableTrigger;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
        m_collider.isTrigger = true;

        if (m_disableTrigger) 
        {
            m_collider.enabled = false;
        }
    }
    protected virtual void Initialize() 
    {
        m_sequencer = FindAnyObjectByType<Sequencer>();
        if (m_player == null)
        {
            Debug.LogWarning($"PlayerReference NULL on {name}");
            return;
        }
        m_camera = m_player.GetComponentInChildren<CinemachineCamera>();
        if (m_camera == null)
        {
            Debug.LogWarning($"CameraReference NULL on {name}");
            return;
        }
    }
}