using UnityEngine;

public class UIC_MainMenu : UIComponentBase<UIG_MainMenu>
{
    private enum MenuState
    {
        Default = 0,
        Main = 1,
        Start = 2,
        Settings = 3,
        Quit = 4
    }
    private UIV_MainMenu_Main m_main;
    private UIV_MainMenu_Settings m_settings;
    private MenuState m_state;
    private BasicFPCC m_player;
    public UIC_MainMenu(UIG_MainMenu group) : base(group)
    {
        m_main = group.Get<UIV_MainMenu_Main>();
        m_settings = group.Get<UIV_MainMenu_Settings>();
    }
    public override void Initialize()
    {
        base.Initialize();
        InitializeButtons();

        m_player = GameObject.FindAnyObjectByType<BasicFPCC>();
        if (m_player == null)
        {
            Debug.LogWarning("UIC_MainMenu: No player found in the scene.");
            return;
        }

        ChangeState(MenuState.Main);        
    }
    private void InitializeButtons()
    {
        m_main.b_start.onClick.AddListener(() =>
        {
            ChangeState(MenuState.Start);
        });
        m_main.b_settings.onClick.AddListener(() =>
        {
            ChangeState(MenuState.Settings);
        });
        m_main.b_quit.onClick.AddListener(() =>
        {
#if !UNITY_EDITOR
            ChangeState(MenuState.Quit);      
#endif
        });
        m_settings.b_back.onClick.AddListener(() =>
        {
            ChangeState(MenuState.Main);
        });
    }
    private void ChangeState(MenuState state)
    {
        if (m_state == state) return;        
        m_state = state;

        m_group.HideAll();
        switch (state)
        {
            case MenuState.Main:                
                m_main.View();
                TogglePlayer(false);
                break;
            case MenuState.Start:
                TogglePlayer(true);

                EffectController ec = EffectController.I;
                if (ec == null) 
                {
                    Debug.LogWarning($"UIC_MainMenu: No EffectController found in the scene.");
                    return;
                }
                ec.PlayEffect(ec.Get("V_CA_Saturation"));
                ec.PlayEffect(ec.Get("A_MV_FadeIn"));
                break;
            case MenuState.Settings:
                m_settings.View();
                break;
            case MenuState.Quit:
                
                break;
        }
    }
    private void TogglePlayer(bool value)
    {
        if (m_player == null)
            return;

        m_player.useLocalInputs = value;
        m_player.SetLockCursor(!value);
    }
}