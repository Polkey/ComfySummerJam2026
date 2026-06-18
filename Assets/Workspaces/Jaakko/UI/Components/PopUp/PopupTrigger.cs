using UnityEngine;

public class PopupTrigger : MonoBehaviour 
{
    [SerializeField] private UIV_PopUP m_view;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        m_view.Bind(PopupDatabase.T_Zoom);
        m_view.Show();
    }    
}