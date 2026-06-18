using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ObjectiveTrigger : MonoBehaviour
{
    [field: SerializeField] public string objectiveTag { get; private set; } = "";
    private BoxCollider m_collider;
    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
        m_collider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        ObjectiveManager.CompleteObjective(objectiveTag);
    }
}
