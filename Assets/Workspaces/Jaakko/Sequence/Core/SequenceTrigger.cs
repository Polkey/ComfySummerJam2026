using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SequenceTrigger : MonoBehaviour 
{
    [SerializeField] private bool m_activateOnce;
    [SerializeField] private bool m_disableTrigger;
    [SerializeField] private SequenceDefinition m_sequence;

    private BoxCollider m_collider;

    bool activated = false;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
        m_collider.isTrigger = true;

        m_collider.enabled = !m_disableTrigger;        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;
        if (m_activateOnce && activated) return;

        if (m_sequence) 
        {
            activated = true;
            Sequencer.I.Play(m_sequence);
        }            
    }
}