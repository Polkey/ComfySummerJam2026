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
    public UIC_MainMenu(UIG_MainMenu group) : base(group)
    {
        m_main = group.Get<UIV_MainMenu_Main>();
        m_settings = group.Get<UIV_MainMenu_Settings>();
    }
    public override void Initialize()
    {
        base.Initialize();
        
        ChangeState(MenuState.Main);        
        InitializeButtons();
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
            ChangeState(MenuState.Quit);
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
                break;
            case MenuState.Start:
                
                break;
            case MenuState.Settings:
                m_settings.View();
                break;
            case MenuState.Quit:
                
                break;
        }
    }
}