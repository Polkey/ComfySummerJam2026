using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIV_Menu_Main : UIViewBase
{
    [field: SerializeField] public Button b_start { get; private set; }
    [field: SerializeField] public Button b_settings { get; private set; }
    [field: SerializeField] public Button b_quit { get; private set; }
    [field: SerializeField] public Button b_credits { get; private set; }
    [field: SerializeField] public Button b_restart { get; private set; }

    public void SetText(Button button, string textValue)
    {
        if (button == null) return;
        TMP_Text text = button.GetComponentInChildren<TMP_Text>();
        if (text != null)
        {
            text.text = textValue;
        }
    }
}