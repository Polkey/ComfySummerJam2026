using UnityEngine;

public static class PopupDatabase 
{
    public static TutorialPopupData T_Movement = new TutorialPopupData()
    {
        sprite = Resources.Load<Sprite>("Sprites/s_movement"),
        text = "use wasd to move around",
        duration = 2f
    };
    public static TutorialPopupData T_Zoom = new TutorialPopupData()
    {
        sprite = Resources.Load<Sprite>("Sprites/s_zoom"),
        text = "press rmb to zoom",
        duration = 5f
    };
    public static TutorialPopupData T_Controls = new TutorialPopupData()
    {
        sprite = Resources.Load<Sprite>("Sprites/s_controls"),
        text = "",
        duration = 3f
    };
    public static TutorialPopupData T_Default = new TutorialPopupData()
    {
        sprite = Resources.Load<Sprite>("Sprites/s_test"),
        text = "",
        duration = 1f
    };
}