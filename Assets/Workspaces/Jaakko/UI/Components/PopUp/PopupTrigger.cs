using UnityEngine;

public class PopupTrigger : MonoBehaviour 
{
    [SerializeField] private UIV_PopUP m_view;

    [SerializeField] private float m_duration;
    [SerializeField] private string m_text;
    [SerializeField] private Sprite m_sprite;
    [SerializeField] private bool m_activateOnce;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;
        if (m_activateOnce && activated) return;

        m_view.Bind(new TutorialPopupData() 
        {
            duration = m_duration,
            text = m_text,
            sprite = m_sprite
        });
        m_view.Show();
        activated = true;
    }    
}