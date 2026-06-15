using UnityEngine;
using UnityEngine.UI;

public class UIV_MainMenu_Main : UIViewBase
{
    [field: SerializeField] public Button b_start { get; private set; }
    [field: SerializeField] public Button b_settings { get; private set; }
    [field: SerializeField] public Button b_quit { get; private set; }
}