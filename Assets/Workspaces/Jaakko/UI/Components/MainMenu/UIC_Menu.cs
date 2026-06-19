using Unity.VisualScripting;
using UnityEngine;
public enum MenuState
{
    Default = 0,
    Main = 1,
    Start = 2,
    Settings = 3,
    Quit = 4,
    Credits = 5
}
public class UIC_Menu : UIComponentBase<UIG_Menu>
{
    private UIV_Menu_Main m_main;
    private UIV_Menu_Settings m_settings;
    private UIV_Menu_Credits m_credits;

    private UIV_PopUP_Tutorial m_popup;
    private MenuState m_state;
    private BasicFPCC m_player;
    public UIC_Menu(UIG_Menu group) : base(group)
    {
        m_main = group.Get<UIV_Menu_Main>();
        m_settings = group.Get<UIV_Menu_Settings>();
        m_popup = group.Get<UIV_PopUP_Tutorial>();
        m_credits = group.Get<UIV_Menu_Credits>();
    }
    public override void OnInput(UIInput input)
    {
        switch (input) 
        {
            case UIInput.Pause:
                if (m_state == MenuState.Start)
                {
                    ChangeState(MenuState.Main);
                }
                break;
            case UIInput.Debug:
                
                break;
        }
    }
    public override void Initialize()
    {
        base.Initialize();

        InitializeButtons();
        InitializeSliders();

        m_player = GameObject.FindAnyObjectByType<BasicFPCC>();
        if (m_player == null)
        {
            Debug.LogWarning("UIC_MainMenu: No player found in the scene.");
            return;
        }

        firstStart = true;
        ChangeState(MenuState.Main);        
    }
    bool firstStart;
    private void InitializeButtons()
    {
        m_main.b_start.onClick.AddListener(() =>
        {
            ChangeState(MenuState.Start);
        });
        m_main.b_settings.onClick.AddListener(() =>
        {
            ChangeState(MenuState.Settings);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.forwardBlipSFX);
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
            AudioManager.instance.PlayOneShot(FMODEvents.instance.backwardBlipSFX);
        });
        m_main.b_credits.onClick.AddListener(() =>
        {
            ChangeState(MenuState.Credits);
        });
        m_credits.b_back.onClick.AddListener(() =>
        {
            ChangeState(MenuState.Main);
        });
    }
    private void InitializeSliders() 
    {
        AudioManager audioManager = AudioManager.instance;
        if (audioManager == null) 
        {
            audioManager = GameObject.FindAnyObjectByType<AudioManager>();
            if (audioManager == null)
            {
                Debug.LogWarning("UIC_MainMenu: No AudioManager found in the scene. Sliders will not function.");
                return;
            }
        }

        m_settings.s_music.minValue = 0f;
        m_settings.s_music.maxValue = 1f;
        m_settings.s_music.value = audioManager.musicVolume;
        m_settings.s_music.onValueChanged.AddListener((value) =>
        {
            audioManager.musicVolume = Mathf.Clamp01(value);
        });

        m_settings.s_sfx.minValue = 0f;
        m_settings.s_sfx.maxValue = 1f;
        m_settings.s_sfx.value = audioManager.sfxVolume;
        m_settings.s_sfx.onValueChanged.AddListener((value) =>
        {
            audioManager.sfxVolume = Mathf.Clamp01(value);
        });

        m_settings.s_master.minValue = 0f;
        m_settings.s_master.maxValue = 1f;
        m_settings.s_master.value = audioManager.masterVolume;
        m_settings.s_master.onValueChanged.AddListener((value) =>
        {
            audioManager.masterVolume = Mathf.Clamp01(value);
        });

        m_settings.s_ambient.minValue = 0f;
        m_settings.s_ambient.maxValue = 1f;
        m_settings.s_ambient.value = audioManager.ambienceVolume;
        m_settings.s_ambient.onValueChanged.AddListener((value) =>
        {
            audioManager.ambienceVolume = Mathf.Clamp01(value);
        });
    }
    private void ChangeState(MenuState state)
    {
        if (m_state == state) return;
        if (m_player.State == PlayerState.Seated 
            || m_player.State == PlayerState.Sequence) return;
        GameEvents.RaiseMenuStateChanged(state);

        m_group.HideAll();
        switch (state)
        {
            case MenuState.Main:
                m_main.View();
                if (firstStart) 
                {
                    m_main.SetText(m_main.b_start, "Start");
                }
                else 
                {
                    m_main.SetText(m_main.b_start, "Continue");
                }
                    break;
            case MenuState.Start:
                if (firstStart) 
                {
                    Sequencer.I.Play<SD_Camera_GoToLookAt>();

                    m_popup.Bind(PopupDatabase.T_Controls);
                    m_popup.Show();
                }
                firstStart = false;
                break;
            case MenuState.Settings:
                m_settings.View();
                break;
            case MenuState.Quit:
                
                break;
            case MenuState.Credits:
                m_credits.View();
                break;
        }
        m_state = state;
    }
}