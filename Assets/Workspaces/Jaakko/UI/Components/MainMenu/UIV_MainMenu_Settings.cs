using UnityEngine;
using UnityEngine.UI;
public class UIV_MainMenu_Settings : UIViewBase
{
    [field: SerializeField] public Button b_back { get; private set; }
    [field: SerializeField] public Slider s_music { get; private set; }
    [field: SerializeField] public Slider s_sfx { get; private set; }

    public override void Init()
    {
        // slider onvaluechanged can be linked here or in UIC_MainMenu.cs
    }
}